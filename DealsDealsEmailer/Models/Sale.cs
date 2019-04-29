using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealsDealsEmailer.Models
{
    public class Sale
    {
        public string SalesRecordNumber { get; set; }
        public string PaidOnDate { get; set; }
        public string ItemTitle { get; set; }
        public string Quantity { get; set; }
        public string SalePrice { get; set; }
        public string CustomLabel { get; set; }
        public string PostageAndPackaging { get; set; }
        public string TotalPrice { get; set; }
        public string DeliveryService { get; set; }
        public string PaymentMethod { get; set; }

    }
}
