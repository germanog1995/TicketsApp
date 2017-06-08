using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string TicketCode { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
    }
}
