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
    public partial class Teacher_Detail : ContentPage
    {
        public Teacher_Detail(TBL_TEACHER t)
        {
            InitializeComponent();
            LoadData(t);
        }
        async void LoadData(TBL_TEACHER t)
        {
            try
            {
               var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == t.DEPARTMENT_FID);
                //TeacherID.Text = t.TEACHER_ID.ToString();
                if (t.Image != null)
                {
                    TeacherImage.Source = t.Image;
                }
                TeacherName.Text = t.TEACHER_NAME;
                TeacherEmail.Text = t.TEACHER_EMAIL;
                TeacherPassword.Text = t.TEACHER_PASSWORD;
                TeacherPhone.Text = t.TEACHER_ADDRESS;
                TeacherAddress.Text = t.TEACHER_ADDRESS;
                DepartmentName.Text = Department.Object.DEPARTMENT_NAME;
                txtStatus.Text = t.Status;

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