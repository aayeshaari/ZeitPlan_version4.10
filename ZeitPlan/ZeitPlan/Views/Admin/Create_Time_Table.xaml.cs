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
    public partial class Create_Time_Table : ContentPage
    {
        public Create_Time_Table()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            LoadingInd.IsRunning = true;

            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {

                CLASS_NAME = x.Object.CLASS_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;

            //COURSE_FID
            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {

                COURSE_NAME = x.Object.COURSE_NAME,

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.COURSE_NAME).ToList();
            ddlCourse.ItemsSource = refinedList1;

            //TEACHER_fid
            var firebaseList2 = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {

                TEACHER_NAME = x.Object.TEACHER_NAME,

            }).ToList();
            var refinedList2 = firebaseList2.Select(x => x.TEACHER_NAME).ToList();
            ddlTeacher.ItemsSource = refinedList2;
            //sLOT_FID
            var firebaseList3 = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Select(x => new TBL_SLOT
            {

                SLOT_NAME = x.Object.SLOT_NAME,

            }).ToList();
            var refinedList3 = firebaseList3.Select(x => x.SLOT_NAME).ToList();
            ddlSlot.ItemsSource = refinedList3;
            //ROOM_FID
            var firebaseList4 = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Select(x => new TBL_ROOM
            {

                ROOM_NO = x.Object.ROOM_NO,

            }).ToList();
            var refinedList4 = firebaseList4.Select(x => x.ROOM_NO).ToList();
            ddlRoom.ItemsSource = refinedList4;

            LoadingInd.IsRunning = false;


        }

        private async void btnCreate2_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Class and try again", "ok");
                    return;
                }
                if (ddlCourse.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Course and try again", "ok");
                    return;
                }
                if (ddlTeacher.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Teacher and try again", "ok");
                    return;
                }
                if (ddlSlot.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Slot and try again", "ok");
                    return;
                }
                if (ddlRoom.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Room and try again", "ok");
                    return;
                }

                if (txtDay.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select day and try again", "ok");
                    return;
                }

                LoadingInd.IsRunning = true;

                //Extracting DDL Values
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());
                var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_NAME == ddlCourse.SelectedItem.ToString());
                var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_NAME == ddlTeacher.SelectedItem.ToString());
                var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_NAME == ddlSlot.SelectedItem.ToString());
                var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_NO == ddlRoom.SelectedItem.ToString());
                var day = txtDay.SelectedItem.ToString();

               

                //Teacher Avail Validation ==============================================
                var TeacherAvailcheck = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>())
                    .Where(
                        x => x.Object.DAY == day &&
                        x.Object.SLOT_FID == Slot.Object.SLOT_ID &&
                        x.Object.TEACHER_FID == Teacher.Object.TEACHER_ID
                     ).FirstOrDefault();

                if (TeacherAvailcheck != null)
                {
                    await DisplayAlert("Error", "Teacher is already assigned to another lecture on this day and slot.", "Ok");
                    return;
                }


                //Room Avail Validation ==============================================
                var RoomAvailcheck = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>())
                    .Where(
                        x => x.Object.DAY == day &&
                        x.Object.SLOT_FID == Slot.Object.SLOT_ID &&
                        x.Object.ROOM_FID == Room.Object.ROOM_ID
                    ).FirstOrDefault();

                if (RoomAvailcheck != null)
                {
                    await DisplayAlert("Error", "Room No. is already assigned to another lecture on this day and slot.", "Ok");
                    return;
                }


                //Subject Assigned Before or Not Validation ==============================================
                var SubjectAvailcheck = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>())
                    .Where(
                        x=> x.Object.COURSE_FID == Course.Object.COURSE_ID
                    ).FirstOrDefault();

                if (SubjectAvailcheck != null)
                {
                    await DisplayAlert("Error", "This Subject is already assigned to another teacher.", "Ok");
                    return;
                }


                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).Max(a => a.Object.TIMETABLE_ID);
                    NewID = ++LastID;
                }

                TBL_TIMETABLE tt = new TBL_TIMETABLE()
                {
                    TIMETABLE_ID = NewID,

                    CLASS_FID = Class.Object.CLASS_ID,
                    COURSE_FID = Course.Object.COURSE_ID,
                    TEACHER_FID = Teacher.Object.TEACHER_ID,
                    ROOM_FID = Room.Object.ROOM_ID,
                    SLOT_FID = Slot.Object.SLOT_ID,
                    DAY = day,

                };

                await App.firebaseDatabase.Child("TBL_TIMETABLE").PostAsync(tt);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "TIMETABLE Created  ", "Ok");

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

