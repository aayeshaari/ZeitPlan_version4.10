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
    public partial class TeacherCourseAssign_Detail : ContentPage
    {
        public TeacherCourseAssign_Detail(TBL_TEACHER_CLASS_ASSIGN tc)
        {
            InitializeComponent();
            LoadData(tc);
        }
        async void LoadData(TBL_TEACHER_CLASS_ASSIGN tc)
        {
            LoadingInd.IsRunning = true;
            try
            {
              
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == tc.CLASS_FID);
                var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == tc.TEACHER_FID);
                CourseName.Text = Class.Object.CLASS_NAME;
                TeacherName.Text = Teacher.Object.TEACHER_NAME;
                LoadingInd.IsRunning = false;
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