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
    public partial class Teacher_List : ContentPage
    {
        public Teacher_List()
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

            LoadingInd.IsRunning = true;

            List<VM_TBL_TEACHER> TeacherWithDeptsList = new List<VM_TBL_TEACHER>();


            var RawTeachers = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).ToList();
            foreach (var item in RawTeachers)
            {
                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == item.Object.DEPARTMENT_FID);

                if (Department == null)
                {
                    continue;
                }

                TeacherWithDeptsList.Add(
                    new VM_TBL_TEACHER
                    {
                        DEPARTMENT_NAME = Department.Object.DEPARTMENT_NAME,
                        Image = item.Object.Image,
                        TEACHER_ADDRESS = item.Object.TEACHER_ADDRESS,
                        TEACHER_ID = item.Object.TEACHER_ID,
                        TEACHER_NAME = item.Object.TEACHER_NAME,
                        TEACHER_PHNO = item.Object.TEACHER_PHNO,
                        Status = item.Object.Status,
                    });
            }



            DataList.ItemsSource = TeacherWithDeptsList;
            LoadingInd.IsRunning = false;

        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as VM_TBL_TEACHER;

            var item = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(a => a.Object.TEACHER_ID == selected.TEACHER_ID);


            await Navigation.PushAsync(new Teacher_Detail(item.Object));

        }
    }
}