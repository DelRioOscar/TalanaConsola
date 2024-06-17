namespace Talana.Response
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool company { get; set; }
        public bool identifier { get; set; }
        public object zendesk_params { get; set; }
    }
}
