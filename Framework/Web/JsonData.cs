using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web
{
    public class JsonData
    {
        public enum Result
        {
            Ok = 0,
            Error = -1,
            Redirect = 1,
            ModelValidation = 2
        }

        public JsonData()
        {
            result = 0;
            count = 0;
            redirect = string.Empty;
            error = string.Empty;
            errorUi = string.Empty;
        }

        //
        public Result result { get; set; }
        public int count { get; set; }
        public string redirect { get; set; }
        public string target { get; set; }
        public dynamic content { get; set; }
        public string error { get; set; }
        public string errorUi { get; set; }
        public dynamic errors { get; set; }
    }
}
