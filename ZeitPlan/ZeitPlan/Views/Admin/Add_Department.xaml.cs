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
    public partial class Add_Department : ContentPage
    {
        public Add_Department()
        {
            InitializeComponent();
        }

        private async void btnDepartmet_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDtName.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }

                var check = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_NAME == txtDtName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.DEPARTMENT_NAME + "This DEPARTMENT_Name is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Max(a => a.Object.DEPARTMENT_ID);
                    NewID = ++LastID;
                }
                TBL_DEPARTMENT d = new TBL_DEPARTMENT()
                {
                    DEPARTMENT_ID = NewID,
                    DEPARTMENT_NAME = txtDtName.Text,

                    
                };

                await App.firebaseDatabase.Child("TBL_DEPARTMENT").PostAsync(d);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Department Added  ", "Ok");

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
