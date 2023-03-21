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
    public partial class Mange_Teacher_Assign : ContentPage
    {
        public Mange_Teacher_Assign()
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

            List<Class_Teacher_Assign> TeacherWithDeptsList = new List<Class_Teacher_Assign>();


            var RawTeachers = (await App.firebaseDatabase.Child("TBL_TEACHER_CLASS_ASSIGN").OnceAsync<TBL_TEACHER_CLASS_ASSIGN>()).ToList();
            foreach (var item in RawTeachers)
            {
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == item.Object.CLASS_FID);

                if (Class == null)
                {
                    continue;
                }
                var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_FID);

                if (Teacher == null)
                {
                    continue;
                }

                TeacherWithDeptsList.Add(
                    new Class_Teacher_Assign
                    {
                        TEACHER_CLASS_ASSIGN_ID = item.Object.TEACHER_CLASS_ASSIGN_ID,
                        CLASS_NAME = Class.Object.CLASS_NAME,
                        TEACHER_NAME = Teacher.Object.TEACHER_NAME

                    });
            }
            DataList.ItemsSource = TeacherWithDeptsList;



            //DataList.ItemsSource = TeacherWithDeptsList;
            LoadingInd.IsRunning = false;


            //DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_TEACHER_CLASS_ASSIGN").OnceAsync<TBL_TEACHER_CLASS_ASSIGN>()).Select(x => new TBL_TEACHER_CLASS_ASSIGN
            //{
            //    TEACHER_CLASS_ASSIGN_ID = x.Object.TEACHER_CLASS_ASSIGN_ID,
            //    CLASS_FID = x.Object.CLASS_FID,
            //    TEACHER_FID = x.Object.TEACHER_FID,
            //}).ToList();

            LoadingInd.IsRunning = false;



        }
        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as Class_Teacher_Assign;

            var item = (await App.firebaseDatabase.Child("TBL_TEACHER_CLASS_ASSIGN").OnceAsync<TBL_TEACHER_CLASS_ASSIGN>()).FirstOrDefault(a => a.Object.TEACHER_CLASS_ASSIGN_ID == Selected.TEACHER_CLASS_ASSIGN_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View");
            if (choice == "View")
            {

                await Navigation.PushAsync(new TeacherCourseAssign_Detail(item.Object));
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.TEACHER_CLASS_ASSIGN_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_TEACHER_CLASS_ASSIGN").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.TEACHER_CLASS_ASSIGN_ID + "Deleted permanently", "ok");
                }
                
            }
        }
    }
}