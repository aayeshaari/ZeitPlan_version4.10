using Firebase.Database;
using Firebase.Storage;
using Plugin.FirebasePushNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using ZeitPlan.LoginSystem;
using ZeitPlan.Models;
using ZeitPlan.Views.Admin;
using ZeitPlan.Views.Student;
using ZeitPlan.Views.Teacher;

namespace ZeitPlan
{
    public partial class App : Application
    {
        public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbZeitPlan.db3");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);

        //Firebase Connections  ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("zeitplan-1fc20.appspot.com");

        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://zeitplan-1fc20-default-rtdb.firebaseio.com/");

        public static TBL_TEACHER LoggedInUser = null;
        public static TBL_STUDENT LoggedInStudent = null;
        public static users LoggedInAdmin = null;

       

        public static string Data { get; set; }
        public App(bool hasNotification = false, IDictionary<string, object> notificationData = null)
        {
            InitializeComponent();
            Device.SetFlags(new[] { "MediaElement_Experimental", "Brush_Experimental" });


            if (!hasNotification)
                //MainPage = new MainPage();
                MainPage = new SplashScreen();
            else
            {
                foreach (var data in notificationData)
                {
                    if (data.Key == "LoginPage")
                    {
                        MainPage = new Login("");
                        return;
                    }
                }
            }
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
        }




        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
