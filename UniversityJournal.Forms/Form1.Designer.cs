namespace UniversityJournal.Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAuth = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnAuth
            // 
            btnAuth.Location = new Point(99, 72);
            btnAuth.Name = "btnAuth";
            btnAuth.Size = new Size(75, 23);
            btnAuth.TabIndex = 0;
            btnAuth.Text = "Войти";
            btnAuth.UseVisualStyleBackColor = true;
            btnAuth.Click += btnAuth_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 54);
            label1.Name = "label1";
            label1.Size = new Size(269, 15);
            label1.TabIndex = 1;
            label1.Text = "Подсистема учёта успеваемости обучающихся";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 190);
            Controls.Add(label1);
            Controls.Add(btnAuth);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAuth;
        private Label label1;
    }
}
