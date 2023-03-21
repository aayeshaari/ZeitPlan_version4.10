using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Admin : ContentPage
    {
        public Add_Admin()
        {
            InitializeComponent();

        }


        private async void btnAdmin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCPass.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (txtPass.Text != txtCPass.Text)
                {
                    await DisplayAlert("Error", "Password do not match", "ok");
                    return;
                }

                var check = (await App.firebaseDatabase.Child("users").OnceAsync<users>()).FirstOrDefault(x => x.Object.Email == txtEmail.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.Email + "This Email is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("users").OnceAsync<users>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("users").OnceAsync<users>()).Max(a => a.Object.UsersId);
                    NewID = ++LastID;
                }




                users cs = new users()
                {
                    Email = txtEmail.Text,
                    Name = txtName.Text,
                    Password = txtPass.Text,
                    Phone = txtPhone.Text,
                    Status = "Pending"

                };

                await App.firebaseDatabase.Child("users").PostAsync(cs);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Admin Added  ", "Ok");

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }

    }
}
