using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination
{
    internal class StockDataAccess
    {
        private string connectionString = "Data Source=exam.db;Version=3;";

        public StockDataAccess()
        {


        }

        public void AddStock(Stock stock)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                string query = "INSERT INTO Stock (StockCode, ItemName, SupplierName, UnitCost, NumberRequired, CurrentStockLevel) " +
                               "VALUES (@StockCode, @ItemName, @SupplierName, @UnitCost, @NumberRequired, @CurrentStockLevel)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.Parameters.AddWithValue("@ItemName", stock.ItemName);
                    command.Parameters.AddWithValue("@SupplierName", stock.SupplierName);
                    command.Parameters.AddWithValue("@UnitCost", stock.UnitCost);
                    command.Parameters.AddWithValue("@NumberRequired", stock.NumberRequired);
                    command.Parameters.AddWithValue("@CurrentStockLevel", stock.CurrentStockLevel);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Stock (
                                        StockCode INTEGER PRIMARY KEY,
                                        ItemName TEXT NOT NULL,
                                        SupplierName TEXT NOT NULL,
                                        UnitCost REAL NOT NULL,
                                        NumberRequired INTEGER NOT NULL,
                                        CurrentStockLevel INTEGER NOT NULL)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStock(Stock stock)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                string query = "UPDATE Stock SET ItemName=@ItemName, SupplierName=@SupplierName, UnitCost=@UnitCost, " +
                               "NumberRequired=@NumberRequired, CurrentStockLevel=@CurrentStockLevel WHERE StockCode=@StockCode";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.Parameters.AddWithValue("@ItemName", stock.ItemName);
                    command.Parameters.AddWithValue("@SupplierName", stock.SupplierName);
                    command.Parameters.AddWithValue("@UnitCost", stock.UnitCost);
                    command.Parameters.AddWithValue("@NumberRequired", stock.NumberRequired);
                    command.Parameters.AddWithValue("@CurrentStockLevel", stock.CurrentStockLevel);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStock(int stockCode)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                string query = "DELETE FROM Stock WHERE StockCode=@StockCode";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockCode", stockCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetStock()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT * FROM Stock";
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}
