namespace AngelsAutomotive.Helpers
{
    public interface IMailHelper //used to send confirmation emails to new users
    {
        void SendMail(string to, string subject, string body);
    }
}
