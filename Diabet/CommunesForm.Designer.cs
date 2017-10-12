namespace Diabet
{
    partial class CommunesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.communesTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCommuneButton = new System.Windows.Forms.LinkLabel();
            this.deleteCommuneButton = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.communesTable)).BeginInit();
            this.SuspendLayout();
            // 
            // communesTable
            // 
            this.communesTable.AllowUserToAddRows = false;
            this.communesTable.AllowUserToDeleteRows = false;
            this.communesTable.AllowUserToResizeColumns = false;
            this.communesTable.AllowUserToResizeRows = false;
            this.communesTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.communesTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.communesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.communesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.communesTable.Location = new System.Drawing.Point(12, 41);
            this.communesTable.MultiSelect = false;
            this.communesTable.Name = "communesTable";
            this.communesTable.RowHeadersVisible = false;
            this.communesTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.communesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.communesTable.Size = new System.Drawing.Size(347, 150);
            this.communesTable.TabIndex = 0;
            this.communesTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.communesTable_CellValueChanged);
            this.communesTable.SelectionChanged += new System.EventHandler(this.communesTable_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "Name";
            this.Column2.HeaderText = "Назва громади";
            this.Column2.Name = "Column2";
            // 
            // addCommuneButton
            // 
            this.addCommuneButton.AutoSize = true;
            this.addCommuneButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addCommuneButton.Location = new System.Drawing.Point(12, 16);
            this.addCommuneButton.Name = "addCommuneButton";
            this.addCommuneButton.Size = new System.Drawing.Size(45, 13);
            this.addCommuneButton.TabIndex = 1;
            this.addCommuneButton.TabStop = true;
            this.addCommuneButton.Text = "Додати";
            this.addCommuneButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addCommuneButton_LinkClicked);
            // 
            // deleteCommuneButton
            // 
            this.deleteCommuneButton.AutoSize = true;
            this.deleteCommuneButton.Enabled = false;
            this.deleteCommuneButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteCommuneButton.Location = new System.Drawing.Point(63, 16);
            this.deleteCommuneButton.Name = "deleteCommuneButton";
            this.deleteCommuneButton.Size = new System.Drawing.Size(55, 13);
            this.deleteCommuneButton.TabIndex = 2;
            this.deleteCommuneButton.TabStop = true;
            this.deleteCommuneButton.Text = "Видалити";
            this.deleteCommuneButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteCommuneButton_LinkClicked);
            // 
            // CommunesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 220);
            this.Controls.Add(this.deleteCommuneButton);
            this.Controls.Add(this.addCommuneButton);
            this.Controls.Add(this.communesTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommunesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Перелік громад";
            ((System.ComponentModel.ISupportInitialize)(this.communesTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView communesTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.LinkLabel addCommuneButton;
        private System.Windows.Forms.LinkLabel deleteCommuneButton;
    }
}