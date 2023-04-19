using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace GPT
{
    public static class Manually
    {
        async public static Task SendMessageUser(ITelegramBotClient botClient, long ChatIdUser, string Message)
        {
            await botClient.SendTextMessageAsync(ChatIdUser, $"{Message}");
            return;
        }
    }
}
