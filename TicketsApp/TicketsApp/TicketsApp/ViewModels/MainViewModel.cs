using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApp.Models;
using TicketsApp.Services;

namespace TicketsApp.ViewModels
{
    public class MainViewModel
    {
        #region Propperties
        public LoginViewModel Login { get; set; }
        public CheckTiketViewModel CheckTicket { get; set; }
        public User CurrentUser { get; set; } 
        #endregion

        #region Contructors
        public MainViewModel()
        {
            Login = new LoginViewModel();
            instance = this;
        } 
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
