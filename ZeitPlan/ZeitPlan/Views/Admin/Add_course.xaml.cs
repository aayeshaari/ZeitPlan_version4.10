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
    public partial class Add_course : ContentPage
    {
        public Add_course()
        {

            InitializeComponent();
            //LoadData();
        }
        //async void LoadData()
        //{
        //    var firebaseList = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
        //    {

        //        TEACHER_NAME = x.Object.TEACHER_NAME,

        //    }).ToList();
        //    var refinedList = firebaseList.Select(x => x.TEACHER_NAME).ToList();
        //    ddlTeacher.ItemsSource = refinedList;
        //}


        private async void btnCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCOName.Text) || string.IsNullOrEmpty(txtCOCredit.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                //if (ddlDegree.SelectedItem == null)
                //{
                //    await DisplayAlert("Error", "please select Degree and try again", "ok");
                //    return;
                //}

                var check = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_NAME == txtCOName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.COURSE_NAME + "This Course_Name is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Max(a => a.Object.COURSE_ID);
                    NewID = ++LastID;
                }
                //List<TBL_TEACHER> tech = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
                //{
                //    TEACHER_ID = x.Object.TEACHER_ID,
                //    TEACHER_NAME = x.Object.TEACHER_NAME

                //}).ToList();
                //int selected = tech[ddlTeacher.SelectedIndex].TEACHER_ID;




                TBL_COURSE c = new TBL_COURSE()
                {
                    COURSE_ID = NewID,
                    COURSE_NAME = txtCOName.Text,
                    CREDIT_HOURS=txtCOCredit.Text,
                    //TEACHER_FID=selected
                };

                await App.firebaseDatabase.Child("TBL_COURSE").PostAsync(c);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Course Added  ", "Ok");

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
