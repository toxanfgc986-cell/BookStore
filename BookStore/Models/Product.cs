namespace BookStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int? ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string ImagePath { get; set; }

        public decimal FinalPrice
        {
            get
            {
                if (Discount > 0)
                {
                    return decimal.Round(Price * (1 - Discount / 100), 2);
                }
                return Price;
            }
        }
    }
}
