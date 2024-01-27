using TipoutChamp;

namespace TipoutChampWinFormsUI;

public partial class MainFormWin : Form
{
    private InputModel roster;
    private BindingSource employeesBindingSource;

    public MainFormWin()
    {
        InitializeComponent();
        InitializeRosterModel();
        BindAndConfigureDataGridView();

        this.FormBorderStyle = FormBorderStyle.FixedSingle;
    }

    private void BindAndConfigureDataGridView()
    {
        employeesBindingSource = new BindingSource();
        employeesBindingSource.DataSource = roster.Employees;
        dataGridView.AllowUserToAddRows = false;
        dataGridView.DataSource = employeesBindingSource;
        dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
        dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
        dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        dataGridView.Columns[dataGridView.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

    }

    private void InitializeRosterModel()
    {
        roster = new InputModel();

        // populate w/ example employees
        roster.Employees.Add(new EmployeeEntry { Name = "John", Role = Roles.Support, HoursWorked = 5, });
        roster.Employees.Add(new EmployeeEntry { Name = "Chooch", Role = Roles.Server, ChargedTips = 150, Sales = 500 });
    }

    private void AddEmployee(Roles role)
    {
        EmployeeEntry newEmployee = new EmployeeEntry { Role = role };
        roster.Employees.Add(newEmployee);
    }

    private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        DataGridViewRow row = dataGridView.Rows[e.RowIndex];
        var roleCell = row.Cells["Role"];
        bool isBartender = roleCell.Value != null && roleCell.Value.Equals(Roles.Bartender);
        bool isServer = roleCell.Value != null && roleCell.Value.Equals(Roles.Server);
        bool isSupport = roleCell.Value != null && roleCell.Value.Equals(Roles.Support);
        bool isCellarEvent = roleCell.Value != null && roleCell.Value.Equals(Roles.CellarEvent);
        bool blackOutCell = false;
       
        // support
        if (isSupport && (e.ColumnIndex == dataGridView.Columns["Sales"].Index ||
            e.ColumnIndex == dataGridView.Columns["NetCash"].Index ||
            e.ColumnIndex == dataGridView.Columns["ChargedTips"].Index))
        {
            blackOutCell = true;
        }
        // server
        else if (isServer && (e.ColumnIndex == dataGridView.Columns["HoursWorked"].Index))
        {
            blackOutCell = true;
        }
        // bartender
        else if (isBartender && (e.ColumnIndex == dataGridView.Columns["NetCash"].Index))
        {
            blackOutCell = true;
        }
        // cellar event
        else if (isCellarEvent && (e.ColumnIndex == dataGridView.Columns["NetCash"].Index ||
            e.ColumnIndex == dataGridView.Columns["ChargedTips"].Index||
            e.ColumnIndex == dataGridView.Columns["HoursWorked"].Index))
        {
            blackOutCell = true;
        }

        if (blackOutCell)
        {
            e.CellStyle.BackColor = Color.Black;
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
        }
        else
        {
            e.CellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
            dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
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
            if (columnName == "Sales" || columnName == "ChargedTips" || columnName == "HoursWorked" || columnName == "NetCash")
            {
                dataGridView.Rows[rowIndex].Cells[columnIndex].Value = 0m;

                dataGridView.Rows[rowIndex].ErrorText = string.Empty;

                e.ThrowException = false;
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

    private void btnAddCellarEvent_Click(object sender, EventArgs e)
    {
        Roles role = Roles.CellarEvent;
        AddEmployee(role);
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
        //TODO
    }

    private void btnPrintTest_Click(object sender, EventArgs e)
    {
        string dateTimeNow = DateTime.Now.ToString("yyyyMMddHHmmss");

        string exePath = Application.StartupPath;
        string fileName = $"EmployeeDetails_{dateTimeNow}.txt";
        string filePath = Path.Combine(exePath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (EmployeeEntry employee in roster.Employees)
            {
                string employeeDetails = $"Name: {employee.Name} Role: {employee.Role.ToString()} HoursWorked: {employee.HoursWorked} ChargedTips: {employee.ChargedTips} Sales: {employee.Sales} NetCash: {employee.NetCash}\n";
                writer.WriteLine(employeeDetails);
            }
        }

        MessageBox.Show($"Employee details have been written to {fileName}.", "Print Test");
    }

}