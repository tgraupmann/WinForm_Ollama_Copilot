using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Ollama_Copilot
{
    public partial class Form1 : Form
    {
        struct WindowFocus
        {
            public IntPtr Hwnd;
            public String Title;
        }

        List<WindowFocus> DetectedWindows = new List<WindowFocus>();

        void DetectForeground()
        {
            WindowFocus win;
            win.Hwnd = NativeUtils.getForegroundWindow();
            if (win.Hwnd == IntPtr.Zero)
            {
                return;
            }
            win.Title = NativeUtils.GetActiveWindowTitle();
            if (string.IsNullOrEmpty(win.Title) ||
                win.Title == "Ollama Copilot")
            {
                return;
            }
            foreach (WindowFocus w in DetectedWindows)
            {
                if (w.Hwnd == win.Hwnd && w.Title == win.Title)
                {
                    return;
                }
            }
            DetectedWindows.Add(win);
            DropDownFocus.Items.Add(win.Title);
            if (DetectedWindows.Count > 0)
            {
                DropDownFocus.SelectedIndex = 0;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TimerDetection.Interval = 250;
            TimerDetection.Start();
        }

        private void BtnPrompt_Click(object sender, EventArgs e)
        {
            if (DropDownFocus.SelectedIndex >= 0)
            {
                WindowFocus win = DetectedWindows[DropDownFocus.SelectedIndex];
                NativeUtils.SetForegroundWindow(win.Hwnd);
                SendKeys.Send("Hello");
            }
        }

        private void TxtPrompt_TextChanged(object sender, EventArgs e)
        {

        }

        private void TimerDetection_Tick(object sender, EventArgs e)
        {
            DetectForeground();
        }
    }
}
