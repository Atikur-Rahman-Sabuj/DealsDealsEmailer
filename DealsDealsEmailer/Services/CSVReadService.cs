using DealsDealsEmailer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

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
                var line = parser.ReadLine();
                while (line!=null)
                {   
                    fields = parser.ReadFields();
                    if (fields != null)
                    {
                        InvoiceEmail invoiceEmail = GetRow(headers.ToList(), fields.ToList());
                        invoiceEmails.Add(invoiceEmail);
                    }
                    line = parser.ReadLine();
                }
            }

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
                        invoiceEmail.Sale.CustomLabel = fields.ElementAt(i);
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

                    default:
                        break;
                }
            }
            return invoiceEmail;
        }
    }
}
