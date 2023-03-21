using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Login_System;
using System.ComponentModel;
using Firebase.Database.Query;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Request_Portol : ContentPage
    {
        //SmtpClient SmtpServer;
        public Request_Portol()
        {
            InitializeComponent();
        }
       
        private async void sendbtn_Clicked(object sender, EventArgs e)

        {
            try
            {
                if (string.IsNullOrEmpty(txtTo.Text) || string.IsNullOrEmpty(txtBody.Text) || ddltype.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }


                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).Max(a => a.Object.REQUEST_PORTAL_ID);
                    NewID = ++LastID;
                }

                TBL_REQUEST_PORTAL t = new TBL_REQUEST_PORTAL()
                {
                    REQUEST_PORTAL_ID = NewID,
                    TO = txtTo.Text,
                    SUBJECT = ddltype.SelectedItem.ToString(),
                    BODY = txtBody.Text,
                    DATE = DateTime.Now.ToShortDateString(),
                    Time = DateTime.Now.ToShortTimeString(),
                    StdID = App.LoggedInStudent.STUDENT_ID
                };

                await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").PostAsync(t);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Request Sent  ", "Ok");
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