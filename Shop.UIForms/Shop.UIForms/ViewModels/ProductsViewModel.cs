
namespace Shop.UIForms.ViewModels
{
    using Shop.Common.Models;
    using Shop.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Product> productsSource;
        private bool isRefreshing;

        public ObservableCollection<Product> ProductsSource {
            get => this.productsSource;
            set => this.SetValue(ref this.productsSource, value);
        }
        
        public bool IsRefreshing {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }


        public ProductsViewModel()
        {
            this.apiService = new ApiService();

            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            await Task.Delay(500);
            var response = await this.apiService.GetListAsync<Product>("http://192.168.1.115/Shop/", "api", "/Products");
           this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var Lista = (List<Product>)response.Result;
            this.ProductsSource = new ObservableCollection<Product>(Lista);
        }
    }
}
