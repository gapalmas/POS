using App.Common.Entities;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace App.Movil.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        private Product _product;
        //private ObservableCollection<ProductImage> _images;
        public ProductDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Product ...";
        }

        public Product Product 
        {
            get => _product;

            set => SetProperty(ref _product, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<Product>("product");
                Title = Product.Description;
            }
        }
    }
}
