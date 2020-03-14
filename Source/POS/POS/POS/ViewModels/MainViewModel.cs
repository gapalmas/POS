using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }

        //ToDo: Remove Instance
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
    }
}
