using Firebase.Database.Query;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.LoginSystem;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
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
            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {

                CLASS_NAME = x.Object.CLASS_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtCPassword.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (txtCPassword.Text != txtPassword.Text)
                {
                    await DisplayAlert("Error", "Password do not match", "ok");
                    return;
                }

                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Class and try again", "ok");
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

                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());


                TBL_STUDENT u = new TBL_STUDENT()
                {
                    STUDENT_ID = NewID,
                    STUDENT_NAME = txtName.Text,
                    STUDENT_EMAIL = txtEmail.Text,
                    STUDENT_PASSWORD = txtPassword.Text,
                    CLASS_FID = Class.Object.CLASS_ID,

                };

                await App.firebaseDatabase.Child("TBL_STUDENT").PostAsync(u);
                LoadingInd.IsRunning = false;
                //App.db.Insert(u);
                await DisplayAlert("Success", "Account Registered", "Ok");
                await Navigation.PushAsync(new Login("Student"));
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }

        //private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length<11 && e.NewTextValue.Length>13)
        //    {
        //        PhoneNumbervalid.IsVisible = true;
        //        PhoneNumbervalid.Text = "InValid Phone!";
        //        PhoneNumbervalid.TextColor = Color.Red;
        //    }
        //    else
        //    {
        //        PhoneNumbervalid.Text = "Valid Phone";
        //        PhoneNumbervalid.TextColor = Color.Green;
        //    }
        //}

        //private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
        //    if (Regex.IsMatch(e.NewTextValue, EmailPattern))
        //    {
        //        EmailValid.IsVisible = true;
        //        EmailValid.Text = "Valid Email";
        //        EmailValid.TextColor = Color.Green;
        //    }
        //    else
        //    {
        //        EmailValid.Text = "InValid Email! Email must contain @ and .com";
        //        EmailValid.TextColor = Color.Red;
        //    }
        //}

        //private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length < 8)
        //    {
        //        PasswordValid.IsVisible = true;
        //        PasswordValid.Text = "Password must be at least 8 characters and dosn't contain underscore(_)";
        //        PasswordValid.TextColor = Color.Red;
        //    }
        //    var passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
        //    if (Regex.IsMatch(e.NewTextValue, passwordPattern))
        //    {

        //        PasswordValid.Text = "Strong Password";
        //        PasswordValid.TextColor = Color.Green;
        //    }
        //    else
        //    {
        //        PasswordValid.Text = "Weak Password! Password must  contain Special_characters(@$!%*#?&),uppercase_letters,Lowercase_letters,Number(s)";
        //        PasswordValid.TextColor = Color.Red;
        //    }
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(""));
        }

        private void btnshoepassword_Clicked(object sender, EventArgs e)
        {
            if (txtPassword.IsPassword == true)
            {
                txtPassword.IsPassword = false;
                btnshoepassword.Source = "eyeshow.jpg";
            }
            else
            {
                txtPassword.IsPassword = true;
                btnshoepassword.Source = "eyenot.png";
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
               if (Regex.IsMatch(e.NewTextValue, EmailPattern))
            {
                lblEmailValid.IsVisible = true;
                lblEmailValid.Text = "Valid Email";
                lblEmailValid.TextColor = Color.Green;
            }
            else
            {
                lblEmailValid.Text = "InValid Email! Email must contain @ and .com";
                lblEmailValid.TextColor = Color.Red;
            }
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 8)
            {
                lblPasswordValid.IsVisible = true;
                lblPasswordValid.Text = "Password must be at least 8 characters and dosn't contain underscore(_)";
                lblPasswordValid.TextColor = Color.Red;
            }
            var passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
            if (Regex.IsMatch(e.NewTextValue, passwordPattern))
            {

                lblPasswordValid.Text = "Strong Password";
                lblPasswordValid.TextColor = Color.Green;
            }
            else
            {
                lblPasswordValid.Text = "Weak Password! Password must  contain Special_characters(@$!%*#?&),uppercase_letters,Lowercase_letters,Number(s)";
                lblPasswordValid.TextColor = Color.Red;
            }
        }
    }
}