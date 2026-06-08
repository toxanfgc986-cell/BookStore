using System;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Article { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string PickupAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
