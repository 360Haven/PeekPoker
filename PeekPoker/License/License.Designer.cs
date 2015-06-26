namespace PeekPoker.License
{
    partial class License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(License));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.agreeButton = new System.Windows.Forms.Button();
            this.notAgreeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(397, 297);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // agreeButton
            // 
            this.agreeButton.Location = new System.Drawing.Point(332, 315);
            this.agreeButton.Name = "agreeButton";
            this.agreeButton.Size = new System.Drawing.Size(75, 23);
            this.agreeButton.TabIndex = 1;
            this.agreeButton.Text = "I Agree";
            this.agreeButton.UseVisualStyleBackColor = true;
            this.agreeButton.Click += new System.EventHandler(this.agreeButton_Click);
            // 
            // notAgreeButton
            // 
            this.notAgreeButton.Location = new System.Drawing.Point(239, 315);
            this.notAgreeButton.Name = "notAgreeButton";
            this.notAgreeButton.Size = new System.Drawing.Size(87, 23);
            this.notAgreeButton.TabIndex = 2;
            this.notAgreeButton.Text = "I do not Agree";
            this.notAgreeButton.UseVisualStyleBackColor = true;
            this.notAgreeButton.Click += new System.EventHandler(this.notAgreeButton_Click);
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 346);
            this.Controls.Add(this.notAgreeButton);
            this.Controls.Add(this.agreeButton);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "License";
            this.Text = "License";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button agreeButton;
        private System.Windows.Forms.Button notAgreeButton;
    }
}