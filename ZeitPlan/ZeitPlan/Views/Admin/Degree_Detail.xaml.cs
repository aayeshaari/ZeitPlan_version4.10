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
    public partial class Degree_Detail : ContentPage
    {
        public Degree_Detail(TBL_DEGREE d)
        {
            InitializeComponent();
            LoadData(d);

        }
        async void LoadData(TBL_DEGREE d)
        {
            try
            {


              
                    var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == d.DEPARTMENT_FID);
                DegreeName.Text = d.DEGREE_NAME;
                DepartmentName.Text = Department.Object.DEPARTMENT_NAME;
                   

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