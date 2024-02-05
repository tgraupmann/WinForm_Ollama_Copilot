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
    internal class SpeakManager
    {
        private bool _WaitingForResponse = false;

        private static readonly HttpClient client = new HttpClient();

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
    }
}
