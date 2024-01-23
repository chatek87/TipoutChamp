namespace TipoutChampUI;

public partial class EmployeeEntryForm : Form
{

    public EmployeeEntryForm()
    {
        InitializeComponent();

        // Wire up the ClearFields method to the btn_clearFields click event
        //this.btn_clearFields.Click += new EventHandler(this.ClearFields);
    }

    private void cmbEmployeeRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Clear existing dynamic controls
        ClearDynamicControls();

        // Determine the selected role
        if (cmbEmployeeRole.SelectedItem != null) 
        {
            string selectedRole = cmbEmployeeRole.SelectedItem.ToString();

            // Calculate the starting point based on the location of cmbEmployeeRole
            Point startingPoint = new Point(cmbEmployeeRole.Location.X, cmbEmployeeRole.Location.Y + cmbEmployeeRole.Height + 10);

            // Create and add controls based on the selected role
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

        foreach (string label in labels)
        {
            // Create and configure the label
            Label newLabel = new Label
            {
                Text = label,
                Location = new Point(currentLocation.X, currentLocation.Y),
                Tag = "DynamicControl" // Use this tag to identify dynamic controls
            };
            this.Controls.Add(newLabel);

            // Update location for the textbox
            currentLocation.Y += 20; // Adjust spacing as needed

            // Create and configure the textbox
            TextBox newTextBox = new TextBox
            {
                Location = new Point(currentLocation.X, currentLocation.Y),
                Tag = "DynamicControl" // Use this tag to identify dynamic controls
            };
            this.Controls.Add(newTextBox);

            // Update the location for the next control
            currentLocation.Y += 40; // Adjust spacing as needed
        }
    }

    private void ClearDynamicControls()
    {
        // Create a list to store controls that will be removed
        List<Control> controlsToRemove = new List<Control>();

        // Find all dynamic controls by checking a specific property, such as Tag
        foreach (Control control in this.Controls)
        {
            if (control.Tag != null && control.Tag.ToString() == "DynamicControl")
            {
                controlsToRemove.Add(control);
            }
        }

        // Remove and dispose of the dynamic controls
        foreach (Control control in controlsToRemove)
        {
            this.Controls.Remove(control);
            control.Dispose();
        }
    }

    private void button2_Click(object sender, EventArgs e)
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

    //private void ClearFields(object sender, EventArgs e)
    //{
    //    foreach (Control control in this.Controls)
    //    {
    //        if (control is TextBox)
    //        {
    //            control.Text = string.Empty;
    //        }
    //    }

    //    //btn_clearFields
    //    cmbEmployeeRole.SelectedIndex = -1;
    //}
    // ... other methods and events ...
}
