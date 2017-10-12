namespace Diabet
{
    partial class MedicamentNameSelectForm
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
            this.medicamentTable = new System.Windows.Forms.DataGridView();
            this.medNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.medicamentTable)).BeginInit();
            this.SuspendLayout();
            // 
            // medicamentTable
            // 
            this.medicamentTable.AllowUserToAddRows = false;
            this.medicamentTable.AllowUserToDeleteRows = false;
            this.medicamentTable.AllowUserToResizeColumns = false;
            this.medicamentTable.AllowUserToResizeRows = false;
            this.medicamentTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.medicamentTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medicamentTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.medicamentTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.medicamentTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.medNameColumn});
            this.medicamentTable.Location = new System.Drawing.Point(12, 12);
            this.medicamentTable.Name = "medicamentTable";
            this.medicamentTable.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medicamentTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.medicamentTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.medicamentTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.medicamentTable.Size = new System.Drawing.Size(313, 163);
            this.medicamentTable.TabIndex = 4;
            this.medicamentTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.medicamentTable_CellDoubleClick);
            this.medicamentTable.SelectionChanged += new System.EventHandler(this.medicamentTable_SelectionChanged);
            // 
            // medNameColumn
            // 
            this.medNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.medNameColumn.DataPropertyName = "Name";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.medNameColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.medNameColumn.HeaderText = "Медикамент";
            this.medNameColumn.Name = "medNameColumn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(332, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Вибрати";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MedicamentNameSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 200);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.medicamentTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MedicamentNameSelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Вибір медикаменту";
            ((System.ComponentModel.ISupportInitialize)(this.medicamentTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView medicamentTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn medNameColumn;
        private System.Windows.Forms.Button button1;
    }
}