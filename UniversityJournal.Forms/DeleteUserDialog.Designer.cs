namespace UniversityJournal.Forms
{
    partial class DeleteUserDialog
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
            cmbUsers = new ComboBox();
            btnDelete = new Button();
            lblInstruction = new Label();
            SuspendLayout();
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(84, 51);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(121, 23);
            cmbUsers.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(108, 80);
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
            lblInstruction.Location = new Point(41, 33);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(215, 15);
            lblInstruction.TabIndex = 2;
            lblInstruction.Text = "Выберите пользователя для удаления";
            // 
            // DeleteUserDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(311, 162);
            Controls.Add(lblInstruction);
            Controls.Add(btnDelete);
            Controls.Add(cmbUsers);
            Name = "DeleteUserDialog";
            Text = "DeleteUserDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsers;
        private Button btnDelete;
        private Label lblInstruction;
    }
}