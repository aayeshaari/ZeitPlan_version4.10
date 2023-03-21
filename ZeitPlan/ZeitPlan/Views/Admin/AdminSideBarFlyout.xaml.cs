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

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public AdminSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class AdminSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminSideBarFlyoutMenuItem> MenuItems { get; set; }

            public AdminSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminSideBarFlyoutMenuItem>(new[]
                {
                     new AdminSideBarFlyoutMenuItem { Id = 0,  Icon="ic_home" , Title = "Home" , TargetType=typeof(AdminHome) },
                     new AdminSideBarFlyoutMenuItem { Id = 1, Icon="ic_profile" , Title = "Profile" , TargetType=typeof(Profile) },
                   new AdminSideBarFlyoutMenuItem { Id = 2,  Icon="ic_noti", Title = "Add Notification" , TargetType=typeof(Add_Notification) },
                  new AdminSideBarFlyoutMenuItem { Id = 3,  Icon="ic_mangtechcourse", Title = "Manage Notification" , TargetType=typeof(Manage_Notification) },
                   new AdminSideBarFlyoutMenuItem { Id = 2,  Icon="ic_dept", Title = "Add Department" , TargetType=typeof(Add_Department) },
                  new AdminSideBarFlyoutMenuItem { Id = 3,  Icon="ic_mangdpt", Title = "Manage Department" , TargetType=typeof(Manage_Department) },
                  new AdminSideBarFlyoutMenuItem { Id =4,  Icon="ic_degree", Title = "Add Degree" , TargetType=typeof(Add_Degree) },
                  new AdminSideBarFlyoutMenuItem { Id =5,  Icon="ic_mangdeg", Title = "Manage Degree" , TargetType=typeof(Manage_Degree) },
                  new AdminSideBarFlyoutMenuItem { Id = 6,  Icon="ic_class", Title = "Add Class" , TargetType=typeof(Add_Class) },
                   new AdminSideBarFlyoutMenuItem { Id = 7,  Icon="ic_mangClass", Title = "Manage Class" , TargetType=typeof(Manage_Class) },
                   new AdminSideBarFlyoutMenuItem { Id = 8, Icon="ic_diary", Title = "Add Course" , TargetType=typeof(Add_course) },
                   new AdminSideBarFlyoutMenuItem { Id = 9,  Icon="ic_mangiary", Title = "Manage Course" , TargetType=typeof(Manage_Course) },
                   new AdminSideBarFlyoutMenuItem { Id = 10,  Icon="ic_classcourse", Title = "Assign Course to class" , TargetType=typeof(Assign_Course_To_Class) },
                  new AdminSideBarFlyoutMenuItem { Id = 11,  Icon="ic_mangCoCl", Title = "Manage Course Assign" , TargetType=typeof(Manage_Course_Assign) },
  
                  new AdminSideBarFlyoutMenuItem { Id = 13, Icon="ic_mangpro",  Title = "Manage Teacher" , TargetType=typeof(Manage_Teacher) },

                  //new AdminSideBarFlyoutMenuItem { Id = 13, Icon="ic_group",  Title = "Manage Student" , TargetType=typeof(Manage_Student) },
                  new AdminSideBarFlyoutMenuItem { Id = 14,  Icon="ic_classteacher", Title = "Assign Class to Teacher" , TargetType=typeof(Assign_Course_To_Teacher) },
                  new AdminSideBarFlyoutMenuItem { Id = 15,  Icon="ic_mangtechcourse", Title = "Manage Teacher Class Assign" , TargetType=typeof(Mange_Teacher_Assign) },
                  new AdminSideBarFlyoutMenuItem { Id = 16,  Icon="ic_room", Title = "Add Room" , TargetType=typeof(Add_Room) },
                  new AdminSideBarFlyoutMenuItem { Id = 17,  Icon="ic_mangroom", Title = "Manage Room" , TargetType=typeof(Manage_Room) },
                  new AdminSideBarFlyoutMenuItem { Id = 18,  Icon="ic_slot", Title = "Add Slot" , TargetType=typeof(Add_Slot) },
                   new AdminSideBarFlyoutMenuItem { Id = 19,  Icon="ic_mangslot", Title = "Manage Slot" , TargetType=typeof(Manage_Slot) },
                   new AdminSideBarFlyoutMenuItem { Id = 18,  Icon="ic_profile", Title = "Add Admin" , TargetType=typeof(Add_Admin) },
                   new AdminSideBarFlyoutMenuItem { Id = 19,  Icon="ic_edit", Title = "Manage Admins" , TargetType=typeof(Manage_Admin) },
                    new AdminSideBarFlyoutMenuItem { Id = 20,  Icon="ic_table", Title = "Create TIMETABLE" , TargetType=typeof(Create_Time_Table) },
                  new AdminSideBarFlyoutMenuItem { Id = 21,  Icon="ic_mangtime", Title = "Manage TIMETABLE" , TargetType=typeof(Mange_TimeTable) },
                  new AdminSideBarFlyoutMenuItem { Id = 23,  Icon="ic_mangresp", Title = "Manage Message " , TargetType=typeof(Manage_Requests) },
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