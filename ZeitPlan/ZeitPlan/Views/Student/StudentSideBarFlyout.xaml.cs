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

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public StudentSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new StudentSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class StudentSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<StudentSideBarFlyoutMenuItem> MenuItems { get; set; }

            public StudentSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<StudentSideBarFlyoutMenuItem>(new[]
                {
                    new StudentSideBarFlyoutMenuItem { Id = 0, Icon="ic_home.png"  , Title = "Home" ,TargetType=typeof(StudentHome) },
                    new StudentSideBarFlyoutMenuItem { Id = 0, Icon="ic_profile"  , Title = "Profile" ,TargetType=typeof(Student_Profile) },
                   
                    new StudentSideBarFlyoutMenuItem { Id = 1, Icon="ic_table.png", Title = "View TimeTable",TargetType=typeof(View_TimeTable) },
                    new StudentSideBarFlyoutMenuItem { Id = 1, Icon="ic_noti.png", Title = "Notification",TargetType=typeof(Manage_Notification) },
               
                    new StudentSideBarFlyoutMenuItem { Id = 5, Icon="ic_email", Title = "Request portal", TargetType=typeof(Request_Portol) },
                    new StudentSideBarFlyoutMenuItem { Id = 4, Icon="ic_phone", Title = "Contact Us", TargetType=typeof(Contact) },
                    new StudentSideBarFlyoutMenuItem { Id = 5, Icon="ic_group", Title = "About Us", TargetType=typeof(About) },

                    new StudentSideBarFlyoutMenuItem { Id = 6, Icon="delete.png", Title="Log out" , TargetType=typeof(Logout) },
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