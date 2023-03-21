
    using System;
    using System.Collections.Generic;


public partial class TBL_TIMETABLE
{
    public int TIMETABLE_ID { get; set; }
    public string DAY { get; set; }
    public int TEACHER_FID { get; set; }
    public int CLASS_FID { get; set; }
    public int SLOT_FID { get; set; }
    public int ROOM_FID { get; set; }
    public int COURSE_FID { get; set; }
   public DateTime DATE{ get; set; }
   
}

