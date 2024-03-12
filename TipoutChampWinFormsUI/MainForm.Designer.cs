namespace TipoutChampWinFormsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView = new DataGridView();
            btnAddBartender = new Button();
            btnAddServer = new Button();
            btnAddSupport = new Button();
            btnGenerateReport = new Button();
            btnAddCellarEvent = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(17, 69);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(873, 348);
            dataGridView.TabIndex = 0;
            // 
            // btnAddBartender
            // 
            btnAddBartender.BackColor = SystemColors.ControlLightLight;
            btnAddBartender.Font = new Font("Segoe UI", 7F);
            btnAddBartender.Location = new Point(17, 19);
            btnAddBartender.Name = "btnAddBartender";
            btnAddBartender.Size = new Size(97, 44);
            btnAddBartender.TabIndex = 3;
            btnAddBartender.Text = "add bartender";
            btnAddBartender.UseVisualStyleBackColor = false;
            btnAddBartender.Click += btnAddBartender_Click;
            // 
            // btnAddServer
            // 
            btnAddServer.BackColor = SystemColors.ControlLightLight;
            btnAddServer.Font = new Font("Segoe UI", 7F);
            btnAddServer.Location = new Point(120, 19);
            btnAddServer.Name = "btnAddServer";
            btnAddServer.Size = new Size(97, 44);
            btnAddServer.TabIndex = 3;
            btnAddServer.Text = "add server";
            btnAddServer.UseVisualStyleBackColor = false;
            btnAddServer.Click += btnAddServer_Click;
            // 
            // btnAddSupport
            // 
            btnAddSupport.BackColor = SystemColors.ControlLightLight;
            btnAddSupport.Font = new Font("Segoe UI", 7F);
            btnAddSupport.Location = new Point(223, 19);
            btnAddSupport.Name = "btnAddSupport";
            btnAddSupport.Size = new Size(97, 44);
            btnAddSupport.TabIndex = 3;
            btnAddSupport.Text = "add support";
            btnAddSupport.UseVisualStyleBackColor = false;
            btnAddSupport.Click += btnAddSupport_Click;
            // 
            // btnGenerateReport
            // 
            btnGenerateReport.BackColor = SystemColors.ControlLightLight;
            btnGenerateReport.Font = new Font("Segoe UI", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGenerateReport.ForeColor = SystemColors.ControlText;
            btnGenerateReport.Location = new Point(742, 19);
            btnGenerateReport.Name = "btnGenerateReport";
            btnGenerateReport.Size = new Size(148, 44);
            btnGenerateReport.TabIndex = 5;
            btnGenerateReport.Text = "generate report";
            btnGenerateReport.UseVisualStyleBackColor = false;
            btnGenerateReport.Click += btnGenerateReport_Click;
            // 
            // btnAddCellarEvent
            // 
            btnAddCellarEvent.BackColor = SystemColors.ControlLightLight;
            btnAddCellarEvent.Font = new Font("Segoe UI", 7F);
            btnAddCellarEvent.Location = new Point(326, 19);
            btnAddCellarEvent.Name = "btnAddCellarEvent";
            btnAddCellarEvent.Size = new Size(97, 44);
            btnAddCellarEvent.TabIndex = 3;
            btnAddCellarEvent.Text = "add cellar event";
            btnAddCellarEvent.UseVisualStyleBackColor = false;
            btnAddCellarEvent.Click += btnAddCellarEvent_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(907, 428);
            Controls.Add(btnGenerateReport);
            Controls.Add(btnAddCellarEvent);
            Controls.Add(btnAddSupport);
            Controls.Add(btnAddServer);
            Controls.Add(btnAddBartender);
            Controls.Add(dataGridView);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Tag = "";
            Text = "  t i p o u t  c h a m p   ";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView;
        private Button btnAddBartender;
        private Button btnAddServer;
        private Button btnAddSupport;
        private Button btnGenerateReport;
        private Button btnAddCellarEvent;
    }
}
