
namespace UniversityJournal.Forms
{
    partial class AddGroupDialog
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
            txtGroupName = new TextBox();
            txtSpecialization = new TextBox();
            txtYear = new TextBox();
            btnSave = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtGroupName
            // 
            txtGroupName.Location = new Point(35, 45);
            txtGroupName.Name = "txtGroupName";
            txtGroupName.Size = new Size(100, 23);
            txtGroupName.TabIndex = 1;
            // 
            // txtSpecialization
            // 
            txtSpecialization.Location = new Point(35, 89);
            txtSpecialization.Name = "txtSpecialization";
            txtSpecialization.Size = new Size(100, 23);
            txtSpecialization.TabIndex = 2;
            // 
            // txtYear
            // 
            txtYear.Location = new Point(35, 133);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(100, 23);
            txtYear.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(35, 162);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Создать группу";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 27);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 0;
            label1.Text = "Название группы";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 71);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 5;
            label2.Text = "Специализация";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 115);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 6;
            label3.Text = "Год";
            // 
            // AddGroupDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(188, 214);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnSave);
            Controls.Add(txtYear);
            Controls.Add(txtSpecialization);
            Controls.Add(txtGroupName);
            Controls.Add(label1);
            Name = "AddGroupDialog";
            Text = "AddGroupDialog";
            Load += AddGroupDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void AddGroupDialog_Load(object sender, EventArgs e)
        {

        }

        #endregion
        private TextBox txtGroupName;
        private TextBox txtSpecialization;
        private TextBox txtYear;
        private Button btnSave;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}