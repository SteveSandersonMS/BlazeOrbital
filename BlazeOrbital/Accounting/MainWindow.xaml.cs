using System.Windows;

namespace BlazeOrbital.Accounting
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RibbonWin_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // This is a simplified mechanism for showing the inventory dynamically
            // Real WPF applications may follow more sophisticated patterns

            if (InventoryPanel is not null)
            {
                InventoryPanel.Children.Clear();
                HomePanel.Visibility = Visibility.Visible;

                if (e.AddedItems.Contains(inventoryTab))
                {
                    HomePanel.Visibility = Visibility.Hidden;
                    InventoryPanel.Children.Add(new Inventory());
                }
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            _ = WpfAppAccessTokenProvider.Instance.RequestAccessToken();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _ = WpfAppAccessTokenProvider.Instance.LogOutAsync();
        }
    }

    // Workaround for compiler error "error MC3050: Cannot find the type 'local:Main'"
    // Although WPF's design-time build can see Razor components, its runtime build cannot yet do so.
    public partial class Main { }
}
