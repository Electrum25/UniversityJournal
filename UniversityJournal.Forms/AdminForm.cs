using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class AdminForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AdminForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            btnAddTeacher.Focus();
            btnAddStudent.Focus();
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddTeacherDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddGroupDialog(_serviceProvider);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddStudentDialog(_serviceProvider);
                form.ShowDialog(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCreateSubject_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddSubjectDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLinkStudentSubject_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new LinkStudentSubjectDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new DeleteUserDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new DeleteGroupDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new DeleteSubjectDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UpdateTeacherDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UpdateStudentDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UpdateGroupDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateSubject_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UpdateSubjectDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnExportToExcel_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Выберите место для сохранения экспорта БД";
                saveFileDialog.FileName = "UniversityJournalExport.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var useCase = scope.ServiceProvider.GetRequiredService<ExportDatabaseToExcelUseCase>();
                            await useCase.ExportToExcel(saveFileDialog.FileName);
                            MessageBox.Show("Экспорт завершен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddAdminDialog(_serviceProvider);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия диалога: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm(_serviceProvider);
            loginForm.Show();
            this.Hide();
        }
    }
}
