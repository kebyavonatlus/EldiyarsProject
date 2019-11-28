using System.ComponentModel;

namespace HelpDeskTest.Enums
{
    public enum RequestType
    {
        
        [Description("Заявка на Сброс пользовательского пароля")]
        IBankRequest = 1,

        [Description("Заявка на доступ")]
        AccessRequest = 2
    }
}