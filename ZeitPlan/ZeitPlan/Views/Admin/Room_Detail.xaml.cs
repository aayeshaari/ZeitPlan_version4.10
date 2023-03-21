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
    public partial class Room_Detail : ContentPage
    {
        public Room_Detail(TBL_ROOM R)
        {
            InitializeComponent();
            LoadData(R);

        }
        async void LoadData(TBL_ROOM R)
        {
            try
            {
                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID == R.DEPARTMENT_FID);
                
               
                RoomNumber.Text = R.ROOM_ID.ToString();
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