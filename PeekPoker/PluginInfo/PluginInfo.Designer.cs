namespace PeekPoker.PluginInfo
{
    partial class PluginInfo
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
            this.pluginListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.authorColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.versionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // pluginListView
            // 
            this.pluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.descriptionColumn,
            this.authorColumn,
            this.versionColumn});
            this.pluginListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluginListView.Location = new System.Drawing.Point(0, 0);
            this.pluginListView.Name = "pluginListView";
            this.pluginListView.Size = new System.Drawing.Size(678, 196);
            this.pluginListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.pluginListView.TabIndex = 3;
            this.pluginListView.UseCompatibleStateImageBehavior = false;
            this.pluginListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 166;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.Text = "Description";
            this.descriptionColumn.Width = 232;
            // 
            // authorColumn
            // 
            this.authorColumn.Text = "Author";
            this.authorColumn.Width = 100;
            // 
            // versionColumn
            // 
            this.versionColumn.Text = "Version";
            this.versionColumn.Width = 67;
            // 
            // PluginInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 196);
            this.Controls.Add(this.pluginListView);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PluginInfo";
            this.Text = "PluginInfo";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView pluginListView;
        internal System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader descriptionColumn;
        internal System.Windows.Forms.ColumnHeader authorColumn;
        internal System.Windows.Forms.ColumnHeader versionColumn;
    }
}