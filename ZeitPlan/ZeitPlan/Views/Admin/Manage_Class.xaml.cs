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
    public partial class Manage_Class : ContentPage
    {
        public Manage_Class()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {
                CLASS_NAME = x.Object.CLASS_NAME,
                SECTION = x.Object.SECTION,
                CLASS_ID = x.Object.CLASS_ID,
                SESSION=x.Object.SESSION,
                SHIFT=x.Object.SHIFT,
                SEMESTER=x.Object.SEMESTER,
                DEGREE_FID=x.Object.DEGREE_FID
                


            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as TBL_CLASS;

            var item = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(a => a.Object.CLASS_ID == Selected.CLASS_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete","View");

            if (choice == "View")
            {
                await Navigation.PushAsync(new Class_Detail(Selected));
                
            }

                if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.CLASS_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_CLASS").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.CLASS_ID + "Deleted permanently", "ok");
                }
               
            }
        }
    }
}
