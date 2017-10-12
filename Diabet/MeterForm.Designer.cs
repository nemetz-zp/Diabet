namespace Diabet
{
    partial class MeterForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deleteDozageButton = new System.Windows.Forms.LinkLabel();
            this.addDozageButton = new System.Windows.Forms.LinkLabel();
            this.medDozageTable = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.deletePackageButton = new System.Windows.Forms.LinkLabel();
            this.addPackageButton = new System.Windows.Forms.LinkLabel();
            this.medPackageTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.deleteAnalizeDozageButton = new System.Windows.Forms.LinkLabel();
            this.addAnalizeDozageButton = new System.Windows.Forms.LinkLabel();
            this.analizeMeterTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medDozageTable)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medPackageTable)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analizeMeterTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(343, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.deleteDozageButton);
            this.tabPage1.Controls.Add(this.addDozageButton);
            this.tabPage1.Controls.Add(this.medDozageTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(335, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Дозування медикаментів";
            // 
            // deleteDozageButton
            // 
            this.deleteDozageButton.AutoSize = true;
            this.deleteDozageButton.Enabled = false;
            this.deleteDozageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteDozageButton.Location = new System.Drawing.Point(59, 16);
            this.deleteDozageButton.Name = "deleteDozageButton";
            this.deleteDozageButton.Size = new System.Drawing.Size(55, 13);
            this.deleteDozageButton.TabIndex = 2;
            this.deleteDozageButton.TabStop = true;
            this.deleteDozageButton.Text = "Видалити";
            this.deleteDozageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteDozageButton_LinkClicked);
            // 
            // addDozageButton
            // 
            this.addDozageButton.AutoSize = true;
            this.addDozageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addDozageButton.Location = new System.Drawing.Point(8, 16);
            this.addDozageButton.Name = "addDozageButton";
            this.addDozageButton.Size = new System.Drawing.Size(45, 13);
            this.addDozageButton.TabIndex = 1;
            this.addDozageButton.TabStop = true;
            this.addDozageButton.Text = "Додати";
            this.addDozageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addDozageButton_LinkClicked);
            // 
            // medDozageTable
            // 
            this.medDozageTable.AllowUserToAddRows = false;
            this.medDozageTable.AllowUserToDeleteRows = false;
            this.medDozageTable.AllowUserToResizeColumns = false;
            this.medDozageTable.AllowUserToResizeRows = false;
            this.medDozageTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medDozageTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.medDozageTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.medDozageTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn});
            this.medDozageTable.Location = new System.Drawing.Point(8, 35);
            this.medDozageTable.MultiSelect = false;
            this.medDozageTable.Name = "medDozageTable";
            this.medDozageTable.RowHeadersVisible = false;
            this.medDozageTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.medDozageTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.medDozageTable.Size = new System.Drawing.Size(318, 150);
            this.medDozageTable.TabIndex = 0;
            this.medDozageTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.medDozageTable_CellValueChanged);
            this.medDozageTable.SelectionChanged += new System.EventHandler(this.medDozageTable_SelectionChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Назва";
            this.NameColumn.Name = "NameColumn";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.deletePackageButton);
            this.tabPage2.Controls.Add(this.addPackageButton);
            this.tabPage2.Controls.Add(this.medPackageTable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(335, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Фасовка медикаментів";
            // 
            // deletePackageButton
            // 
            this.deletePackageButton.AutoSize = true;
            this.deletePackageButton.Enabled = false;
            this.deletePackageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deletePackageButton.Location = new System.Drawing.Point(59, 16);
            this.deletePackageButton.Name = "deletePackageButton";
            this.deletePackageButton.Size = new System.Drawing.Size(55, 13);
            this.deletePackageButton.TabIndex = 5;
            this.deletePackageButton.TabStop = true;
            this.deletePackageButton.Text = "Видалити";
            this.deletePackageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deletePackageButton_LinkClicked);
            // 
            // addPackageButton
            // 
            this.addPackageButton.AutoSize = true;
            this.addPackageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addPackageButton.Location = new System.Drawing.Point(8, 16);
            this.addPackageButton.Name = "addPackageButton";
            this.addPackageButton.Size = new System.Drawing.Size(45, 13);
            this.addPackageButton.TabIndex = 4;
            this.addPackageButton.TabStop = true;
            this.addPackageButton.Text = "Додати";
            this.addPackageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addPackageButton_LinkClicked);
            // 
            // medPackageTable
            // 
            this.medPackageTable.AllowUserToAddRows = false;
            this.medPackageTable.AllowUserToDeleteRows = false;
            this.medPackageTable.AllowUserToResizeColumns = false;
            this.medPackageTable.AllowUserToResizeRows = false;
            this.medPackageTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medPackageTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.medPackageTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.medPackageTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.medPackageTable.Location = new System.Drawing.Point(8, 35);
            this.medPackageTable.MultiSelect = false;
            this.medPackageTable.Name = "medPackageTable";
            this.medPackageTable.RowHeadersVisible = false;
            this.medPackageTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.medPackageTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.medPackageTable.Size = new System.Drawing.Size(318, 150);
            this.medPackageTable.TabIndex = 3;
            this.medPackageTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.medPackageTable_CellValueChanged);
            this.medPackageTable.SelectionChanged += new System.EventHandler(this.medPackageList_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Назва";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.deleteAnalizeDozageButton);
            this.tabPage3.Controls.Add(this.addAnalizeDozageButton);
            this.tabPage3.Controls.Add(this.analizeMeterTable);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(335, 236);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Аналізи";
            // 
            // deleteAnalizeDozageButton
            // 
            this.deleteAnalizeDozageButton.AutoSize = true;
            this.deleteAnalizeDozageButton.Enabled = false;
            this.deleteAnalizeDozageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteAnalizeDozageButton.Location = new System.Drawing.Point(59, 16);
            this.deleteAnalizeDozageButton.Name = "deleteAnalizeDozageButton";
            this.deleteAnalizeDozageButton.Size = new System.Drawing.Size(55, 13);
            this.deleteAnalizeDozageButton.TabIndex = 8;
            this.deleteAnalizeDozageButton.TabStop = true;
            this.deleteAnalizeDozageButton.Text = "Видалити";
            this.deleteAnalizeDozageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteAnalizeDozageButton_LinkClicked);
            // 
            // addAnalizeDozageButton
            // 
            this.addAnalizeDozageButton.AutoSize = true;
            this.addAnalizeDozageButton.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addAnalizeDozageButton.Location = new System.Drawing.Point(8, 16);
            this.addAnalizeDozageButton.Name = "addAnalizeDozageButton";
            this.addAnalizeDozageButton.Size = new System.Drawing.Size(45, 13);
            this.addAnalizeDozageButton.TabIndex = 7;
            this.addAnalizeDozageButton.TabStop = true;
            this.addAnalizeDozageButton.Text = "Додати";
            this.addAnalizeDozageButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addAnalizeDozageButton_LinkClicked);
            // 
            // analizeMeterTable
            // 
            this.analizeMeterTable.AllowUserToAddRows = false;
            this.analizeMeterTable.AllowUserToDeleteRows = false;
            this.analizeMeterTable.AllowUserToResizeColumns = false;
            this.analizeMeterTable.AllowUserToResizeRows = false;
            this.analizeMeterTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.analizeMeterTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.analizeMeterTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analizeMeterTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.analizeMeterTable.Location = new System.Drawing.Point(8, 35);
            this.analizeMeterTable.MultiSelect = false;
            this.analizeMeterTable.Name = "analizeMeterTable";
            this.analizeMeterTable.RowHeadersVisible = false;
            this.analizeMeterTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.analizeMeterTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.analizeMeterTable.Size = new System.Drawing.Size(318, 150);
            this.analizeMeterTable.TabIndex = 6;
            this.analizeMeterTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.analizeMeterTable_CellValueChanged);
            this.analizeMeterTable.SelectionChanged += new System.EventHandler(this.analizeMeterList_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Назва";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // MeterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 262);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MeterForm";
            this.ShowInTaskbar = false;
            this.Text = "Одиниці вимірювання";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medDozageTable)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medPackageTable)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analizeMeterTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.LinkLabel deleteDozageButton;
        private System.Windows.Forms.LinkLabel addDozageButton;
        private System.Windows.Forms.DataGridView medDozageTable;
        private System.Windows.Forms.LinkLabel deletePackageButton;
        private System.Windows.Forms.LinkLabel addPackageButton;
        private System.Windows.Forms.DataGridView medPackageTable;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.LinkLabel deleteAnalizeDozageButton;
        private System.Windows.Forms.LinkLabel addAnalizeDozageButton;
        private System.Windows.Forms.DataGridView analizeMeterTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}