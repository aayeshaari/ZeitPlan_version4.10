using Firebase.Database.Query;
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
    public partial class Manage_Slot : ContentPage
    {
        public Manage_Slot()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Select(x => new TBL_SLOT
            {
                SLOT_ID = x.Object.SLOT_ID,
                SLOT_NAME = x.Object.SLOT_NAME,
                
               
                SLOT_START_TIME=x.Object.SLOT_START_TIME,
                SLOT_END_TIME=x.Object.SLOT_END_TIME,


            }).ToList();
        }



        private async  void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_SLOT;

            var item = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(a => a.Object.SLOT_ID == selected.SLOT_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit");
            if (choice == "View")
            {

                await Navigation.PushAsync(new Slot_Detail(selected));
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.SLOT_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_SLOT").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.SLOT_ID + "Deleted permanently", "ok");
                }
                if (choice == "Edit")
                { }
            }
        }
    }
    }
