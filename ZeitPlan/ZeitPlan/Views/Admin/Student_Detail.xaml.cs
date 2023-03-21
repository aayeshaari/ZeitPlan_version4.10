using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.View_Model;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Student_Detail : ContentPage
    {
        public Student_Detail(TBL_STUDENT s)
        {
            InitializeComponent();
            LoadData(s);


        }
        async void LoadData(TBL_STUDENT s)
        {
            try
            {



                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == s.CLASS_FID);
                StudentName.Text = s.STUDENT_NAME;
                StudentEmail.Text = s.STUDENT_EMAIL;
                StudentPassword.Text = s.STUDENT_PASSWORD;
                ClassName.Text = Class.Object.CLASS_NAME;
                Status.Text = s.Status;

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