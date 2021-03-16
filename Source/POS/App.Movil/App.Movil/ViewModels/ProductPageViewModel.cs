using App.Common.Entities;
using App.Common.Responses;
using App.Common.Services;
using App.Movil.ItemViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace App.Movil.ViewModels
{
    public class ProductPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<ProductItemViewModel> _products;
        private bool _IsRunning;
        private string _search;
        private List<Product> _myProducts;
        private DelegateCommand _searchCommand;


        public ProductPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            this.navigationService = navigationService;
            _apiService = apiService;
            Title = "Products";
            LoadProductsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowProducts));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowProducts();
            }
        }

        public bool IsRunning
        {
            get => _IsRunning;
            set => SetProperty(ref _IsRunning, value);
        }

        public ObservableCollection<ProductItemViewModel> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        private async void LoadProductsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet Connection Error", "Accept");
                return;
            }

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Product>(url, "/api", "/Product");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myProducts = (List<Product>)response.Result;
            ShowProducts();
            //Products = new ObservableCollection<Product>(_products);
            IsRunning = false;
        }

        private void ShowProducts()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p=> new ProductItemViewModel(navigationService)
                {
                   Description = p.Description,
                   Id = p.Id,
                   PartNumber = p.PartNumber,
                   ImagePath = p.ImagePath
                    
                }).ToList());
            }
            else
            {
                Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p => new ProductItemViewModel(navigationService)
                {
                    Description = p.Description,
                    Id = p.Id,
                    PartNumber = p.PartNumber,
                    ImagePath = p.ImagePath

                }).Where(p=> p.Description.ToLower().Contains(Search.ToLower())).ToList());
            }
        }
    }
}
