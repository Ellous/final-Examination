using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination
{
    internal class Stock
    {
        public int StockCode { get; set; }
        public string ItemName { get; set; }
        public string SupplierName { get; set; }
        public double UnitCost { get; set; }
        public int NumberRequired { get; set; }
        public int CurrentStockLevel { get; set; }

        public Stock(int stockCode, string itemName, string supplierName, double unitCost, int numberRequired, int currentStockLevel)
        {
            StockCode = stockCode;
            ItemName = itemName;
            SupplierName = supplierName;
            UnitCost = unitCost;
            NumberRequired = numberRequired;
            CurrentStockLevel = currentStockLevel;
        }

        public void UpdateStockLevel(int quantity)
        {
            CurrentStockLevel += quantity;
        }

        public void UpdateNumberRequired(int quantity)
        {
            NumberRequired = quantity;
        }
    }
}
