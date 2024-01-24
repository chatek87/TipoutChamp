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
            btnAddEmployee.Location = new Point(121, 33);
            btnAddEmployee.Name = "btnAddEmployee";
            btnAddEmployee.Size = new Size(80, 55);
            btnAddEmployee.TabIndex = 1;
            btnAddEmployee.Text = "add employee";
            btnAddEmployee.UseVisualStyleBackColor = true;
            btnAddEmployee.Click += btnAddEmployee_Click;
            // 
            // btnDeleteEmployee
            // 
            btnDeleteEmployee.Location = new Point(768, 33);
            btnDeleteEmployee.Name = "btnDeleteEmployee";
            btnDeleteEmployee.Size = new Size(82, 55);
            btnDeleteEmployee.TabIndex = 2;
            btnDeleteEmployee.Text = "delete employee";
            btnDeleteEmployee.UseVisualStyleBackColor = true;
            btnDeleteEmployee.Click += btnDeleteEmployee_Click;
            // 
            // MainFormWin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 673);
            Controls.Add(btnDeleteEmployee);
            Controls.Add(btnAddEmployee);
            Controls.Add(dataGridView);
            Name = "MainFormWin";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btnAddEmployee;
        private Button btnDeleteEmployee;
    }
}
