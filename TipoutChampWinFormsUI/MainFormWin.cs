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
        //SetupRoleButtons();

        //maybe move this all to a separate class to setup datagridview
        employeesBindingSource = new BindingSource();
        employeesBindingSource.DataSource = roster.Employees;

        dataGridView.AllowUserToAddRows = false;
        dataGridView.DataSource = employeesBindingSource;
        dataGridView.CellEndEdit += DataGridView_CellEndEdit;
        dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
        dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);

    }

    private void BindDataGridView()
    {
        BindingSource source = new BindingSource(roster.Employees, null);
        dataGridView.DataSource = source;
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
        DataGridViewTextBoxColumn roleColumn = new DataGridViewTextBoxColumn();
        roleColumn.Name = "Role";
        roleColumn.HeaderText = "Role";
        roleColumn.DataPropertyName = "Role"; // This should match the name of the property in your Employee class
        roleColumn.ReadOnly = true; // Make this column read-only
        dataGridView.Columns.Add(roleColumn);

    }

    private void AddEmployee(Roles role)
    {
        Employee newEmployee = new Employee { Role = role };
        roster.Employees.Add(newEmployee);
    }

    private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == dataGridView.Columns["Role"].Index)
        {
            var roleCell = dataGridView.Rows[e.RowIndex].Cells["Role"];
            bool isSupport = roleCell.Value != null && roleCell.Value.ToString() == "Support";

            var salesCell = dataGridView.Rows[e.RowIndex].Cells["Sales"];
            var chargedTipsCell = dataGridView.Rows[e.RowIndex].Cells["ChargedTips"];

            salesCell.ReadOnly = isSupport;
            chargedTipsCell.ReadOnly = isSupport;

            salesCell.Style.BackColor = isSupport ? Color.Black : dataGridView.DefaultCellStyle.BackColor;
            chargedTipsCell.Style.BackColor = isSupport ? Color.Black : dataGridView.DefaultCellStyle.BackColor;
        }
    }
    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.ColumnIndex == dataGridView.Columns["Sales"].Index || e.ColumnIndex == dataGridView.Columns["ChargedTips"].Index)
        {
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            var roleCell = row.Cells["Role"];
            bool isSupport = roleCell.Value != null && roleCell.Value.ToString() == "Support";

            e.CellStyle.BackColor = isSupport ? Color.Black : dataGridView.DefaultCellStyle.BackColor;
        }
    }

    private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        if (e.Context == DataGridViewDataErrorContexts.Formatting ||
            e.Context == DataGridViewDataErrorContexts.Parsing)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            string columnName = dataGridView.Columns[columnIndex].Name;
            if (columnName == "Sales" || columnName == "ChargedTips" || columnName == "HoursWorked")
            {
                dataGridView.Rows[rowIndex].Cells[columnIndex].Value = 0m;

                dataGridView.Rows[rowIndex].ErrorText = string.Empty;

                e.ThrowException = false;
            }
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

    private void btnAddBartender_Click(object sender, EventArgs e)
    {
        Roles role = Roles.Bartender;
        AddEmployee(role);

    }

    private void btnAddServer_Click(object sender, EventArgs e)
    {
        Roles role = Roles.Server;
        AddEmployee(role);
    }

    private void btnAddSupport_Click(object sender, EventArgs e)
    {
        Roles role = Roles.Support;
        AddEmployee(role);
    }
}