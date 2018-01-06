using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public class CorparateResult <T>
    {

       
            public CorparateResult()
            {
                StringDataList = new List<string>();
               Exist = false;
                Status = Constants.CorparateStatus.Successful;
               // Array = new Array[] { };
                Message = "Sorry, we are unable to process your request at this time, please try again later.";
            }
            public Constants.CorparateStatus Status { get; set; }
            public bool Exist { get; set; }
           // public Array Array { get; set; }
           // public bool IsUserSessionExpired { get; set; }
            public string Message { get; set; }

          public List<string> StringDataList { get; set; }
          public  List<T> GenericList { get; set; }
          public T GenericOne { get; set; }
        }

    }

