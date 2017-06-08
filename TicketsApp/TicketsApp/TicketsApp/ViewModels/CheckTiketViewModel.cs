using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketsApp.Classes;
using TicketsApp.Models;
using TicketsApp.Services;
using Xamarin.Forms;

namespace TicketsApp.ViewModels
{
    public class CheckTiketViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private string message;
        private string ticketCode;
        private bool isRunning;
        private bool isEnabled;
        private Color color;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Propperties
        public Color Color
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                }
            }
            get
            {
                return color;
            }
        }

        public string TicketCode
        {
            set
            {
                if (ticketCode != value)
                {
                    ticketCode = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TicketCode"));
                }
            }
            get
            {
                return ticketCode;
            }
        }
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
            get
            {
                return message;
            }
        }
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        #endregion

        #region Contructors
        public CheckTiketViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            Message = "Wait for read a ticket...";
        }
        #endregion

        #region Commands
        public ICommand CheckTicketCommand { get { return new RelayCommand(CheckTicket); } }

        private async void CheckTicket()
        {
            if (string.IsNullOrEmpty(ticketCode))
            {
                await dialogService.ShowMessage("Error", "You must enter the ticket code.");
                return;
            }
            
            if(ticketCode.Length > 4)
            {
                await dialogService.ShowMessage("Error", "The ticket code can only be 4 digits.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var response = await apiService.GetTicket("http://checkticketsback.azurewebsites.net/", "/api", "/Tickets", TicketCode);
            var ticket = (Ticket)response.Result;
            if (!response.IsSuccess)    
            {
                var mainViewModel = MainViewModel.GetInstance();
                var ticketRequest = new Ticket
                {
                    TicketCode = ticketCode,
                    UserId = mainViewModel.CurrentUser.UserId,
                    DateTime = DateTime.Now,
                };
                response = await apiService.Post("http://checkticketsback.azurewebsites.net", "/api", "/Tickets", ticketRequest);

                Color = Color.Green;
                Message = string.Format("{0}, ALLOW ACCESS!", ticketCode);

                IsRunning = false;
                IsEnabled = true;
                return;
            }

            Color = Color.Red;
            Message = string.Format("{0}, TICKET READ BEFORE!",ticket.TicketCode);
            IsRunning = false;
            IsEnabled = true;


        }
        #endregion
    }
}
