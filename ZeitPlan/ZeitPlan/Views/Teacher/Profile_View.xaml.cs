using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile_View : ContentPage
    {
        public Profile_View()
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
            try
            {


                var RawData = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).ToList();
                var RefinedList = new List<View_Model.View_Teacher>();
                foreach (var item in RawData)
                {
                    var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == item.Object.DEPARTMENT_FID);
                    //var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == item.Object.COURSE_FID);
                    //var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_ID);
                    //var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_ID == item.Object.SLOT_FID);
                    //var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_ID == item.Object.ROOM_FID);

                    if (item.Object.TEACHER_ID != App.LoggedInUser.TEACHER_ID)
                    {
                        continue;
                    }

                    RefinedList.Add(new View_Model.View_Teacher
                    {
                        TEACHER_ID = item.Object.TEACHER_ID,
                        Image = item.Object.Image,
                        TEACHER_ADDRESS = item.Object.TEACHER_ADDRESS,
                        TEACHER_EMAIL = item.Object.TEACHER_EMAIL,
                        TEACHER_PHNO = item.Object.TEACHER_EMAIL,
                        TEACHER_PASSWORD = item.Object.TEACHER_PASSWORD,
                        TEACHER_NAME = item.Object.TEACHER_NAME,
                        DEPARTMENT_NAME = Department.Object.DEPARTMENT_NAME,

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

        private void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}