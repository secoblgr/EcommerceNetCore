namespace WebApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public  string err { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
