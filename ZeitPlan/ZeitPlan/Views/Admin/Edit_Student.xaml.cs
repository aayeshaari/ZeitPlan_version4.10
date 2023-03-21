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
	public partial class Edit_Student : ContentPage
	{
        public static TBL_STUDENT St = null;
        public Edit_Student(TBL_STUDENT s)
        {

            InitializeComponent();
            St = s;
            txtStName.Text = s.STUDENT_NAME;
            txtStEmail.Text = s.STUDENT_EMAIL;
            txtStPassword.Text = s.STUDENT_PASSWORD;


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
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {

                CLASS_NAME = x.Object.CLASS_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;

        }
        private async void btnstudent_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStName.Text) || string.IsNullOrEmpty(txtStEmail.Text) || string.IsNullOrEmpty(txtStPassword.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Class and try again", "ok");
                    return;
                }

                var check = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_EMAIL == txtStEmail.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.STUDENT_EMAIL + "This Student is already Added .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Max(a => a.Object.STUDENT_ID);
                    NewID = ++LastID;
                }
                var OldTech = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_ID == St.STUDENT_ID);

                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());

                OldTech.Object.STUDENT_ID = St.STUDENT_ID;
                OldTech.Object.STUDENT_NAME = St.STUDENT_NAME;
                OldTech.Object.STUDENT_EMAIL = St.STUDENT_EMAIL;
                OldTech.Object.STUDENT_PASSWORD = St.STUDENT_PASSWORD;
                OldTech.Object.CLASS_FID = St.CLASS_FID;


                await App.firebaseDatabase.Child("TBL_STUDENT").Child(OldTech.Key).PutAsync(OldTech.Object);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Student Updated  ", "Ok");
                await Navigation.PushAsync(new Manage_Student());
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