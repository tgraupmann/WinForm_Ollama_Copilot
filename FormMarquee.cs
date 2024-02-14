using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinForm_Ollama_Copilot
{
    public partial class FormMarquee : Form
    {
        public const int TITLE_PADDING = 35;

        public Form1 _mParent = null;

        public FormMarquee()
        {
            InitializeComponent();
        }

        private void FormMarquee_Load(object sender, EventArgs e)
        {
            this.BackColor = this.TransparencyKey = Color.Red;
        }

        public void SetupInputEvents()
        {
            this.LocationChanged += new System.EventHandler(this.FormMarquee_LocationChanged);
            this.Resize += new System.EventHandler(this.FormMarquee_Resize);
        }

        private void FormMarquee_LocationChanged(object sender, EventArgs e)
        {
            _mParent.TxtX.Text = this.Location.X.ToString();
            Form1.UpdateConfiguration("OCR.X", _mParent.TxtX.Text);

            int y = this.Location.Y;
            y += TITLE_PADDING;
            _mParent.TxtY.Text = y.ToString();
            Form1.UpdateConfiguration("OCR.Y", _mParent.TxtY.Text);
        }
        private void FormMarquee_Resize(object sender, EventArgs e)
        {
            _mParent.TxtWidth.Text = this.Width.ToString();

            int height = this.Height - TITLE_PADDING;
            _mParent.TxtHeight.Text = height.ToString();
        }
    }
}
