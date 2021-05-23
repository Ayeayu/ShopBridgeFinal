using System;
using System.Collections.Generic;

namespace ShopBridgeInventory.Models
{
    public partial class InventoryProducts
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
