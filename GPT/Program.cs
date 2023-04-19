using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Telegram.Bot.Types;
using System.Threading;
using System.IO;
using Telegram.Bot.Types.Payments;

namespace GPT
{
    class Program
    {
        static TelegramBotClient botClient;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Бот запускается. Идёт проверка ключей!");
            Console.ResetColor();

            if (ReadKey.ReadApi())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Бот запущен!");
                Console.ResetColor();
            }

            else { Console.ReadKey(); return; }

            botClient = new TelegramBotClient(Tokens.BotToken);

            botClient.StartReceiving(Update, Error);

            Console.ReadKey();
        }

        /// <summary>
        /// Метод обработки ошибок в работе бота
        /// </summary>
        /// <param name="botClient">Переменная бота использыуется, например, для обработки ответов</param>
        /// <param name="exp"></param>
        /// <param name="token">Переменная бота хранти токен бота</param>
        /// <returns></returns>
        private async static Task Error(ITelegramBotClient botClient, Exception exp, CancellationToken token)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка!");
            Console.WriteLine($"{exp}");
            Console.ResetColor();

            return;
        }

        /// <summary>
        /// Асинхранный метод для работы с пользователем. Тут обрабатываются все команды пользователя.
        /// </summary>
        /// <param name="botClient">Переменная бота использыуется, например, для обработки ответов</param>
        /// <param name="update">Переменная бота использвется, например, для получения новых соббщений</param>
        /// <param name="token">Переменная бота хранти токен бота</param>
        /// <returns></returns>
        private async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            //Start-------------------------------------------------------------------------------------------------------Заполнение класса User для последующей работы с пользователем

            var Message = update.Message;

            try
            {
                User.ChatId = Message.Chat.Id;
                User.FirstName = Message.Chat.FirstName;
                User.LastName = Message.Chat.LastName;
                User.Message = Message.Text;
            }

            catch (NullReferenceException ex) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(ex); Console.ResetColor(); return; }

            if (User.Message == "")
                return;

            //End-------------------------------------------------------------------------------------------------------Заполнение класса User для последующей работы с пользователем

            //Start-------------------------------------------------------------------------------------------------------Предварительный ответ на сообщение пользователя

            if (User.Message != null)
            {
                await botClient.SendTextMessageAsync(User.ChatId, "Обработка сообщения, подождите!");
                //await botClient.SendChatActionAsync(User.ChatId, chatAction: Telegram.Bot.Types.Enums.ChatAction.Typing);
            }

            else { return; }

            //End-------------------------------------------------------------------------------------------------------Предварительный ответ на сообщение пользователя

            //Start-------------------------------------------------------------------------------------------------------Команды бота

            if (User.Message == "/start")
            {
                await botClient.SendTextMessageAsync(User.ChatId, "Бот ждёт вашего вопроса!");
                return;
            }

            try
            {
                if (User.Message == "/SendMessage" && User.ChatId == 5615841140)
                {
                    await botClient.SendTextMessageAsync(User.ChatId, "Введи через амперсант (&) сначала id получателя, затем текст без пробелов:");
                    string TextMessage = await AwaitResponce.UserResponce(botClient);

                    string[] messageAndId = TextMessage.Split('&');

                    await Manually.SendMessageUser(botClient, Convert.ToInt64(messageAndId[0]), messageAndId[1]);
                    await botClient.SendTextMessageAsync(User.ChatId, $"Сообщение с текстом {messageAndId[1]} для пользователя с ID {messageAndId[0]} отправлено!");

                    return;
                }

                else if (User.Message != "/SendMessage" && User.ChatId != 5615841140) { await botClient.SendTextMessageAsync(User.ChatId, "У вас нет доступа к этой команде!"); return; }
            }

            catch (Exception ex) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(ex); Console.ResetColor(); return; }

            //End-------------------------------------------------------------------------------------------------------Команды бота

            //Start-------------------------------------------------------------------------------------------------------Ответ бота

            var result = await AwaitResponce.BotResponce(User.Message);
            await botClient.SendTextMessageAsync(User.ChatId, result);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{User.ReturnUserInfo()}");
            Console.ResetColor();

            Console.WriteLine($"Ответ бота: {result}");

            await SaveChatHistory(User.ChatId, User.Message, User.FirstName, result.ToString());

            return;

            //End-------------------------------------------------------------------------------------------------------Ответ бота
        }

        /// <summary>
        /// Асинхронный метод использвуется для сохранения чата пользователя
        /// </summary>
        /// <param name="ChatId">ID пользователя</param>
        /// <param name="messageText">Текст который написал пользователь</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="Bot">Токен бота</param>
        /// <returns></returns>
        async static Task SaveChatHistory(long ChatId, string messageText, string name, string Bot)
        {
            try
            {
                using (var fs = new FileStream($"F:\\LogsGPT\\" + $"{ChatId}" + $" {name}" + ".txt", FileMode.Append))
                {
                    byte[] buffer = Encoding.Default.GetBytes($"{DateTime.Now}" + "User: " + messageText + "\n" + "Bot: " + $"{Bot}");

                    await fs.WriteAsync(buffer, 0, buffer.Length);
                    Console.WriteLine("Текст записан в файл");
                }
            }

            catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Ошибка записи в файл у пользователя c ID: {ChatId} Имя: {name}"); Console.ResetColor(); return; }

            return;
        }
    }
}
