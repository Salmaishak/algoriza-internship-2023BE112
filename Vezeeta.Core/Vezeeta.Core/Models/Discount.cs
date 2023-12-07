using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Models
{
    public enum discountType { percentage, value}
    public enum discountActivity { active, deactive}
    public class Discount
    {
        public int discountID { get; set; }
        public string discountName { get; set; }
        public discountType discountType { get; set; }
       public int numOfRequests { get; set; }
        public int valueOfDiscount { get; set; }
        public discountActivity discountActivity { get; set; }

        public List<Booking> bookings { get; set; }
    }
}
