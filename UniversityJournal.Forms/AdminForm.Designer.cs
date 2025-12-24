namespace UniversityJournal.Forms
{
    partial class AdminForm
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
            btnAddTeacher = new Button();
            btnAddStudent = new Button();
            btnAddGroup = new Button();
            btnCreateSubject = new Button();
            btnLinkStudentSubject = new Button();
            btnDeleteUser = new Button();
            btnDeleteGroup = new Button();
            btnDeleteSubject = new Button();
            btnUpdateTeacher = new Button();
            btnUpdateStudent = new Button();
            btnUpdateGroup = new Button();
            btnUpdateSubject = new Button();
            btnExportToExcel = new Button();
            btnAddAdmin = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // btnAddTeacher
            // 
            btnAddTeacher.Location = new Point(54, 107);
            btnAddTeacher.Name = "btnAddTeacher";
            btnAddTeacher.Size = new Size(164, 23);
            btnAddTeacher.TabIndex = 0;
            btnAddTeacher.Text = "Добавить преподавателя";
            btnAddTeacher.UseVisualStyleBackColor = true;
            btnAddTeacher.Click += btnAddTeacher_Click;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Location = new Point(54, 136);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(164, 23);
            btnAddStudent.TabIndex = 1;
            btnAddStudent.Text = "Добавить студента";
            btnAddStudent.UseVisualStyleBackColor = true;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // btnAddGroup
            // 
            btnAddGroup.Location = new Point(54, 165);
            btnAddGroup.Name = "btnAddGroup";
            btnAddGroup.Size = new Size(164, 23);
            btnAddGroup.TabIndex = 2;
            btnAddGroup.Text = "Добавить группу";
            btnAddGroup.UseVisualStyleBackColor = true;
            btnAddGroup.Click += btnAddGroup_Click;
            // 
            // btnCreateSubject
            // 
            btnCreateSubject.Location = new Point(54, 194);
            btnCreateSubject.Name = "btnCreateSubject";
            btnCreateSubject.Size = new Size(164, 23);
            btnCreateSubject.TabIndex = 3;
            btnCreateSubject.Text = "Создать предмет";
            btnCreateSubject.UseVisualStyleBackColor = true;
            btnCreateSubject.Click += btnCreateSubject_Click;
            // 
            // btnLinkStudentSubject
            // 
            btnLinkStudentSubject.Location = new Point(54, 223);
            btnLinkStudentSubject.Name = "btnLinkStudentSubject";
            btnLinkStudentSubject.Size = new Size(164, 43);
            btnLinkStudentSubject.TabIndex = 4;
            btnLinkStudentSubject.Text = "Привязать и отвязать к предмету";
            btnLinkStudentSubject.UseVisualStyleBackColor = true;
            btnLinkStudentSubject.Click += btnLinkStudentSubject_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.Location = new Point(474, 78);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(138, 23);
            btnDeleteUser.TabIndex = 5;
            btnDeleteUser.Text = "Удалить пользователя";
            btnDeleteUser.UseVisualStyleBackColor = true;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // btnDeleteGroup
            // 
            btnDeleteGroup.Location = new Point(474, 107);
            btnDeleteGroup.Name = "btnDeleteGroup";
            btnDeleteGroup.Size = new Size(138, 23);
            btnDeleteGroup.TabIndex = 6;
            btnDeleteGroup.Text = "Удалить группу";
            btnDeleteGroup.UseVisualStyleBackColor = true;
            btnDeleteGroup.Click += btnDeleteGroup_Click;
            // 
            // btnDeleteSubject
            // 
            btnDeleteSubject.Location = new Point(474, 136);
            btnDeleteSubject.Name = "btnDeleteSubject";
            btnDeleteSubject.Size = new Size(138, 23);
            btnDeleteSubject.TabIndex = 7;
            btnDeleteSubject.Text = "Удалить предмет";
            btnDeleteSubject.UseVisualStyleBackColor = true;
            btnDeleteSubject.Click += btnDeleteSubject_Click;
            // 
            // btnUpdateTeacher
            // 
            btnUpdateTeacher.Location = new Point(266, 78);
            btnUpdateTeacher.Name = "btnUpdateTeacher";
            btnUpdateTeacher.Size = new Size(168, 23);
            btnUpdateTeacher.TabIndex = 8;
            btnUpdateTeacher.Text = "Изменить преподователя";
            btnUpdateTeacher.UseVisualStyleBackColor = true;
            btnUpdateTeacher.Click += btnUpdateTeacher_Click;
            // 
            // btnUpdateStudent
            // 
            btnUpdateStudent.Location = new Point(266, 107);
            btnUpdateStudent.Name = "btnUpdateStudent";
            btnUpdateStudent.Size = new Size(168, 23);
            btnUpdateStudent.TabIndex = 9;
            btnUpdateStudent.Text = "Изменить студента";
            btnUpdateStudent.UseVisualStyleBackColor = true;
            btnUpdateStudent.Click += btnUpdateStudent_Click;
            // 
            // btnUpdateGroup
            // 
            btnUpdateGroup.Location = new Point(266, 136);
            btnUpdateGroup.Name = "btnUpdateGroup";
            btnUpdateGroup.Size = new Size(168, 23);
            btnUpdateGroup.TabIndex = 10;
            btnUpdateGroup.Text = "Изменить группу";
            btnUpdateGroup.UseVisualStyleBackColor = true;
            btnUpdateGroup.Click += btnUpdateGroup_Click;
            // 
            // btnUpdateSubject
            // 
            btnUpdateSubject.Location = new Point(266, 165);
            btnUpdateSubject.Name = "btnUpdateSubject";
            btnUpdateSubject.Size = new Size(168, 23);
            btnUpdateSubject.TabIndex = 11;
            btnUpdateSubject.Text = "Изменить предмет";
            btnUpdateSubject.UseVisualStyleBackColor = true;
            btnUpdateSubject.Click += btnUpdateSubject_Click;
            // 
            // btnExportToExcel
            // 
            btnExportToExcel.Location = new Point(474, 289);
            btnExportToExcel.Name = "btnExportToExcel";
            btnExportToExcel.Size = new Size(138, 23);
            btnExportToExcel.TabIndex = 12;
            btnExportToExcel.Text = "Экспорт БД";
            btnExportToExcel.UseVisualStyleBackColor = true;
            btnExportToExcel.Click += btnExportToExcel_Click;
            // 
            // btnAddAdmin
            // 
            btnAddAdmin.Location = new Point(54, 78);
            btnAddAdmin.Name = "btnAddAdmin";
            btnAddAdmin.Size = new Size(164, 23);
            btnAddAdmin.TabIndex = 13;
            btnAddAdmin.Text = "Создать админа";
            btnAddAdmin.UseVisualStyleBackColor = true;
            btnAddAdmin.Click += btnAddAdmin_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(537, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 23);
            btnLogout.TabIndex = 14;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 346);
            Controls.Add(btnLogout);
            Controls.Add(btnAddAdmin);
            Controls.Add(btnExportToExcel);
            Controls.Add(btnUpdateSubject);
            Controls.Add(btnUpdateGroup);
            Controls.Add(btnUpdateStudent);
            Controls.Add(btnUpdateTeacher);
            Controls.Add(btnDeleteSubject);
            Controls.Add(btnDeleteGroup);
            Controls.Add(btnDeleteUser);
            Controls.Add(btnLinkStudentSubject);
            Controls.Add(btnCreateSubject);
            Controls.Add(btnAddGroup);
            Controls.Add(btnAddStudent);
            Controls.Add(btnAddTeacher);
            Name = "AdminForm";
            Text = "AdminForm";
            Load += AdminForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnAddTeacher;
        private Button btnAddStudent;
        private Button btnAddGroup;
        private Button btnCreateSubject;
        private Button btnLinkStudentSubject;
        private Button btnDeleteUser;
        private Button btnDeleteGroup;
        private Button btnDeleteSubject;
        private Button btnUpdateTeacher;
        private Button btnUpdateStudent;
        private Button btnUpdateGroup;
        private Button btnUpdateSubject;
        private Button btnExportToExcel;
        private Button btnAddAdmin;
        private Button btnLogout;
    }
}
