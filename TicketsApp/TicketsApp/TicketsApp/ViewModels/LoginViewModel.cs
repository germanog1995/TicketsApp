using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApp.Services;

namespace TicketsApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
        } 
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion
    }
}
