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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormWin));
            dataGridView = new DataGridView();
            btnAddBartender = new Button();
            btnAddServer = new Button();
            btnAddSupport = new Button();
            btnCalculate = new Button();
            btnPrintTest = new Button();
            btnAddCellarEvent = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(17, 85);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(776, 413);
            dataGridView.TabIndex = 0;
            // 
            // btnAddBartender
            // 
            btnAddBartender.Font = new Font("Segoe UI", 7F);
            btnAddBartender.Location = new Point(17, 35);
            btnAddBartender.Name = "btnAddBartender";
            btnAddBartender.Size = new Size(97, 44);
            btnAddBartender.TabIndex = 3;
            btnAddBartender.Text = "add bartender";
            btnAddBartender.UseVisualStyleBackColor = true;
            btnAddBartender.Click += btnAddBartender_Click;
            // 
            // btnAddServer
            // 
            btnAddServer.Font = new Font("Segoe UI", 7F);
            btnAddServer.Location = new Point(120, 35);
            btnAddServer.Name = "btnAddServer";
            btnAddServer.Size = new Size(97, 44);
            btnAddServer.TabIndex = 3;
            btnAddServer.Text = "add server";
            btnAddServer.UseVisualStyleBackColor = true;
            btnAddServer.Click += btnAddServer_Click;
            // 
            // btnAddSupport
            // 
            btnAddSupport.Font = new Font("Segoe UI", 7F);
            btnAddSupport.Location = new Point(223, 35);
            btnAddSupport.Name = "btnAddSupport";
            btnAddSupport.Size = new Size(97, 44);
            btnAddSupport.TabIndex = 3;
            btnAddSupport.Text = "add support";
            btnAddSupport.UseVisualStyleBackColor = true;
            btnAddSupport.Click += btnAddSupport_Click;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(699, 35);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(94, 42);
            btnCalculate.TabIndex = 4;
            btnCalculate.Text = "calculate!";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // btnPrintTest
            // 
            btnPrintTest.Location = new Point(498, 42);
            btnPrintTest.Name = "btnPrintTest";
            btnPrintTest.Size = new Size(94, 29);
            btnPrintTest.TabIndex = 5;
            btnPrintTest.Text = "print test";
            btnPrintTest.UseVisualStyleBackColor = true;
            btnPrintTest.Click += btnPrintTest_Click;
            // 
            // btnAddCellarEvent
            // 
            btnAddCellarEvent.Font = new Font("Segoe UI", 7F);
            btnAddCellarEvent.Location = new Point(326, 35);
            btnAddCellarEvent.Name = "btnAddCellarEvent";
            btnAddCellarEvent.Size = new Size(97, 44);
            btnAddCellarEvent.TabIndex = 3;
            btnAddCellarEvent.Text = "add cellar event";
            btnAddCellarEvent.UseVisualStyleBackColor = true;
            btnAddCellarEvent.Click += btnAddCellarEvent_Click;
            // 
            // MainFormWin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 526);
            Controls.Add(btnPrintTest);
            Controls.Add(btnCalculate);
            Controls.Add(btnAddCellarEvent);
            Controls.Add(btnAddSupport);
            Controls.Add(btnAddServer);
            Controls.Add(btnAddBartender);
            Controls.Add(dataGridView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainFormWin";
            StartPosition = FormStartPosition.CenterParent;
            Tag = "";
            Text = "t i p o u t  c h a m p";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btnAddBartender;
        private Button btnAddServer;
        private Button btnAddSupport;
        private Button btnCalculate;
        private Button btnPrintTest;
        private Button btnAddCellarEvent;
    }
}
