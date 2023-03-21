using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Teacher_Profile : ContentPage
    {
        public Teacher_Profile()
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
                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == App.LoggedInUser.DEPARTMENT_FID);

                txtName.Text = App.LoggedInUser.TEACHER_NAME;
                txtEmail.Text = App.LoggedInUser.TEACHER_EMAIL;
                txtPassword.Text = App.LoggedInUser.TEACHER_PASSWORD;
                txtPhone.Text = App.LoggedInUser.TEACHER_PHNO;
                TImage.Source = App.LoggedInUser.Image;
                txtAddress.Text = App.LoggedInUser.TEACHER_ADDRESS;
                txtDeptName.Text = Department.Object.DEPARTMENT_NAME;
                txtStatus.Text = App.LoggedInUser.Status;

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }







        }

        //private void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        //{

        //}
    }
}