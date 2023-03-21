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
    public partial class TImeTable_Detail : ContentPage
    {
        public TImeTable_Detail(TBL_TIMETABLE t)
        {
            InitializeComponent();
            TimeTableID.Text = t.TIMETABLE_ID.ToString();
            ClassFID.Text = t.CLASS_FID.ToString();
            CourseFID.Text = t.CLASS_FID.ToString();
            TeacherFID.Text = t.TEACHER_FID.ToString();
            RoomFID.Text = t.ROOM_FID.ToString();
           SlotFID.Text = t.SLOT_FID.ToString();
        }
    }
}