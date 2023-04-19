using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class MessagesConsole
    {
        public static string ErrorUser() { return "Ошибка пользователя!"; }
        public static string ErrorWrite() { return "Ошибка записи в файл!"; }
        public static string ErrorAccessDenied() { return "У вас нет доступа к этой команде!"; }
        public static string Error() { return "Ошибка!"; }
    }
}
