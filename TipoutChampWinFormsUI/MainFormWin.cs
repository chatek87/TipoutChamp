using System.Windows.Forms;
using TipoutChamp;

namespace TipoutChampWinFormsUI;

public partial class MainFormWin : Form
{
    private RosterModel roster;
    private BindingSource employeesBindingSource;

    public MainFormWin()
    {
        InitializeComponent();
        InitializeRosterModel();
        InitializeDataGridView();

        //maybe move this all to a separate class to setup datagridview
        employeesBindingSource = new BindingSource();
        employeesBindingSource.DataSource = roster.Employees;

        dataGridView.AllowUserToAddRows = false;
        dataGridView.DataSource = employeesBindingSource;
        dataGridView.CellEndEdit += DataGridView_CellEndEdit;
    }

    private void InitializeRosterModel()
    {
        roster = new RosterModel();

        // example employees
        roster.Employees.Add(new Employee { Name = "John", Role = Roles.Support, HoursWorked = 5, });
        roster.Employees.Add(new Employee { Name = "Chooch", Role = Roles.Server, ChargedTips = 150, Sales = 500 });
    }
    private void InitializeDataGridView()
    {
        // Create the combobox column
        DataGridViewComboBoxColumn roleColumn = new DataGridViewComboBoxColumn();
        roleColumn.Name = "Role";
        roleColumn.HeaderText = "Role";
        roleColumn.DataPropertyName = "Role"; // This should match the name of the property in your Employee class
        roleColumn.DataSource = Enum.GetValues(typeof(Roles)); // Set the enum as the data source
        roleColumn.ValueType = typeof(Roles);

        // Add the combobox column to the DataGridView
        dataGridView.Columns.Add(roleColumn);

        // Other DataGridView initializations (if necessary)
        // ...
    }


    private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == dataGridView.Columns["Role"].Index)
        {
            var roleCell = dataGridView.Rows[e.RowIndex].Cells["Role"];
            bool isSupport = roleCell.Value != null && roleCell.Value.ToString() == "Support";

            dataGridView.Rows[e.RowIndex].Cells["Sales"].ReadOnly = isSupport;
            dataGridView.Rows[e.RowIndex].Cells["ChargedTips"].ReadOnly = isSupport;
        }
    }

    private void btnAddEmployee_Click(object sender, EventArgs e)
    {
        // Add a new Employee object to the BindingList
        roster.Employees.Add(new Employee());
    }

    private void btnDeleteEmployee_Click(object sender, EventArgs e)
    {
        // Remove the selected Employee object from the BindingList
        if (dataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = dataGridView.SelectedRows[0];
            Employee employee = selectedRow.DataBoundItem as Employee;
            if (employee != null)
            {
                roster.Employees.Remove(employee);
            }
        }
    }

    private void BindDataGridView()
    {
        BindingSource source = new BindingSource(roster.Employees, null);
        dataGridView.DataSource = source;
    }


}
