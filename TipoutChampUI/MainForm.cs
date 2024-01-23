using System.Windows.Forms;
using TipoutChamp;

namespace TipoutChampUI;

public partial class MainForm : Form
{
    private RosterModel _rosterModel;
    public MainForm()
    {
        InitializeComponent();
        _rosterModel = new RosterModel();

        dataGridView.DataSource = _rosterModel.Employees;
        dataGridView.AutoGenerateColumns = true;
    }

    private void cmbEmployeeRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearDynamicControls();

        if (cmbEmployeeRole.SelectedItem != null)
        {
            string selectedRole = cmbEmployeeRole.SelectedItem.ToString();

            Point startingPoint = new Point(cmbEmployeeRole.Location.X, cmbEmployeeRole.Location.Y + cmbEmployeeRole.Height + 10);

            if (selectedRole == "Bartender")
            {
                AddDynamicControls(new string[] { "hours worked", "charged tips" }, startingPoint);
            }
            else if (selectedRole == "Server")
            {
                AddDynamicControls(new string[] { "charged tips" }, startingPoint);
            }
            else if (selectedRole == "Support")
            {
                AddDynamicControls(new string[] { "hours worked" }, startingPoint);
            }
        }
    }

    private void AddDynamicControls(string[] labels, Point startingPoint)
    {
        Point currentLocation = startingPoint;
        int i = 2;
        foreach (string label in labels)
        {
            Label newLabel = new Label
            {
                Text = label,
                Location = new Point(currentLocation.X, currentLocation.Y),
                Tag = "DynamicControl" // Use this tag to identify dynamic controls
            };
            this.Controls.Add(newLabel);

            currentLocation.Y += 20; // Adjust spacing as needed

            TextBox newTextBox = new TextBox
            {
                Name = $"txt{label.Trim(' ')}",
                Location = new Point(currentLocation.X, currentLocation.Y),
                Tag = "DynamicControl",
                TabIndex = i
            };
            this.Controls.Add(newTextBox);

            currentLocation.Y += 40; // Adjust spacing as needed
            i++;
        }
    }

    private void ClearDynamicControls()
    {
        List<Control> controlsToRemove = new List<Control>();

        foreach (Control control in this.Controls)
        {
            if (control.Tag != null && control.Tag.ToString() == "DynamicControl")
            {
                controlsToRemove.Add(control);
            }
        }

        foreach (Control control in controlsToRemove)
        {
            this.Controls.Remove(control);
            control.Dispose();
        }
    }

    private void ClearFields()
    {
        foreach (Control control in this.Controls)
        {
            if (control is TextBox)
            {
                control.Text = string.Empty;
            }
        }

        cmbEmployeeRole.SelectedIndex = -1;
    }

    // this is the 'clear fields' button
    private void button2_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    // this is the 'add to roster' button
    private void button1_Click(object sender, EventArgs e)
    {
        if (cmbEmployeeRole.SelectedItem != null)
        {
            string selectedRole = cmbEmployeeRole.SelectedItem.ToString();
            string employeeName = txtEmployeeName.Text; // Replace with the actual name of your TextBox for employee name

            // Initialize variables to store the parsed values
            decimal hoursWorked = 0m;
            decimal chargedTips = 0m;

            // Find and parse the Hours Worked TextBox, if it exists
            TextBox txtHoursWorked = this.Controls.Find("txthoursworked", true).FirstOrDefault() as TextBox;
            if (txtHoursWorked != null && !string.IsNullOrWhiteSpace(txtHoursWorked.Text))
            {
                decimal.TryParse(txtHoursWorked.Text, out hoursWorked);
            }

            // Find and parse the Charged Tips TextBox, if it exists
            TextBox txtChargedTips = this.Controls.Find("txtchargedtips", true).FirstOrDefault() as TextBox;
            if (txtChargedTips != null && !string.IsNullOrWhiteSpace(txtChargedTips.Text))
            {
                decimal.TryParse(txtChargedTips.Text, out chargedTips);
            }

            // Create an Employee object based on the selected role
            Employee newEmployee = null;
            switch (selectedRole)
            {
                case "Bartender":
                    newEmployee = new Bartender
                    {
                        Name = employeeName,
                        HoursWorked = hoursWorked,
                        ChargedTips = chargedTips
                    };
                    break;
                case "Server":
                    newEmployee = new Server
                    {
                        Name = employeeName,
                        ChargedTips = chargedTips
                    };
                    break;
                case "Support":
                    newEmployee = new Support
                    {
                        Name = employeeName,
                        HoursWorked = hoursWorked
                    };
                    break;
            }

            // Add the new Employee to the RosterModel's employees list
            if (newEmployee != null)
            {
                _rosterModel.Employees.Add(newEmployee);
            }
        }
        dataGridView.Refresh();
        ClearFields();
    }

}
