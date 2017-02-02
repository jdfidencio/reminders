using Reminders.Database;
using Reminders.Pages;
using Reminders.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Reminders
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new RemindersListPage());
        }

        //acesso estático ao singleton do banco
        private static RemindersDB _remindersDB;
        public static RemindersDB SingletonRemindersDB
        {
            get
            {
                if (_remindersDB == null)
                {
                    _remindersDB = new RemindersDB(DependencyService.Get<IFileHelperService>().GetLocalFilePath("Reminders.db"));
                }
                return _remindersDB;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
