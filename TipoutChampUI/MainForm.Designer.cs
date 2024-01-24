namespace TipoutChampUI
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            cmbEmployeeRole = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            txtEmployeeName = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label3 = new Label();
            lblRoster = new Label();
            rosterModelBindingSource = new BindingSource(components);
            dataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)rosterModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // cmbEmployeeRole
            // 
            cmbEmployeeRole.FormattingEnabled = true;
            cmbEmployeeRole.Items.AddRange(new object[] { "Bartender", "Server", "Support" });
            cmbEmployeeRole.Location = new Point(84, 99);
            cmbEmployeeRole.Name = "cmbEmployeeRole";
            cmbEmployeeRole.Size = new Size(151, 28);
            cmbEmployeeRole.TabIndex = 1;
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
            txtEmployeeName.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(281, 98);
            button1.Name = "button1";
            button1.Size = new Size(106, 29);
            button1.TabIndex = 8;
            button1.Text = "add to roster";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(281, 50);
            button2.Name = "button2";
            button2.Size = new Size(106, 29);
            button2.TabIndex = 7;
            button2.Text = "clear fields";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(104, 28);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 9;
            label3.Text = "employee entry";
            // 
            // lblRoster
            // 
            lblRoster.AutoSize = true;
            lblRoster.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblRoster.Location = new Point(594, 43);
            lblRoster.Name = "lblRoster";
            lblRoster.Size = new Size(62, 20);
            lblRoster.TabIndex = 10;
            lblRoster.Text = "ROSTER";
            // 
            // rosterModelBindingSource
            // 
            rosterModelBindingSource.DataSource = typeof(TipoutChamp.RosterModel);
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToOrderColumns = true;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.DataSource = rosterModelBindingSource;
            dataGridView.Location = new Point(424, 66);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(428, 309);
            dataGridView.TabIndex = 11;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 471);
            Controls.Add(dataGridView);
            Controls.Add(lblRoster);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtEmployeeName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbEmployeeRole);
            Name = "MainForm";
            Text = "t i p o u t   c h a m p ";
            ((System.ComponentModel.ISupportInitialize)rosterModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
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
        private Label label3;
        private Label lblRoster;
        private BindingSource rosterModelBindingSource;
        private DataGridView dataGridView;
    }
}
