using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Login_System;
using ZeitPlan.Views.Admin;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public TeacherSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new TeacherSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class TeacherSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<TeacherSideBarFlyoutMenuItem> MenuItems { get; set; }

            public TeacherSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<TeacherSideBarFlyoutMenuItem>(new[]
                {
                    new TeacherSideBarFlyoutMenuItem { Id = 0, Icon="ic_home"  , Title = "Home" ,TargetType=typeof(TeacherHome) },
                    new TeacherSideBarFlyoutMenuItem { Id = 0, Icon="ic_Profile.png"  , Title = "Profile" ,TargetType=typeof(Teacher_Profile) },

                    new TeacherSideBarFlyoutMenuItem { Id = 1, Icon="ic_table", Title = "View TimeTable",TargetType=typeof(View_TimeTable) },

                    new TeacherSideBarFlyoutMenuItem { Id = 6, Icon="ic_list.png", Title = "Student List", TargetType=typeof(Student_List) },
                      new TeacherSideBarFlyoutMenuItem { Id = 6, Icon="ic_mangpro.png", Title = "Manage Student", TargetType=typeof(Manage_Student)},
                    new TeacherSideBarFlyoutMenuItem { Id = 6, Icon="ic_noti.png", Title = "Notification", TargetType=typeof(Manage_Notification) },

                    //new TeacherSideBarFlyoutMenuItem { Id = 7, Icon="ic_mangClass", Title = "Manage Student", TargetType=typeof(Manage_Student) },
                             new TeacherSideBarFlyoutMenuItem { Id = 6, Icon="ic_email.png", Title = "Request Portal", TargetType=typeof(Request_Portol) },
                     new TeacherSideBarFlyoutMenuItem { Id = 8, Icon="ic_phone", Title = "Contact Us", TargetType=typeof(Contact) },
                      new TeacherSideBarFlyoutMenuItem { Id = 9, Icon="ic_group", Title = "About Us", TargetType=typeof(About) },
                       new TeacherSideBarFlyoutMenuItem { Id = 10, Icon="delete.png", Title = "Log out", TargetType=typeof(Logout) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}