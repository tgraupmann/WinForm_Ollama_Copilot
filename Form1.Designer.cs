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
            this.BtnPrompt = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnPaste = new System.Windows.Forms.Button();
            this.DropDownModels = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DropDownFocus = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblResponse = new System.Windows.Forms.Label();
            this.TxtResponse = new System.Windows.Forms.TextBox();
            this.TimerDetection = new System.Windows.Forms.Timer(this.components);
            this.TimerModels = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtPrompt
            // 
            this.TxtPrompt.AllowDrop = true;
            this.TxtPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrompt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPrompt.Location = new System.Drawing.Point(3, 4);
            this.TxtPrompt.Multiline = true;
            this.TxtPrompt.Name = "TxtPrompt";
            this.TxtPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtPrompt.Size = new System.Drawing.Size(467, 235);
            this.TxtPrompt.TabIndex = 0;
            this.TxtPrompt.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragDrop);
            this.TxtPrompt.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtPrompt_DragEnter);
            // 
            // BtnPrompt
            // 
            this.BtnPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrompt.Location = new System.Drawing.Point(536, 216);
            this.BtnPrompt.Name = "BtnPrompt";
            this.BtnPrompt.Size = new System.Drawing.Size(64, 23);
            this.BtnPrompt.TabIndex = 1;
            this.BtnPrompt.Text = "Prompt";
            this.BtnPrompt.UseVisualStyleBackColor = true;
            this.BtnPrompt.Click += new System.EventHandler(this.BtnPrompt_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.BtnPaste);
            this.panel1.Controls.Add(this.DropDownModels);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DropDownFocus);
            this.panel1.Controls.Add(this.BtnPrompt);
            this.panel1.Controls.Add(this.TxtPrompt);
            this.panel1.Location = new System.Drawing.Point(5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 272);
            this.panel1.TabIndex = 2;
            // 
            // BtnPaste
            // 
            this.BtnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPaste.Location = new System.Drawing.Point(476, 216);
            this.BtnPaste.Name = "BtnPaste";
            this.BtnPaste.Size = new System.Drawing.Size(54, 23);
            this.BtnPaste.TabIndex = 10;
            this.BtnPaste.Text = "Paste";
            this.BtnPaste.UseVisualStyleBackColor = true;
            this.BtnPaste.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // DropDownModels
            // 
            this.DropDownModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDownModels.FormattingEnabled = true;
            this.DropDownModels.Location = new System.Drawing.Point(479, 126);
            this.DropDownModels.Name = "DropDownModels";
            this.DropDownModels.Size = new System.Drawing.Size(121, 21);
            this.DropDownModels.TabIndex = 9;
            this.DropDownModels.SelectedIndexChanged += new System.EventHandler(this.CboModel_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.BtnLoad);
            this.panel3.Controls.Add(this.BtnClear);
            this.panel3.Controls.Add(this.BtnSave);
            this.panel3.Location = new System.Drawing.Point(476, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(127, 116);
            this.panel3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "History";
            // 
            // BtnLoad
            // 
            this.BtnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLoad.Location = new System.Drawing.Point(25, 55);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(75, 23);
            this.BtnLoad.TabIndex = 6;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.Location = new System.Drawing.Point(25, 84);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 4;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(25, 26);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(539, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Control+Enter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DropDownFocus
            // 
            this.DropDownFocus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DropDownFocus.FormattingEnabled = true;
            this.DropDownFocus.Location = new System.Drawing.Point(4, 246);
            this.DropDownFocus.Name = "DropDownFocus";
            this.DropDownFocus.Size = new System.Drawing.Size(599, 21);
            this.DropDownFocus.TabIndex = 2;
            this.DropDownFocus.SelectedIndexChanged += new System.EventHandler(this.DropDownFocus_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.LblResponse);
            this.panel2.Controls.Add(this.TxtResponse);
            this.panel2.Location = new System.Drawing.Point(5, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(614, 159);
            this.panel2.TabIndex = 3;
            // 
            // LblResponse
            // 
            this.LblResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblResponse.AutoSize = true;
            this.LblResponse.Location = new System.Drawing.Point(4, 4);
            this.LblResponse.Name = "LblResponse";
            this.LblResponse.Size = new System.Drawing.Size(58, 13);
            this.LblResponse.TabIndex = 1;
            this.LblResponse.Text = "Response:";
            // 
            // TxtResponse
            // 
            this.TxtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtResponse.Location = new System.Drawing.Point(4, 22);
            this.TxtResponse.Multiline = true;
            this.TxtResponse.Name = "TxtResponse";
            this.TxtResponse.ReadOnly = true;
            this.TxtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtResponse.Size = new System.Drawing.Size(599, 134);
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(476, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Control+V";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 450);
            this.Name = "Form1";
            this.Text = "Ollama Copilot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPrompt;
        private System.Windows.Forms.Button BtnPrompt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblResponse;
        private System.Windows.Forms.TextBox TxtResponse;
        private System.Windows.Forms.ComboBox DropDownFocus;
        private System.Windows.Forms.Timer TimerDetection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.ComboBox DropDownModels;
        private System.Windows.Forms.Timer TimerModels;
        private System.Windows.Forms.Button BtnPaste;
        private System.Windows.Forms.Label label3;
    }
}

