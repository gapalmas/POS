using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }

        //ToDo: Quitar instancia
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
    }
}
