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

        bool _mMouseDown = false;
        bool _mMouseOver = false;
        public static Point _sMouseMoveStart = Point.Empty;
        public static Point _sMouseMoveEnd = Point.Empty;
        public static Point _sMouseMoveOffset = Point.Empty;

        private Image _mCaptureImage = null;

        private static readonly HttpClient client = new HttpClient();

        private bool _mIsRecognizing = false;

        public void LoadConfig(Form1 form1)
        {
            _mForm1 = form1;

            int x;
            int y;
            int.TryParse(_mForm1.TxtX.Text, out x);
            int.TryParse(_mForm1.TxtY.Text, out y);
            _sMouseMoveStart = new Point(x, y);
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
                _sMouseMoveStart = new Point(e.X + _sMouseMoveOffset.X, e.Y + _sMouseMoveOffset.Y);
                _sMouseMoveOffset = Point.Empty;
            }
        }

        public void PictureMouseUp(object sender, MouseEventArgs e)
        {
            if (_mMouseDown)
            {
                _sMouseMoveEnd = new Point(e.X + _sMouseMoveOffset.X, e.Y + _sMouseMoveOffset.Y);
                _sMouseMoveOffset = new Point(_sMouseMoveStart.X - _sMouseMoveEnd.X, _sMouseMoveStart.Y - _sMouseMoveEnd.Y);
            }
            _mMouseDown = false;
        }

        public void PictureMouseMove(object sender, MouseEventArgs e)
        {
            if (_mMouseDown)
            {
                _sMouseMoveEnd = new Point(e.X + _sMouseMoveOffset.X, e.Y + _sMouseMoveOffset.Y);

                _mForm1.TxtX.Text = (_sMouseMoveStart.X - _sMouseMoveEnd.X).ToString();
                _mForm1.TxtY.Text = (_sMouseMoveStart.Y - _sMouseMoveEnd.Y).ToString();
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

        private string CaptureBase64String(ComboBox dropDownDisplay, PictureBox pictureBox)
        {
            string base64String = string.Empty;

            Graphics captureGraphics = null;
            Graphics g = null;
            Bitmap bmp = null;
            //Brush brushBlack = null;
            Brush brushCapture = null;
            Pen pen = null;

            try
            {
                int selectedIndex = dropDownDisplay.SelectedIndex;

                if (selectedIndex < 1 || (selectedIndex - 1) >= Screen.AllScreens.Length)
                {
                    return string.Empty; // skip capture
                }

                // get the selected screen
                Screen screen = Screen.AllScreens[selectedIndex - 1];

                // capture from screen
                Rectangle captureRectangle = screen.Bounds;
                // create graphics
                captureGraphics = Graphics.FromImage(_mCaptureImage);
                // copy pixels from screen
                captureGraphics.CopyFromScreen(
                    captureRectangle.Left + _sMouseMoveStart.X - _sMouseMoveEnd.X,
                    captureRectangle.Top + _sMouseMoveStart.Y - _sMouseMoveEnd.Y, 0, 0, captureRectangle.Size);

                // do some cropping
                g = pictureBox.CreateGraphics();

                // clear the picture box
                //brushBlack = new SolidBrush(Color.Black);
                //g.FillRectangle(brushBlack, 0, 0, pictureBox.Width, pictureBox.Height);

                Rectangle rectCropArea = new Rectangle(
                    0,
                    0,
                    pictureBox.Width,
                    pictureBox.Height);

                g.DrawImage(_mCaptureImage, 0, 0);
                bmp = new Bitmap(_mCaptureImage);
                bmp = bmp.Clone(rectCropArea, bmp.PixelFormat);

                // convert to base64 string
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();
                    base64String = Convert.ToBase64String(byteImage);
                }
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
                //if (brushBlack != null)
                //{
                //    brushBlack.Dispose();
                //}
                if (brushCapture != null)
                {
                    brushCapture.Dispose();
                }
                if (bmp != null)
                {
                    bmp.Dispose();
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

                var response = await client.SendAsync(httpRequestMessage);
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

        public async Task<string> GetTextFromScreen(ComboBox dropDownDisplay, PictureBox pictureBox)
        {
            string base64String = CaptureBase64String(dropDownDisplay, pictureBox);
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
