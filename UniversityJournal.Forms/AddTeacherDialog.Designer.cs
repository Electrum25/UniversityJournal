
namespace UniversityJournal.Forms
{
    partial class AddTeacherDialog
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
            txtLogin = new TextBox();
            label1 = new Label();
            txtPassword = new TextBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtPatronymic = new TextBox();
            btnSave = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(31, 45);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(124, 23);
            txtLogin.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 27);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 1;
            label1.Text = "Логин";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(31, 89);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(124, 23);
            txtPassword.TabIndex = 2;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(31, 132);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(124, 23);
            txtFirstName.TabIndex = 3;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(31, 176);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(124, 23);
            txtLastName.TabIndex = 4;
            // 
            // txtPatronymic
            // 
            txtPatronymic.Location = new Point(31, 222);
            txtPatronymic.Name = "txtPatronymic";
            txtPatronymic.Size = new Size(124, 23);
            txtPatronymic.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(31, 251);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(124, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Добавить учителя";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 71);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 7;
            label2.Text = "Пароль";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 114);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Имя";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 158);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 9;
            label4.Text = "Фамилия";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 202);
            label5.Name = "label5";
            label5.Size = new Size(58, 15);
            label5.TabIndex = 10;
            label5.Text = "Отчество";
            // 
            // AddTeacherDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(187, 313);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnSave);
            Controls.Add(txtPatronymic);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(txtPassword);
            Controls.Add(label1);
            Controls.Add(txtLogin);
            Name = "AddTeacherDialog";
            Text = "AddTeacherDialog";
            Load += AddTeacherDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddTeacherDialog_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private TextBox txtLogin;
        private Label label1;
        private TextBox txtPassword;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPatronymic;
        private Button btnSave;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}