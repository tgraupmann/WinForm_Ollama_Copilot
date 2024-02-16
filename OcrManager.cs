using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;

namespace WinForm_Ollama_Copilot
{
    internal class OcrManager
    {
        Form1 _mForm1 = null;

        public int _mWidth = 0;
        public int _mHeight = 0;

        public bool _mMouseDown = false;
        public bool _mMouseOver = false;
        public Point _mMouseMoveStart = Point.Empty;
        public Point _mMouseMoveEnd = Point.Empty;
        public Point _mMouseMoveOffset = Point.Empty;

        private Image _mCaptureImage = null;

        private readonly HttpClient _mClient = new HttpClient();

        private bool _mIsRecognizing = false;

        public void LoadConfig(Form1 form1)
        {
            _mForm1 = form1;

            int x;
            int y;
            int.TryParse(_mForm1.TxtX.Text, out x);
            int.TryParse(_mForm1.TxtY.Text, out y);
            _mMouseMoveStart = new Point(x, y);
        }

        public void Uninit()
        {
            if (_mCaptureImage != null)
            {
                _mCaptureImage.Dispose();
                _mCaptureImage = null;
            }
        }

        public void Init(int width, int height)
        {
            if (_mCaptureImage == null)
            {
                _mWidth = width;
                _mHeight = height;
                _mCaptureImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            }
        }

        #region Input Events

        public void SetInputEvents(PictureBox pictureBox)
        {
            pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureMouseDown);
            pictureBox.MouseEnter += new System.EventHandler(this.PictureMouseEnter);
            pictureBox.MouseLeave += new System.EventHandler(this.PictureMouseLeave);
            pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureMouseMove);
            pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureMouseUp);
        }

        public void PictureMouseDown(object sender, MouseEventArgs e)
        {
            if (!_mMouseDown && _mMouseOver)
            {
                _mMouseDown = true;
                _mMouseMoveStart = new Point(e.X + _mMouseMoveOffset.X, e.Y + _mMouseMoveOffset.Y);
                _mMouseMoveOffset = Point.Empty;
            }
        }

        public void PictureMouseUp(object sender, MouseEventArgs e)
        {
            if (_mMouseDown)
            {
                _mMouseMoveEnd = new Point(e.X + _mMouseMoveOffset.X, e.Y + _mMouseMoveOffset.Y);
                _mMouseMoveOffset = new Point(_mMouseMoveStart.X - _mMouseMoveEnd.X, _mMouseMoveStart.Y - _mMouseMoveEnd.Y);
            }
            _mMouseDown = false;
        }

        public void PictureMouseMove(object sender, MouseEventArgs e)
        {
            if (_mMouseDown)
            {
                _mMouseMoveEnd = new Point(e.X + _mMouseMoveOffset.X, e.Y + _mMouseMoveOffset.Y);

                _mForm1.TxtX.Text = (_mMouseMoveStart.X - _mMouseMoveEnd.X).ToString();
                _mForm1.TxtY.Text = (_mMouseMoveStart.Y - _mMouseMoveEnd.Y).ToString();
            }
        }

        public void PictureMouseEnter(object sender, EventArgs e)
        {
            _mMouseOver = true;
        }

        public void PictureMouseLeave(object sender, EventArgs e)
        {
            _mMouseOver = false;
        }

        #endregion Input Events

        private string CaptureBase64String(PictureBox pictureBox)
        {
            string base64String = string.Empty;

            Graphics captureGraphics = null;
            Graphics g = null;
            Bitmap captureBitmap = null;
            Brush brushCapture = null;
            Pen pen = null;

            try
            {
                // create graphics
                captureGraphics = Graphics.FromImage(_mCaptureImage);
                // copy pixels from screen
                captureGraphics.CopyFromScreen(
                    _mMouseMoveStart.X - _mMouseMoveEnd.X,
                    _mMouseMoveStart.Y - _mMouseMoveEnd.Y,
                    0,
                    0,
                    new Size(_mWidth, _mHeight));

                // do some cropping
                g = pictureBox.CreateGraphics();

                // extract the image
                captureBitmap = new Bitmap(_mCaptureImage);

                // convert to base64 string
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    captureBitmap.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();
                    base64String = Convert.ToBase64String(byteImage);
                }

                g.DrawImage(_mCaptureImage, 0, 0, pictureBox.Width, pictureBox.Height);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to capture image exception: {0}", ex);
            }
            finally
            {
                // cleanup
                if (pen != null)
                {
                    pen.Dispose();
                }
                if (brushCapture != null)
                {
                    brushCapture.Dispose();
                }
                if (captureBitmap != null)
                {
                    captureBitmap.Dispose();
                }
                if (g != null)
                {
                    g.Dispose();
                }
                if (captureGraphics != null)
                {
                    captureGraphics.Dispose();
                }
            }

            return base64String;
        }

        private async Task<string> Base64ImageToString(string base64String)
        {
            string result = string.Empty;
            try
            {

                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "http://localhost:11439/image_to_string");

                JObject jobject = new JObject();
                jobject["data"] = base64String;

                string pJsonContent = jobject.ToString();

                HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                httpRequestMessage.Content = httpContent;

                var productValue = new ProductInfoHeaderValue("OcrManager_Client", "1.0");
                var commentValue = new ProductInfoHeaderValue("(+http://localhost:11439/image_to_string)");
                httpRequestMessage.Headers.UserAgent.Add(productValue);
                httpRequestMessage.Headers.UserAgent.Add(commentValue);

                var response = await _mClient.SendAsync(httpRequestMessage);
                if (response != null)
                {
                    using (HttpContent content = response.Content)
                    {
                        string text = await content.ReadAsStringAsync();
                        try
                        {
                            JObject responseJson = JObject.Parse(text);
                            result = responseJson["result"].ToString();
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
            return result;
        }

        public async Task<string> GetTextFromScreen(PictureBox pictureBox)
        {
            string base64String = CaptureBase64String(pictureBox);
            if (_mIsRecognizing)
            {
                return string.Empty;
            }
            _mIsRecognizing = true;
            string text = await Base64ImageToString(base64String);
            _mIsRecognizing = false;
            return text;
        }
    }
}
