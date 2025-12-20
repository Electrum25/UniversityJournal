namespace UniversityJournal.Forms
{
    partial class DeleteSubjectDialog
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
            chkDeleteWithRelated = new CheckBox();
            btnDelete = new Button();
            lblInstruction = new Label();
            SuspendLayout();
            // 
            // cmbSubjects
            // 
            cmbSubjects.FormattingEnabled = true;
            cmbSubjects.Location = new Point(104, 56);
            cmbSubjects.Name = "cmbSubjects";
            cmbSubjects.Size = new Size(121, 23);
            cmbSubjects.TabIndex = 0;
            // 
            // chkDeleteWithRelated
            // 
            chkDeleteWithRelated.AutoSize = true;
            chkDeleteWithRelated.Location = new Point(48, 85);
            chkDeleteWithRelated.Name = "chkDeleteWithRelated";
            chkDeleteWithRelated.Size = new Size(252, 19);
            chkDeleteWithRelated.TabIndex = 1;
            chkDeleteWithRelated.Text = "Удалить вместе со связанными данными";
            chkDeleteWithRelated.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(127, 110);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(73, 38);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(186, 15);
            lblInstruction.TabIndex = 3;
            lblInstruction.Text = "Выберите предмет для удаления";
            // 
            // DeleteSubjectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 169);
            Controls.Add(lblInstruction);
            Controls.Add(btnDelete);
            Controls.Add(chkDeleteWithRelated);
            Controls.Add(cmbSubjects);
            Name = "DeleteSubjectDialog";
            Text = "DeleteSubjectDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbSubjects;
        private CheckBox chkDeleteWithRelated;
        private Button btnDelete;
        private Label lblInstruction;
    }
}