using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.BackgroundVideoView;
using ZeitPlan.LoginSystem;

namespace ZeitPlan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            

        }



        private void btnadmin_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Login("Admin"));
        }

        private void btnteacher_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Login("Teacher"));
        }

        private void btnStudent_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Login("Student"));

        }


        
    }
}
