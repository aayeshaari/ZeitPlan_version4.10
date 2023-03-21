using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitPlan.Views.Admin
{
    public class AdminSideBarFlyoutMenuItem
    {
        public AdminSideBarFlyoutMenuItem()
        {
            TargetType = typeof(AdminSideBarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}