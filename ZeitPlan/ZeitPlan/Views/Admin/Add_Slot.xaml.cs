using Firebase.Database.Query;
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
    public partial class Add_Slot : ContentPage
    {
        public Add_Slot()
        {
            InitializeComponent();
            // LoadData();
        }
        //async void LoadData()
        //{
        //var firebaseList = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Select(x => new TBL_SLOT
        //{

        //    SLOT_NAME = x.Object.SLOT_NAME,
        //    SLOT_START_TIME = x.Object.SLOT_START_TIME,
        //    SLOT_END_TIME = x.Object.SLOT_END_TIME,

        //}).ToList();
        //var refinedList = firebaseList.Select(x => x.SLOT_NAME).ToList();
        //lblSlotName.ItemsSource = refinedList;
        // }



        private async void btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblSlotName.Text))
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }





                var check = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_NAME == lblSlotName.Text);

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.SLOT_NAME + "This Slot Name is already Picked .", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Max(a => a.Object.SLOT_ID);
                    NewID = ++LastID;
                }



                TBL_SLOT s = new TBL_SLOT()
                {
                    SLOT_ID = NewID,
                    SLOT_NAME = lblSlotName.Text,
                   
                    SLOT_START_TIME = tp.Time,
                    SLOT_END_TIME = tp1.Time
                };

                await App.firebaseDatabase.Child("TBL_SLOT").PostAsync(s);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Slot Added  ", "Ok");

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