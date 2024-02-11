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

        public void CaptureScreen(ComboBox dropDownDisplay, PictureBox pictureBox)
        {
            Graphics captureGraphics = null;
            Graphics g = null;
            Bitmap bmp = null;
            Brush brush = null;
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
                if (brush != null)
                {
                    brush.Dispose();
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
