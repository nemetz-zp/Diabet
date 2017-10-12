namespace Diabet
{
    partial class AnalizeListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteAnalizeLink = new System.Windows.Forms.LinkLabel();
            this.addAnalizeLink = new System.Windows.Forms.LinkLabel();
            this.analizesTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analizesTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteAnalizeLink);
            this.groupBox1.Controls.Add(this.addAnalizeLink);
            this.groupBox1.Controls.Add(this.analizesTable);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список зареєстрованих аналізів";
            // 
            // deleteAnalizeLink
            // 
            this.deleteAnalizeLink.AutoSize = true;
            this.deleteAnalizeLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.deleteAnalizeLink.Location = new System.Drawing.Point(59, 27);
            this.deleteAnalizeLink.Name = "deleteAnalizeLink";
            this.deleteAnalizeLink.Size = new System.Drawing.Size(55, 13);
            this.deleteAnalizeLink.TabIndex = 2;
            this.deleteAnalizeLink.TabStop = true;
            this.deleteAnalizeLink.Text = "Видалити";
            this.deleteAnalizeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // addAnalizeLink
            // 
            this.addAnalizeLink.AutoSize = true;
            this.addAnalizeLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.addAnalizeLink.Location = new System.Drawing.Point(8, 27);
            this.addAnalizeLink.Name = "addAnalizeLink";
            this.addAnalizeLink.Size = new System.Drawing.Size(45, 13);
            this.addAnalizeLink.TabIndex = 1;
            this.addAnalizeLink.TabStop = true;
            this.addAnalizeLink.Text = "Додати";
            this.addAnalizeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // analizesTable
            // 
            this.analizesTable.AllowUserToAddRows = false;
            this.analizesTable.AllowUserToDeleteRows = false;
            this.analizesTable.AllowUserToResizeColumns = false;
            this.analizesTable.AllowUserToResizeRows = false;
            this.analizesTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.analizesTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.analizesTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.analizesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analizesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.analizesTable.Location = new System.Drawing.Point(7, 49);
            this.analizesTable.MultiSelect = false;
            this.analizesTable.Name = "analizesTable";
            this.analizesTable.RowHeadersVisible = false;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.analizesTable.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.analizesTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.analizesTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.analizesTable.Size = new System.Drawing.Size(339, 182);
            this.analizesTable.TabIndex = 0;
            this.analizesTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.analizesTable_CellDoubleClick);
            this.analizesTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.analizesTable.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "Name";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Назва";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "AnalizeMeter";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Од. виміру";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // AnalizeListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 262);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnalizeListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Аналізи";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analizesTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView analizesTable;
        private System.Windows.Forms.LinkLabel deleteAnalizeLink;
        private System.Windows.Forms.LinkLabel addAnalizeLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}