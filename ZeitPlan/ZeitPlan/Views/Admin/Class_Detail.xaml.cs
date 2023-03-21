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
    public partial class Class_Detail : ContentPage
    {
        public Class_Detail(TBL_CLASS c)
        {
            InitializeComponent();
            LoadData(c);
        }

        async void LoadData(TBL_CLASS c)
        {
            try
            {
                 var Degree = (await App.firebaseDatabase.Child("TBL_DEGREE").OnceAsync<TBL_DEGREE>()).FirstOrDefault(x => x.Object.DEGREE_ID ==c .DEGREE_FID);

                ClassName.Text = c.CLASS_NAME;
                Section.Text = c.SECTION;
                Session.Text = c.SESSION;
                Shift.Text = c.SHIFT;
                Semester.Text = c.SEMESTER;
                DegreeName.Text = Degree.Object.DEGREE_NAME;
               
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
