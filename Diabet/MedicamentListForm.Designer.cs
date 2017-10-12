namespace Diabet
{
    partial class MedicamentListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteAgentLink = new System.Windows.Forms.LinkLabel();
            this.addAgentLink = new System.Windows.Forms.LinkLabel();
            this.agentsTable = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deleteMedicamentLink = new System.Windows.Forms.LinkLabel();
            this.addMedicamentLink = new System.Windows.Forms.LinkLabel();
            this.medicamentTable = new System.Windows.Forms.DataGridView();
            this.medNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addFromCopyConcreteMedLink = new System.Windows.Forms.LinkLabel();
            this.deleteConcreteMedLink = new System.Windows.Forms.LinkLabel();
            this.concreteMedTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addConcreteMedLink = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentsTable)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medicamentTable)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.concreteMedTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteAgentLink);
            this.groupBox1.Controls.Add(this.addAgentLink);
            this.groupBox1.Controls.Add(this.agentsTable);
            this.groupBox1.Location = new System.Drawing.Point(10, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список діючих речовин";
            // 
            // deleteAgentLink
            // 
            this.deleteAgentLink.AutoSize = true;
            this.deleteAgentLink.Enabled = false;
            this.deleteAgentLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteAgentLink.Location = new System.Drawing.Point(58, 25);
            this.deleteAgentLink.Name = "deleteAgentLink";
            this.deleteAgentLink.Size = new System.Drawing.Size(55, 13);
            this.deleteAgentLink.TabIndex = 2;
            this.deleteAgentLink.TabStop = true;
            this.deleteAgentLink.Text = "Видалити";
            this.deleteAgentLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteAgentLink_LinkClicked);
            // 
            // addAgentLink
            // 
            this.addAgentLink.AutoSize = true;
            this.addAgentLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addAgentLink.Location = new System.Drawing.Point(7, 25);
            this.addAgentLink.Name = "addAgentLink";
            this.addAgentLink.Size = new System.Drawing.Size(45, 13);
            this.addAgentLink.TabIndex = 1;
            this.addAgentLink.TabStop = true;
            this.addAgentLink.Text = "Додати";
            this.addAgentLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addAgentLink_LinkClicked);
            // 
            // agentsTable
            // 
            this.agentsTable.AllowUserToAddRows = false;
            this.agentsTable.AllowUserToDeleteRows = false;
            this.agentsTable.AllowUserToResizeColumns = false;
            this.agentsTable.AllowUserToResizeRows = false;
            this.agentsTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.agentsTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.agentsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.agentsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.agentsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn});
            this.agentsTable.Location = new System.Drawing.Point(6, 48);
            this.agentsTable.MultiSelect = false;
            this.agentsTable.Name = "agentsTable";
            this.agentsTable.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.agentsTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.agentsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.agentsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.agentsTable.Size = new System.Drawing.Size(466, 91);
            this.agentsTable.TabIndex = 0;
            this.agentsTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.agentsTable_CellValueChanged);
            this.agentsTable.SelectionChanged += new System.EventHandler(this.agentsTable_SelectionChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.NameColumn.HeaderText = "Назва";
            this.NameColumn.Name = "NameColumn";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deleteMedicamentLink);
            this.groupBox2.Controls.Add(this.addMedicamentLink);
            this.groupBox2.Controls.Add(this.medicamentTable);
            this.groupBox2.Location = new System.Drawing.Point(10, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 176);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Список медикаментів";
            // 
            // deleteMedicamentLink
            // 
            this.deleteMedicamentLink.AutoSize = true;
            this.deleteMedicamentLink.Enabled = false;
            this.deleteMedicamentLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteMedicamentLink.Location = new System.Drawing.Point(58, 23);
            this.deleteMedicamentLink.Name = "deleteMedicamentLink";
            this.deleteMedicamentLink.Size = new System.Drawing.Size(55, 13);
            this.deleteMedicamentLink.TabIndex = 5;
            this.deleteMedicamentLink.TabStop = true;
            this.deleteMedicamentLink.Text = "Видалити";
            this.deleteMedicamentLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteMedicamentLink_LinkClicked);
            // 
            // addMedicamentLink
            // 
            this.addMedicamentLink.AutoSize = true;
            this.addMedicamentLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addMedicamentLink.Location = new System.Drawing.Point(7, 23);
            this.addMedicamentLink.Name = "addMedicamentLink";
            this.addMedicamentLink.Size = new System.Drawing.Size(45, 13);
            this.addMedicamentLink.TabIndex = 4;
            this.addMedicamentLink.TabStop = true;
            this.addMedicamentLink.Text = "Додати";
            this.addMedicamentLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addMedicamentLink_LinkClicked);
            // 
            // medicamentTable
            // 
            this.medicamentTable.AllowUserToAddRows = false;
            this.medicamentTable.AllowUserToDeleteRows = false;
            this.medicamentTable.AllowUserToResizeColumns = false;
            this.medicamentTable.AllowUserToResizeRows = false;
            this.medicamentTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.medicamentTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medicamentTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.medicamentTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.medicamentTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.medNameColumn});
            this.medicamentTable.Location = new System.Drawing.Point(6, 44);
            this.medicamentTable.MultiSelect = false;
            this.medicamentTable.Name = "medicamentTable";
            this.medicamentTable.RowHeadersVisible = false;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medicamentTable.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.medicamentTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.medicamentTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.medicamentTable.Size = new System.Drawing.Size(466, 120);
            this.medicamentTable.TabIndex = 3;
            this.medicamentTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.medicamentTable_CellValueChanged);
            this.medicamentTable.SelectionChanged += new System.EventHandler(this.medicamentTable_SelectionChanged);
            // 
            // medNameColumn
            // 
            this.medNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.medNameColumn.DataPropertyName = "Name";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medNameColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.medNameColumn.HeaderText = "Медикамент";
            this.medNameColumn.Name = "medNameColumn";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 414);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(547, 388);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Діючі речовини та медикаменти";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(547, 388);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Товарні назви";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.addFromCopyConcreteMedLink);
            this.groupBox3.Controls.Add(this.deleteConcreteMedLink);
            this.groupBox3.Controls.Add(this.concreteMedTable);
            this.groupBox3.Controls.Add(this.addConcreteMedLink);
            this.groupBox3.Location = new System.Drawing.Point(7, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(532, 360);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Список товарних назв";
            // 
            // addFromCopyConcreteMedLink
            // 
            this.addFromCopyConcreteMedLink.AutoSize = true;
            this.addFromCopyConcreteMedLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addFromCopyConcreteMedLink.Location = new System.Drawing.Point(60, 20);
            this.addFromCopyConcreteMedLink.Name = "addFromCopyConcreteMedLink";
            this.addFromCopyConcreteMedLink.Size = new System.Drawing.Size(104, 13);
            this.addFromCopyConcreteMedLink.TabIndex = 8;
            this.addFromCopyConcreteMedLink.TabStop = true;
            this.addFromCopyConcreteMedLink.Text = "Додати на основі...";
            this.addFromCopyConcreteMedLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addFromCopyConcreteMedLink_LinkClicked);
            // 
            // deleteConcreteMedLink
            // 
            this.deleteConcreteMedLink.AutoSize = true;
            this.deleteConcreteMedLink.Enabled = false;
            this.deleteConcreteMedLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteConcreteMedLink.Location = new System.Drawing.Point(174, 20);
            this.deleteConcreteMedLink.Name = "deleteConcreteMedLink";
            this.deleteConcreteMedLink.Size = new System.Drawing.Size(55, 13);
            this.deleteConcreteMedLink.TabIndex = 7;
            this.deleteConcreteMedLink.TabStop = true;
            this.deleteConcreteMedLink.Text = "Видалити";
            this.deleteConcreteMedLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deleteConcreteMedLink_LinkClicked);
            // 
            // concreteMedTable
            // 
            this.concreteMedTable.AllowUserToAddRows = false;
            this.concreteMedTable.AllowUserToDeleteRows = false;
            this.concreteMedTable.AllowUserToResizeColumns = false;
            this.concreteMedTable.AllowUserToResizeRows = false;
            this.concreteMedTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.concreteMedTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.concreteMedTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.concreteMedTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.concreteMedTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.concreteMedTable.DefaultCellStyle = dataGridViewCellStyle11;
            this.concreteMedTable.Location = new System.Drawing.Point(7, 38);
            this.concreteMedTable.MultiSelect = false;
            this.concreteMedTable.Name = "concreteMedTable";
            this.concreteMedTable.ReadOnly = true;
            this.concreteMedTable.RowHeadersVisible = false;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.concreteMedTable.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.concreteMedTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.concreteMedTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.concreteMedTable.Size = new System.Drawing.Size(519, 300);
            this.concreteMedTable.TabIndex = 0;
            this.concreteMedTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.concreteMedTable_CellDoubleClick);
            this.concreteMedTable.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ListOfAgents";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column1.HeaderText = "Діюча речовина";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "LongName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column2.HeaderText = "Товарна назва медикаменту";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Price";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column3.HeaderText = "Орієнтована ціна";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 70;
            // 
            // addConcreteMedLink
            // 
            this.addConcreteMedLink.AutoSize = true;
            this.addConcreteMedLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addConcreteMedLink.Location = new System.Drawing.Point(9, 20);
            this.addConcreteMedLink.Name = "addConcreteMedLink";
            this.addConcreteMedLink.Size = new System.Drawing.Size(45, 13);
            this.addConcreteMedLink.TabIndex = 6;
            this.addConcreteMedLink.TabStop = true;
            this.addConcreteMedLink.Text = "Додати";
            this.addConcreteMedLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addConcreteMedLink_LinkClicked);
            // 
            // MedicamentListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 414);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MedicamentListForm";
            this.ShowInTaskbar = false;
            this.Text = "Медикаменти";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agentsTable)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.medicamentTable)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.concreteMedTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView agentsTable;
        private System.Windows.Forms.LinkLabel deleteAgentLink;
        private System.Windows.Forms.LinkLabel addAgentLink;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel deleteMedicamentLink;
        private System.Windows.Forms.LinkLabel addMedicamentLink;
        private System.Windows.Forms.DataGridView medicamentTable;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel deleteConcreteMedLink;
        private System.Windows.Forms.DataGridView concreteMedTable;
        private System.Windows.Forms.LinkLabel addConcreteMedLink;
        private System.Windows.Forms.LinkLabel addFromCopyConcreteMedLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn medNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}