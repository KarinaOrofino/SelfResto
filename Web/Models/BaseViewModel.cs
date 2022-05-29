namespace Web.Models
{
    public abstract class BaseViewModel
    {
        public virtual string ToJsonModel()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}