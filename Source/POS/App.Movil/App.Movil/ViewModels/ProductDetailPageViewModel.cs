using Prism.Navigation;

namespace App.Movil.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        public ProductDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Product";
        }
    }
}
