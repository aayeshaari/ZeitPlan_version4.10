using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Login_System
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        
        public Logout()
        {
            
            InitializeComponent();
        }

        private  void Button_Clicked(object sender, EventArgs e)
        {

            
                App.db.DropTable<TBL_TEACHER>();
           
           
                App.db.DropTable<TBL_STUDENT>();
           
                App.db.DropTable<users>();
           
            App.Current.MainPage = new MainPage();
        }
    }
}