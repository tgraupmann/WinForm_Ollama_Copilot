using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TxtPrompt.KeyDown += TxtPrompt_KeyDown;
            TxtPrompt.KeyUp += TxtPrompt_KeyUp;

            TimerDetection.Interval = 250;
            TimerDetection.Start();
        }

        private async void PromptOllama()
        {
            if (DropDownFocus.SelectedIndex < 0)
            {
                TxtResponse.Text = "You need to set focus to a window to send the Ollama response.";
            }
            else
            {
                TxtResponse.Text = "Ollama is thinking...";

                JArray messages = new JArray();
                JObject message = new JObject()
                {
                    ["role"] = "user",
                    ["content"] = TxtPrompt.Text,
                };
                messages.Add(message);
                await SendPostRequestAsync("http://localhost:11434/api/chat", new { model = "llama2", messages = messages });
            }
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
            if (_mDetectedWindows.Count > 0)
            {
                DropDownFocus.SelectedIndex = 0;
            }
        }

        private void TimerDetection_Tick(object sender, EventArgs e)
        {
            DetectForeground();
        }

        public async Task SendPostRequestAsync(string url, object data)
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

                    Clipboard.SetText(text);
                    TxtResponse.Text = text;

                    WindowState = FormWindowState.Minimized;

                    WindowFocus win = _mDetectedWindows[DropDownFocus.SelectedIndex];
                    NativeUtils.SetForegroundWindow(win.Hwnd);

                    // send with control+V to paste.
                    // This is better than the UI going crazy sending one key at a time
                    SendKeys.Send("^v");


                }
            }
            catch
            {
                TxtResponse.Text = "Response failed. Try again.";
            }
        }


        private void BtnPrompt_Click(object sender, EventArgs e)
        {
            PromptOllama();
        }
    }
}
