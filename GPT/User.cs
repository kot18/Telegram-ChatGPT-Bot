using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class User
    {
        public static long ChatId { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Message { get; set; }

        public static string ReturnUserInfo()
        {
            return $"ID пользователя: {ChatId},\n Имя пользрвателя: {FirstName},\n Фамилия пользователя: {LastName},\n Текст сообщения: {Message}";
        }
    }
}
