using System;
using System.Collections.Generic;
using System.Text;

namespace ZeitPlan.View_Model
{
    class View_TimeTable
    {
        public int ID { get; set; }
        public string CLASS_NAME { get; set; }
        public string COURSE_NAME { get; set; }
        public string TEACHER_NAME { get; set; }
        public string SLOT_NAME { get; set; }
        public TimeSpan SLOT_START_TIME { get; set; }
        public TimeSpan SLOT_END_TIME { get; set; }
        public string DAY { get; set; }
        public DateTime DATE { get; set; }
        public string ROOM_NO { get; set; }
    }
}
