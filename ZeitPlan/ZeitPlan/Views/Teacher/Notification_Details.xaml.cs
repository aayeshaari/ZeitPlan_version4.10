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
    public partial class Notification_Details : ContentPage
    {
        public Notification_Details(TBL_NOTICEBOARD r)
        {
            InitializeComponent();
            LoadData(r);
        }
        async void LoadData(TBL_NOTICEBOARD r)
        {
            try
            {
                //var Student = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_ID == r.StdID);
                //var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == Student.Object.CLASS_FID);

                txtDate.Text = r.NOTI_DATE;
                txtTime.Text = r.NOTI_TIME;
                txtTo.Text = r.NOTI_TO;
                txtPriority.Text = r.NOTI_PRIORITY;
                txtStatus.Text = r.NOTI_STATUS;
                txtTo.Text = r.NOTI_TO;
                txtBody.Text = r.NOTI_TEXT;
                NtImage.Source = r.NITI_IMAGE;
               
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
            }
        }
    }
}
