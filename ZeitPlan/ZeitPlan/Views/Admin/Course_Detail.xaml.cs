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
    public partial class Course_Detail : ContentPage
    {
        public Course_Detail(TBL_COURSE c)
        {
            InitializeComponent();
          
            CourseName.Text = c.COURSE_NAME;
            CreditHours.Text = c.CREDIT_HOURS;
            
            //DegreeFID.Text = c.DEGREE_FID;
        }
    }
}