namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Shop.UIForms.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            //Inicializa valores
            Email = "adovan0608@gmail.com";
            Password = "123456";
        }

        private async void Login()
        {
            if(string.IsNullOrEmpty(Email)) {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduzca un correo", "Aceptar");
                return;             
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduzca una contraseña", "Aceptar");
                return;
            }

            MainViewModel.Get_Instance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

        }
    }
}
