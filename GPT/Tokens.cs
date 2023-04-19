using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class Tokens
    {
        public static OpenAIAPI apiKey;
        public static string BotToken { get; set; }

        public static void SetApi(string Key)
        {
            apiKey = new OpenAIAPI(Key);
        }
    }
}
