using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace WinForm_Ollama_Copilot
{
    public partial class Form1 : Form
    {
        private struct WindowFocus
        {
            public IntPtr Hwnd;
            public String Title;
        }

        private List<WindowFocus> _mDetectedWindows = new List<WindowFocus>();

        private JArray _mHistory = new JArray();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DropDownModels.Items.Add("-- Select a model --");
            DropDownModels.SelectedIndex = 0;
            UpdateModels();

            DropDownFocus.Items.Add("-- Select a destination application --");
            DropDownFocus.SelectedIndex = 0;

            TxtPrompt.KeyDown += TxtPrompt_KeyDown;
            TxtPrompt.KeyUp += TxtPrompt_KeyUp;

            TimerDetection.Interval = 250;
            TimerDetection.Start();
        }

        private async Task SendGetRequestApiTagsAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);
                    
                    JObject jsonResponse = JObject.Parse(responseBody);
                    foreach (JObject model in jsonResponse["models"])
                    {
                        string nameVersion = (string)model["name"];
                        if (!string.IsNullOrEmpty(nameVersion))
                        {
                            if (nameVersion.Contains(":"))
                            {
                                nameVersion = nameVersion.Split(":".ToCharArray())[0];
                                DropDownModels.Items.Add(nameVersion);
                            }
                        }
                    }
                }
            }
            catch
            {
                TxtResponse.Text = "Tags Response failed. Try again.";
            }
        }

        private async void UpdateModels()
        {
            await SendGetRequestApiTagsAsync("http://localhost:11434/api/tags");
        }

        private async Task SendPostRequestApiChatAsync(string url, object data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }

                    var responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);

                    // format response into proper JSON
                    string text = string.Join(",", responseBody.Split("\n".ToCharArray()));
                    if (text.EndsWith(","))
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                    text = string.Format("[{0}]", text);

                    JArray jsonResponse = JArray.Parse(text);

                    text = String.Empty;
                    for (int i = 0; i < jsonResponse.Count; ++i)
                    {
                        text += jsonResponse[i]["message"]["content"].ToString();
                    }

                    JObject message = new JObject()
                    {
                        ["role"] = "assistant",
                        ["content"] = text,
                    };
                    _mHistory.Add(message);

                    String title = GetSelectedTitle();
                    if (!String.IsNullOrEmpty(title) && title.ToLower().Contains("excel"))
                    {
                        text = text.Replace("|", "\t");
                    }

                    Clipboard.SetText(text);
                    TxtResponse.Text = text.Replace("\n", "\r\n");

                    if (DropDownFocus.SelectedIndex > 0)
                    {
                        WindowState = FormWindowState.Minimized;

                        WindowFocus win = _mDetectedWindows[DropDownFocus.SelectedIndex - 1];
                        NativeUtils.SetForegroundWindow(win.Hwnd);

                        // send with control+V to paste.
                        // This is better than the UI going crazy sending one key at a time
                        SendKeys.Send("^v");
                    }
                }
            }
            catch
            {
                TxtResponse.Text = "Chat Response failed. Try again.";
            }
        }

        private string GetModel()
        {
            if (DropDownModels.Items.Count > 1)
            {
                return (string)DropDownModels.Items[DropDownModels.SelectedIndex].ToString();
            }
            else
            {
                return null;
            }
        }

        private async void PromptOllama()
        {
            TxtResponse.Text = "Ollama is thinking...";

            JObject message = new JObject()
            {
                ["role"] = "user",
                ["content"] = TxtPrompt.Text,
            };
            _mHistory.Add(message);
            await SendPostRequestApiChatAsync("http://localhost:11434/api/chat", new { model = GetModel(), messages = _mHistory });
        }

        private void TxtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (e.Control))
            {
                // Suppress the Enter key
                e.SuppressKeyPress = true;
            }
        }

        private void TxtPrompt_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (e.Control))
            {
                PromptOllama();

                // Suppress the Enter key
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            TimerDetection.Stop();
            Application.Exit();
        }

        void DetectForeground()
        {
            WindowFocus win;
            win.Hwnd = NativeUtils.getForegroundWindow();
            if (win.Hwnd == IntPtr.Zero)
            {
                return;
            }
            win.Title = NativeUtils.GetActiveWindowTitle();
            if (string.IsNullOrEmpty(win.Title) ||
                win.Title == "Ollama Copilot")
            {
                return;
            }
            foreach (WindowFocus w in _mDetectedWindows)
            {
                if (w.Hwnd == win.Hwnd && w.Title == win.Title)
                {
                    return;
                }
            }
            _mDetectedWindows.Add(win);
            DropDownFocus.Items.Add(win.Title);
        }

        string GetSelectedTitle()
        {
            if (DropDownFocus.SelectedIndex <= 0)
            {
                return null;
            }
            return _mDetectedWindows[DropDownFocus.SelectedIndex - 1].Title;
        }

        private void TimerDetection_Tick(object sender, EventArgs e)
        {
            DetectForeground();
        }

        private void BtnPrompt_Click(object sender, EventArgs e)
        {
            PromptOllama();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Chat History (*.history)|*.history";
                    saveFileDialog.Title = "Save Chat History";
                    saveFileDialog.FileName = string.Format("{0}.history", DateTime.Now.ToString("yyyy-MM-dd"));

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                        {
                            string json = _mHistory.ToString();
                            writer.Write(json);
                            writer.Flush();
                        }
                    }
                }
                TxtResponse.Text = "History saved.";
            }
            catch
            {
                TxtResponse.Text = "Failed to save history";
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Chat History (*.history)|*.history";
                    openFileDialog.Title = "Load Chat History";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader reader = new StreamReader(openFileDialog.OpenFile()))
                        {
                            _mHistory = JArray.Parse(reader.ReadToEnd());
                        }
                    }
                }
                TxtResponse.Text = "History loaded.";
            }
            catch
            {
                TxtResponse.Text = "Failed to load history";
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            _mHistory.Clear();
        }

        private void CboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnPrompt.Enabled = DropDownModels.SelectedIndex > 0;
        }
    }
}
