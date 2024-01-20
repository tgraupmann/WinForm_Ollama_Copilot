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
            this.TxtPrompt = new System.Windows.Forms.TextBox();
            this.BtnPrompt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPrompt
            // 
            this.TxtPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrompt.Location = new System.Drawing.Point(12, 10);
            this.TxtPrompt.Multiline = true;
            this.TxtPrompt.Name = "TxtPrompt";
            this.TxtPrompt.Size = new System.Drawing.Size(519, 419);
            this.TxtPrompt.TabIndex = 0;
            // 
            // BtnPrompt
            // 
            this.BtnPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPrompt.Location = new System.Drawing.Point(537, 10);
            this.BtnPrompt.Name = "BtnPrompt";
            this.BtnPrompt.Size = new System.Drawing.Size(75, 23);
            this.BtnPrompt.TabIndex = 1;
            this.BtnPrompt.Text = "Prompt";
            this.BtnPrompt.UseVisualStyleBackColor = true;
            this.BtnPrompt.Click += new System.EventHandler(this.BtnPrompt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.BtnPrompt);
            this.Controls.Add(this.TxtPrompt);
            this.Name = "Form1";
            this.Text = "Ollama Copilot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtPrompt;
        private System.Windows.Forms.Button BtnPrompt;
    }
}

