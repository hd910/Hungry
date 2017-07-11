using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Hungry
{
    public partial class App : Application
    {
        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            MainPage = new NavigationPage(new LoadingPage());

            
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
