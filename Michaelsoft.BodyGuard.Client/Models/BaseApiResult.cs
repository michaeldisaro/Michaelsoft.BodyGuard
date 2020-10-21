namespace Michaelsoft.BodyGuard.Client.Models
{
    public class BaseApiResult
    {

        public bool Success { get; set; }

        public string Message { get; set; }
        
        public string Bearer { get; set; }
        
        public dynamic Response { get; set; }

    }
}