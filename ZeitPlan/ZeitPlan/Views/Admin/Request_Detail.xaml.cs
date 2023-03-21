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
    public partial class Request_Detail : ContentPage
    {
        public Request_Detail(TBL_REQUEST_PORTAL r)
        {
            InitializeComponent();
            LoadData(r);
        }
        async void LoadData(TBL_REQUEST_PORTAL r)
        {
            try
            {
                var Student = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_ID == r.StdID);
                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_ID == Student.Object.CLASS_FID);

                txtTo.Text = r.TO;
                txtSubject.Text = r.SUBJECT;
                txtBody.Text = r.BODY;
                txtStudentName.Text = Student.Object.STUDENT_NAME;
                txtStudentClass.Text = Class.Object.CLASS_NAME;

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
            }
        }
    }
}
