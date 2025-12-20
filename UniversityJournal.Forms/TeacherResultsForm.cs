using Microsoft.Extensions.DependencyInjection;
using System.Xml.XPath;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class TeacherResultsForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Guid _teacherId;  
        private List<Group> _groups;

        public TeacherResultsForm(IServiceProvider serviceProvider, Guid teacherId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _teacherId = teacherId;
            LoadGroups();
            LoadDataTypes();
        }

        private async void LoadGroups()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IGroupRepository>();
                _groups = await repo.GetAll() ?? new List<Group>();

                cmbGroups.DataSource = _groups.Select(g => new
                {
                    Display = $"{g.GroupName} ({g.Specialization}, {g.Year})",
                    Value = g.GroupId
                }).ToList();
                cmbGroups.DisplayMember = "Display";
                cmbGroups.ValueMember = "Value";
            }
        }

        private void LoadDataTypes()
        {
            cmbDataType.DataSource = new List<string> { "Посещаемость", "Оценки", "Финальные оценки" };
        }

        private async void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadResults();
        }

        private async void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadResults();
        }

        private async Task LoadResults()
        {
            if (cmbGroups.SelectedValue is Guid groupId && cmbDataType.SelectedItem is string dataType)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var useCase = scope.ServiceProvider.GetRequiredService<GetTeacherResultsUseCase>();
                    var results = await useCase.Handle(_teacherId, groupId, dataType);
                    dgvResults.DataSource = results;
                }
            }
        }
    }
}