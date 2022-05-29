namespace KO.Framework.Web
{
    public class JsonApiData
    {

        public enum Result
        {
            Ok = 0,
            Error = -1
        }

        public JsonApiData()
        {
            result = Result.Ok;
            message = string.Empty;
        }

        public Result result { get; set; }

        public string message { get; set; }

        public dynamic content;

        
    }
}
