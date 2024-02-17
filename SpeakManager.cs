using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Ollama_Copilot
{
    internal class SpeakManager
    {
        private bool _WaitingForResponse = false;

        private static readonly HttpClient client = new HttpClient();

        public class ResultVoices
        {
            public List<string> _mVoices = new List<string>();
            public string _mError = null;
        }

        public async Task<ResultVoices> GetVoices()
        {
            ResultVoices result = new ResultVoices();

            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "http://localhost:11438/get_voices");

                var productValue = new ProductInfoHeaderValue("Speak_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11438/get_voices)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await client.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        string pJsonContent = await content.ReadAsStringAsync();
                        JArray jarray = JArray.Parse(pJsonContent);
                        foreach (JObject jobject in jarray)
                        {
                            result._mVoices.Add(jobject["name"].ToString());
                        }
                    }
                }
            }
            catch
            {
                result._mError = "Failed to reach TTS server. Is the server running?";
            }
            return result;
        }

        public async Task<bool> IsSpeaking()
        {
            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "http://localhost:11438/is_speaking");

                var productValue = new ProductInfoHeaderValue("Speak_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11438/is_speaking)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await client.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        string pJsonContent = await content.ReadAsStringAsync();
                        JObject jobject = JObject.Parse(pJsonContent);
                        return jobject["speaking"].ToString() == "True";                        
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {

                }
            }
            return false;
        }

        public async void Speak(int voice, string sentence)
        {
            if (_WaitingForResponse)
            {
                return;
            }

            _WaitingForResponse = true;

            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "http://localhost:11438/speak");

                JObject jobject = new JObject();
                jobject["voice"] = voice;

                jobject["sentence"] = sentence;

                string pJsonContent = jobject.ToString();

                HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;

                var productValue = new ProductInfoHeaderValue("Speak_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11438/speak)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await client.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        await content.ReadAsStringAsync();                        
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

        public async Task Stop()
        {
            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "http://localhost:11438/stop");

                var productValue = new ProductInfoHeaderValue("Speak_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11438/stop)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await client.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        await content.ReadAsStringAsync();
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
    }
}
