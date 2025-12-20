namespace UniversityJournal.Forms
{
    partial class UpdateStudentDialog
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
            cmbStudents = new ComboBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            cmbNewGroup = new ComboBox();
            btnUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // cmbGroups
            // 
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(122, 43);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(121, 23);
            cmbGroups.TabIndex = 0;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // cmbStudents
            // 
            cmbStudents.FormattingEnabled = true;
            cmbStudents.Location = new Point(122, 95);
            cmbStudents.Name = "cmbStudents";
            cmbStudents.Size = new Size(121, 23);
            cmbStudents.TabIndex = 1;
            cmbStudents.SelectedIndexChanged += cmbStudents_SelectedIndexChanged;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(122, 148);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(121, 23);
            txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(122, 198);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(121, 23);
            txtLastName.TabIndex = 3;
            // 
            // cmbNewGroup
            // 
            cmbNewGroup.FormattingEnabled = true;
            cmbNewGroup.Location = new Point(122, 252);
            cmbNewGroup.Name = "cmbNewGroup";
            cmbNewGroup.Size = new Size(121, 23);
            cmbNewGroup.TabIndex = 4;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(142, 295);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 5;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(122, 25);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 6;
            label1.Text = "Группа";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(122, 77);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 7;
            label2.Text = "Студент";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(122, 130);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Имя";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(122, 180);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 9;
            label4.Text = "Фамилия";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(122, 234);
            label5.Name = "label5";
            label5.Size = new Size(82, 15);
            label5.TabIndex = 10;
            label5.Text = "Новая группа";
            // 
            // UpdateStudentDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 336);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnUpdate);
            Controls.Add(cmbNewGroup);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(cmbStudents);
            Controls.Add(cmbGroups);
            Name = "UpdateStudentDialog";
            Text = "UpdateStudentDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbGroups;
        private ComboBox cmbStudents;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private ComboBox cmbNewGroup;
        private Button btnUpdate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}