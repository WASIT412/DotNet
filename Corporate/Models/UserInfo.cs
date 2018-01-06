using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public sealed class UserInfo
    {
        // Singlton 
        private static UserInfo instence = null;
        public static UserInfo GetInstence
        {
            get
            {
                if (instence == null)
                    instence = new UserInfo();
                return instence;
            }
        }
        public int UserID{ get; set;}
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
    }
}