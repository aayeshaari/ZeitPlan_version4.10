
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
    public partial class CourseAssign_detail : ContentPage
    {
        public CourseAssign_detail(TBL_COURSE_ASSIGN ca)
        {
            InitializeComponent();
            LoadData(ca);
           
        }
        async void LoadData(TBL_COURSE_ASSIGN ca)
        {
            LoadingInd.IsRunning = true;
            try
            {
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == ca.CLASS_FID);
                var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == ca.COURSE_FID);
                CourseName.Text = Course.Object.COURSE_NAME;
                ClassName.Text = Class.Object.CLASS_NAME;
            }
            catch(Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }
    }
}