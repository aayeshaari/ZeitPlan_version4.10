using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Core.Models;
using System.Timers;
using ZeitPlan.Views.Student;
using ZeitPlan.Views.Teacher;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHome : ContentPage
    {
        public AdminHome()
        {
            InitializeComponent();
           
        }
       
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }

       

        private async void btnCliked_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        private async void btntime_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Mange_TimeTable());
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Manage_Notification() );
        }

        private async void btnmsg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Manage_Requests());
        }

        private async void btnnotifi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Manage_Notification());
        }

        private async void btnstudt_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Admin.Student_List() );
        }

        private async void btcteach_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Admin.Teacher_List());
        }
    }
}