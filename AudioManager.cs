using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WinForm_Ollama_Copilot
{
    internal class AudioManager
    {
        public int _mInputThreshold = 1000; // of 32768
        public int _mVolume = 0;

        WaveInEvent Wave;

        readonly List<int> AudioIntValues = new List<int>();

        const int DEFAULT_SAMPLE_RATE = 32000;
        const int DEFAULT_CHANNEL_COUNT = 1;

        private int SampleRate = DEFAULT_SAMPLE_RATE;
        private int ChannelCount = DEFAULT_CHANNEL_COUNT;
        readonly int BitDepth = 16;
        readonly int BufferMilliseconds = 1000; // ms

        private static readonly HttpClient client = new HttpClient();

        public List<string> InputDevices = new List<string>();

        public List<string> DetectedWords = new List<string>();

        private bool _WaitingForResponse = false;

        private DateTime _mTimerSend = DateTime.MinValue;

        public bool _mIgnoreRecording = false;

        public AudioManager()
        {
            #region Input devices

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var caps = WaveIn.GetCapabilities(i);
                InputDevices.Add(caps.ProductName);
            }

            #endregion Input devices
        }

        private async void Translate()
        {
            _WaitingForResponse = true;

            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "http://localhost:11437/translate");

                JObject jobject = new JObject();
                jobject["sampleRate"] = SampleRate;

                JArray jarray = new JArray();

                float volume = AudioIntValues.Max();
                if (volume < 1)
                {
                    volume = 1;
                }

                for (int i = 0; i < AudioIntValues.Count; i++)
                {
                    float data = (float)(AudioIntValues[i] / volume);
                    jarray.Add(data);
                }

                jobject["data"] = jarray;

                AudioIntValues.Clear();

                string pJsonContent = jobject.ToString();

                HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;

                var productValue = new ProductInfoHeaderValue("Whisper_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11437/transcribe)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await client.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        string text = await content.ReadAsStringAsync();
                        try
                        {
                            JObject responseJson = JObject.Parse(text);
                            string sentence = responseJson["text"].ToString();
                            DetectedWords.Add(sentence);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
            }
            _WaitingForResponse = false;
        }

        public void StopInputDeviceRecording()
        {
            if (Wave != null)
            {
                Wave.StopRecording();
                Wave.Dispose();
            }
        }

        public void SelectInputDevice(int deviceNumber)
        {
            StopInputDeviceRecording();

            if (deviceNumber < 0)
                return;

            if (deviceNumber < WaveIn.DeviceCount)
            {
                SampleRate = DEFAULT_SAMPLE_RATE;
                ChannelCount = DEFAULT_CHANNEL_COUNT;

                Wave = new WaveInEvent()
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new WaveFormat(SampleRate, BitDepth, ChannelCount),
                    BufferMilliseconds = BufferMilliseconds
                };

                Wave.DataAvailable += Wave_DataAvailable;
                Wave.StartRecording();
            }
        }

        void Wave_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_mIgnoreRecording)
            {
                return;
            }

            if (e.BytesRecorded == 0)
            {
                return;
            }

            List<int> tempSample = new List<int>();
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                int data = BitConverter.ToInt16(e.Buffer, i);
                tempSample.Add(data);
            }

            _mVolume = tempSample.Max();

            // capture audio that's loud enough
            if (_mVolume > _mInputThreshold)
            {
                _mTimerSend = DateTime.Now + TimeSpan.FromMilliseconds(500); // Send audio after done talking
                for (int i = 0; i < tempSample.Count; ++i)
                {
                    int data = tempSample[i];
                    AudioIntValues.Add(data);
                }
            }

            if (AudioIntValues.Count > 0 &&
                _mTimerSend < DateTime.Now)
            {
                // reset timer
                _mTimerSend = DateTime.MinValue;

                if (_WaitingForResponse)
                {
                    return;
                }

                Translate();
            }
        }
    }
}
