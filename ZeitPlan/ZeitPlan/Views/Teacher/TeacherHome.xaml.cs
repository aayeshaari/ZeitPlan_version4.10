using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Views.Admin;
using ZeitPlan.Views.Student;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherHome : ContentPage
    {
        public TeacherHome()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Manage_Notification());
        }

        private async void btnClicked_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Teacher_Profile());
        }

        private async void btntime_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View_TimeTable());
        }

        private async void btnstudt_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Student_List());
        }

        //private async void btnteach_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Teacher_List());
        //}

        private async void btnmsg_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Request_Portol());
        }

        private async void btnnotifi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Manage_Notification());
        }
    }
}