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
        public ResultsForm(string resultsText)
        {
            InitializeComponent();
            resultsTextBox = new TextBox();
            resultsTextBox.Multiline = true;
            resultsTextBox.ScrollBars = ScrollBars.Vertical;
            resultsTextBox.ReadOnly = true;
            resultsTextBox.Dock = DockStyle.Fill;
            this.Controls.Add(resultsTextBox);

            resultsTextBox.Text = resultsText;
            resultsTextBox.BringToFront();

        }
    }
}
