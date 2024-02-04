//#define TEST_SAVE_AUDIO

using NAudio.CoreAudioApi;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Ollama_Copilot
{
    internal class AudioManager
    {
        NAudio.Wave.WaveInEvent Wave;

        readonly List<int> AudioIntValues = new List<int>();
#if TEST_SAVE_AUDIO
        readonly List<float> AudioFloatValues = new List<float>();
#endif

        const int DEFAULT_SAMPLE_RATE = 32000;
        const int DEFAULT_CHANNEL_COUNT = 1;

        private int SampleRate = DEFAULT_SAMPLE_RATE;
        private int ChannelCount = DEFAULT_CHANNEL_COUNT;
        readonly int BitDepth = 16;
        readonly int BufferMilliseconds = 1000; // ms

        private static readonly HttpClient client = new HttpClient();

        public List<string> InputDevices = new List<string>();

        public List<string> DetectedWords = new List<string>();

        public float Gain = 10f;

        private bool _WaitingForResponse = false;

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
            const int INPUT_THRESHOLD = 5000;

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

            int volume = tempSample.Max();

            // capture audio that's loud enough
            if (volume > INPUT_THRESHOLD)
            {
                for (int i = 0; i < tempSample.Count; ++i)
                {
                    int data = tempSample[i];
                    AudioIntValues.Add(data);
#if TEST_SAVE_AUDIO
                AudioFloatValues.Add(data / 32767.0f);
#endif
                }
            }

            if (AudioIntValues.Count > DEFAULT_SAMPLE_RATE)
            {
#if TEST_SAVE_AUDIO
                // save audio values as a wav file
                try
                {
                    // Create a WaveFileWriter object
                    using (WaveFileWriter writer = new WaveFileWriter("audio.wav", Wave.WaveFormat))
                    {
                        // Write the float array to the WAV file
                        writer.WriteSamples(AudioFloatValues.ToArray(), 0, AudioFloatValues.Count);
                        AudioFloatValues.Clear();
                    }

                }
                catch
                {

                }
#endif

                if (_WaitingForResponse)
                {
                    return;
                }

                // wait for talking to end
                if (volume <= INPUT_THRESHOLD)
                {                    
                    Translate();
                }
            }
        }
    }
}
