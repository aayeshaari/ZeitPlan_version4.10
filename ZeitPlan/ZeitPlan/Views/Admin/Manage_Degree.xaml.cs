using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.View_Model;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Degree : ContentPage
    {
        public Manage_Degree()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Select(x => new TBL_DEGREE
            {
                DEGREE_ID=x.Object.DEGREE_ID,
                DEGREE_NAME = x.Object.DEGREE_NAME,
                DEPARTMENT_FID = x.Object.DEPARTMENT_FID,
               

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_DEGREE;

            var item = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault(a => a.Object.DEGREE_ID == selected.DEGREE_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit");
            if (choice == "View")
            {

                await Navigation.PushAsync(new Degree_Detail(selected));
               
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.DEGREE_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_DEGREE").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.DEGREE_ID + "Deleted permanently", "ok");
                }
                if (choice == "Edit")
                { }
            }
        }


    }
}
