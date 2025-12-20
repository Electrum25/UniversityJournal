namespace UniversityJournal.Forms
{
    partial class DeleteGroupDialog
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
            cmbGroups = new ComboBox();
            btnDelete = new Button();
            lblInstruction = new Label();
            chkDeleteWithStudents = new CheckBox();
            SuspendLayout();
            // 
            // cmbGroups
            // 
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(81, 72);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(121, 23);
            cmbGroups.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(104, 101);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblInstruction
            // 
            lblInstruction.AutoSize = true;
            lblInstruction.Location = new Point(63, 54);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(178, 15);
            lblInstruction.TabIndex = 2;
            lblInstruction.Text = "Выберите группу для удаления";
            // 
            // chkDeleteWithStudents
            // 
            chkDeleteWithStudents.AutoSize = true;
            chkDeleteWithStudents.Location = new Point(63, 143);
            chkDeleteWithStudents.Name = "chkDeleteWithStudents";
            chkDeleteWithStudents.Size = new Size(193, 19);
            chkDeleteWithStudents.TabIndex = 3;
            chkDeleteWithStudents.Text = "Удалить вместе со студентами";
            chkDeleteWithStudents.UseVisualStyleBackColor = true;
            // 
            // DeleteGroupDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 222);
            Controls.Add(chkDeleteWithStudents);
            Controls.Add(lblInstruction);
            Controls.Add(btnDelete);
            Controls.Add(cmbGroups);
            Name = "DeleteGroupDialog";
            Text = "DeleteGroupDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbGroups;
        private Button btnDelete;
        private Label lblInstruction;
        private CheckBox chkDeleteWithStudents;
    }
}