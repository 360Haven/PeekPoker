namespace PeekPoker.Search
{
    partial class Search
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.ResultsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.resultRefreshButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.totalTextBoxText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.stopSearchButton = new System.Windows.Forms.Button();
            this.searchRangeButton = new System.Windows.Forms.Button();
            this.startRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.searchRangeValueTextBox = new System.Windows.Forms.TextBox();
            this.lengthRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.newValueTextBox = new System.Windows.Forms.TextBox();
            this.ifChangeRadioButton = new System.Windows.Forms.RadioButton();
            this.ifLessThanRadioButton = new System.Windows.Forms.RadioButton();
            this.ifGreaterThanRadioButton = new System.Windows.Forms.RadioButton();
            this.ifEqualsRadioButton = new System.Windows.Forms.RadioButton();
            this.defaultRadioButton = new System.Windows.Forms.RadioButton();
            this.Results_Label = new System.Windows.Forms.Label();
            this.ResultCountBox = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.ResultsContextMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resultGrid);
            this.groupBox2.Location = new System.Drawing.Point(14, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 227);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result/s";
            // 
            // resultGrid
            // 
            this.resultGrid.AllowUserToAddRows = false;
            this.resultGrid.AllowUserToDeleteRows = false;
            this.resultGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.resultGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.ContextMenuStrip = this.ResultsContextMenu;
            this.resultGrid.Location = new System.Drawing.Point(7, 14);
            this.resultGrid.MultiSelect = false;
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.ReadOnly = true;
            this.resultGrid.RowHeadersVisible = false;
            this.resultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultGrid.Size = new System.Drawing.Size(383, 200);
            this.resultGrid.TabIndex = 14;
            this.resultGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultGridCellValueChanged);
            this.resultGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultGrid_KeyDown);
            // 
            // ResultsContextMenu
            // 
            this.ResultsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CCopy});
            this.ResultsContextMenu.Name = "ResultsContextMenu";
            this.ResultsContextMenu.Size = new System.Drawing.Size(103, 26);
            // 
            // CCopy
            // 
            this.CCopy.Name = "CCopy";
            this.CCopy.Size = new System.Drawing.Size(102, 22);
            this.CCopy.Text = "Copy";
            this.CCopy.Click += new System.EventHandler(this.ResultCopy);
            // 
            // resultRefreshButton
            // 
            this.resultRefreshButton.Location = new System.Drawing.Point(56, 181);
            this.resultRefreshButton.Name = "resultRefreshButton";
            this.resultRefreshButton.Size = new System.Drawing.Size(89, 32);
            this.resultRefreshButton.TabIndex = 13;
            this.resultRefreshButton.Text = "Refresh";
            this.resultRefreshButton.UseVisualStyleBackColor = true;
            this.resultRefreshButton.Click += new System.EventHandler(this.ResultRefreshClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.totalTextBoxText);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.stopSearchButton);
            this.groupBox1.Controls.Add(this.searchRangeButton);
            this.groupBox1.Controls.Add(this.startRangeAddressTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.searchRangeValueTextBox);
            this.groupBox1.Controls.Add(this.lengthRangeAddressTextBox);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 106);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Range Selection Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Total 0x:";
            // 
            // totalTextBoxText
            // 
            this.totalTextBoxText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.totalTextBoxText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.totalTextBoxText.Location = new System.Drawing.Point(405, 36);
            this.totalTextBoxText.Name = "totalTextBoxText";
            this.totalTextBoxText.ReadOnly = true;
            this.totalTextBoxText.Size = new System.Drawing.Size(87, 21);
            this.totalTextBoxText.TabIndex = 20;
            this.totalTextBoxText.Text = "C000FFFF";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(192, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "Length 0x:";
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Enabled = false;
            this.stopSearchButton.Location = new System.Drawing.Point(504, 59);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(110, 39);
            this.stopSearchButton.TabIndex = 18;
            this.stopSearchButton.Text = "Stop Search";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            this.stopSearchButton.Click += new System.EventHandler(this.StopSearchButtonClick);
            // 
            // searchRangeButton
            // 
            this.searchRangeButton.Location = new System.Drawing.Point(504, 15);
            this.searchRangeButton.Name = "searchRangeButton";
            this.searchRangeButton.Size = new System.Drawing.Size(110, 39);
            this.searchRangeButton.TabIndex = 9;
            this.searchRangeButton.Text = "Search Hex Value";
            this.searchRangeButton.UseVisualStyleBackColor = true;
            this.searchRangeButton.Click += new System.EventHandler(this.SearchRangeButtonClick);
            // 
            // startRangeAddressTextBox
            // 
            this.startRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.startRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.startRangeAddressTextBox.Location = new System.Drawing.Point(105, 36);
            this.startRangeAddressTextBox.Name = "startRangeAddressTextBox";
            this.startRangeAddressTextBox.Size = new System.Drawing.Size(80, 21);
            this.startRangeAddressTextBox.TabIndex = 5;
            this.startRangeAddressTextBox.Text = "C0000000";
            this.startRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Search for 0x:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Start Offset 0x:";
            // 
            // searchRangeValueTextBox
            // 
            this.searchRangeValueTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchRangeValueTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchRangeValueTextBox.Location = new System.Drawing.Point(105, 66);
            this.searchRangeValueTextBox.Name = "searchRangeValueTextBox";
            this.searchRangeValueTextBox.Size = new System.Drawing.Size(383, 21);
            this.searchRangeValueTextBox.TabIndex = 8;
            this.searchRangeValueTextBox.Text = "FF00FF00FF00";
            this.searchRangeValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchRangeValueTextBox_KeyPress);
            this.searchRangeValueTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchRangeValueTextBoxKeyUp);
            this.searchRangeValueTextBox.Leave += new System.EventHandler(this.SearchRangeValueTextBoxLeave);
            // 
            // lengthRangeAddressTextBox
            // 
            this.lengthRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.lengthRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.lengthRangeAddressTextBox.Location = new System.Drawing.Point(266, 36);
            this.lengthRangeAddressTextBox.Name = "lengthRangeAddressTextBox";
            this.lengthRangeAddressTextBox.Size = new System.Drawing.Size(70, 21);
            this.lengthRangeAddressTextBox.TabIndex = 7;
            this.lengthRangeAddressTextBox.Text = "FFFF";
            this.lengthRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.newValueTextBox);
            this.groupBox3.Controls.Add(this.resultRefreshButton);
            this.groupBox3.Controls.Add(this.ifChangeRadioButton);
            this.groupBox3.Controls.Add(this.ifLessThanRadioButton);
            this.groupBox3.Controls.Add(this.ifGreaterThanRadioButton);
            this.groupBox3.Controls.Add(this.ifEqualsRadioButton);
            this.groupBox3.Controls.Add(this.defaultRadioButton);
            this.groupBox3.Location = new System.Drawing.Point(419, 157);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(224, 227);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Next Search";
            // 
            // newValueTextBox
            // 
            this.newValueTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.newValueTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.newValueTextBox.Location = new System.Drawing.Point(7, 72);
            this.newValueTextBox.Name = "newValueTextBox";
            this.newValueTextBox.Size = new System.Drawing.Size(201, 21);
            this.newValueTextBox.TabIndex = 9;
            this.newValueTextBox.Text = "FF00FF00FF00";
            // 
            // ifChangeRadioButton
            // 
            this.ifChangeRadioButton.AutoSize = true;
            this.ifChangeRadioButton.Location = new System.Drawing.Point(7, 155);
            this.ifChangeRadioButton.Name = "ifChangeRadioButton";
            this.ifChangeRadioButton.Size = new System.Drawing.Size(206, 19);
            this.ifChangeRadioButton.TabIndex = 4;
            this.ifChangeRadioButton.Text = "If Change (Unknown Value Keep)";
            this.ifChangeRadioButton.UseVisualStyleBackColor = true;
            // 
            // ifLessThanRadioButton
            // 
            this.ifLessThanRadioButton.AutoSize = true;
            this.ifLessThanRadioButton.Location = new System.Drawing.Point(7, 128);
            this.ifLessThanRadioButton.Name = "ifLessThanRadioButton";
            this.ifLessThanRadioButton.Size = new System.Drawing.Size(174, 19);
            this.ifLessThanRadioButton.TabIndex = 3;
            this.ifLessThanRadioButton.Text = "If Less Than Current (Keep)";
            this.ifLessThanRadioButton.UseVisualStyleBackColor = true;
            // 
            // ifGreaterThanRadioButton
            // 
            this.ifGreaterThanRadioButton.AutoSize = true;
            this.ifGreaterThanRadioButton.Location = new System.Drawing.Point(7, 102);
            this.ifGreaterThanRadioButton.Name = "ifGreaterThanRadioButton";
            this.ifGreaterThanRadioButton.Size = new System.Drawing.Size(189, 19);
            this.ifGreaterThanRadioButton.TabIndex = 2;
            this.ifGreaterThanRadioButton.Text = "If Greater Than Current (Keep)";
            this.ifGreaterThanRadioButton.UseVisualStyleBackColor = true;
            // 
            // ifEqualsRadioButton
            // 
            this.ifEqualsRadioButton.AutoSize = true;
            this.ifEqualsRadioButton.Location = new System.Drawing.Point(7, 45);
            this.ifEqualsRadioButton.Name = "ifEqualsRadioButton";
            this.ifEqualsRadioButton.Size = new System.Drawing.Size(128, 19);
            this.ifEqualsRadioButton.TabIndex = 1;
            this.ifEqualsRadioButton.Text = "If Equals to: (Keep)";
            this.ifEqualsRadioButton.UseVisualStyleBackColor = true;
            // 
            // defaultRadioButton
            // 
            this.defaultRadioButton.AutoSize = true;
            this.defaultRadioButton.Checked = true;
            this.defaultRadioButton.Location = new System.Drawing.Point(7, 18);
            this.defaultRadioButton.Name = "defaultRadioButton";
            this.defaultRadioButton.Size = new System.Drawing.Size(164, 19);
            this.defaultRadioButton.TabIndex = 0;
            this.defaultRadioButton.TabStop = true;
            this.defaultRadioButton.Text = "Default (Original Refresh)";
            this.defaultRadioButton.UseVisualStyleBackColor = true;
            // 
            // Results_Label
            // 
            this.Results_Label.AutoSize = true;
            this.Results_Label.Location = new System.Drawing.Point(22, 133);
            this.Results_Label.Name = "Results_Label";
            this.Results_Label.Size = new System.Drawing.Size(51, 15);
            this.Results_Label.TabIndex = 23;
            this.Results_Label.Text = "Results:";
            // 
            // ResultCountBox
            // 
            this.ResultCountBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ResultCountBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.ResultCountBox.Location = new System.Drawing.Point(79, 130);
            this.ResultCountBox.Name = "ResultCountBox";
            this.ResultCountBox.ReadOnly = true;
            this.ResultCountBox.Size = new System.Drawing.Size(564, 21);
            this.ResultCountBox.TabIndex = 22;
            this.ResultCountBox.Text = "0";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 390);
            this.Controls.Add(this.ResultCountBox);
            this.Controls.Add(this.Results_Label);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Search";
            this.Text = "Search";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.ResultsContextMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Button resultRefreshButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopSearchButton;
        private System.Windows.Forms.Button searchRangeButton;
        private System.Windows.Forms.TextBox startRangeAddressTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox searchRangeValueTextBox;
        private System.Windows.Forms.TextBox lengthRangeAddressTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox newValueTextBox;
        private System.Windows.Forms.RadioButton ifChangeRadioButton;
        private System.Windows.Forms.RadioButton ifLessThanRadioButton;
        private System.Windows.Forms.RadioButton ifGreaterThanRadioButton;
        private System.Windows.Forms.RadioButton ifEqualsRadioButton;
        private System.Windows.Forms.RadioButton defaultRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totalTextBoxText;
        private System.Windows.Forms.ContextMenuStrip ResultsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CCopy;
        private System.Windows.Forms.Label Results_Label;
        private System.Windows.Forms.TextBox ResultCountBox;
    }
}