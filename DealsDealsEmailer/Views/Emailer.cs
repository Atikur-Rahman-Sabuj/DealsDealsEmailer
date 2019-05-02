using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DealsDealsEmailer.Models;
using DealsDealsEmailer.Services;
using Microsoft.VisualBasic.FileIO;

namespace DealsDealsEmailer.Views
{
    public partial class Emailer : Form
    {
        string FilePath;
        string FileName;
        bool IsSell;
        List<InvoiceEmail> InvoiceEmail;
        public Emailer()
        {
            InitializeComponent();
            dgvCustomers.AutoGenerateColumns = false;
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

        private void btnSelectCsv_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                FilePath = openFileDialog1.FileName;
                FileName = openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.Length - 4);
                InvoiceEmail = new CSVReadService().ReadCSV(FilePath);
                

                //Emails = new EmailDataAccess().GenerateEmails(Employees, EmailSubject, "jill.thurston@semcon.com", HolidayMood, LastDate);
                Utilities.BindListToGridView(InvoiceEmail, dgvCustomers);
                for (int i = 0; i < InvoiceEmail.Count; i++)
                {
                    dgvCustomers.Rows[i].Cells[0].Value = i + 1;
                }
               // ShowHideControls(true);
                lblFileName.Text = FileName + ".csv";
               // tbxEmail.Text = "";
                //tbxSubject.Text = "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                dgvCustomers.Visible = false;
               // btnSendEmails.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(0))
            {
                IsSell = true;
                
            }
            else
            {
                IsSell = false;
            }
        }
    }
}
