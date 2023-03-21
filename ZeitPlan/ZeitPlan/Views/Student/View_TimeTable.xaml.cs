using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.View_Model;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_TimeTable : ContentPage
    {
        public View_TimeTable()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadDataAll();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }
        }
        async void LoadDataAll()
        {
            try
            {

                var RawData = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).ToList();
                var RefinedList = new List<View_Model.View_TimeTable>();
                foreach (var item in RawData)
                {
                    var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == item.Object.CLASS_FID);
                    var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == item.Object.COURSE_FID);
                    var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_FID);
                    var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_ID == item.Object.SLOT_FID);
                    var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_ID == item.Object.ROOM_FID);

                    if (Class.Object.CLASS_ID != App.LoggedInStudent.CLASS_FID)
                    {
                        continue;
                    }

                    RefinedList.Add(new View_Model.View_TimeTable
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
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;

            }
        }

        async void LoadDataDayWise()
        {
            try
            {
               
                var RawData = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).ToList();
                var RefinedList = new List<View_Model.View_TimeTable>();
                foreach (var item in RawData)
                {
                    var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == item.Object.CLASS_FID);
                    var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == item.Object.COURSE_FID);
                    var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_FID);
                    var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_ID == item.Object.SLOT_FID);
                    var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_ID == item.Object.ROOM_FID);

                    if (Class.Object.CLASS_ID != App.LoggedInStudent.CLASS_FID)
                    {
                        continue;
                    }

                    if (ddlDay.SelectedItem != null)
                    {
                        string SelectedDay = ddlDay.SelectedItem.ToString();
                        if (SelectedDay != item.Object.DAY)
                        {
                            continue;
                        }
                    }


                    RefinedList.Add(new View_Model.View_TimeTable
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
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;

            }
        }
       
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            LoadingInd.IsRunning = true;
            LoadDataAll();
            LoadingInd.IsRunning = false;
        }

        private void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadingInd.IsRunning = true;
            LoadDataDayWise();
            LoadingInd.IsRunning = false;
        }        
    }
}