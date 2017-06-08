using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApp.ViewModels;
using TicketsApp.Views;

namespace TicketsApp.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CheckTicketPage":
                    await App.Current.MainPage.Navigation.PushAsync(new CheckTicketPage());
                    break;

                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
