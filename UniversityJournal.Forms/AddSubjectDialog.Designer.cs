namespace UniversityJournal.Forms
{
    partial class AddSubjectDialog
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
            label1 = new Label();
            cmbTeacher = new ComboBox();
            label2 = new Label();
            txtSubjectName = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 36);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 0;
            label1.Text = "Преподаватель";
            // 
            // cmbTeacher
            // 
            cmbTeacher.FormattingEnabled = true;
            cmbTeacher.Location = new Point(65, 54);
            cmbTeacher.Name = "cmbTeacher";
            cmbTeacher.Size = new Size(121, 23);
            cmbTeacher.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 80);
            label2.Name = "label2";
            label2.Size = new Size(114, 15);
            label2.TabIndex = 2;
            label2.Text = "Название предмета";
            // 
            // txtSubjectName
            // 
            txtSubjectName.Location = new Point(65, 98);
            txtSubjectName.Name = "txtSubjectName";
            txtSubjectName.Size = new Size(121, 23);
            txtSubjectName.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(65, 127);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(121, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Создать предмет";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AddSubjectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 200);
            Controls.Add(btnSave);
            Controls.Add(txtSubjectName);
            Controls.Add(label2);
            Controls.Add(cmbTeacher);
            Controls.Add(label1);
            Name = "AddSubjectDialog";
            Text = "AddSubjectDialog";
            Load += AddSubjectDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbTeacher;
        private Label label2;
        private TextBox txtSubjectName;
        private Button btnSave;
    }
}