namespace TipoutChampWinFormsUI
{
    partial class MainFormWin
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
            dataGridView = new DataGridView();
            btnAddEmployee = new Button();
            btnDeleteEmployee = new Button();
            btnAddBartender = new Button();
            btnAddServer = new Button();
            btnAddSupport = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(89, 109);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(776, 426);
            dataGridView.TabIndex = 0;
            // 
            // btnAddEmployee
            // 
            btnAddEmployee.Location = new Point(89, 35);
            btnAddEmployee.Name = "btnAddEmployee";
            btnAddEmployee.Size = new Size(87, 55);
            btnAddEmployee.TabIndex = 1;
            btnAddEmployee.Text = "add employee";
            btnAddEmployee.UseVisualStyleBackColor = true;
            btnAddEmployee.Click += btnAddEmployee_Click;
            // 
            // btnDeleteEmployee
            // 
            btnDeleteEmployee.Location = new Point(783, 48);
            btnDeleteEmployee.Name = "btnDeleteEmployee";
            btnDeleteEmployee.Size = new Size(82, 55);
            btnDeleteEmployee.TabIndex = 2;
            btnDeleteEmployee.Text = "delete employee";
            btnDeleteEmployee.UseVisualStyleBackColor = true;
            btnDeleteEmployee.Click += btnDeleteEmployee_Click;
            // 
            // btnAddBartender
            // 
            btnAddBartender.Location = new Point(198, 35);
            btnAddBartender.Name = "btnAddBartender";
            btnAddBartender.Size = new Size(82, 55);
            btnAddBartender.TabIndex = 3;
            btnAddBartender.Text = "add bartender";
            btnAddBartender.UseVisualStyleBackColor = true;
            // 
            // btnAddServer
            // 
            btnAddServer.Location = new Point(296, 35);
            btnAddServer.Name = "btnAddServer";
            btnAddServer.Size = new Size(82, 55);
            btnAddServer.TabIndex = 3;
            btnAddServer.Text = "add server";
            btnAddServer.UseVisualStyleBackColor = true;
            // 
            // btnAddSupport
            // 
            btnAddSupport.Location = new Point(384, 35);
            btnAddSupport.Name = "btnAddSupport";
            btnAddSupport.Size = new Size(82, 55);
            btnAddSupport.TabIndex = 3;
            btnAddSupport.Text = "add support";
            btnAddSupport.UseVisualStyleBackColor = true;
            // 
            // MainFormWin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 673);
            Controls.Add(btnAddSupport);
            Controls.Add(btnAddServer);
            Controls.Add(btnAddBartender);
            Controls.Add(btnDeleteEmployee);
            Controls.Add(btnAddEmployee);
            Controls.Add(dataGridView);
            Name = "MainFormWin";
            StartPosition = FormStartPosition.CenterParent;
            Tag = "";
            Text = "t i p o u t  c h a m p";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btnAddEmployee;
        private Button btnDeleteEmployee;
        private Button btnAddBartender;
        private Button btnAddServer;
        private Button btnAddSupport;
    }
}
