using System.ComponentModel;
using TipoutChamp;

namespace TipoutChampWinFormsUI;

public partial class MainForm : Form
{
    private InputModel input;       // declaration
    private BindingSource employeesBindingSource;
    private Random random = new Random();

    public MainForm()
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
        //dataGridView.Dock = DockStyle.Fill;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void InitializeRosterModel()
    {
        input = new InputModel();   // instantiation
        PopulateWithSampleData();
        
    }

    private void PopulateWithSampleData()
    {
        input.Employees.Add(new EmployeeEntry { Name = "Bartender 1", Role = Roles.Bartender, HoursWorked = 7, Sales = 1000, ChargedTips = 200 });
        input.Employees.Add(new EmployeeEntry { Name = "Bartender 2", Role = Roles.Bartender, HoursWorked = 6, Sales = 1000, ChargedTips = 200 });
        input.Employees.Add(new EmployeeEntry { Name = "Server 1", Role = Roles.Server, ChargedTips = 225, CashPayments = 220, Sales = 1500 });
        input.Employees.Add(new EmployeeEntry { Name = "Server 2", Role = Roles.Server, ChargedTips = 356, Sales = 1800 });
        input.Employees.Add(new EmployeeEntry { Name = "Server 3", Role = Roles.Server, ChargedTips = 300, Sales = 1500, CashPayments = 27 });
        input.Employees.Add(new EmployeeEntry { Name = "Support 1", Role = Roles.Support, HoursWorked = 5, });
        input.Employees.Add(new EmployeeEntry { Name = "Support 2", Role = Roles.Support, HoursWorked = 3, });
        input.Employees.Add(new EmployeeEntry { Name = "CellarEvent 1", Role = Roles.CellarEvent, Sales = 2000 });
        input.Employees.Add(new EmployeeEntry { Name = "CellarEvent 2", Role = Roles.CellarEvent, Sales = 2000, ToBar = 2 });
    }

    private void AddEmployee(Roles role)
    {
        EmployeeEntry newEmployee = new EmployeeEntry { Role = role };
        input.Employees.Add(newEmployee);
        SortAndBindData();
    }
    private void SortAndBindData()
    {
        var sortedList = input.Employees.OrderBy(e => e.Role).ToList();
        input.Employees.Clear();
        foreach (var employee in sortedList)
        {
            input.Employees.Add(employee);
        }
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
            e.ColumnIndex == dataGridView.Columns["CashPayments"].Index ||
            e.ColumnIndex == dataGridView.Columns["ChargedTips"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToBar"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToSupport"].Index))
        {
            blackOutCell = true;
        }
        // server
        else if (isServer && (e.ColumnIndex == dataGridView.Columns["HoursWorked"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToBar"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToSupport"].Index))
        {
            blackOutCell = true;
        }
        // bartender
        else if (isBartender && (e.ColumnIndex == dataGridView.Columns["CashPayments"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToBar"].Index ||
            e.ColumnIndex == dataGridView.Columns["ToSupport"].Index))
        {
            blackOutCell = true;
        }
        // cellar event
        else if (isCellarEvent && (e.ColumnIndex == dataGridView.Columns["CashPayments"].Index ||
            e.ColumnIndex == dataGridView.Columns["ChargedTips"].Index ||
            e.ColumnIndex == dataGridView.Columns["HoursWorked"].Index))
        {
            blackOutCell = true;
        }

        if (blackOutCell)
        {
            e.CellStyle.BackColor = Color.Gainsboro;
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
            if (columnName == "Sales" || columnName == "ChargedTips" || columnName == "HoursWorked" || columnName == "CashPayments")
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
        ResultsForm resultsForm = new ResultsForm(calculator.ReportString);
        resultsForm.ShowDialog();
        var fileName = calculator.WriteReportToTextFileReturnFileName(calculator.ReportString);
        MessageBox.Show($"Tipout Report has been written to {fileName}.", "s u c c e s s !");
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        int randomNumber = random.Next(1, 11);
        //int randomInterval = random.Next(50, 2000);
        if (randomNumber == 1)
        {
            TomFlashForm flashForm = new TomFlashForm();
            flashForm.ShowInTaskbar = false;
            flashForm.StartPosition = FormStartPosition.CenterScreen;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            //timer.Interval = randomInterval; 
            timer.Interval = 100;
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                flashForm.Close();
            };
            timer.Start();

            flashForm.ShowDialog();
        }
    }

}
