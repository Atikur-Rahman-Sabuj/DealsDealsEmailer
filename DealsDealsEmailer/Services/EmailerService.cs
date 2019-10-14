using DealsDealsEmailer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealsDealsEmailer.Services
{
    public class EmailerService
    {
        public void SendSellEmails(List<InvoiceEmail> invoiceEmails, DataGridView dgv, String subject)
        {
            for (int i = 0; i < invoiceEmails.Count; i++)
            {
                InvoiceEmail invoiceEmail = invoiceEmails.ElementAt(i);
                string ToMail = invoiceEmail.Email;
                string FromMail = "info@dealsdeals.co.uk";
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info@dealsdeals.co.uk", "Laxmi2121");
                client.Port = 2525;
                client.Host = "mail.dealsdeals.co.uk";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                MailAddress From = new MailAddress(FromMail, "DealsDeals");
                MailAddress To = new MailAddress(ToMail, invoiceEmail.BuyerAddress.FullName);
                MailMessage mailMessage = new MailMessage(From, To);
                mailMessage.Subject = "Invoice for your recent eBay Order "+ invoiceEmail.ItemNumber;
                mailMessage.IsBodyHtml = true;
                try
                {
                    mailMessage.Body = GetSellMessageBody(invoiceEmail);
                
                    client.Send(mailMessage);
                    dgv.Rows[i].Cells["Status"].Value = "Sent";
                }
                catch (Exception ex)
                {
                    dgv.Rows[i].Cells["Status"].Value = "Not Sent";
                    SendDeveloperMail(ex.Message, i, invoiceEmail.Email);
                }
            }

        }

        private void SendDeveloperMail(string message, int index, string email)
        {
            try
            {
                string ToMail = "sabuj.probook@gmail.com";
                string FromMail = "info@dealsdeals.co.uk";
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info@dealsdeals.co.uk", "Laxmi2121");
                client.Port = 2525;
                client.Host = "mail.dealsdeals.co.uk";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                MailAddress From = new MailAddress(FromMail, "DealsDeals");
                MailAddress To = new MailAddress(ToMail, "Deals deals developer");
                MailMessage mailMessage = new MailMessage(From, To);
                mailMessage.Subject = "Exception report";
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = "To: "+email +nl(1)+ "On index: "+ index + nl(1)+"Message: "+ message;
            
                client.Send(mailMessage);
           
            }
            catch (Exception ex)
            {
            }
        }

        public void SendFeedbackEmail(List<InvoiceEmail> invoiceEmails, DataGridView dgv, string subject)
        {
            for (int i = 0; i < invoiceEmails.Count; i++)
            {
                InvoiceEmail invoiceEmail = invoiceEmails.ElementAt(i);
                string ToMail = invoiceEmail.Email;
                string FromMail = "info@dealsdeals.co.uk";
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info@dealsdeals.co.uk", "Laxmi2121");
                client.Port = 2525;
                client.Host = "mail.dealsdeals.co.uk";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                MailAddress From = new MailAddress(FromMail, "DealsDeals");
                MailAddress To = new MailAddress(ToMail, invoiceEmail.BuyerAddress.FullName);
                MailMessage mailMessage = new MailMessage(From, To);
                mailMessage.Subject = "Feedback for your recent eBay Order " + invoiceEmail.ItemNumber;
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = GetFeedbackMessageBody(invoiceEmail);
                try
                {
                    client.Send(mailMessage);
                    dgv.Rows[i].Cells["Status"].Value = "Sent";
                }
                catch (Exception ex)
                {
                    dgv.Rows[i].Cells["Status"].Value = "Not Sent";
                }
            }
        }

        private string GetFeedbackMessageBody(InvoiceEmail invoiceEmail)
        {
            string body = "Hi " + invoiceEmail.BuyerAddress.FullName + nl(2);
            body += "Thank you very much for your purchase of " + invoiceEmail.Sale.ItemTitle + nl(2);
            body += "I was just checking through my feedback page and noticed you have not yet left feedback for this purchase. If everything was satisfactory please consider leaving feedback by clicking on the link." + nl(2);
            body += "http://feedback.ebay.com/ws/eBayISAPI.dll?LeaveFeedback2&useridto="+invoiceEmail.UserId+"&item="+invoiceEmail.ItemNumber+"&transactid="+invoiceEmail.TransactionId + nl(2);
            body += "It only takes a few seconds and really helps me out.  Also you will receive a 10 % discount code from www.dealdeals.co.uk" + nl(3);
            body += "Best wishes" + nl(2);
            body += "Joshua" + nl(1);
            body += "www.dealsdeals.co.uk";


            return body;
        }
        private string nl(int lineNumber)
        {
            string line = "";
            for(int i = 0; i < lineNumber; i++)
            {
                line = line + Environment.NewLine;
            }
            return line;
        }

        private string GetSellMessageBody(InvoiceEmail invoiceEmail)
        {
            decimal saleprice = CalculateSalePrice(invoiceEmail.Sale.SalePrice);
            decimal totalPrice = saleprice * Convert.ToInt32(invoiceEmail.Sale.Quantity);
            decimal postageshipping = CalculateSalePrice(invoiceEmail.Sale.PostageAndPackaging);
            
            //decimal tax = 0;
            //if (invoiceEmail.BuyerAddress.Country.Equals("United Kingdom"))
            //{
            //    tax = (Convert.ToDecimal("20") / Convert.ToDecimal("120")) * totalPrice;
            //}
            //decimal subTotal = totalPrice - tax;

            string title = "";
            try
            {
                title = invoiceEmail.Sale.ItemTitle.Substring(0, 50);
            }
            catch (Exception)
            {
                title = invoiceEmail.Sale.ItemTitle;
            }
                
            //decimal grandTotal = CalculateSalePrice(invoiceEmail.Sale.TotalPrice);
            decimal grandTotal = CalculateSalePrice(invoiceEmail.Sale.TotalPrice);
            string currency = GetCurrency(invoiceEmail.Sale.SalePrice);
            string body = @"<div style='color:black !important; background: #ecebeb;overflow: hidden;padding: 2%;padding-left: 3px;padding-right: 3px'>
    <div style='background: white;padding: 10px;overflow: hidden;border: 1px solid #cccccc;box-shadow: 0px 3px 9px -4px black;'>
        <img style='height:100px;width: auto' src='https://docs.google.com/uc?id=1sQrh4B8D96n9DgdniuHep6tCe8wVoYHZ'/>
        <div style='float:right;text-align: right'>
            <p style='margin: 5px'>HDM Retail LTD</p>
            <p style='margin: 5px'>8 Lammas Close</p>
            <p style='margin: 5px'>Solihull</p>
            <p style='margin: 5px'>B92 8PA</p>
            <p style='margin: 5px'>VAT No: GB 232760325</p>
        </div>
    </div>
    <div style='background: white;overflow: hidden;'>
        <div style='padding:5px'>
            <div style='background: powderblue;overflow: hidden;'>
                <div style='width:60%;padding-left: 10px;padding-top: 20px;float: left;border-right: 1px dotted #6e6e6e'>
                    <p style='margin-bottom:20px;font-size: 20px'><b>Thank you for your order from DealsDeals</b></p>
                    <p >Please visit our website at <b>www.dealsdeals.co.uk</b> for more great offers</p>
                </div>
                <div style='width:30%; float:right;padding-left: 10px'>
                    <p style='margin-bottom:20px'><b>Order questions?</b></p>
                    <p><b>Call Us: </b>0800 448 8228</p>
                    <p><b>Email: </b>info@dealsdeals.co.uk</p>
                </div>
            </div>
        </div>
        <div style='text-align: center;padding:5px'>
            <h3 style='margin-top: 10px;margin-bottom: 5px'>Your Invoice #" + invoiceEmail.Sale.SalesRecordNumber + @"</h3>
            <p style='margin-top: 10px;margin-bottom: 5px'>Placed on " + invoiceEmail.Sale.PaidOnDate + @"</p>
            <p style='margin-top: 5px;margin-bottom: 5px'>Please visit our website www.dealsdeals.co.uk</p>
            <p style='margin-top: 5px;margin-bottom: 5px'>There is a 10% discount when you use voucher code Ebay10 at checkout</p>
            <div style='border:1px solid #e5e5e5'>
                <div style='background:gainsboro;overflow: hidden;padding-top: 2px;padding-bottom: 2px'>
                    <p style='padding:10px;width:40%;float: left;text-align: left;margin:0px;'><b>ITEM IN ORDER</b></p>
                    <div style='width:40%;float:right;text-align: right'>
                        <p style='padding:10px;width:25%;float:left;margin:0px'><b>QTY</b></p>
                        <p style='padding:10px;width:40%;float:right;margin:0px'><b>PRICE</b></p>
                    </div>
                </div>
                <div style='overflow: hidden;padding-top: 2px;padding-bottom: 2px'>
                    <p style='padding:10px;padding-bottom: 5px;width:50%;float: left;text-align: left;margin:0px;'><b>" + title + @"</b></p>
                    <div style='width:40%;float:right;text-align: right'>
                        <p style='padding:10px;padding-bottom: 5px;width:25%;float:left;margin:0px'>" + invoiceEmail.Sale.Quantity + @"</p>
                        <p style='padding:10px;padding-bottom: 5px;width:40%;float:right;margin:0px'>" + currency + saleprice.ToString() + @"</p>
                    </div>
                    <div style='clear:both'></div>
                    <p style='text-align:left;padding: 0px;margin:0px;padding-left: 10px'>SKU: " + invoiceEmail.Sale.CustomLabel + @"</p>
                </div>
            </div>
            <hr/>
            <div style='background: powderblue;padding:10px;text-align: left;overflow:hidden'>
				<div style='width:30%;float:right;text-align:right;padding-right:10px;overflow:hidden'>
					<p>" + currency + String.Format("{0:0.00}", totalPrice) + @"</p> 
					<p>" + currency + String.Format("{0:0.00}", postageshipping) + @"</p>   
					<p>" + currency + String.Format("{0:0.00}", grandTotal) + @"</p>
				</div>
				<div style='width:60%;float:right;text-align:right;padding-right:10px;overflow:hidden'>
					<p>Subtotal</p>   
					<p>Shipping & Handling</p>   
					<p>Grand Total</p>
				</div>
            </div>
        </div>
        <div style='padding:3%;padding-top: 5px;overflow:hidden'> 
            <div style='width:40%;float: left;'>
                <p style='margin-top:5px;margin-bottom:5px'><b>BILL TO:</b></p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.BuyerAddress.FullName + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.BuyerAddress.Address1 + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.BuyerAddress.Address2 + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.BuyerAddress.Town + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.BuyerAddress.County + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.BuyerAddress.PostCode + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.BuyerAddress.Country + @"</p>
            </div>
            <div style='width:40%;float:right'>
                <p style='margin-top:5px;margin-bottom:5px'><b>SHIP TO:</b></p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.PosttoAddress.FullName + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.PosttoAddress.Address1 + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.PosttoAddress.Address2 + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.PosttoAddress.Town + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.PosttoAddress.County + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'> " + invoiceEmail.PosttoAddress.PostCode + @"</p>
                <p style='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.PosttoAddress.Country + @"</p>
            </div>
        </div>
        <div style='padding-left:20px;padding-right:20px'> 
            <div style='width:40%;float: left;'>
                <p style='margin-top:5px;margin-bottom:5px'><b>SHIPPING METHOD:</b></p>
                <pstyle='margin-top:3px;margin-bottom:0px'>Select shipping method: " + invoiceEmail.Sale.DeliveryService + @"</p>
            </div>
            <div style='width:40%;float:right'>
                <p style='margin-top:5px;margin-bottom:5px'><b>PAYMENT METHOD:</b></p>
                <pstyle='margin-top:3px;margin-bottom:0px'>" + invoiceEmail.Sale.PaymentMethod + @"</p>
            </div>
        </div>
    </div>
    <div style='text-align: center;padding-top: 15px;font-size: 20px;'>
        Thank You, DealsDeals
    </div>
</div>";
            return body;
        }

        private string GetCurrency(string salePrice)
        {
            string currency = "";
            try
            {
                if (salePrice.Contains("EUR"))
                {
                    currency = "EUR ";
                }
                else if (salePrice.Contains("US"))
                {
                    currency = "US $";
                }
                else if (salePrice.Contains("AU"))
                {
                    currency = "AU $";
                }
                else
                {
                    currency = "&pound;";
                }

            }
            catch (Exception)
            {
            }
            return currency;
        }

        private decimal CalculateSalePrice(string salePrice)
        {
            decimal sp = 0;
            try
            {
                if(salePrice.Contains("�"))
                {
                    sp = Convert.ToDecimal(salePrice.Substring(1));
                }
                else if(salePrice.Contains("£"))
                {
                    sp = Convert.ToDecimal(salePrice.Substring(1));
                }
                else if (salePrice.Contains("EUR") || salePrice.Contains("US") || salePrice.Contains("AU")) 
                {
                    sp = Convert.ToDecimal(salePrice.Substring(4));
                }
                
            }
            catch (Exception)
            {
            }
            return sp;
        }
    }
}
