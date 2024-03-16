using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipoutChampWinFormsUI
{
    public partial class ResultsForm : Form
    {
        private TextBox resultsTextBox;
        private string reportText;

        public ResultsForm(string reportString)
        {
            reportText = reportString;
            InitializeComponent();
            resultsTextBox = new TextBox();
            resultsTextBox.Multiline = true;
            resultsTextBox.ScrollBars = ScrollBars.Vertical;
            resultsTextBox.ReadOnly = true;
            resultsTextBox.Dock = DockStyle.Fill;
            this.Controls.Add(resultsTextBox);

            resultsTextBox.Text = reportText;
            resultsTextBox.SelectionStart = resultsTextBox.Text.Length;
            resultsTextBox.SelectionLength = 0;
            //resultsTextBox.BringToFront();

            this.Width = 450;
            this.Height = 1000;
        }

        private void ResultsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
