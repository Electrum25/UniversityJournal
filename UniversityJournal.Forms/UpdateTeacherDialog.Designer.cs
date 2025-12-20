namespace UniversityJournal.Forms
{
    partial class UpdateTeacherDialog
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
            cmbTeachers = new ComboBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtPatronymic = new TextBox();
            btnUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // cmbTeachers
            // 
            cmbTeachers.FormattingEnabled = true;
            cmbTeachers.Location = new Point(95, 55);
            cmbTeachers.Name = "cmbTeachers";
            cmbTeachers.Size = new Size(121, 23);
            cmbTeachers.TabIndex = 0;
            cmbTeachers.SelectedIndexChanged += cmbTeachers_SelectedIndexChanged;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(95, 107);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(121, 23);
            txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(95, 159);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(121, 23);
            txtLastName.TabIndex = 2;
            // 
            // txtPatronymic
            // 
            txtPatronymic.Location = new Point(95, 207);
            txtPatronymic.Name = "txtPatronymic";
            txtPatronymic.Size = new Size(121, 23);
            txtPatronymic.TabIndex = 3;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(95, 255);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Обновить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(95, 37);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 5;
            label1.Text = "Преподователь";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(95, 89);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 6;
            label2.Text = "Имя";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(95, 141);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 7;
            label3.Text = "Фамилия";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(95, 189);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Отчество";
            // 
            // UpdateTeacherDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 324);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnUpdate);
            Controls.Add(txtPatronymic);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(cmbTeachers);
            Name = "UpdateTeacherDialog";
            Text = "UpdateTeacherDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbTeachers;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPatronymic;
        private Button btnUpdate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}