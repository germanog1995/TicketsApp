using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsApp.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }

        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
    }
}
