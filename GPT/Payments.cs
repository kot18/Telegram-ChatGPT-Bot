using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT
{
    public static class Payments
    {
        //if (User.Message == "/getSub")
        //{
        //    PayUserId = User.ChatId;

        //    try
        //    {
        //        List<LabeledPrice> myList = new List<LabeledPrice>();

        //        myList.Add(new LabeledPrice("Цена", 100 * 100));

        //        await botClient.SendInvoiceAsync(
        //                        chatId: User.ChatId,
        //                        title: "Подписка на бота",
        //                        description: "Тест",
        //                        payload: "ЮКасса",
        //                        providerToken: "381764678:TEST:54284",
        //                        currency: "RUB",
        //                        startParameter: "test",
        //                        prices: myList);

        //        return;
        //    }

        //    catch (Exception ex) { Console.WriteLine(ex); return; }
        //}

        //try
        //{
        //    PreCheckoutQuery preCheckout = update.PreCheckoutQuery;
        //    if (preCheckout != null && PayTue == false)
        //    {
        //        await botClient.AnswerPreCheckoutQueryAsync(update.PreCheckoutQuery.Id, token);

        //        PayTue = true;

        //        if (PayTue)
        //        {
        //            PayTue = false;
        //            await botClient.SendTextMessageAsync(PayUserId, "Оплата прошла успешно!");
        //        }

        //        return;
        //    }
        //}

        //catch (Exception ex) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(ex); Console.ResetColor(); return; }
    }
}
