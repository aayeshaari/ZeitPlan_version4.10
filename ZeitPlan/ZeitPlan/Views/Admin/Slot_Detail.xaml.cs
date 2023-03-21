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
    public partial class Slot_Detail : ContentPage
    {
        public Slot_Detail(TBL_SLOT s)
        {
            InitializeComponent();
     
            SlotName.Text = s.SLOT_NAME;
           
            Slot_Start_Time.Text = s.SLOT_START_TIME.ToString();
            Slot_End_Time.Text = s.SLOT_END_TIME.ToString();

        }
    }
}