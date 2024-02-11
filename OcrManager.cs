using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Ollama_Copilot
{
    internal class OcrManager
    {
        bool _mMouseDown = false;
        bool _mMouseOver = false;
        public static Point _sMouseMoveStart = Point.Empty;
        public static Point _sMouseMoveEnd = Point.Empty;
        public static Point _sMouseMoveOffset = Point.Empty;

        private Image _mCaptureImage = null;

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

        public void CaptureScreen(ComboBox dropDownDisplay, PictureBox pictureBox)
        {
            Graphics captureGraphics = null;
            Graphics g = null;
            Bitmap bmp = null;
            Brush brushBlack = null;
            Brush brushCapture = null;
            Pen pen = null;

            try
            {
                int selectedIndex = dropDownDisplay.SelectedIndex;

                if (selectedIndex < 1 || (selectedIndex - 1) >= Screen.AllScreens.Length)
                {
                    return; // skip capture
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
                if (brushBlack != null)
                {
                    brushBlack.Dispose();
                }
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
        }
    }
}
