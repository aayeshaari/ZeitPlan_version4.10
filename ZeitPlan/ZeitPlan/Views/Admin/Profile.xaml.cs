using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            try
            {

                txtName.Text = App.LoggedInAdmin.Name;
                txtEmail.Text = App.LoggedInAdmin.Email;
                txtPassword.Text = App.LoggedInAdmin.Password;
                txtPhone.Text = App.LoggedInAdmin.Phone;


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }







        }

    }
}