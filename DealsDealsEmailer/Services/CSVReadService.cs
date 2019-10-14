using DealsDealsEmailer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace DealsDealsEmailer.Services
{
    public class CSVReadService
    {
        public List<InvoiceEmail> ReadCSV(string _filepath)
        {
            List<InvoiceEmail> invoiceEmails = new List<InvoiceEmail>();
            
            using (TextFieldParser parser = new TextFieldParser(_filepath))
            {
                parser.Delimiters = new string[] { "," };
                var fields = parser.ReadFields();
                var headers = fields;
                fields = parser.ReadFields();
                while (fields!=null)
                {   
                    
                    if (fields != null)
                    {
                        InvoiceEmail invoiceEmail=new InvoiceEmail();
                        try
                        {
                            invoiceEmail = GetRow(headers.ToList(), fields.ToList());
                            invoiceEmails.Add(invoiceEmail);
                        }
                        catch (Exception)
                        {

                        }
                        
                    }
                    fields = parser.ReadFields();
                }
            }
            invoiceEmails = invoiceEmails.Where(a => a.Email.Contains("@")).ToList();
            return invoiceEmails;
        }
        public InvoiceEmail GetRow(List<string> headers, List<string> fields)
        {
            InvoiceEmail invoiceEmail = new InvoiceEmail();
            for (int i = 0; i<fields.Count;i++)
            {
                switch (headers.ElementAt(i).ToLower())
                {
                    case ("buyer email"):
                        invoiceEmail.Email = fields.ElementAt(i);
                        break;
                    case "buyer name":
                        invoiceEmail.BuyerAddress.FullName = fields.ElementAt(i);
                        invoiceEmail.Name = fields.ElementAt(i);
                        break;
                    case "buyer address 1":
                        invoiceEmail.BuyerAddress.Address1 = fields.ElementAt(i);
                        break;
                    case "buyer address 2":
                        invoiceEmail.BuyerAddress.Address2 = fields.ElementAt(i);
                        break;
                    case "buyer city":
                        invoiceEmail.BuyerAddress.Town = fields.ElementAt(i);
                        break;
                    case "buyer county":
                        invoiceEmail.BuyerAddress.County = fields.ElementAt(i);
                        break;
                    case "buyer postcode":
                        invoiceEmail.BuyerAddress.PostCode = fields.ElementAt(i);
                        break;
                    case "buyer country":
                        invoiceEmail.BuyerAddress.Country = fields.ElementAt(i);
                        break;



                    case "post to name":
                        invoiceEmail.PosttoAddress.FullName = fields.ElementAt(i);
                        break;
                    case "post to address 1":
                        invoiceEmail.PosttoAddress.Address1 = fields.ElementAt(i);
                        break;
                    case "post to address 2":
                        invoiceEmail.PosttoAddress.Address2 = fields.ElementAt(i);
                        break;
                    case "post to city":
                        invoiceEmail.PosttoAddress.Town = fields.ElementAt(i);
                        break;
                    case "post to county":
                        invoiceEmail.PosttoAddress.County = fields.ElementAt(i);
                        break;
                    case "post to postcode":
                        invoiceEmail.PosttoAddress.PostCode = fields.ElementAt(i);
                        break;
                    case "post to country":
                        invoiceEmail.PosttoAddress.Country = fields.ElementAt(i);
                        break;


                    case "sales record number":
                        invoiceEmail.Sale.SalesRecordNumber = fields.ElementAt(i);
                        break;
                    //showing sale date on paid on date
                    case "sale date":
                        invoiceEmail.Sale.PaidOnDate = fields.ElementAt(i);
                        break;
                    case "item title":
                        invoiceEmail.Sale.ItemTitle = fields.ElementAt(i);
                        break;
                    case "quantity":
                        invoiceEmail.Sale.Quantity = fields.ElementAt(i);
                        break;
                    case "sold for":
                        invoiceEmail.Sale.SalePrice = fields.ElementAt(i);
                        break;
                    case "custom label":
                        invoiceEmail.Sale.CustomLabel = GetStringorNumber(fields.ElementAt(i));
                        break;
                    case "postage and packaging":
                        invoiceEmail.Sale.PostageAndPackaging = fields.ElementAt(i);
                        break;
                    case "total price":
                        invoiceEmail.Sale.TotalPrice = fields.ElementAt(i);
                        break;
                    case "delivery service":
                        invoiceEmail.Sale.DeliveryService = fields.ElementAt(i);
                        break;
                    case "payment method":
                        invoiceEmail.Sale.PaymentMethod = fields.ElementAt(i);
                        break;
                    case "transaction id":
                        invoiceEmail.TransactionId = GetStringorNumber(fields.ElementAt(i));
                        break;
                    case "user id":
                        invoiceEmail.UserId = fields.ElementAt(i);
                        break;
                    case "item number":
                        invoiceEmail.ItemNumber = GetStringorNumber(fields.ElementAt(i));
                        break;

                    default:
                        break;
                }
            }
            return invoiceEmail;
        }
        private string GetStringorNumber(string value)
        {
            try
            {
                string oval = decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture).ToString();
                return oval;
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
