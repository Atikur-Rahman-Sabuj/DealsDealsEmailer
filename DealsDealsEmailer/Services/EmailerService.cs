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
                MailAddress To = new MailAddress(ToMail, invoiceEmail.Address.FullName);
                MailMessage mailMessage = new MailMessage(From, To);
                mailMessage.Subject = "Invoice for your recent eBay Order "+ invoiceEmail.ItemNumber;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = GetSellMessageBody(invoiceEmail);
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
                MailAddress To = new MailAddress(ToMail, invoiceEmail.Address.FullName);
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
            string body = "Hi " + invoiceEmail.Address.FullName + nl(2);
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
            string body = @"<div style='color:black;'>
    <div style='background: gainsboro;padding: 10px;overflow: hidden; color:black;'>
        <img style='height:100px;width: auto' src='https://docs.google.com/uc?id=1sQrh4B8D96n9DgdniuHep6tCe8wVoYHZ'/>
        <div style='float:right;text-align: right'>
            <p>HDM Retail LTD</p>
            <p>8 Lammas Close</p>
            <p>Solihull</p>
            <p>B92 8PA</p>
            <p>VAT No: GB 232760325</p>
        </div>
    </div>
    <div style='padding:5px'>
        <div style='background: powderblue;padding-top: 20px;padding-bottom: 30px;overflow: hidden;'>
            <div style='width:60%;padding-left: 10px;padding-top: 10px;float: left;border-right: 1px dotted #6e6e6e'>
                <p style='margin-bottom:30px'>Thank you for your order from DealsDelas</p>
                <p >Please visit our website at <b>www.dealsdeals.co.uk</b> for more great offerss</p>
            </div>
            <div style='width:30%; float:right;padding-left: 10px'>
                <p style='margin-bottom:20px'><b>Order questions?</b></p>
                <p><b>Call Us: </b>0800 448 8228</p>
                <p><b>Email: </b>info@dealsdeals.co.uk</p>
            </div>
        </div>
    </div>
    <div style='text-align: center;padding:5px'>
        <h3>Your Invoice " + invoiceEmail.Sale.SalesRecordNumber+@"</h3>
        <p style='margin-top:20px;margin-bottom: 25px'>Placed on "+invoiceEmail.Sale.PaidOnDate+ @"</p>
        <p>Please visit our website www.dealsdeals.co.uk</p>
        <p>There is a 10% discount when you use voucher code Ebay10 at checkout</p>
        <div style='background:gainsboro;overflow: hidden;padding-top: 15px;padding-bottom: 15px'>
            <p style='padding:10px;width:40%;float: left;text-align: left;margin:0px;'>ITEM IN ORDER</p>
            <div style='width:40%;float:right;text-align: right'>
                <p style='padding:10px;width:25%;float:left;margin:0px'>QTY</p>
                <p style='padding:10px;width:40%;float:right;margin:0px'>PRICE</p>
            </div>
        </div>
        <div style='overflow: hidden;padding-top: 15px;padding-bottom: 15px'>
            <p style='padding:10px;width:40%;float: left;text-align: left;margin:0px;'>" + invoiceEmail.Sale.ItemTitle.Substring(0,50)+ @"</p>
            <div style='width:40%;float:right;text-align: right'>
                <p style='padding:10px;width:25%;float:left;margin:0px'>" + invoiceEmail.Sale.Quantity + @"</p>
                <p style='padding:10px;width:40%;float:right;margin:0px'>"+ "&pound;" + invoiceEmail.Sale.SalePrice.Substring(1) + @"</p>
            </div>
            <div style='clear:both'></div>
            <p style='text-align:left;padding-left: 10px'>SKU: " + invoiceEmail.Sale.CustomLabel + @"</p>
        </div>
        <hr/>
        <div style='padding:5px'>
            <div style='background: powderblue;padding:20px;text-align: left'>
                <p>Shipping & Handling:	" + "&pound;" + invoiceEmail.Sale.PostageAndPackaging.Substring(1) + @"</p>   
                <p>Tax	20% of " + "&pound;" + invoiceEmail.Sale.TotalPrice.Substring(1) + @" if " + invoiceEmail.Address.Country + @" = United Kingdom else 0% </p>
                <p>Grand Total: " + "&pound;" + invoiceEmail.Sale.TotalPrice.Substring(1) + @"</p>
            </div>
        </div>
    </div>
    <div style='padding:20px;'> 
        <div style='width:40%;float: left;'>
            <p><b>BILL TO:</b></p>
            <p>" + invoiceEmail.Address.FullName + @"</p>
            <p>" + invoiceEmail.Address.Address1 + @"</p>
            <p> " + invoiceEmail.Address.Address2 + @"</p>
            <p>" + invoiceEmail.Address.Town + @"</p>
            <p> " + invoiceEmail.Address.County + @"</p>
            <p> " + invoiceEmail.Address.PostCode + @"</p>
            <p>" + invoiceEmail.Address.Country + @"</p>
        </div>
        <div style='width:40%;float:right'>
            <p><b>SHIP TO:</b></p>
            <p>" + invoiceEmail.Address.FullName + @"</p>
            <p>" + invoiceEmail.Address.Address1 + @"</p>
            <p> " + invoiceEmail.Address.Address2 + @"</p>
            <p>" + invoiceEmail.Address.Town + @"</p>
            <p> " + invoiceEmail.Address.County + @"</p>
            <p> " + invoiceEmail.Address.PostCode + @"</p>
            <p>" + invoiceEmail.Address.Country + @"</p>
        </div>
    </div>
    <div style='padding:20px;'> 
        <div style='width:40%;float: left;'>
            <p><b>SHIPPING METHOD:</b></p>
            <p>Selected shipping method: " + invoiceEmail.Sale.DeliveryService + @"</p>
        </div>
        <div style='width:40%;float:right'>
            <p><b>PAYMENT METHOD:</b></p>
            <p>" + invoiceEmail.Sale.PaymentMethod + @"</p>
            <br/>
            <p>" + invoiceEmail.Email + @"</p>
        </div>
    </div>
</div>";
            return body;
        }
    }
}
