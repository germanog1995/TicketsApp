﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketsApp.Models;
using TicketsApp.Services;

namespace TicketsApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Propperties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }
        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
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
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            IsEnabled = true;
            Email = null;
            Password = null;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter the user email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a password.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var response = await apiService.GetUserByEmail("http://checkticketsback.azurewebsites.net/", "/api", "/Users/Login",email,password);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", "Problem ocurred retrieving user information, try latter.");
                return;
            }

            var user = (User)response.Result;

            IsRunning = false;
            IsEnabled = true;

            var mainViewModel = MainViewModel.GetInstance();            

            mainViewModel.CurrentUser = user;

            mainViewModel.CheckTicket = new CheckTiketViewModel();
            await navigationService.Navigate("CheckTicketPage");
        }
        #endregion


    }
}
