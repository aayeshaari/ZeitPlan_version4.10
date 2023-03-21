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
    public partial class Mange_TimeTable : ContentPage
    {
        public Mange_TimeTable()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadData();
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
            try
            {
                LoadingInd.IsRunning = true;


                var RawData = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).ToList();
                var RefinedList = new List<View_TimeTable>();
                foreach (var item in RawData)
                {
                    var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == item.Object.CLASS_FID);
                    var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == item.Object.COURSE_FID);
                    var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_FID);
                    var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_ID == item.Object.SLOT_FID);
                    var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_ID == item.Object.ROOM_FID);

                    RefinedList.Add(new View_TimeTable
                    {
                        ID = item.Object.TIMETABLE_ID,
                        CLASS_NAME = Class.Object.CLASS_NAME,
                        COURSE_NAME = Course.Object.COURSE_NAME,
                        ROOM_NO = Room.Object.ROOM_NO,
                        SLOT_NAME = Slot.Object.SLOT_NAME,
                        SLOT_END_TIME = Slot.Object.SLOT_START_TIME,
                        SLOT_START_TIME = Slot.Object.SLOT_END_TIME,
                        DAY = item.Object.DAY,
                        TEACHER_NAME = Teacher.Object.TEACHER_NAME,

                    }

                    );


                }
                DataList.ItemsSource = RefinedList;
                LoadingInd.IsRunning = false;

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;

            }
        }
        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as View_TimeTable;

            var item = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault(a => a.Object.TIMETABLE_ID == Selected.ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete");

            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.TIMETABLE_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_TIMETABLE").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.TIMETABLE_ID + "Deleted permanently", "ok");
                }
             
            }



        }

    }
}