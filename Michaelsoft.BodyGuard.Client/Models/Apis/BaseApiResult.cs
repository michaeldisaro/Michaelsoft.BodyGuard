namespace Michaelsoft.BodyGuard.Client.Models.Apis
{
    public class BaseApiResult
    {

        public bool Success { get; set; }

        public string Message { get; set; }
        
        public dynamic Response { get; set; }

    }
}