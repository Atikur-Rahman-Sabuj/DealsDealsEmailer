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
            invoiceEmails = invoiceEmails.Where(a => a.Email.Substring(a.Email.Length - 3) == "com").ToList();
            return invoiceEmails;
        }
        public InvoiceEmail GetRow(List<string> headers, List<string> fields)
        {
            InvoiceEmail invoiceEmail = new InvoiceEmail();
            for (int i = 0; i<fields.Count;i++)
            {
                switch (headers.ElementAt(i))
                {
                    case "Buyer Email":
                        invoiceEmail.Email = fields.ElementAt(i);
                        break;
                    case "Buyer Full name":
                        invoiceEmail.Address.FullName = fields.ElementAt(i);
                        invoiceEmail.Name = fields.ElementAt(i);
                        break;
                    case "Buyer Address 1":
                        invoiceEmail.Address.Address1 = fields.ElementAt(i);
                        break;
                    case "Buyer Address 2":
                        invoiceEmail.Address.Address2 = fields.ElementAt(i);
                        break;
                    case "Buyer Town/City":
                        invoiceEmail.Address.Town = fields.ElementAt(i);
                        break;
                    case "Buyer County":
                        invoiceEmail.Address.County = fields.ElementAt(i);
                        break;
                    case "Buyer Postcode":
                        invoiceEmail.Address.PostCode = fields.ElementAt(i);
                        break;
                    case "Buyer Country":
                        invoiceEmail.Address.Country = fields.ElementAt(i);
                        break;
                    case "Sales Record Number":
                        invoiceEmail.Sale.SalesRecordNumber = fields.ElementAt(i);
                        break;
                    case "Paid on Date":
                        invoiceEmail.Sale.PaidOnDate = fields.ElementAt(i);
                        break;
                    case "Item Title":
                        invoiceEmail.Sale.ItemTitle = fields.ElementAt(i);
                        break;
                    case "Quantity":
                        invoiceEmail.Sale.Quantity = fields.ElementAt(i);
                        break;
                    case "Sale Price":
                        invoiceEmail.Sale.SalePrice = fields.ElementAt(i);
                        break;
                    case "Custom Label":
                        invoiceEmail.Sale.CustomLabel = GetStringorNumber(fields.ElementAt(i));
                        break;
                    case "Postage and Packaging":
                        invoiceEmail.Sale.PostageAndPackaging = fields.ElementAt(i);
                        break;
                    case "Total Price":
                        invoiceEmail.Sale.TotalPrice = fields.ElementAt(i);
                        break;
                    case "Delivery Service":
                        invoiceEmail.Sale.DeliveryService = fields.ElementAt(i);
                        break;
                    case "Payment Method":
                        invoiceEmail.Sale.PaymentMethod = fields.ElementAt(i);
                        break;
                    case "Transaction ID":
                        invoiceEmail.TransactionId = decimal.Parse( fields.ElementAt(i), NumberStyles.Any, CultureInfo.InvariantCulture).ToString();
                        break;
                    case "User Id":
                        invoiceEmail.UserId = fields.ElementAt(i);
                        break;
                    case "Item Number":
                        invoiceEmail.ItemNumber = decimal.Parse( fields.ElementAt(i), NumberStyles.Any, CultureInfo.InvariantCulture).ToString();
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
