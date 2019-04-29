using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DealsDealsEmailer.Services;
using Microsoft.VisualBasic.FileIO;

namespace DealsDealsEmailer.Views
{
    public partial class Emailer : Form
    {
        string FilePath;
        public Emailer()
        {
            InitializeComponent();
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;
            new CSVReadService().ReadCSV(FilePath);
            //using (TextFieldParser parser = new TextFieldParser(FilePath))
            //{
            //    parser.Delimiters = new string[] { "," };
            //    while (true)
            //    {
            //        var fields = parser.ReadFields();
            //        var line = parser.ReadLine();
            //    }
            //}
        }
    }
}
