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
    public partial class Add_Class : ContentPage
    {
        public Add_Class()
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
            var firebaseList = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).Select(x => new TBL_DEGREE
            {

                DEGREE_NAME = x.Object.DEGREE_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEGREE_NAME).ToList();
            ddlDegree.ItemsSource = refinedList;



        }
        private async void btnClass_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClName.Text) || string.IsNullOrEmpty(txtClSession.Text) || string.IsNullOrEmpty(txtClSection.Text) || string.IsNullOrEmpty(txtClShift.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (ddlDegree.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Degree and try again", "ok");
                    return;
                }

                var check = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == txtClName.Text && x.Object.SEMESTER == txtClSemester.Text && x.Object.SESSION == txtClSession.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.CLASS_NAME + "This Class is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Max(a => a.Object.CLASS_ID);
                    NewID = ++LastID;
                }
                var Degree = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault(x => x.Object.DEGREE_NAME == ddlDegree.SelectedItem.ToString());




                TBL_CLASS cs = new TBL_CLASS()
                {
                    CLASS_ID = NewID,
                    CLASS_NAME = txtClName.Text,
                    SEMESTER = txtClSemester.Text,
                    SESSION = txtClSession.Text,
                    SECTION = txtClSection.Text,
                    SHIFT = txtClShift.Text,
                    DEGREE_FID = Degree.Object.DEGREE_ID,
                    

                };

                await App.firebaseDatabase.Child("TBL_CLASS").PostAsync(cs);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Class Added  ", "Ok");

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
