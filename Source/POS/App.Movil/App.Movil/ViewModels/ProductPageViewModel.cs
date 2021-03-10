using App.Common.Entities;
using App.Common.Responses;
using App.Common.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace App.Movil.ViewModels
{
    public class ProductPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private ObservableCollection<Product> _products;

        public ProductPageViewModel(INavigationService navigationService, IApiService apiService) : base (navigationService)
        {
            _apiService = apiService;
            Title = "Products";
            LoadProductsAsync();
        }

        public ObservableCollection<Product> Products 
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

            
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Product>(url, "/api", "/Product");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var query = (List<Product>)response.Result;

            Products = new ObservableCollection<Product>(query);
        }
    }
}
