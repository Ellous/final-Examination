using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Examination
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StockDataAccess dataAccess = new StockDataAccess();

        public MainWindow()
        {
            InitializeComponent();
            LoadStockData();
        }

        private void LoadStockData()
        {
            DataTable stockData = dataAccess.GetStock();
            List<Stock> stocks = new List<Stock>();

            foreach (DataRow row in stockData.Rows)
            {
                stocks.Add(new Stock(
                    int.Parse(row["StockCode"].ToString()),
                    row["ItemName"].ToString(),
                    row["SupplierName"].ToString(),
                    double.Parse(row["UnitCost"].ToString()),
                    int.Parse(row["NumberRequired"].ToString()),
                    int.Parse(row["CurrentStockLevel"].ToString())
                ));
            }

            dgStock.ItemsSource = stocks;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Stock newStock = new Stock(
                int.Parse(txtStockCode.Text),
                txtItemName.Text,
                txtSupplierName.Text,
                double.Parse(txtUnitCost.Text),
                int.Parse(txtNumberRequired.Text),
                int.Parse(txtCurrentStockLevel.Text)
            );

            dataAccess.AddStock(newStock);
            LoadStockData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Stock selectedStock)
            {
                selectedStock.ItemName = txtItemName.Text;
                selectedStock.SupplierName = txtSupplierName.Text;
                selectedStock.UnitCost = double.Parse(txtUnitCost.Text);
                selectedStock.NumberRequired = int.Parse(txtNumberRequired.Text);
                selectedStock.CurrentStockLevel = int.Parse(txtCurrentStockLevel.Text);

                dataAccess.UpdateStock(selectedStock);
                LoadStockData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Stock selectedStock)
            {
                dataAccess.DeleteStock(selectedStock.StockCode);
                LoadStockData();
            }
        }

        private void BtnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            foreach (Stock stock in dgStock.ItemsSource as List<Stock>)
            {
                if (stock.NumberRequired > 0)
                {
                    stock.UpdateStockLevel(stock.NumberRequired);
                    stock.UpdateNumberRequired(0);

                    dataAccess.UpdateStock(stock);
                }
            }

            LoadStockData();
        }

        // Event handler for txtUnitCost TextChanged event
        private void txtUnitCost_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Example: Validate the input to ensure it is a valid number
            double unitCost;
            if (!double.TryParse(txtUnitCost.Text, out unitCost))
            {
                MessageBox.Show("Please enter a valid numeric value for Unit Cost.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUnitCost.Text = ""; // Clear the invalid input
            }
        }

        // Event handler for dgStock SelectionChanged event
        private void dgStock_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgStock.SelectedItem is Stock selectedStock)
            {
                txtStockCode.Text = selectedStock.StockCode.ToString();
                txtItemName.Text = selectedStock.ItemName;
                txtSupplierName.Text = selectedStock.SupplierName;
                txtUnitCost.Text = selectedStock.UnitCost.ToString();
                txtNumberRequired.Text = selectedStock.NumberRequired.ToString();
                txtCurrentStockLevel.Text = selectedStock.CurrentStockLevel.ToString();
            }
        }
    }
}