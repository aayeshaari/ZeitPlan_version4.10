using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeitPlan.Models
{
   public class users
    {
        [PrimaryKey,AutoIncrement]
        public int  UsersId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

    }
}
