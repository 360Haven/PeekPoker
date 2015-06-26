namespace PeekPoker.Dump
{
    partial class Dump
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.dumpLengthTextBox = new System.Windows.Forms.TextBox();
            this.dumpStartOffsetTextBox = new System.Windows.Forms.TextBox();
            this.dumpMemoryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(28, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(157, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Allocated Data / Virtual";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.AllocatedDataButtonClick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(28, 41);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(157, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Base File / Image";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.BaseFileButtonClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(28, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(157, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Physical RAM";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.PhysicalRamButtonClick);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(11, 135);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(88, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "Dump Length 0x:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(8, 107);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 13);
            this.label33.TabIndex = 3;
            this.label33.Text = "Starting Offset 0x:";
            // 
            // dumpLengthTextBox
            // 
            this.dumpLengthTextBox.Location = new System.Drawing.Point(105, 132);
            this.dumpLengthTextBox.Name = "dumpLengthTextBox";
            this.dumpLengthTextBox.Size = new System.Drawing.Size(102, 20);
            this.dumpLengthTextBox.TabIndex = 2;
            this.dumpLengthTextBox.Text = "FF";
            this.dumpLengthTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // dumpStartOffsetTextBox
            // 
            this.dumpStartOffsetTextBox.Location = new System.Drawing.Point(105, 104);
            this.dumpStartOffsetTextBox.Name = "dumpStartOffsetTextBox";
            this.dumpStartOffsetTextBox.Size = new System.Drawing.Size(102, 20);
            this.dumpStartOffsetTextBox.TabIndex = 1;
            this.dumpStartOffsetTextBox.Text = "C0000000";
            this.dumpStartOffsetTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // dumpMemoryButton
            // 
            this.dumpMemoryButton.Location = new System.Drawing.Point(73, 158);
            this.dumpMemoryButton.Name = "dumpMemoryButton";
            this.dumpMemoryButton.Size = new System.Drawing.Size(75, 23);
            this.dumpMemoryButton.TabIndex = 0;
            this.dumpMemoryButton.Text = "Dump";
            this.dumpMemoryButton.UseVisualStyleBackColor = true;
            this.dumpMemoryButton.Click += new System.EventHandler(this.DumpMemoryButtonClick);
            // 
            // Dump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 191);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dumpMemoryButton);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.dumpStartOffsetTextBox);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.dumpLengthTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Dump";
            this.Text = "Dump";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox dumpLengthTextBox;
        private System.Windows.Forms.TextBox dumpStartOffsetTextBox;
        private System.Windows.Forms.Button dumpMemoryButton;
    }
}