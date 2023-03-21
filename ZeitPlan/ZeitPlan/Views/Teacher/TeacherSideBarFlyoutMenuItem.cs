using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitPlan.Views.Teacher
{
    public class TeacherSideBarFlyoutMenuItem
    {
        public TeacherSideBarFlyoutMenuItem()
        {
            TargetType = typeof(TeacherSideBarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}