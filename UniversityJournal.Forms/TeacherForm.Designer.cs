
namespace UniversityJournal.Forms
{
    partial class TeacherForm
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
            cmbSubject = new ComboBox();
            cmbStudent = new ComboBox();
            txtLabNumber = new TextBox();
            txtScore = new TextBox();
            txtComment = new TextBox();
            btnSetGrade = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbStatus = new ComboBox();
            btnMarkAttendance = new Button();
            label6 = new Label();
            dtpDate = new DateTimePicker();
            txtFinalGrade = new TextBox();
            btnUpdateFinalGrade = new Button();
            label7 = new Label();
            btnViewResults = new Button();
            btnLogout = new Button();
            SuspendLayout();
            // 
            // cmbSubject
            // 
            cmbSubject.FormattingEnabled = true;
            cmbSubject.Location = new Point(37, 112);
            cmbSubject.Name = "cmbSubject";
            cmbSubject.Size = new Size(121, 23);
            cmbSubject.TabIndex = 0;
            cmbSubject.SelectedIndexChanged += cmbSubject_SelectedIndexChanged;
            // 
            // cmbStudent
            // 
            cmbStudent.FormattingEnabled = true;
            cmbStudent.Location = new Point(37, 68);
            cmbStudent.Name = "cmbStudent";
            cmbStudent.Size = new Size(121, 23);
            cmbStudent.TabIndex = 1;
            // 
            // txtLabNumber
            // 
            txtLabNumber.Location = new Point(301, 68);
            txtLabNumber.Name = "txtLabNumber";
            txtLabNumber.Size = new Size(121, 23);
            txtLabNumber.TabIndex = 2;
            // 
            // txtScore
            // 
            txtScore.Location = new Point(301, 112);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(121, 23);
            txtScore.TabIndex = 3;
            // 
            // txtComment
            // 
            txtComment.Location = new Point(301, 156);
            txtComment.Name = "txtComment";
            txtComment.Size = new Size(121, 23);
            txtComment.TabIndex = 4;
            // 
            // btnSetGrade
            // 
            btnSetGrade.Location = new Point(326, 185);
            btnSetGrade.Name = "btnSetGrade";
            btnSetGrade.Size = new Size(75, 23);
            btnSetGrade.TabIndex = 5;
            btnSetGrade.Text = "Оценка";
            btnSetGrade.UseVisualStyleBackColor = true;
            btnSetGrade.Click += btnSetGrade_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 94);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 6;
            label1.Text = "Предмет";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 50);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 7;
            label2.Text = "Студент";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(301, 50);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 8;
            label3.Text = "Лабораторная";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(301, 94);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 9;
            label4.Text = "Оценка";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(301, 138);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 10;
            label5.Text = "Комментарий";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(602, 68);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(121, 23);
            cmbStatus.TabIndex = 11;
            // 
            // btnMarkAttendance
            // 
            btnMarkAttendance.Location = new Point(582, 126);
            btnMarkAttendance.Name = "btnMarkAttendance";
            btnMarkAttendance.Size = new Size(166, 23);
            btnMarkAttendance.TabIndex = 12;
            btnMarkAttendance.Text = "Отметить посещаемость";
            btnMarkAttendance.UseVisualStyleBackColor = true;
            btnMarkAttendance.Click += btnMarkAttendance_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(602, 50);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 13;
            label6.Text = "Посещаемость";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(562, 97);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 23);
            dtpDate.TabIndex = 14;
            // 
            // txtFinalGrade
            // 
            txtFinalGrade.Location = new Point(611, 185);
            txtFinalGrade.Name = "txtFinalGrade";
            txtFinalGrade.Size = new Size(100, 23);
            txtFinalGrade.TabIndex = 15;
            // 
            // btnUpdateFinalGrade
            // 
            btnUpdateFinalGrade.Location = new Point(562, 214);
            btnUpdateFinalGrade.Name = "btnUpdateFinalGrade";
            btnUpdateFinalGrade.Size = new Size(186, 23);
            btnUpdateFinalGrade.TabIndex = 16;
            btnUpdateFinalGrade.Text = "Поставить итоговую оценку";
            btnUpdateFinalGrade.UseVisualStyleBackColor = true;
            btnUpdateFinalGrade.Click += btnUpdateFinalGrade_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(611, 167);
            label7.Name = "label7";
            label7.Size = new Size(100, 15);
            label7.TabIndex = 17;
            label7.Text = "Итоговая оценка";
            // 
            // btnViewResults
            // 
            btnViewResults.Location = new Point(12, 415);
            btnViewResults.Name = "btnViewResults";
            btnViewResults.Size = new Size(155, 23);
            btnViewResults.TabIndex = 18;
            btnViewResults.Text = "Просматреть журнал";
            btnViewResults.UseVisualStyleBackColor = true;
            btnViewResults.Click += btnViewResults_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(713, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 23);
            btnLogout.TabIndex = 19;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // TeacherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogout);
            Controls.Add(btnViewResults);
            Controls.Add(label7);
            Controls.Add(btnUpdateFinalGrade);
            Controls.Add(txtFinalGrade);
            Controls.Add(dtpDate);
            Controls.Add(label6);
            Controls.Add(btnMarkAttendance);
            Controls.Add(cmbStatus);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSetGrade);
            Controls.Add(txtComment);
            Controls.Add(txtScore);
            Controls.Add(txtLabNumber);
            Controls.Add(cmbStudent);
            Controls.Add(cmbSubject);
            Name = "TeacherForm";
            Text = "TeacherForm";
            Load += TeacherForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        private ComboBox cmbSubject;
        private ComboBox cmbStudent;
        private TextBox txtLabNumber;
        private TextBox txtScore;
        private TextBox txtComment;
        private Button btnSetGrade;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox cmbStatus;
        private Button btnMarkAttendance;
        private Label label6;
        private DateTimePicker dtpDate;
        private TextBox txtFinalGrade;
        private Button btnUpdateFinalGrade;
        private Label label7;
        private Button btnViewResults;
        private Button btnLogout;
    }
}