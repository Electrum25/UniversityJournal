namespace UniversityJournal.Forms
{
    partial class UpdateGroupDialog
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
            txtGroupName = new TextBox();
            txtSpecialization = new TextBox();
            nudYear = new NumericUpDown();
            btnUpdate = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
            SuspendLayout();
            // 
            // cmbGroups
            // 
            cmbGroups.FormattingEnabled = true;
            cmbGroups.Location = new Point(141, 61);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(121, 23);
            cmbGroups.TabIndex = 0;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // txtGroupName
            // 
            txtGroupName.Location = new Point(141, 115);
            txtGroupName.Name = "txtGroupName";
            txtGroupName.Size = new Size(121, 23);
            txtGroupName.TabIndex = 1;
            // 
            // txtSpecialization
            // 
            txtSpecialization.Location = new Point(141, 175);
            txtSpecialization.Name = "txtSpecialization";
            txtSpecialization.Size = new Size(121, 23);
            txtSpecialization.TabIndex = 2;
            // 
            // nudYear
            // 
            nudYear.Location = new Point(141, 223);
            nudYear.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
            nudYear.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudYear.Name = "nudYear";
            nudYear.Size = new Size(121, 23);
            nudYear.TabIndex = 3;
            nudYear.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(141, 267);
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
            label1.Location = new Point(141, 43);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 5;
            label1.Text = "Группа";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(141, 97);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 6;
            label2.Text = "Имя группы";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(141, 157);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 7;
            label3.Text = "Специлизация";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(141, 205);
            label4.Name = "label4";
            label4.Size = new Size(26, 15);
            label4.TabIndex = 8;
            label4.Text = "Год";
            // 
            // UpdateGroupDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 347);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnUpdate);
            Controls.Add(nudYear);
            Controls.Add(txtSpecialization);
            Controls.Add(txtGroupName);
            Controls.Add(cmbGroups);
            Name = "UpdateGroupDialog";
            Text = "UpdateGroupDialog";
            ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbGroups;
        private TextBox txtGroupName;
        private TextBox txtSpecialization;
        private NumericUpDown nudYear;
        private Button btnUpdate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}