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
        // Create the combobox column
        DataGridViewComboBoxColumn roleColumn = new DataGridViewComboBoxColumn();
        roleColumn.Name = "Role";
        roleColumn.HeaderText = "Role";
        roleColumn.DataPropertyName = "Role"; // This should match the name of the property in your Employee class
        roleColumn.DataSource = Enum.GetValues(typeof(Roles)); // Set the enum as the data source
        roleColumn.ValueType = typeof(Roles);

        // Add the combobox column to the DataGridView
        dataGridView.Columns.Add(roleColumn);

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

            // Set the background color of read-only cells to gray
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
        // Check if the error is due to a formatting or parsing issue
        if (e.Context == DataGridViewDataErrorContexts.Formatting ||
            e.Context == DataGridViewDataErrorContexts.Parsing)
        {
            // Get the column and row index of the offending cell
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            // Check if the column corresponds to a decimal field
            string columnName = dataGridView.Columns[columnIndex].Name;
            if (columnName == "Sales" || columnName == "ChargedTips" || columnName == "HoursWorked")
            {
                // Reset the value to 0
                dataGridView.Rows[rowIndex].Cells[columnIndex].Value = 0m;

                // Set the error text to empty to avoid the default error dialog
                dataGridView.Rows[rowIndex].ErrorText = string.Empty;

                // Cancel the default error dialog
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




}