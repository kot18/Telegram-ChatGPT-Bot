using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GPT
{
    public static class AwaitResponce
    {
        async public static Task<string> BotResponce(string UserMessage)
        {
            var result = await Tokens.apiKey.Chat.CreateChatCompletionAsync(new ChatRequest()
            {
                Model = Model.ChatGPTTurbo,
                Temperature = 0.1,
                MaxTokens = 1000,
                Messages = new ChatMessage[] { new ChatMessage(ChatMessageRole.User, $"{UserMessage}") }
            });

            return result.ToString();
        }

        async public static Task<string> UserResponce(ITelegramBotClient botClient)
        {
            var offset = 0;
            var returnNewMessage = "";

            while (true)
            {
                var newMessage = await botClient.GetUpdatesAsync(offset, limit: 0);
                var stopCheck = false;

                foreach (var messages in newMessage)
                {
                    offset = messages.Id + 1;

                    if (messages.Message != null)
                    {
                        var m = messages.Message.Text;

                        if (m[0] != '/')
                        {
                            returnNewMessage = m;
                            stopCheck = true;

                            break;
                        }
                    }
                }

                if (stopCheck)
                    break;
            }

            return returnNewMessage;
        }
    }
}
