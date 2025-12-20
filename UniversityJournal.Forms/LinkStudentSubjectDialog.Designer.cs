
namespace UniversityJournal.Forms
{
    partial class LinkStudentSubjectDialog
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
            cmbStudent = new ComboBox();
            label2 = new Label();
            cmbSubject = new ComboBox();
            btnSave = new Button();
            cmbExistingLinks = new ComboBox();
            btnUnlink = new Button();
            lblExistingLinks = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 55);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Студент";
            // 
            // cmbStudent
            // 
            cmbStudent.FormattingEnabled = true;
            cmbStudent.Location = new Point(79, 73);
            cmbStudent.Name = "cmbStudent";
            cmbStudent.Size = new Size(121, 23);
            cmbStudent.TabIndex = 1;
            cmbStudent.SelectedIndexChanged += cmbStudent_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 99);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "Предмет";
            // 
            // cmbSubject
            // 
            cmbSubject.FormattingEnabled = true;
            cmbSubject.Location = new Point(79, 117);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new Size(121, 23);
            cmbSubject.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(79, 146);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Привязать";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // cmbExistingLinks
            // 
            cmbExistingLinks.FormattingEnabled = true;
            cmbExistingLinks.Location = new Point(285, 73);
            cmbExistingLinks.Name = "cmbExistingLinks";
            cmbExistingLinks.Size = new Size(121, 23);
            cmbExistingLinks.TabIndex = 5;
            // 
            // btnUnlink
            // 
            btnUnlink.Location = new Point(285, 102);
            btnUnlink.Name = "btnUnlink";
            btnUnlink.Size = new Size(75, 23);
            btnUnlink.TabIndex = 6;
            btnUnlink.Text = "Разорвать связь";
            btnUnlink.UseVisualStyleBackColor = true;
            btnUnlink.Click += btnUnlink_Click;
            // 
            // lblExistingLinks
            // 
            lblExistingLinks.AutoSize = true;
            lblExistingLinks.Location = new Point(285, 55);
            lblExistingLinks.Name = "lblExistingLinks";
            lblExistingLinks.Size = new Size(128, 15);
            lblExistingLinks.TabIndex = 7;
            lblExistingLinks.Text = "Существующие связи";
            // 
            // LinkStudentSubjectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(481, 235);
            Controls.Add(lblExistingLinks);
            Controls.Add(btnUnlink);
            Controls.Add(cmbExistingLinks);
            Controls.Add(btnSave);
            Controls.Add(cmbSubject);
            Controls.Add(label2);
            Controls.Add(cmbStudent);
            Controls.Add(label1);
            Name = "LinkStudentSubjectDialog";
            Text = "LinkStudentSubjectDialog";
            Load += LinkStudentSubjectDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void LinkStudentSubjectDialog_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        private Label label1;
        private ComboBox cmbStudent;
        private Label label2;
        private ComboBox cmbSubject;
        private Button btnSave;
        private ComboBox cmbExistingLinks;
        private Button btnUnlink;
        private Label lblExistingLinks;
    }
}