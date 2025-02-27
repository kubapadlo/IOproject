using PocztaDesktop.Views;

namespace PocztaDesktop
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ParcelsPage), typeof(ParcelsPage));
            Routing.RegisterRoute(nameof(AdminMainPage), typeof(AdminMainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(EmployesList), typeof(EmployesList));
            Routing.RegisterRoute(nameof(AddEmployeeForm), typeof(AddEmployeeForm));
            Routing.RegisterRoute(nameof(AddParcelsForm), typeof(AddParcelsForm));
            Routing.RegisterRoute(nameof(WarehouseMainPage), typeof(WarehouseMainPage));
            Routing.RegisterRoute(nameof(LadunkiPage), typeof(LadunkiPage));   
        }
    }
}
