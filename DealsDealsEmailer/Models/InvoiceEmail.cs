﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealsDealsEmailer.Models
{
    public class InvoiceEmail
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string TransactionId { get; set; }
        public string ItemNumber { get; set; }
        public string UserId { get; set; }
        public Sale Sale { get; set; }
        public Address Address { get; set; }
        public InvoiceEmail()
        {
            Sale = new Sale();
            Address = new Address();
        }
    }
}
