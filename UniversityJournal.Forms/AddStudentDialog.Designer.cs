
namespace UniversityJournal.Forms
{
    partial class AddStudentDialog
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
            txtPassword = new TextBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            btnSave = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbGroup = new ComboBox();
            SuspendLayout();
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(25, 40);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(121, 23);
            txtLogin.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(25, 84);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(121, 23);
            txtPassword.TabIndex = 1;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(25, 128);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(121, 23);
            txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(25, 172);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(121, 23);
            txtLastName.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(25, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(121, 23);
            btnSave.TabIndex = 5;
            btnSave.Text = "Добавить студента";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 22);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 6;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 66);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 7;
            label2.Text = "Пароль";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 110);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 8;
            label3.Text = "Имя";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 154);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 9;
            label4.Text = "Фамилия";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 198);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 10;
            label5.Text = "Группа";
            // 
            // cmbGroup
            // 
            cmbGroup.FormattingEnabled = true;
            cmbGroup.Location = new Point(25, 216);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(121, 23);
            cmbGroup.TabIndex = 11;
            // 
            // AddStudentDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(181, 294);
            Controls.Add(cmbGroup);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(txtLastName);
            Controls.Add(txtFirstName);
            Controls.Add(txtPassword);
            Controls.Add(txtLogin);
            Name = "AddStudentDialog";
            Text = "AddStudentDialog";
            Load += AddStudentDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddStudentDialog_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        private TextBox txtLogin;
        private TextBox txtPassword;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private Button btnSave;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cmbGroup;
    }
}