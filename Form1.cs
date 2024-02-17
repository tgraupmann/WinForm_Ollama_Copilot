using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Net;
using YoutubeTranscriptApi;
using static WinForm_Ollama_Copilot.OcrManager;

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

        private JArray _mImages = new JArray();

        private readonly string _mDefaultModel = ReadConfiguration("SelectedModel");

        private AudioManager _mAudioManager = new AudioManager();

        private SpeakManager _mSpeakManager = new SpeakManager();

        private OcrManager _mOcrManager = new OcrManager();

        public static void UpdateConfiguration(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement kv = config.AppSettings.Settings[key];
            if (kv == null)
            {
                // Key doesn't exist, create it
                kv = new KeyValueConfigurationElement(key, value);
                config.AppSettings.Settings.Add(kv);
            }
            else
            {
                // Key exists, update its value
                kv.Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static string ReadConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static int ReadConfigurationInt(string key, int valueDefault = 0)
        {
            string txtResult = ConfigurationManager.AppSettings[key];
            int result;
            if (int.TryParse(txtResult, out result))
            {
                return result;
            }
            else
            {
                return valueDefault;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string strCopyResponseToClipboard = ReadConfiguration("CopyResponseToClipboard");
            ChkResponseClipboard.Checked = strCopyResponseToClipboard == "True";

            LoadTab();

            LoadOCR();

            bool locationChanged = false;

            string strLocationX = ReadConfiguration("LocationX");
            int locationX = Location.X;
            if (int.TryParse(strLocationX, out locationX))
            {
                locationChanged = true;
            }

            string strLocationY = ReadConfiguration("LocationY");
            int locationY = Location.Y;
            if (int.TryParse(strLocationY, out locationY))
            {
                locationChanged = true;
            }

            if (locationChanged)
            {
                Location = new Point(locationX, locationY);
            }

            this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);


            string strWidth = ReadConfiguration("Width");
            int width = Width;
            if (int.TryParse(strWidth, out width))
            {
                Width = width;
            }

            string strHeight = ReadConfiguration("Height");
            int height = Height;
            if (int.TryParse(strHeight, out height))
            {
                Height = height;
            }

            this.Resize += new System.EventHandler(this.Form1_Resize);


            string strWindowState = ReadConfiguration("WindowState");
            if (strWindowState == "Maximized")
            {
                WindowState = FormWindowState.Maximized;
            }


            string strFontSize = ReadConfiguration("FontSize");
            float fontSize;
            if (float.TryParse(strFontSize, out fontSize))
            {
                TxtPrompt.Font = new System.Drawing.Font(TxtPrompt.Font.FontFamily, fontSize);
                TxtResponse.Font = new System.Drawing.Font(TxtResponse.Font.FontFamily, fontSize);
            }

            UpdateModels();
            LoadHistory();

            const int DEFAULT_AUDIO_INPUT_THRESHOLD = 5;
            string strAudioInputThreshold = ReadConfiguration("AudioInputThreshold");
            int audioInputThreshold;
            if (int.TryParse(strAudioInputThreshold, out audioInputThreshold))
            {
                SliderTheshold.Value = audioInputThreshold;
            }
            else
            {
                SliderTheshold.Value = DEFAULT_AUDIO_INPUT_THRESHOLD;
            }
            SliderTheshold_Scroll(null, null);

            string strOutputSpeak = ReadConfiguration("OutputSpeak");
            ChkOutputSpeak.Checked = strOutputSpeak == "True";

            DropDownInputDevice.Items.Add("-- Select an input device --");

            string defaultInputDevice = ReadConfiguration("SelectedInputDevice");

            bool found = false;
            foreach (string device in _mAudioManager.InputDevices)
            {
                DropDownInputDevice.Items.Add(device);
                if (device == defaultInputDevice)
                {
                    DropDownInputDevice.SelectedIndex = DropDownInputDevice.Items.Count - 1;
                    found = true;
                }
            }
            if (!found)
            {
                DropDownInputDevice.SelectedIndex = 0;
            }

            DropDownModels.Items.Add("-- Select a model --");
            DropDownModels.SelectedIndex = 0;

            DropDownFocus.Items.Add("-- Select a destination application --");
            DropDownFocus.SelectedIndex = 0;

            string defaultApplication = ReadConfiguration("SelectedApplication");
            string defaultApplicationHwnd = ReadConfiguration("SelectedApplicationHwnd");
            int hwndVal = 0;
            if (!string.IsNullOrEmpty(defaultApplication) &&
                int.TryParse(defaultApplicationHwnd, out hwndVal))
            {
                WindowFocus win;
                win.Hwnd = (IntPtr)hwndVal;
                win.Title = defaultApplication;
                _mDetectedWindows.Add(win);

                DropDownFocus.Items.Add(defaultApplication);
                DropDownFocus.SelectedIndex = DropDownFocus.Items.Count - 1;
            }

            tabControl1.KeyDown += TxtPrompt_KeyDown;
            tabControl1.KeyUp += TxtPrompt_KeyUp;

            TimerDetection.Interval = 250;
            TimerDetection.Start();

            TimerModels.Interval = 5000;
            TimerModels.Start();

            string strDictation = ReadConfiguration("Dictation");
            ChkDictation.Checked = strDictation == "True";

            string strStayAwake = ReadConfiguration("StayAwake");
            ChkStayAwake.Checked = strStayAwake == "True";

            TimerAwake.Interval = 15000;
            TimerAwake.Start();

            TimerDictation.Interval = 1000;
            TimerDictation.Start();

            TimerVolume.Interval = 100;
            TimerVolume.Start();

            TimerSpeaking.Interval = 100;
            TimerSpeaking.Start();

            await PopulateVoices();
        }

        private async Task PopulateVoices()
        {
            #region DropDownVoices

            DropDownOutputVoice.Items.Add("-- Select an output voice --");
            string strOutputVoice = ReadConfiguration("OutputVoice");
            DropDownOutputVoice.SelectedIndex = 0;
            List<string> voices = null;
            do
            {
                voices = await _mSpeakManager.GetVoices();
                if (voices.Count == 0)
                {
                    await Task.Delay(3000);
                    continue;
                }
                foreach (string name in voices)
                {
                    DropDownOutputVoice.Items.Add(name);
                }

                for (int i = 0; i < DropDownOutputVoice.Items.Count; ++i)
                {
                    if (DropDownOutputVoice.Items[i].ToString() == strOutputVoice)
                    {
                        DropDownOutputVoice.SelectedIndex = i;
                        break;
                    }
                }
            }
            while (voices.Count == 0);

            #endregion DropDownVoices
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
                                string name = nameVersion.Split(":".ToCharArray())[0];
                                bool exists = false;
                                foreach (string modelName in DropDownModels.Items)
                                {
                                    if (modelName == name)
                                    {
                                        exists = true;
                                        break;
                                    }
                                }
                                if (!exists)
                                {
                                    DropDownModels.Items.Add(name);
                                    if (!string.IsNullOrEmpty(_mDefaultModel))
                                    {
                                        if (name == _mDefaultModel)
                                        {
                                            DropDownModels.SelectedIndex = DropDownModels.Items.Count - 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                DropDownModels.Enabled = true;
            }
            catch
            {
                DropDownModels.Enabled = false;
            }
        }

        private async Task SendGetRequestApiVersionAsync(string url)
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
                    LblVersion.Text = string.Format("Ollama API Version: {0}", jsonResponse["version"].ToString());
                }
            }
            catch
            {
                LblVersion.Text = "Ollama API Version: UNKNOWN";
            }
        }

        private async void UpdateModels()
        {
            await SendGetRequestApiTagsAsync("http://localhost:11434/api/tags");
            await SendGetRequestApiVersionAsync("http://localhost:11434/api/version");
        }

        private async Task SendPostRequestApiGenerateAsync(string url, object data)
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
                        text += jsonResponse[i]["response"].ToString();
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

                    if (!string.IsNullOrEmpty(text))
                    {
                        if (ChkResponseClipboard.Checked)
                        {
                            Clipboard.SetText(text);
                        }
                        TxtResponse.Text = text.Replace("\n", "\r\n").Trim();

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
                    else
                    {
                        TxtResponse.Text = "No response";
                    }

                }
                BtnPrompt.Enabled = true;
            }
            catch
            {
                BtnPrompt.Enabled = false;
            }
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

                    if (ChkResponseClipboard.Checked)
                    {
                        Clipboard.SetText(text);
                    }
                    TxtResponse.Text = text.Replace("\n", "\r\n").Trim();
                    Speak();

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
                BtnPrompt.Enabled = true;
            }
            catch
            {
                BtnPrompt.Enabled = false;
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

        private async void PromptOllamaGenerate()
        {
            if (string.IsNullOrEmpty(TxtPrompt.Text))
            {
                TxtResponse.Text = "Prompt text cannot be empty. Submit again.";
                return;
            }
            else
            {
                TxtResponse.Text = "Ollama is thinking...";
            }

            await SendPostRequestApiGenerateAsync("http://localhost:11434/api/generate", new { model = GetModel(), prompt = TxtPrompt.Text, images = _mImages });
        }

        private static string CombineWhitespace(string input)
        {
            string pattern = @"\s+";
            string replacement = " ";
            string result = Regex.Replace(input, pattern, replacement);
            return result;
        }

        // write a function that finds all urls in a string
        private async Task<string> ReplaceLinksWithText(string text)
        {
            const string urlPattern = @"http[s]?://[^ ]+";
            var matches = Regex.Matches(text, urlPattern);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var url = match.Value;
                try
                {
                    string content = string.Empty;
                    Uri uri = new Uri(url);
                    switch (uri.Host.ToLower())
                    {
                        case "www.youtube.com":
                            try
                            {
                                var queryDictionary = System.Web.HttpUtility.ParseQueryString(uri.Query);
                                string videoId = queryDictionary["v"];
                                using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
                                {
                                    var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
                                    foreach (var item in transcriptItems)
                                    {
                                        content += item.Text + " ";
                                    }
                                }
                                text = text.Replace(url, "---\r\n" + content + "\r\n---\r\n");
                                return text;
                            }
                            catch
                            {
                                text = text.Replace(url, url + "\r\n---\r\nCould not be transcribed.\r\n---\r\n");
                                return text;
                            }
                        case "youtu.be":
                            try
                            {
                                string videoId = uri.LocalPath.Substring(1);
                                using (var youTubeTranscriptApi = new YouTubeTranscriptApi())
                                {
                                    var transcriptItems = youTubeTranscriptApi.GetTranscript(videoId);
                                    foreach (var item in transcriptItems)
                                    {
                                        content += item.Text + " ";
                                    }
                                }
                                text = text.Replace(url, "---\r\n" + content + "\r\n---\r\n");
                                return text;
                            }
                            catch
                            {
                                text = text.Replace(url, url + "\r\n---\r\nCould not be transcribed.\r\n---\r\n");
                                return text;
                            }
                    }

                    HtmlWeb web = new HtmlWeb();
                    web.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                    var tcs = new TaskCompletionSource<HttpWebResponse>();
                    web.PostResponse = delegate (HttpWebRequest request, HttpWebResponse response)
                    {
                        if (!string.IsNullOrEmpty(response.ContentType))
                        {
                            switch (response.ContentType.ToLower())
                            {
                                case "text/xml;charset=utf-8":
                                case "text/plain; charset=utf-8":
                                    using (var reader = new StreamReader(response.GetResponseStream()))
                                    {

                                        content = reader.ReadToEnd();
                                    }
                                    break;
                            }
                        }
                        tcs.SetResult(response);
                    };
                    HtmlDocument doc = web.Load(url);
                    var httpWebResponse = await tcs.Task;
                    var contentType = httpWebResponse.ContentType;
                    if (!string.IsNullOrEmpty(contentType))
                    {
                        switch (contentType.ToLower())
                        {
                            case "text/xml;charset=utf-8":
                            case "text/plain; charset=utf-8":
                                break;
                            default:
                                content = CombineWhitespace(doc.DocumentNode.InnerText);
                                break;
                        }
                    }
                    text = text.Replace(url, "---\r\n" + content + "\r\n---\r\n");
                }
                catch
                {

                }
            }
            return text;
        }

        private async void PromptOllamaChat()
        {
            TxtResponse.Text = "Ollama is thinking...";

            string text = TxtPrompt.Text;
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    text = await ReplaceLinksWithText(text);
                    TxtPrompt.Text = text;
                }
                catch
                {

                }


                JObject message = new JObject()
                {
                    ["role"] = "user",
                    ["content"] = TxtPrompt.Text,
                };
                _mHistory.Add(message);
                await SendPostRequestApiChatAsync("http://localhost:11434/api/chat", new { model = GetModel(), messages = _mHistory });
            }
        }

        bool _mKeyDownControl = false;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (_mKeyDownControl)
            {
                switch (m.Msg)
                {
                    case 0x20A: // WM_MOUSEWHEEL
                                // Determine the amount of the wheel movement.
                        short delta = (short)(m.WParam.ToInt32() >> 16);

                        // If delta is positive, the wheel was rotated upwards.
                        // If delta is negative, the wheel was rotated downwards.
                        if (delta > 0)
                        {
                            // Handle scroll up
                            try
                            {
                                float fontSize = TxtPrompt.Font.Size + 1;
                                UpdateConfiguration("FontSize", fontSize.ToString());
                                TxtPrompt.Font = new System.Drawing.Font(TxtPrompt.Font.FontFamily, fontSize);
                                TxtResponse.Font = new System.Drawing.Font(TxtResponse.Font.FontFamily, fontSize);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            // Handle scroll down
                            try
                            {
                                float fontSize = Math.Max(8, TxtPrompt.Font.Size - 1);
                                UpdateConfiguration("FontSize", fontSize.ToString());
                                TxtPrompt.Font = new System.Drawing.Font(TxtPrompt.Font.FontFamily, fontSize);
                                TxtResponse.Font = new System.Drawing.Font(TxtResponse.Font.FontFamily, fontSize);
                            }
                            catch
                            {
                            }
                        }
                        break;
                }
            }
        }


        private void TxtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            _mKeyDownControl = e.Control;

            if ((e.KeyCode == Keys.Enter) && (e.Control))
            {
                // Suppress the Enter key
                e.SuppressKeyPress = true;
            }
        }

        private void TxtPrompt_KeyUp(object sender, KeyEventArgs e)
        {
            _mKeyDownControl = !e.Control;

            if ((e.KeyCode == Keys.Enter) && (e.Control))
            {
                if (DropDownModels.SelectedIndex == 0)
                {
                    TxtResponse.Text = "Select a model to prompt.";
                }
                else
                {
                    if (tabControl1.SelectedTab == TabImages)
                    {
                        PromptOllamaGenerate();
                    }
                    else
                    {
                        if (DropDownModels.SelectedIndex > 0)
                        {
                            PromptOllamaChat();
                        }
                    }
                }

                // Suppress the Enter key
                e.SuppressKeyPress = true;
            }

            if ((e.KeyCode == Keys.V) && (e.Control))
            {
                if (Clipboard.ContainsImage())
                {
                    BtnPaste_Click(null, null);

                    // Suppress the Enter key
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.Invoke((MethodInvoker)async delegate
            {
                await _mSpeakManager.Stop();
                _mAudioManager.StopInputDeviceRecording();
                SaveHistory();
                TimerDetection.Stop();
                TimerModels.Stop();
                TimerDictation.Stop();
                TimerVolume.Stop();
                TimerSpeaking.Stop();
                TimerCapture.Stop();
                _mOcrManager.Uninit();
                Application.Exit();
            });
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

        private void RenderProgressBar()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Update the UI
                if (ChkDictation.Checked)
                {
                    // Update volume progressbar
                    PbVolume.Value = _mAudioManager._mVolume;

                    LblVolume.Text = string.Format("{0:F1} %", 100 * _mAudioManager._mVolume / 32767.0f);
                }
            });
        }

        private void TimerDetection_Tick(object sender, EventArgs e)
        {
            DetectForeground();
        }

        private void BtnPrompt_Click(object sender, EventArgs e)
        {
            PromptOllamaChat();
        }

        const string TEMP_HISTORY = "temp.history";

        private void LoadHistory()
        {
            try
            {
                if (File.Exists(TEMP_HISTORY))
                {
                    using (StreamReader reader = new StreamReader(TEMP_HISTORY))
                    {
                        _mHistory = JArray.Parse(reader.ReadToEnd());
                    }
                }

                if (_mHistory.Count > 0)
                {
                    JObject lastMessage = _mHistory[_mHistory.Count - 1].ToObject<JObject>();
                    if (lastMessage["role"].ToString() == "user")
                    {
                        TxtPrompt.Text = lastMessage["content"].ToString().Replace("\n", "\r\n").Trim();
                    }
                    else if (lastMessage["role"].ToString() == "assistant")
                    {
                        TxtResponse.Text = lastMessage["content"].ToString().Replace("\n", "\r\n").Trim();
                        if (_mHistory.Count > 1)
                        {
                            lastMessage = _mHistory[_mHistory.Count - 2].ToObject<JObject>();
                            if (lastMessage["role"].ToString() == "user")
                            {
                                TxtPrompt.Text = lastMessage["content"].ToString().Replace("\n", "\r\n").Trim();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void SaveHistory()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(TEMP_HISTORY))
                {
                    string json = _mHistory.ToString();
                    writer.Write(json);
                    writer.Flush();
                }
            }
            catch
            {
            }
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

            if (DropDownModels.SelectedIndex > 0)
            {
                UpdateConfiguration("SelectedModel", (string)DropDownModels.Items[DropDownModels.SelectedIndex]);
            }
        }

        private void TimerModels_Tick(object sender, EventArgs e)
        {
            UpdateModels();
        }

        private void DropDownFocus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownFocus.SelectedIndex > 0)
            {
                UpdateConfiguration("SelectedApplication", (string)DropDownFocus.Items[DropDownFocus.SelectedIndex]);
                UpdateConfiguration("SelectedApplicationHwnd", _mDetectedWindows[DropDownFocus.SelectedIndex - 1].Hwnd.ToInt32().ToString());
            }
            else
            {
                UpdateConfiguration("SelectedApplication", null);
                UpdateConfiguration("SelectedApplicationHwnd", null);
            }
        }

        private void TxtPrompt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private string ConvertImageToBase64(string filePath)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fs.CopyTo(memoryStream);
                }
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void TxtPrompt_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    string imgBase64 = ConvertImageToBase64(file);
                    _mImages.Add(imgBase64);
                }
                LblImageCollection.Text = _mImages.Count.ToString();
                if (_mImages.Count > 0)
                {
                    BtnImageSubmit.Enabled = true;
                }
            }
            catch
            {
                TxtResponse.Text = "Failed to read image!";
            }
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownModels.SelectedIndex <= 0)
                {
                    TxtResponse.Text = "Select a model";
                }
                else
                {

                    if (Clipboard.ContainsImage())
                    {
                        Image image = Clipboard.GetImage();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);
                            byte[] imageBytes = ms.ToArray();
                            string base64Image = Convert.ToBase64String(imageBytes);
                            _mImages.Add(base64Image);
                        }
                        LblImageCollection.Text = _mImages.Count.ToString();
                        if (_mImages.Count > 0)
                        {
                            BtnImageSubmit.Enabled = true;
                        }
                    }
                }
            }
            catch
            {
                TxtResponse.Text = "Failed to paste from clipboard!";
            }
        }

        private async Task SendPostRequestApiChatPingAsync(string url, object data)
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

                    string ignore = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(ignore))
                    {
                        //ignore
                    }
                }
            }
            catch
            {
            }
        }

        private async void TimerAwake_Tick(object sender, EventArgs e)
        {
            if (ChkStayAwake.Checked)
            {
                JObject message = new JObject()
                {
                    ["role"] = "user",
                    ["content"] = string.Format("The time is {0}", DateTime.Now)
                };
                JArray pingMessage = new JArray();
                pingMessage.Add(message);
                await SendPostRequestApiChatPingAsync("http://localhost:11434/api/chat", new { model = GetModel(), messages = pingMessage });
            }
        }

        private void ChkStayAwake_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfiguration("StayAwake", ChkStayAwake.Checked.ToString());
        }

        private void ChkDictation_CheckedChanged(object sender, EventArgs e)
        {
            // store the toggle in the configuration
            UpdateConfiguration("Dictation", ChkDictation.Checked.ToString());

            // start recording if dictation is checked
            if (ChkDictation.Checked)
            {
                if (DropDownInputDevice.SelectedIndex > 0)
                {
                    _mAudioManager.SelectInputDevice(DropDownInputDevice.SelectedIndex - 1);
                }
            }
            // stop recording if dictation is unchecked
            else
            {
                _mAudioManager.StopInputDeviceRecording();
            }
        }

        private void CboInputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // store the selected input device in the configuration
            if (DropDownInputDevice.SelectedIndex > 0)
            {
                string deviceName = DropDownInputDevice.SelectedItem.ToString();
                UpdateConfiguration("SelectedInputDevice", deviceName);
            }
            else
            {
                UpdateConfiguration("SelectedInputDevice", null);
            }

            // start recording if dictation is checked
            if (ChkDictation.Checked && DropDownInputDevice.SelectedIndex > 0)
            {
                _mAudioManager.SelectInputDevice(DropDownInputDevice.SelectedIndex - 1);
            }
            else
            {
                _mAudioManager.StopInputDeviceRecording();
            }
        }

        private void TimerDictation_Tick(object sender, EventArgs e)
        {
            List<string> detectedWords = _mAudioManager.DetectedWords;
            if (detectedWords.Count > 0)
            {
                _mAudioManager.DetectedWords = new List<string>();
                if (ChkDictation.Checked)
                {
                    // get text carret position
                    int selectionIndex = TxtPrompt.SelectionStart;

                    // build the transcript
                    string transcript = string.Empty;

                    // add a space to the beginning of the transcript
                    if (TxtPrompt.Text.Length == 0 ||
                        selectionIndex <= 0 ||
                        (selectionIndex < TxtPrompt.Text.Length &&
                        TxtPrompt.Text.Substring(selectionIndex - 1, 1) == " "))
                    {
                    }
                    else
                    {
                        transcript += " ";
                    }

                    // detect commands
                    bool submitPrompt = false;
                    string input = string.Join(" ", detectedWords).Trim();
                    string searchInput = input.ToLower();
                    string cleanedInput = Regex.Replace(searchInput, @"[^a-z\s]", "").Trim();
                    if (cleanedInput.EndsWith("prompt clear"))
                    {
                        TxtPrompt.Text = "";
                        return;
                    }
                    else if (cleanedInput.EndsWith("prompt submit"))
                    {
                        submitPrompt = true;
                        int index = input.ToLower().LastIndexOf("prompt");
                        input = input.Substring(0, index);
                    }
                    else if (cleanedInput.EndsWith("response clear"))
                    {
                        TxtResponse.Text = "";
                        return;
                    }
                    else if (cleanedInput.EndsWith("response play"))
                    {
                        if (DropDownOutputVoice.SelectedIndex > 0)
                        {
                            BtnPlay_Click(null, null);
                        }
                        return;
                    }
                    if (detectedWords.Count > 0)
                    {
                        transcript += input.Trim();
                        if (selectionIndex == TxtPrompt.Text.Length)
                        {
                            TxtPrompt.Text += transcript;
                        }
                        else
                        {
                            TxtPrompt.Text = TxtPrompt.Text.Insert(selectionIndex, transcript);
                        }
                        selectionIndex += transcript.Length;
                        TxtPrompt.SelectionStart = selectionIndex;

                        // add a space to the end of the transcript
                        if (selectionIndex < TxtPrompt.Text.Length &&
                            TxtPrompt.Text.Substring(selectionIndex, 1) != " ")
                        {
                            TxtPrompt.Text = TxtPrompt.Text.Insert(selectionIndex, " ");
                        }
                    }

                    if (submitPrompt)
                    {
                        PromptOllamaChat();
                    }
                }
            }
        }

        private void TimerVolume_Tick(object sender, EventArgs e)
        {
            RenderProgressBar();
        }

        private void SliderTheshold_Scroll(object sender, EventArgs e)
        {
            _mAudioManager._mInputThreshold = (int)(SliderTheshold.Value / 100f * 32768);

            LblThreshold.Text = string.Format("{0}%", SliderTheshold.Value);
            UpdateConfiguration("AudioInputThreshold", SliderTheshold.Value.ToString());
        }
        private void ChkOutputSpeak_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfiguration("OutputSpeak", ChkOutputSpeak.Checked.ToString());
        }

        private void DropDownOutputVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownOutputVoice.SelectedIndex > 0)
            {
                UpdateConfiguration("OutputVoice", DropDownOutputVoice.SelectedItem.ToString());

                if (_mAudioManager._mIsSpeaking)
                {
                    BtnPlay_Click(null, null);
                }
            }
            else
            {
                UpdateConfiguration("OutputVoice", null);
            }
        }

        private void Speak()
        {
            if (DropDownOutputVoice.SelectedIndex > 0)
            {
                _mSpeakManager.Speak(DropDownOutputVoice.SelectedIndex - 1, TxtResponse.Text);
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (DropDownOutputVoice.SelectedIndex > 0)
            {
                int voice = DropDownOutputVoice.SelectedIndex - 1;
                if (TxtResponse.SelectionStart > 0)
                {
                    if (TxtResponse.SelectionLength > 0)
                    {
                        _mSpeakManager.Speak(voice, TxtResponse.Text.Substring(TxtResponse.SelectionStart, TxtResponse.SelectionLength));
                    }
                    else
                    {
                        _mSpeakManager.Speak(voice, TxtResponse.Text.Substring(TxtResponse.SelectionStart));
                    }
                }
                else
                {
                    _mSpeakManager.Speak(voice, TxtResponse.Text);
                }
            }
        }
        private async void BtnStop_Click(object sender, EventArgs e)
        {
            await _mSpeakManager.Stop();
        }

        private async void TimerSpeaking_Tick(object sender, EventArgs e)
        {
            bool speaking = await _mSpeakManager.IsSpeaking();
            _mAudioManager._mIsSpeaking = speaking;
            if (ChkDictation.Checked)
            {
                if (speaking)
                {
                    if (ChkDictation.Enabled)
                    {
                        ChkDictation.Enabled = false;
                    }
                }
                else
                {
                    if (!ChkDictation.Enabled)
                    {
                        ChkDictation.Enabled = true;
                    }
                }
            }
            else
            {
                if (!ChkDictation.Enabled)
                {
                    ChkDictation.Enabled = true; // allow interaction
                }
            }
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                UpdateConfiguration("LocationX", Location.X.ToString());
                UpdateConfiguration("LocationY", Location.Y.ToString());
            }

            switch (WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    UpdateConfiguration("WindowState", WindowState.ToString());
                    break;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                UpdateConfiguration("Width", Width.ToString());
                UpdateConfiguration("Height", Height.ToString());
            }

            switch (WindowState)
            {
                case FormWindowState.Normal:
                case FormWindowState.Maximized:
                    UpdateConfiguration("WindowState", WindowState.ToString());
                    break;
            }
        }

        private void LoadOCR()
        {
            this.ChkOCR.Checked = ReadConfiguration("OCR") == "True";
            this.ChkOCR.CheckedChanged += new System.EventHandler(this.ChkOCR_CheckedChanged);

            this.TxtX.Text = ReadConfigurationInt("OCR.X", Location.X).ToString();
            this.TxtX.TextChanged += new System.EventHandler(this.TxtX_TextChanged);

            this.TxtY.Text = ReadConfigurationInt("OCR.Y", Location.Y).ToString();
            this.TxtY.TextChanged += new System.EventHandler(this.TxtY_TextChanged);

            int width = ReadConfigurationInt("OCR.Width", this.Width);
            this.TxtWidth.Text = width.ToString();
            this.TxtWidth.TextChanged += new System.EventHandler(this.TxtWidth_TextChanged);

            int height = ReadConfigurationInt("OCR.Height", this.Height);
            this.TxtHeight.Text = height.ToString();
            this.TxtHeight.TextChanged += new System.EventHandler(this.TxtHeight_TextChanged);

            _mOcrManager.LoadConfig(this);
            _mOcrManager.SetInputEvents(PicBoxPreview);
            _mOcrManager.Init(width, height);

            TimerCapture.Interval = 100;
            TimerCapture.Start();
        }

        private void ChkOCR_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfiguration("OCR", ChkOCR.Checked.ToString());
        }

        private void TxtX_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            int val;
            if (int.TryParse(control.Text, out val))
            {
                UpdateConfiguration("OCR.X", val.ToString());
                if (!_mOcrManager._mMouseDown)
                {
                    _mOcrManager._mMouseMoveStart.X = val;
                    _mOcrManager._mMouseMoveEnd.X = 0;
                }
            }
        }

        private void TxtY_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            int val;
            if (int.TryParse(control.Text, out val))
            {
                UpdateConfiguration("OCR.Y", val.ToString());
                if (!_mOcrManager._mMouseDown)
                {
                    _mOcrManager._mMouseMoveStart.Y = val;
                    _mOcrManager._mMouseMoveEnd.Y = 0;
                }
            }
        }

        private void TxtWidth_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            int val;
            if (int.TryParse(control.Text, out val) &&
                val > 0 &&
                val < 2048)
            {
                UpdateConfiguration("OCR.Width", val.ToString());
                _mOcrManager.Uninit();
                _mOcrManager.Init(val, _mOcrManager._mHeight);
            }
        }

        private void TxtHeight_TextChanged(object sender, EventArgs e)
        {
            TextBox control = sender as TextBox;
            int val;
            if (int.TryParse(control.Text, out val) &&
                val > 0 &&
                val < 2048)
            {
                UpdateConfiguration("OCR.Height", val.ToString());
                _mOcrManager.Uninit();
                _mOcrManager.Init(_mOcrManager._mWidth, val);
            }
        }

        private void LoadTab()
        {
            string selectedTab = ReadConfiguration("SelectedTab");
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Text == selectedTab)
                {
                    tabControl1.SelectedTab = tab;
                    break;
                }
            }

            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTab = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
            UpdateConfiguration("SelectedTab", selectedTab);
        }

        List<string> _mRecentTexts = new List<string>();

        private bool CheckForUniqueRecentText(string text)
        {
            // reduce text to just letters
            string match = Regex.Replace(text, @"[^a-zA-Z]", "").ToLower();
            if (_mRecentTexts.Contains(match))
            {
                return true;
            }
            else
            {
                _mRecentTexts.Add(match);
                if (_mRecentTexts.Count > 10)
                {
                    _mRecentTexts.RemoveAt(0);
                }
                return false;
            }
        }

        private async void TimerCapture_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != TabOCR)
            {
                return;
            }

            if (ChkOCR.Checked)
            {
                ResultOCR result = await _mOcrManager.GetTextFromScreen(PicBoxPreview);
                if (tabControl1.SelectedTab != TabOCR)
                {
                    return;
                }
                if (result == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(result._mError))
                {
                    TxtResponse.Text = result._mError;
                    return;
                }
                string text = result._mText;
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }

                text = text.Replace("\n", "\r\n").Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    if (ChkOutputSpeak.Checked &&
                        DropDownOutputVoice.SelectedIndex > 0)
                    {
                        if (TxtResponse.Text != text &&
                            !_mAudioManager._mIsSpeaking &&
                            !CheckForUniqueRecentText(text))
                        {
                            TxtResponse.Text = text;
                            Speak();
                        }

                    }
                    else
                    {
                        if (TxtResponse.Text != text &&
                            !CheckForUniqueRecentText(text))
                        {
                            TxtResponse.Text = text;
                        }
                    }

                }
            }
        }

        private int StrToInt(string str)
        {
            int val;
            if (int.TryParse(str, out val))
            {
                return val;
            }
            return 0;
        }

        private void BtnMarquee_Click(object sender, EventArgs e)
        {
            if (FormMarquee._sIsOpen)
            {
                return;
            }

            // open form marquee
            FormMarquee formMarquee = new FormMarquee();

            // show form marquee
            formMarquee.Show(this);

            formMarquee._mParent = this;

            int x = StrToInt(this.TxtX.Text);
            int y = StrToInt(this.TxtY.Text) - FormMarquee.TITLE_PADDING;
            int width = StrToInt(this.TxtWidth.Text);
            int height = StrToInt(this.TxtHeight.Text) + FormMarquee.TITLE_PADDING;

            formMarquee.Location = new Point(x, y);
            formMarquee.Width = width;
            formMarquee.Height = height;

            formMarquee.SetupInputEvents();
        }

        private void BtnImageClear_Click(object sender, EventArgs e)
        {
            _mImages.Clear();
            LblImageCollection.Text = "0";
            BtnImageSubmit.Enabled = false;
        }

        private void BtnImageSubmit_Click(object sender, EventArgs e)
        {
            if (_mImages.Count > 0)
            {
                PromptOllamaGenerate();
            }
        }

        private void ChkResponseClipboard_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfiguration("CopyResponseToClipboard", ChkResponseClipboard.Checked.ToString());
        }
    }
}
