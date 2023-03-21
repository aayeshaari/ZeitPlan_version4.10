using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Student_Profile : ContentPage
    {
        public Student_Profile()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            try
            {
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == App.LoggedInStudent.CLASS_FID);

                txtName.Text = App.LoggedInStudent.STUDENT_NAME;
                txtEmail.Text = App.LoggedInStudent.STUDENT_EMAIL;
                txtPassword.Text = App.LoggedInStudent.STUDENT_PASSWORD;
                txtClassName.Text = Class.Object.CLASS_NAME;


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