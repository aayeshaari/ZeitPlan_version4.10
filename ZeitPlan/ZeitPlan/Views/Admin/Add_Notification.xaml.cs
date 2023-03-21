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
using ZeitPlan.View_Model;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Notification : ContentPage
    {
        Xamarin.Essentials.FileResult pic = null;
        public Add_Notification()
        {
            InitializeComponent();

        }

        private async void sendbtn_Clicked(object sender, EventArgs e)

        {
            try
            {
                if (string.IsNullOrEmpty(txtBody.Text) || ddltype.SelectedItem == null || ddlPrio.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "Ok");
                    return;
                }



                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_NOTICEBOARD").OnceAsync<TBL_NOTICEBOARD>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_NOTICEBOARD").OnceAsync<TBL_NOTICEBOARD>()).Max(a => a.Object.NOTI_ID);
                    NewID = ++LastID;
                }

                string StoredImageURL = "NA";

                if (pic != null)
                {
                    var stream = await pic.OpenReadAsync();

                    StoredImageURL = await App.FirebaseStorage
                      .Child("ItemImages")
                      .Child(NewID + "_" + "_Notification_" + DateTime.Now.ToShortDateString() + ".jpg")
                   .PutAsync(stream);
                }
                else
                {
                    StoredImageURL = "push_notifi.jpg";
                }


                var to = ddltype.SelectedItem.ToString();
                var prio = ddlPrio.SelectedItem.ToString();

                TBL_NOTICEBOARD t = new TBL_NOTICEBOARD()
                {
                    
                    NOTI_ID = NewID,
                    NITI_IMAGE = StoredImageURL,
                    NOTI_DATE = DateTime.Now.ToShortDateString(),
                    NOTI_TIME = DateTime.Now.ToShortTimeString(),
                    NOTI_STATUS = "Active",
                    NOTI_TEXT = txtBody.Text,
                    NOTI_PRIORITY = prio,
                    NOTI_TO = to,

                };
                


                await App.firebaseDatabase.Child("TBL_NOTICEBOARD").PostAsync(t);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Request Sent  ", "Ok");
                //App.Current.MainPage = new Manage_Notification();
            }
            catch (Exception ex)
            {

                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Select Image From", "Close", "", "Camera", "Gallery");
            if (action == "Camera")
            {
                pic = await Xamarin.Essentials.MediaPicker.CapturePhotoAsync();
            }
            if (action == "Gallery")
            {
                pic = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            }

            if (pic != null)
            {
                PickPlaceHolder.Source = pic.FullPath;
            }
           
        }

    }
}