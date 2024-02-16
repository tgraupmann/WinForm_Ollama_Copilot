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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPrompt = new System.Windows.Forms.TabPage();
            this.BtnPrompt = new System.Windows.Forms.Button();
            this.ChkDictation = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TabSTT = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblVolume = new System.Windows.Forms.Label();
            this.LblThreshold = new System.Windows.Forms.Label();
            this.DropDownInputDevice = new System.Windows.Forms.ComboBox();
            this.PbVolume = new System.Windows.Forms.ProgressBar();
            this.SliderTheshold = new System.Windows.Forms.TrackBar();
            this.TabImages = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnPaste = new System.Windows.Forms.Button();
            this.TabOCR = new System.Windows.Forms.TabPage();
            this.BtnMarquee = new System.Windows.Forms.Button();
            this.TxtY = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtHeight = new System.Windows.Forms.TextBox();
            this.TxtWidth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtX = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ChkOCR = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PicBoxPreview = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TabHistory = new System.Windows.Forms.TabPage();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TabConfig = new System.Windows.Forms.TabPage();
            this.DropDownModels = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ChkStayAwake = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DropDownFocus = new System.Windows.Forms.ComboBox();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.ChkOutputSpeak = new System.Windows.Forms.CheckBox();
            this.DropDownOutputVoice = new System.Windows.Forms.ComboBox();
            this.BtnStop = new System.Windows.Forms.Button();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.LblVersion = new System.Windows.Forms.Label();
            this.LblResponse = new System.Windows.Forms.Label();
            this.TxtResponse = new System.Windows.Forms.TextBox();
            this.TimerDetection = new System.Windows.Forms.Timer(this.components);
            this.TimerModels = new System.Windows.Forms.Timer(this.components);
            this.TimerAwake = new System.Windows.Forms.Timer(this.components);
            this.TimerDictation = new System.Windows.Forms.Timer(this.components);
            this.TimerVolume = new System.Windows.Forms.Timer(this.components);
            this.TimerSpeaking = new System.Windows.Forms.Timer(this.components);
            this.TimerCapture = new System.Windows.Forms.Timer(this.components);
            this.PanelTop.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TabPrompt.SuspendLayout();
            this.TabSTT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTheshold)).BeginInit();
            this.TabImages.SuspendLayout();
            this.TabOCR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxPreview)).BeginInit();
            this.TabHistory.SuspendLayout();
            this.TabConfig.SuspendLayout();
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
            this.TxtPrompt.Size = new System.Drawing.Size(490, 161);
            this.TxtPrompt.TabIndex = 0;
            this.TxtPrompt.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragDrop);
            this.TxtPrompt.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragEnter);
            // 
            // PanelTop
            // 
            this.PanelTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelTop.Controls.Add(this.tabControl1);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(584, 207);
            this.PanelTop.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TabPrompt);
            this.tabControl1.Controls.Add(this.TabSTT);
            this.tabControl1.Controls.Add(this.TabImages);
            this.tabControl1.Controls.Add(this.TabOCR);
            this.tabControl1.Controls.Add(this.TabHistory);
            this.tabControl1.Controls.Add(this.TabConfig);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(577, 198);
            this.tabControl1.TabIndex = 15;
            // 
            // TabPrompt
            // 
            this.TabPrompt.Controls.Add(this.TxtPrompt);
            this.TabPrompt.Controls.Add(this.BtnPrompt);
            this.TabPrompt.Controls.Add(this.ChkDictation);
            this.TabPrompt.Controls.Add(this.label1);
            this.TabPrompt.Location = new System.Drawing.Point(4, 22);
            this.TabPrompt.Name = "TabPrompt";
            this.TabPrompt.Padding = new System.Windows.Forms.Padding(3);
            this.TabPrompt.Size = new System.Drawing.Size(569, 172);
            this.TabPrompt.TabIndex = 0;
            this.TabPrompt.Text = "Prompt";
            this.TabPrompt.UseVisualStyleBackColor = true;
            // 
            // BtnPrompt
            // 
            this.BtnPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrompt.Location = new System.Drawing.Point(499, 143);
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
            this.ChkDictation.Location = new System.Drawing.Point(503, 6);
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
            this.label1.Location = new System.Drawing.Point(502, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "CTRL+ ENTER";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabSTT
            // 
            this.TabSTT.Controls.Add(this.label6);
            this.TabSTT.Controls.Add(this.label4);
            this.TabSTT.Controls.Add(this.label2);
            this.TabSTT.Controls.Add(this.LblVolume);
            this.TabSTT.Controls.Add(this.LblThreshold);
            this.TabSTT.Controls.Add(this.DropDownInputDevice);
            this.TabSTT.Controls.Add(this.PbVolume);
            this.TabSTT.Controls.Add(this.SliderTheshold);
            this.TabSTT.Location = new System.Drawing.Point(4, 22);
            this.TabSTT.Name = "TabSTT";
            this.TabSTT.Padding = new System.Windows.Forms.Padding(3);
            this.TabSTT.Size = new System.Drawing.Size(569, 172);
            this.TabSTT.TabIndex = 1;
            this.TabSTT.Text = "STT";
            this.TabSTT.UseVisualStyleBackColor = true;
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
            // DropDownInputDevice
            // 
            this.DropDownInputDevice.FormattingEnabled = true;
            this.DropDownInputDevice.Location = new System.Drawing.Point(96, 58);
            this.DropDownInputDevice.Name = "DropDownInputDevice";
            this.DropDownInputDevice.Size = new System.Drawing.Size(286, 21);
            this.DropDownInputDevice.TabIndex = 5;
            this.DropDownInputDevice.SelectedIndexChanged += new System.EventHandler(this.CboInputDevice_SelectedIndexChanged);
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
            // TabImages
            // 
            this.TabImages.Controls.Add(this.label3);
            this.TabImages.Controls.Add(this.BtnPaste);
            this.TabImages.Location = new System.Drawing.Point(4, 22);
            this.TabImages.Name = "TabImages";
            this.TabImages.Size = new System.Drawing.Size(569, 172);
            this.TabImages.TabIndex = 2;
            this.TabImages.Text = "Images";
            this.TabImages.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(236, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "CTRL+V";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPaste
            // 
            this.BtnPaste.Location = new System.Drawing.Point(236, 81);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(87, 23);
            this.BtnPaste.TabIndex = 10;
            this.BtnPaste.Text = "Paste";
            this.BtnPaste.UseVisualStyleBackColor = true;
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // TabOCR
            // 
            this.TabOCR.Controls.Add(this.BtnMarquee);
            this.TabOCR.Controls.Add(this.TxtY);
            this.TabOCR.Controls.Add(this.label11);
            this.TabOCR.Controls.Add(this.TxtHeight);
            this.TabOCR.Controls.Add(this.TxtWidth);
            this.TabOCR.Controls.Add(this.label13);
            this.TabOCR.Controls.Add(this.TxtX);
            this.TabOCR.Controls.Add(this.label12);
            this.TabOCR.Controls.Add(this.label10);
            this.TabOCR.Controls.Add(this.ChkOCR);
            this.TabOCR.Controls.Add(this.label9);
            this.TabOCR.Controls.Add(this.PicBoxPreview);
            this.TabOCR.Controls.Add(this.label8);
            this.TabOCR.Location = new System.Drawing.Point(4, 22);
            this.TabOCR.Name = "TabOCR";
            this.TabOCR.Size = new System.Drawing.Size(569, 172);
            this.TabOCR.TabIndex = 3;
            this.TabOCR.Text = "OCR";
            this.TabOCR.UseVisualStyleBackColor = true;
            // 
            // BtnMarquee
            // 
            this.BtnMarquee.Location = new System.Drawing.Point(233, 128);
            this.BtnMarquee.Name = "BtnMarquee";
            this.BtnMarquee.Size = new System.Drawing.Size(75, 23);
            this.BtnMarquee.TabIndex = 6;
            this.BtnMarquee.Text = "Marquee";
            this.BtnMarquee.UseVisualStyleBackColor = true;
            this.BtnMarquee.Click += new System.EventHandler(this.BtnMarquee_Click);
            // 
            // TxtY
            // 
            this.TxtY.Location = new System.Drawing.Point(233, 51);
            this.TxtY.Name = "TxtY";
            this.TxtY.Size = new System.Drawing.Size(90, 20);
            this.TxtY.TabIndex = 3;
            this.TxtY.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(213, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Y:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtHeight
            // 
            this.TxtHeight.Location = new System.Drawing.Point(233, 82);
            this.TxtHeight.Name = "TxtHeight";
            this.TxtHeight.Size = new System.Drawing.Size(90, 20);
            this.TxtHeight.TabIndex = 5;
            this.TxtHeight.Text = "256";
            // 
            // TxtWidth
            // 
            this.TxtWidth.Location = new System.Drawing.Point(95, 81);
            this.TxtWidth.Name = "TxtWidth";
            this.TxtWidth.Size = new System.Drawing.Size(90, 20);
            this.TxtWidth.TabIndex = 4;
            this.TxtWidth.Text = "256";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(191, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "Height:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtX
            // 
            this.TxtX.Location = new System.Drawing.Point(94, 52);
            this.TxtX.Name = "TxtX";
            this.TxtX.Size = new System.Drawing.Size(90, 20);
            this.TxtX.TabIndex = 2;
            this.TxtX.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(53, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Width:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "X:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChkOCR
            // 
            this.ChkOCR.AutoSize = true;
            this.ChkOCR.Location = new System.Drawing.Point(11, 5);
            this.ChkOCR.Name = "ChkOCR";
            this.ChkOCR.Size = new System.Drawing.Size(56, 17);
            this.ChkOCR.TabIndex = 0;
            this.ChkOCR.Text = "Active";
            this.ChkOCR.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(479, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Preview";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PicBoxPreview
            // 
            this.PicBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PicBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicBoxPreview.Location = new System.Drawing.Point(431, 5);
            this.PicBoxPreview.Name = "PicBoxPreview";
            this.PicBoxPreview.Size = new System.Drawing.Size(128, 128);
            this.PicBoxPreview.TabIndex = 2;
            this.PicBoxPreview.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(191, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Capture Area:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabHistory
            // 
            this.TabHistory.Controls.Add(this.BtnLoad);
            this.TabHistory.Controls.Add(this.BtnClear);
            this.TabHistory.Controls.Add(this.BtnSave);
            this.TabHistory.Location = new System.Drawing.Point(4, 22);
            this.TabHistory.Name = "TabHistory";
            this.TabHistory.Size = new System.Drawing.Size(569, 172);
            this.TabHistory.TabIndex = 4;
            this.TabHistory.Text = "History";
            this.TabHistory.UseVisualStyleBackColor = true;
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(253, 98);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(58, 23);
            this.BtnLoad.TabIndex = 6;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(253, 40);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(58, 23);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(253, 69);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(58, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TabConfig
            // 
            this.TabConfig.Controls.Add(this.DropDownModels);
            this.TabConfig.Controls.Add(this.label7);
            this.TabConfig.Controls.Add(this.ChkStayAwake);
            this.TabConfig.Controls.Add(this.label5);
            this.TabConfig.Controls.Add(this.DropDownFocus);
            this.TabConfig.Location = new System.Drawing.Point(4, 22);
            this.TabConfig.Name = "TabConfig";
            this.TabConfig.Size = new System.Drawing.Size(569, 172);
            this.TabConfig.TabIndex = 5;
            this.TabConfig.Text = "Config";
            this.TabConfig.UseVisualStyleBackColor = true;
            // 
            // DropDownModels
            // 
            this.DropDownModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDownModels.FormattingEnabled = true;
            this.DropDownModels.Location = new System.Drawing.Point(10, 25);
            this.DropDownModels.Name = "DropDownModels";
            this.DropDownModels.Size = new System.Drawing.Size(547, 21);
            this.DropDownModels.TabIndex = 9;
            this.DropDownModels.SelectedIndexChanged += new System.EventHandler(this.CboModel_SelectedIndexChanged);
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
            this.DropDownFocus.Size = new System.Drawing.Size(547, 21);
            this.DropDownFocus.TabIndex = 2;
            this.DropDownFocus.SelectedIndexChanged += new System.EventHandler(this.DropDownFocus_SelectedIndexChanged);
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
            // TimerCapture
            // 
            this.TimerCapture.Tick += new System.EventHandler(this.TimerCapture_Tick);
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
            this.TabPrompt.ResumeLayout(false);
            this.TabPrompt.PerformLayout();
            this.TabSTT.ResumeLayout(false);
            this.TabSTT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderTheshold)).EndInit();
            this.TabImages.ResumeLayout(false);
            this.TabOCR.ResumeLayout(false);
            this.TabOCR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxPreview)).EndInit();
            this.TabHistory.ResumeLayout(false);
            this.TabConfig.ResumeLayout(false);
            this.TabConfig.PerformLayout();
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
        private System.Windows.Forms.TabPage TabPrompt;
        private System.Windows.Forms.TabPage TabSTT;
        private System.Windows.Forms.TabPage TabImages;
        private System.Windows.Forms.TabPage TabOCR;
        private System.Windows.Forms.TabPage TabHistory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage TabConfig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox PicBoxPreview;
        public System.Windows.Forms.TextBox TxtY;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox TxtWidth;
        public System.Windows.Forms.TextBox TxtX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ChkOCR;
        public System.Windows.Forms.TextBox TxtHeight;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer TimerCapture;
        private System.Windows.Forms.Button BtnMarquee;
    }
}

