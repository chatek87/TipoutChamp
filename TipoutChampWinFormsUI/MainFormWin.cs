using TipoutChamp;

namespace TipoutChampWinFormsUI;

public partial class MainFormWin : Form
{
    private InputModel input;
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
        employeesBindingSource.DataSource = input.Employees;
        dataGridView.AllowUserToAddRows = false;
        dataGridView.DataSource = employeesBindingSource;
        dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);
        dataGridView.DataError += new DataGridViewDataErrorEventHandler(dataGridView_DataError);
        dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        dataGridView.Columns[dataGridView.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

    }

    private void InitializeRosterModel()
    {
        input = new InputModel();

        // populate w/ example employees
        input.Employees.Add(new EmployeeEntry { Name = "Hoss", Role = Roles.Bartender, HoursWorked = 7, Sales = 1000, ChargedTips = 200});
        input.Employees.Add(new EmployeeEntry { Name = "John", Role = Roles.Support, HoursWorked = 5, });
        input.Employees.Add(new EmployeeEntry { Name = "Chooch", Role = Roles.Server, ChargedTips = 150, Sales = 500 });
        input.Employees.Add(new EmployeeEntry { Name = "Cheech", Role = Roles.Server, ChargedTips = 150, Sales = 500 });
        input.Employees.Add(new EmployeeEntry { Name = "Chaach", Role = Roles.Server, ChargedTips = 150, Sales = 500 });
        input.Employees.Add(new EmployeeEntry { Name = "Gomphus", Role = Roles.CellarEvent, Sales = 2000 });
    }

    private void AddEmployee(Roles role)
    {
        EmployeeEntry newEmployee = new EmployeeEntry { Role = role };
        input.Employees.Add(newEmployee);
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

    private void btnGenerateReport_Click(object sender, EventArgs e)
    {
        var calculator = new TipoutCalculator(input);
        var fileName = calculator.WriteReportToTextFileReturnFileName(calculator.ReportString);
        ResultsForm resultsForm = new ResultsForm(calculator.ReportString);
        resultsForm.ShowDialog(); // Show the form as a modal dialog
        MessageBox.Show($"Tipout Report has been written to {fileName}.", "Print Test");
    }
}