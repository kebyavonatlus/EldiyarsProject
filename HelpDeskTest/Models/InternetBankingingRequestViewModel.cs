namespace HelpDeskTest.Models
{
    public class InternetBankingingRequestViewModel
    {
        public Request Request { get; set; } = new Request();
        public ResetUserPasswordRequest IBank { get; set; } = new ResetUserPasswordRequest();
    }
}