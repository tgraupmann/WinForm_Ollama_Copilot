using NAudio.CoreAudioApi;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Ollama_Copilot
{
    internal class AudioManager
    {
        NAudio.Wave.WaveInEvent Wave;

        readonly List<int> AudioValues = new List<int>();

        const int DEFAULT_SAMPLE_RATE = 16000;
        const int DEFAULT_CHANNEL_COUNT = 1;

        private int SampleRate = DEFAULT_SAMPLE_RATE;
        private int ChannelCount = DEFAULT_CHANNEL_COUNT;
        readonly int BitDepth = 16;
        readonly int BufferMilliseconds = 1000;

        private static readonly HttpClient client = new HttpClient();

        public List<string> InputDevices = new List<string>();

        public List<string> DetectedWords = new List<string>();

        public float Gain = 2f;

        public AudioManager()
        {
            #region Input devices

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
                InputDevices.Add(caps.ProductName);
            }

            #endregion Input devices
        }

        private async void Translate()
        {
            try
            {
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "http://localhost:11437/translate");

                JObject jobject = new JObject();
                jobject["sampleRate"] = SampleRate;

                JArray jarray = new JArray();
                float k = Gain / 32767.0f;
                for (int i = 0; i < AudioValues.Count; i++)
                {
                    float data = (float)(AudioValues[i] * k);
                    jarray.Add(data);
                }

                jobject["data"] = jarray;

                AudioValues.Clear();

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

            if (deviceNumber < NAudio.Wave.WaveIn.DeviceCount)
            {
                SampleRate = DEFAULT_SAMPLE_RATE;
                ChannelCount = DEFAULT_CHANNEL_COUNT;

                Wave = new NAudio.Wave.WaveInEvent()
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
                    BufferMilliseconds = BufferMilliseconds
                };

                Wave.DataAvailable += Wave_DataAvailable;
                Wave.StartRecording();
            }            
        }

        void Wave_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (e.BytesRecorded == 0)
            {
                return;
            }

            int bufferLength = e.BytesRecorded / 2;

            List<int> tempSample = new List<int>();
            for (int i = 0; i < bufferLength; i += 2)
            {
                int data = BitConverter.ToInt16(e.Buffer, i);
                tempSample.Add(data);
            }

            float volume = tempSample.Max() / 32767.0f;

            if (AudioValues.Count == 0 &&
                volume < 0.02f)
            {
                // trim noise
                return;
            }

            for (int i = 0; i < tempSample.Count; ++i)
            {
                int data = tempSample[i];
                AudioValues.Add(data);
            }

            if (AudioValues.Count > DEFAULT_SAMPLE_RATE)
            {
                if (volume > 0.02f && AudioValues.Count < (5 * DEFAULT_SAMPLE_RATE))
                {
                    // wait to translate while talking for a while
                    return;
                }
                Translate();
            }
        }
    }
}
