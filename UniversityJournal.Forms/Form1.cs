using Microsoft.Extensions.DependencyInjection;

namespace UniversityJournal.Forms
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public Form1(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnAuth.Focus();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm(_serviceProvider);
            loginForm.Owner = this; 
            loginForm.ShowDialog(); 
        }
    }
}