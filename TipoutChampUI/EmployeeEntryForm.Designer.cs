namespace TipoutChampUI
{
    partial class EmployeeEntryForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbEmployeeRole = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            txtEmployeeName = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // cmbEmployeeRole
            // 
            cmbEmployeeRole.FormattingEnabled = true;
            cmbEmployeeRole.Items.AddRange(new object[] { "Bartender", "Server", "Support" });
            cmbEmployeeRole.Location = new Point(84, 99);
            cmbEmployeeRole.Name = "cmbEmployeeRole";
            cmbEmployeeRole.Size = new Size(151, 28);
            cmbEmployeeRole.TabIndex = 0;
            cmbEmployeeRole.SelectedIndexChanged += cmbEmployeeRole_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 54);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 1;
            label1.Text = "name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(43, 102);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 2;
            label2.Text = "role";
            // 
            // txtEmployeeName
            // 
            txtEmployeeName.Location = new Point(84, 51);
            txtEmployeeName.Name = "txtEmployeeName";
            txtEmployeeName.Size = new Size(151, 27);
            txtEmployeeName.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(270, 51);
            button1.Name = "button1";
            button1.Size = new Size(66, 29);
            button1.TabIndex = 4;
            button1.Text = "add";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(355, 51);
            button2.Name = "button2";
            button2.Size = new Size(66, 29);
            button2.TabIndex = 4;
            button2.Text = "clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // EmployeeEntryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtEmployeeName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbEmployeeRole);
            Name = "EmployeeEntryForm";
            Text = "t i p o u t   c h a m p ";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbEmployeeRole;
        private Label label1;
        private Label label2;
        private TextBox txtEmployeeName;
        private Button button1;
        private Button button2;
    }
}
