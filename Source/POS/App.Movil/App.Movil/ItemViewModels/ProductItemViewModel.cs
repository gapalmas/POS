using App.Common.Entities;
using App.Movil.Views;
using Prism.Commands;
using Prism.Navigation;

namespace App.Movil.ItemViewModels
{
    public class ProductItemViewModel : Product
    {
        private readonly INavigationService navigationService;
        private DelegateCommand _selectProductCommand;
        public ProductItemViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public DelegateCommand SelectProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand(SelectProductAsync));
        private async void SelectProductAsync()
        {
            await navigationService.NavigateAsync(nameof(ProductDetailPage));
        }
    }
}
