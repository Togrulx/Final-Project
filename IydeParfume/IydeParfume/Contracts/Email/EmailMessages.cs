using IydeParfume.Contracts.Email;

namespace IydeParfume.Contracts.Email
{
    public class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Activation account";
            public const string NOTIFICATION_MESSAGE = $"Sifarishinizin Statusu Yenilendi";

        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Your activation url : {EmailMessageKeyword.ACTIVATION_URL}";
        }
    }
}
