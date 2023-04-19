using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class ReadKey
    {
        private static string GPT_api { get; set; }
        private static string TelegramBot_token { get; set; }

        public static bool ReadApi()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string specificFolder = Path.Combine(folder, "GPT_Bot_Keys");

            if (!Directory.Exists(specificFolder))
            {
                Directory.CreateDirectory(specificFolder);
            }

            if (!File.Exists(specificFolder + "\\" + "GPT_api.txt"))
            {
                File.Create(specificFolder + "\\" + "GPT_api.txt");
            }

            if (!File.Exists(specificFolder + "\\" + "TelegramBot_token.txt"))
            {
                File.Create(specificFolder + "\\" + "TelegramBot_token.txt");
            }

            GPT_api = specificFolder + "\\" + "GPT_api.txt";
            TelegramBot_token = specificFolder + "\\" + "TelegramBot_token.txt";

            try
            {
                using (StreamReader reader = new StreamReader(GPT_api))
                {
                    string Key = reader.ReadLine();
                    Tokens.SetApi(Key);

                    if (Key == null)
                    {
                        reader.Close();
                        InputKeys();

                        return false;
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Ключ для OpenAI получен!");
                    Console.ResetColor();
                }
            }

            catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Ошибка чтения ключа. Возможно файл GPT_api.txt отсутствует или в нём не прописан ключ!"); Console.ResetColor(); return false; }

            try
            {
                using (StreamReader reader = new StreamReader(TelegramBot_token))
                {
                    Tokens.BotToken = reader.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Ключ для телеграма получен!");
                    Console.ResetColor();
                }
            }

            catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"Ошибка чтения ключа. Возможно файл TelegramBot_token.txt отсутствует или в нём не прописан ключ!"); Console.ResetColor(); return false; }

            return true;
        }

        private static void InputKeys()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("ВВОДИТЕ ТОЛЬКО ПРАВИЛЬНЫЕ КЛЮЧИ!");
            Console.ResetColor();

            Console.Write("Введите API ключ выданный вам BotFather: ");
            string BotKey = Console.ReadLine();

            using (FileStream fstream = new FileStream(TelegramBot_token, FileMode.Open))
            {
                byte[] buffer = Encoding.Default.GetBytes(BotKey);

                fstream.Write(buffer, 0, buffer.Length);
                fstream.Close();
            }    

            Console.Write("Введите API ключ выданный вам OpenAI: ");
            string GPTKey = Console.ReadLine();

            try
            {
                using (FileStream fstream1 = new FileStream(GPT_api, FileMode.Open))
                {
                    byte[] buffer = Encoding.Default.GetBytes(GPTKey);

                    fstream1.Write(buffer, 0, buffer.Length);
                    fstream1.Close();
                }
            }

            catch (Exception ex) { Console.WriteLine(ex); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ключи получены, перезапустите программу!");
            Console.ResetColor();
        }
    }
}
