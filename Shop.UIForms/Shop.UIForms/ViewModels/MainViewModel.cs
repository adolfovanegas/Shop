using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel Get_Instance()
        {
            if(instance==null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
