using DealsDealsEmailer.Models;
using DealsDealsEmailer.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DealsDealsEmailer.Views
{
    public partial class Emailer : Form
    {
        string FilePath;
        string FileName;
        bool IsSell;
        string SellSubject;
        string FeedbackSubject;
        List<InvoiceEmail> InvoiceEmails;
        public Emailer()
        {
            InitializeComponent();
            SellSubject = "Invoice for your order";
            FeedbackSubject = "Plese leave feedback for your order";
            IsSell = true;
            ShowHideControls(false);
            btnSelectCsv.Hide();
            dgvCustomers.AutoGenerateColumns = false;
        }



        private void btnSelectCsv_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                FilePath = openFileDialog1.FileName;
                FileName = openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.Length - 4);
                InvoiceEmails = new CSVReadService().ReadCSV(FilePath);
                

                //Emails = new EmailDataAccess().GenerateEmails(Employees, EmailSubject, "jill.thurston@semcon.com", HolidayMood, LastDate);
                Utilities.BindListToGridView(InvoiceEmails, dgvCustomers);
                ShowHideControls(true);
                for (int i = 0; i < InvoiceEmails.Count; i++)
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
               // dgvCustomers.Visible = false;
               // btnSendEmails.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex.Equals(0))
            {
                IsSell = true;
                btnSelectCsv.Show();
                
            }
            else
            {
                IsSell = false;
                btnSelectCsv.Show();
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            if (IsSell)
            {
                new EmailerService().SendSellEmails(InvoiceEmails, dgvCustomers, "");
            }
            else
            {
                new EmailerService().SendFeedbackEmail(InvoiceEmails, dgvCustomers, "");
            }
        }
        private void ShowHideControls(bool IsVisible)
        {
            lblFileName.Visible = IsVisible;
            btnReset.Visible = IsVisible;
            btnSendMail.Visible = IsVisible;
            dgvCustomers.Visible = IsVisible;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            InvoiceEmails.Clear();
            ShowHideControls(false);
        }
    }
}
