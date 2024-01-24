using System.Windows.Forms;
using TipoutChamp;

namespace TipoutChampWinFormsUI
{
    public partial class MainForm : Form
    {
        private RosterModel roster;
        private BindingSource employeesBindingSource;

        public MainForm()
        {
            InitializeComponent();
            InitializeRosterModel();
            BindDataGridView();

            employeesBindingSource = new BindingSource();

            employeesBindingSource.DataSource = roster.Employees;
            dataGridView.DataSource = employeesBindingSource;
        }

        private void InitializeRosterModel()
        {
            roster = new RosterModel();

            // example employees
            roster.Employees.Add(new Employee { Name = "John", HoursWorked = 5, ChargedTips = 100 });
            roster.Employees.Add(new Employee { Name = "Jane", ChargedTips = 150, Sales = 500 });
        }

        private void BindDataGridView()
        {
            BindingSource source = new BindingSource(roster.Employees, null);
            dataGridView.DataSource = source;

            // Optionally, customize the DataGridView columns, for example:
            // dataGridView.Columns["Name"].HeaderText = "Employee Name";
            // dataGridView.Columns["HoursWorked"].Visible = false; // Hide for non-bartenders/support
            // Add more customization as needed
        }
    }
}
