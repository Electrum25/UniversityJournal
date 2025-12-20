namespace UniversityJournal.Forms
{
    partial class TeacherResultsForm
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
            cmbDataType = new ComboBox();
            cmbGroups = new ComboBox();
            dgvResults = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // cmbDataType
            // 
            cmbDataType.FormattingEnabled = true;
            cmbDataType.Location = new Point(12, 30);
            cmbDataType.Name = "cmbDataType";
            cmbDataType.Size = new Size(121, 23);
            cmbDataType.TabIndex = 0;
            cmbDataType.SelectedIndexChanged += cmbDataType_SelectedIndexChanged;
            // 
            // cmbGroups
            // 
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(155, 30);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(121, 23);
            cmbGroups.TabIndex = 1;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // dgvResults
            // 
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(12, 59);
            dgvResults.Name = "dgvResults";
            dgvResults.Size = new Size(776, 379);
            dgvResults.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 3;
            label1.Text = "Вид";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(155, 12);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 4;
            label2.Text = "Группы";
            // 
            // TeacherResultsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvResults);
            Controls.Add(cmbGroups);
            Controls.Add(cmbDataType);
            Name = "TeacherResultsForm";
            Text = "TeacherResultsForm";
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbDataType;
        private ComboBox cmbGroups;
        private DataGridView dgvResults;
        private Label label1;
        private Label label2;
    }
}