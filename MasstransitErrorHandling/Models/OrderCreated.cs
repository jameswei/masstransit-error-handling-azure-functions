using System;

namespace MasstransitErrorHandling.Models
{
    public class OrderCreated
    {
        public string OrderId { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
