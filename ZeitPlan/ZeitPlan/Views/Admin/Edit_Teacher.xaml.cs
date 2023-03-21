using Firebase.Database.Query;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class Edit_Teacher : ContentPage
	{
        private MediaFile _mediaFile;
        public static string PicPath = "image_picker";
        public static TBL_TEACHER Tech = null;
        public static bool isNewPicSeleted = false;
        public Edit_Teacher(TBL_TEACHER t)
        {
            InitializeComponent();
            Tech = t;
            txtTechName.Text = t.TEACHER_NAME;
            txtTechEmail.Text = t.TEACHER_EMAIL;
            txtTechPassword.Text = t.TEACHER_PASSWORD;
            txtTechPhone.Text = t.TEACHER_PHNO;
            txtTechAdress.Text = t.TEACHER_ADDRESS;
            Previewpic.Source = t.Image;
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
            var firebaseList = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Select(x => new TBL_DEPARTMENT
            {

                DEPARTMENT_NAME = x.Object.DEPARTMENT_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEPARTMENT_NAME).ToList();
            ddlDepartment.ItemsSource = refinedList;
            //TEACHER_COURSE_ASSIGN_FID
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select image source", "Close", "", "From Galary", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not take photo supported ", "ok");
                        return;
                    }
                    var mediaoptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaoptions);
                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error taking img.... ", "ok");
                        return;
                    }
                    _mediaFile = SelectedImg;
                    PicPath = SelectedImg.Path;
                    Previewpic.Source = SelectedImg.Path;
                }
                if (response == "From Galary")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }
                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    _mediaFile = SelectedImg;
                    PicPath = SelectedImg.Path;
                    Previewpic.Source = SelectedImg.Path;

                }
                isNewPicSeleted = true;

            }
            catch (Exception ex)
            {

                await DisplayAlert("Message", "Something went wrong\n" + ex.Message, "ok");
            }

        }

        private async void btncat_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTechName.Text) || string.IsNullOrEmpty(txtTechEmail.Text) || string.IsNullOrEmpty(txtTechPassword.Text) || string.IsNullOrEmpty(txtTechPhone.Text) || string.IsNullOrEmpty(txtTechAdress.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (ddlDepartment.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Degree and try again", "ok");
                    return;
                }
                string img = Tech.Image;
                if (isNewPicSeleted == true)
                {
                    var StoredImageURL = await App.FirebaseStorage
                  .Child("TechImages")
                  .Child(Tech.TEACHER_ID.ToString() + "_" + txtTechName.Text + ".jpg")
                  .PutAsync(_mediaFile.GetStream());
                    img = StoredImageURL;
                }



                var OldTech = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == Tech.TEACHER_ID);

                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == Tech.DEPARTMENT_FID);

                OldTech.Object.TEACHER_ID = Tech.TEACHER_ID;
                OldTech.Object.TEACHER_NAME = txtTechName.Text;
                OldTech.Object.TEACHER_EMAIL = txtTechEmail.Text;
                OldTech.Object.TEACHER_ADDRESS = txtTechAdress.Text;
                OldTech.Object.TEACHER_PASSWORD = txtTechPassword.Text;
                OldTech.Object.TEACHER_PHNO = txtTechPhone.Text;
                OldTech.Object.Image = img;
                OldTech.Object.DEPARTMENT_FID = Department.Object.DEPARTMENT_ID;
                //TEACHER_COURSE_ASSIGN_FID=selected1,


                await App.firebaseDatabase.Child("TBL_TEACHER").Child(OldTech.Key).PutAsync(OldTech.Object);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Teacher Updated  ", "Ok");
                await Navigation.PushAsync(new Manage_Teacher());

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