using BlazeOrbital.ManufacturingHub.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Controls;

namespace BlazeOrbital.Accounting
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        public Inventory()
        {
            var services = new ServiceCollection();
            services.AddBlazorWebView();
            services.AddManufacturingDataClient((services, options) =>
            {
                options.BaseUri = WpfAppAccessTokenProvider.Instance.BackendUrl;
                options.MessageHandler = WpfAppAccessTokenProvider.Instance.CreateMessageHandler(services);
            });

            // Sets up EF Core with Sqlite
            services.AddManufacturingDataDbContext();

            Resources.Add("services", services.BuildServiceProvider());

            InitializeComponent();
        }
    }
}
