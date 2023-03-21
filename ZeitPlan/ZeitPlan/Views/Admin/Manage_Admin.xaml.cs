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
    public partial class Manage_Admin : ContentPage
    {
        public Manage_Admin()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadData();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }
        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("users").OnceAsync<users>()).Select(x => new users
            {
                Email = x.Object.Email,
                Name = x.Object.Name,
                Password = x.Object.Password,
                Phone = x.Object.Phone,
                UsersId = x.Object.UsersId,
                Status = x.Object.Status,

            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as users;

            var item = (await App.firebaseDatabase.Child("users").OnceAsync<users>()).FirstOrDefault(a => a.Object.UsersId == selected.UsersId);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Approve", "Block", "Pending");
            if (choice == "View")
            {
                await DisplayAlert("Detail", "" +
                    "\nID:" + item.Object.UsersId +
                    "\nName:" + item.Object.Name +
                    "\nEmail:" + item.Object.Email +
                    "\nPassword:" + item.Object.Name +
                    "\nPhone:" + item.Object.Phone
                    , "ok");


            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.Name, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_TEACHER").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.Name + "Deleted permanently", "ok");
                }
            }
            if (choice == "Approve")
            {
                item.Object.Status = "Approved";
                await App.firebaseDatabase.Child("users").Child(item.Key).PutAsync(item.Object);
                LoadData();
                await DisplayAlert("Confirmation", item.Object.Name + " is Approved", "ok");
            }
            if (choice == "Block")
            {
                item.Object.Status = "Blocked";
                await App.firebaseDatabase.Child("users").Child(item.Key).PutAsync(item.Object);
                LoadData();
                await DisplayAlert("Confirmation", item.Object.Name + " is Blocked", "ok");
            }
            if (choice == "Pending")
            {
                item.Object.Status = "Pending";
                await App.firebaseDatabase.Child("users").Child(item.Key).PutAsync(item.Object);
                LoadData();
                await DisplayAlert("Confirmation", item.Object.Name + " is Pending", "ok");
            }
        }
    }
}