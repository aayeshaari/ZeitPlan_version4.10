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
    public partial class Manage_Course : ContentPage
    {
        public Manage_Course()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {
                COURSE_ID = x.Object.COURSE_ID,
                COURSE_NAME = x.Object.COURSE_NAME,
                CREDIT_HOURS = x.Object.CREDIT_HOURS,

                //TEACHER_FID = selected,


            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_COURSE;

            var item = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(a => a.Object.COURSE_ID == selected.COURSE_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit");
            if (choice == "View")
            {
                //await DisplayAlert("Detail", "" +
                //    "\nCat ID:" + item.Object.CatId +
                //    " \nName:" + item.Object.Name +
                //    "\nEmail:" + item.Object.Email +
                //    "\nPassword:  *******" +
                //    "\nPhone:" + item.Object.Phone +
                //   "", "ok");
                await Navigation.PushAsync(new Course_Detail(selected));
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.COURSE_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_COURSE").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.COURSE_ID + "Deleted permanently", "ok");
                }
                if (choice == "Edit")
                { }
            }
        }

       
    }
    }



