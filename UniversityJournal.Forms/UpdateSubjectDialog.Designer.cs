namespace UniversityJournal.Forms
{
    partial class UpdateSubjectDialog
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
            cmbSubjects = new ComboBox();
            txtSubjectName = new TextBox();
            cmbTeachers = new ComboBox();
            btnUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // cmbSubjects
            // 
            cmbSubjects.FormattingEnabled = true;
            cmbSubjects.Location = new Point(71, 57);
            cmbSubjects.Name = "cmbSubjects";
            cmbSubjects.Size = new Size(121, 23);
            cmbSubjects.TabIndex = 0;
            cmbSubjects.SelectedIndexChanged += cmbSubjects_SelectedIndexChanged;
            // 
            // txtSubjectName
            // 
            txtSubjectName.Location = new Point(71, 104);
            txtSubjectName.Name = "txtSubjectName";
            txtSubjectName.Size = new Size(121, 23);
            txtSubjectName.TabIndex = 1;
            // 
            // cmbTeachers
            // 
            cmbTeachers.FormattingEnabled = true;
            cmbTeachers.Location = new Point(71, 156);
            cmbTeachers.Name = "cmbTeachers";
            cmbTeachers.Size = new Size(121, 23);
            cmbTeachers.TabIndex = 2;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(71, 185);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(71, 39);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 4;
            label1.Text = "Предмет";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(71, 86);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 5;
            label2.Text = "Название";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(71, 138);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 6;
            label3.Text = "Преподователь";
            // 
            // UpdateSubjectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 257);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnUpdate);
            Controls.Add(cmbTeachers);
            Controls.Add(txtSubjectName);
            Controls.Add(cmbSubjects);
            Name = "UpdateSubjectDialog";
            Text = "UpdateSubjectDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbSubjects;
        private TextBox txtSubjectName;
        private ComboBox cmbTeachers;
        private Button btnUpdate;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}