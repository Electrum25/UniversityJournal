
namespace UniversityJournal.Forms
{
    partial class StudentForm
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
            dgvGrades = new DataGridView();
            dgvAttendances = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvGrades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttendances).BeginInit();
            SuspendLayout();
            // 
            // dgvGrades
            // 
            dgvGrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGrades.Location = new Point(0, 1);
            dgvGrades.Name = "dgvGrades";
            dgvGrades.Size = new Size(395, 447);
            dgvGrades.TabIndex = 0;
            // 
            // dgvAttendances
            // 
            dgvAttendances.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAttendances.Location = new Point(401, 1);
            dgvAttendances.Name = "dgvAttendances";
            dgvAttendances.Size = new Size(398, 447);
            dgvAttendances.TabIndex = 1;
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvAttendances);
            Controls.Add(dgvGrades);
            Name = "StudentForm";
            Text = "StudentForm";
            Load += StudentForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGrades).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttendances).EndInit();
            ResumeLayout(false);
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private DataGridView dgvGrades;
        private DataGridView dgvAttendances;
    }
}