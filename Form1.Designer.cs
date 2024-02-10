namespace WinForm_Ollama_Copilot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TxtPrompt = new System.Windows.Forms.TextBox();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.ChkStayAwake = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnPrompt = new System.Windows.Forms.Button();
            this.ChkDictation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblVolume = new System.Windows.Forms.Label();
            this.LblThreshold = new System.Windows.Forms.Label();
            this.PbVolume = new System.Windows.Forms.ProgressBar();
            this.SliderTheshold = new System.Windows.Forms.TrackBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.DropDownFocus = new System.Windows.Forms.ComboBox();
            this.BtnPaste = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DropDownModels = new System.Windows.Forms.ComboBox();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.ChkOutputSpeak = new System.Windows.Forms.CheckBox();
            this.DropDownOutputVoice = new System.Windows.Forms.ComboBox();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.DropDownInputDevice = new System.Windows.Forms.ComboBox();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblResponse = new System.Windows.Forms.Label();
            this.TxtResponse = new System.Windows.Forms.TextBox();
            this.TimerDetection = new System.Windows.Forms.Timer(this.components);
            this.TimerModels = new System.Windows.Forms.Timer(this.components);
            this.TimerAwake = new System.Windows.Forms.Timer(this.components);
            this.TimerDictation = new System.Windows.Forms.Timer(this.components);
            this.TimerVolume = new System.Windows.Forms.Timer(this.components);
            this.TimerSpeaking = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PanelTop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTheshold)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.PanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtPrompt
            // 
            this.TxtPrompt.AllowDrop = true;
            this.TxtPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrompt.Location = new System.Drawing.Point(6, 5);
            this.TxtPrompt.MaxLength = 100000;
            this.TxtPrompt.Multiline = true;
            this.TxtPrompt.Name = "TxtPrompt";
            this.TxtPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtPrompt.Size = new System.Drawing.Size(427, 161);
            this.TxtPrompt.TabIndex = 0;
            this.TxtPrompt.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragDrop);
            this.TxtPrompt.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragEnter);
            // 
            // PanelTop
            // 
            this.PanelTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelTop.Controls.Add(this.tabControl1);
            this.PanelTop.Controls.Add(this.BtnPaste);
            this.PanelTop.Controls.Add(this.label3);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(584, 207);
            this.PanelTop.TabIndex = 2;
            // 
            // ChkStayAwake
            // 
            this.ChkStayAwake.AutoSize = true;
            this.ChkStayAwake.Location = new System.Drawing.Point(10, 73);
            this.ChkStayAwake.Name = "ChkStayAwake";
            this.ChkStayAwake.Size = new System.Drawing.Size(83, 17);
            this.ChkStayAwake.TabIndex = 2;
            this.ChkStayAwake.Text = "Stay Awake";
            this.ChkStayAwake.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkStayAwake.UseVisualStyleBackColor = true;
            this.ChkStayAwake.CheckedChanged += new System.EventHandler(this.ChkStayAwake_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(514, 198);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TxtPrompt);
            this.tabPage1.Controls.Add(this.BtnPrompt);
            this.tabPage1.Controls.Add(this.ChkDictation);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(506, 172);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Prompt";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnPrompt
            // 
            this.BtnPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrompt.Location = new System.Drawing.Point(436, 143);
            this.BtnPrompt.Name = "BtnPrompt";
            this.BtnPrompt.Size = new System.Drawing.Size(64, 23);
            this.BtnPrompt.TabIndex = 1;
            this.BtnPrompt.Text = "Prompt";
            this.BtnPrompt.UseVisualStyleBackColor = true;
            this.BtnPrompt.Click += new System.EventHandler(this.BtnPrompt_Click);
            // 
            // ChkDictation
            // 
            this.ChkDictation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkDictation.AutoSize = true;
            this.ChkDictation.Location = new System.Drawing.Point(440, 6);
            this.ChkDictation.Name = "ChkDictation";
            this.ChkDictation.Size = new System.Drawing.Size(60, 17);
            this.ChkDictation.TabIndex = 4;
            this.ChkDictation.Text = "Dictate";
            this.ChkDictation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkDictation.UseVisualStyleBackColor = true;
            this.ChkDictation.CheckedChanged += new System.EventHandler(this.ChkDictation_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(439, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "CTRL+ ENTER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.LblVolume);
            this.tabPage2.Controls.Add(this.LblThreshold);
            this.tabPage2.Controls.Add(this.DropDownInputDevice);
            this.tabPage2.Controls.Add(this.PbVolume);
            this.tabPage2.Controls.Add(this.SliderTheshold);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(751, 172);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "STT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Input Threshold:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Volume:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblVolume
            // 
            this.LblVolume.AutoSize = true;
            this.LblVolume.Location = new System.Drawing.Point(230, 10);
            this.LblVolume.Name = "LblVolume";
            this.LblVolume.Size = new System.Drawing.Size(21, 13);
            this.LblVolume.TabIndex = 12;
            this.LblVolume.Text = "0%";
            this.LblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblThreshold
            // 
            this.LblThreshold.AutoSize = true;
            this.LblThreshold.Location = new System.Drawing.Point(230, 35);
            this.LblThreshold.Name = "LblThreshold";
            this.LblThreshold.Size = new System.Drawing.Size(21, 13);
            this.LblThreshold.TabIndex = 14;
            this.LblThreshold.Text = "0%";
            this.LblThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PbVolume
            // 
            this.PbVolume.BackColor = System.Drawing.Color.Black;
            this.PbVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.PbVolume.Location = new System.Drawing.Point(96, 6);
            this.PbVolume.Maximum = 32767;
            this.PbVolume.Name = "PbVolume";
            this.PbVolume.Size = new System.Drawing.Size(286, 20);
            this.PbVolume.TabIndex = 11;
            // 
            // SliderTheshold
            // 
            this.SliderTheshold.AutoSize = false;
            this.SliderTheshold.Location = new System.Drawing.Point(96, 32);
            this.SliderTheshold.Maximum = 100;
            this.SliderTheshold.Name = "SliderTheshold";
            this.SliderTheshold.Size = new System.Drawing.Size(286, 20);
            this.SliderTheshold.TabIndex = 13;
            this.SliderTheshold.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SliderTheshold.Scroll += new System.EventHandler(this.SliderTheshold_Scroll);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(751, 172);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Images";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(751, 172);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "OCR";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.BtnLoad);
            this.tabPage5.Controls.Add(this.BtnClear);
            this.tabPage5.Controls.Add(this.BtnSave);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(751, 172);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "History";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(39, 87);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(58, 23);
            this.BtnLoad.TabIndex = 6;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(39, 29);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(58, 23);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(39, 58);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(54, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.DropDownModels);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Controls.Add(this.ChkStayAwake);
            this.tabPage6.Controls.Add(this.label5);
            this.tabPage6.Controls.Add(this.DropDownFocus);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(506, 172);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Config";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Forward response to application:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DropDownFocus
            // 
            this.DropDownFocus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDownFocus.FormattingEnabled = true;
            this.DropDownFocus.Location = new System.Drawing.Point(10, 142);
            this.DropDownFocus.Name = "DropDownFocus";
            this.DropDownFocus.Size = new System.Drawing.Size(484, 21);
            this.DropDownFocus.TabIndex = 2;
            this.DropDownFocus.SelectedIndexChanged += new System.EventHandler(this.DropDownFocus_SelectedIndexChanged);
            // 
            // BtnPaste
            // 
            this.BtnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPaste.Location = new System.Drawing.Point(524, 171);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(57, 23);
            this.BtnPaste.TabIndex = 10;
            this.BtnPaste.Text = "Paste";
            this.BtnPaste.UseVisualStyleBackColor = true;
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(524, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "CTRL+V";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DropDownModels
            // 
            this.DropDownModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDownModels.FormattingEnabled = true;
            this.DropDownModels.Location = new System.Drawing.Point(10, 25);
            this.DropDownModels.Name = "DropDownModels";
            this.DropDownModels.Size = new System.Drawing.Size(484, 21);
            this.DropDownModels.TabIndex = 9;
            this.DropDownModels.SelectedIndexChanged += new System.EventHandler(this.CboModel_SelectedIndexChanged);
            // 
            // PanelBottom
            // 
            this.PanelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelBottom.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelBottom.Controls.Add(this.ChkOutputSpeak);
            this.PanelBottom.Controls.Add(this.DropDownOutputVoice);
            this.PanelBottom.Controls.Add(this.BtnStop);
            this.PanelBottom.Controls.Add(this.BtnPlay);
            this.PanelBottom.Controls.Add(this.LblVersion);
            this.PanelBottom.Controls.Add(this.LblResponse);
            this.PanelBottom.Controls.Add(this.TxtResponse);
            this.PanelBottom.Location = new System.Drawing.Point(0, 208);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(584, 303);
            this.PanelBottom.TabIndex = 3;
            // 
            // ChkOutputSpeak
            // 
            this.ChkOutputSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChkOutputSpeak.AutoSize = true;
            this.ChkOutputSpeak.Location = new System.Drawing.Point(8, 280);
            this.ChkOutputSpeak.Name = "ChkOutputSpeak";
            this.ChkOutputSpeak.Size = new System.Drawing.Size(57, 17);
            this.ChkOutputSpeak.TabIndex = 10;
            this.ChkOutputSpeak.Text = "Speak";
            this.ChkOutputSpeak.UseVisualStyleBackColor = true;
            this.ChkOutputSpeak.CheckedChanged += new System.EventHandler(this.ChkOutputSpeak_CheckedChanged);
            // 
            // DropDownOutputVoice
            // 
            this.DropDownOutputVoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DropDownOutputVoice.FormattingEnabled = true;
            this.DropDownOutputVoice.Location = new System.Drawing.Point(74, 277);
            this.DropDownOutputVoice.Name = "DropDownOutputVoice";
            this.DropDownOutputVoice.Size = new System.Drawing.Size(316, 21);
            this.DropDownOutputVoice.TabIndex = 9;
            this.DropDownOutputVoice.SelectedIndexChanged += new System.EventHandler(this.DropDownOutputVoice_SelectedIndexChanged);
            // 
            // BtnStop
            // 
            this.BtnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStop.Location = new System.Drawing.Point(492, 276);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 7;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnPlay
            // 
            this.BtnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPlay.Location = new System.Drawing.Point(411, 276);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(75, 23);
            this.BtnPlay.TabIndex = 6;
            this.BtnPlay.Text = "Play";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // DropDownInputDevice
            // 
            this.DropDownInputDevice.FormattingEnabled = true;
            this.DropDownInputDevice.Location = new System.Drawing.Point(96, 58);
            this.DropDownInputDevice.Name = "DropDownInputDevice";
            this.DropDownInputDevice.Size = new System.Drawing.Size(286, 21);
            this.DropDownInputDevice.TabIndex = 5;
            this.DropDownInputDevice.SelectedIndexChanged += new System.EventHandler(this.CboInputDevice_SelectedIndexChanged);
            // 
            // LblVersion
            // 
            this.LblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblVersion.Location = new System.Drawing.Point(416, 5);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(151, 16);
            this.LblVersion.TabIndex = 3;
            this.LblVersion.Text = "Ollama API Version";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblResponse
            // 
            this.LblResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblResponse.AutoSize = true;
            this.LblResponse.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LblResponse.Location = new System.Drawing.Point(7, 7);
            this.LblResponse.Name = "LblResponse";
            this.LblResponse.Size = new System.Drawing.Size(58, 13);
            this.LblResponse.TabIndex = 1;
            this.LblResponse.Text = "Response:";
            this.LblResponse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtResponse
            // 
            this.TxtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtResponse.Location = new System.Drawing.Point(4, 27);
            this.TxtResponse.Multiline = true;
            this.TxtResponse.Name = "TxtResponse";
            this.TxtResponse.ReadOnly = true;
            this.TxtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtResponse.Size = new System.Drawing.Size(563, 243);
            this.TxtResponse.TabIndex = 0;
            // 
            // TimerDetection
            // 
            this.TimerDetection.Tick += new System.EventHandler(this.TimerDetection_Tick);
            // 
            // TimerModels
            // 
            this.TimerModels.Interval = 5000;
            this.TimerModels.Tick += new System.EventHandler(this.TimerModels_Tick);
            // 
            // TimerAwake
            // 
            this.TimerAwake.Interval = 10000;
            this.TimerAwake.Tick += new System.EventHandler(this.TimerAwake_Tick);
            // 
            // TimerDictation
            // 
            this.TimerDictation.Tick += new System.EventHandler(this.TimerDictation_Tick);
            // 
            // TimerVolume
            // 
            this.TimerVolume.Tick += new System.EventHandler(this.TimerVolume_Tick);
            // 
            // TimerSpeaking
            // 
            this.TimerSpeaking.Interval = 500;
            this.TimerSpeaking.Tick += new System.EventHandler(this.TimerSpeaking_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Input Device:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Selected Model:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(584, 511);
            this.Controls.Add(this.PanelBottom);
            this.Controls.Add(this.PanelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Name = "Form1";
            this.Text = "Ollama Copilot v1.0.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PanelTop.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTheshold)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.PanelBottom.ResumeLayout(false);
            this.PanelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPrompt;
        private System.Windows.Forms.Panel PanelTop;
        private System.Windows.Forms.Panel PanelBottom;
        private System.Windows.Forms.Label LblResponse;
        private System.Windows.Forms.TextBox TxtResponse;
        private System.Windows.Forms.ComboBox DropDownFocus;
        private System.Windows.Forms.Timer TimerDetection;
        private System.Windows.Forms.Timer TimerModels;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnPrompt;
        private System.Windows.Forms.Button BtnPaste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DropDownModels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ChkStayAwake;
        private System.Windows.Forms.Timer TimerAwake;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.CheckBox ChkDictation;
        private System.Windows.Forms.ComboBox DropDownInputDevice;
        private System.Windows.Forms.Timer TimerDictation;
        private System.Windows.Forms.ProgressBar PbVolume;
        private System.Windows.Forms.Label LblVolume;
        private System.Windows.Forms.Timer TimerVolume;
        private System.Windows.Forms.TrackBar SliderTheshold;
        private System.Windows.Forms.Label LblThreshold;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Button BtnPlay;
        private System.Windows.Forms.ComboBox DropDownOutputVoice;
        private System.Windows.Forms.CheckBox ChkOutputSpeak;
        private System.Windows.Forms.Timer TimerSpeaking;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

