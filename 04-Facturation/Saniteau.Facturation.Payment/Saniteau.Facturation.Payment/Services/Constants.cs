using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Payment.Services
{
    public class Constants
    {
        public static string SavedTokenPath =  Directory.GetCurrentDirectory() + "/App_Data/SavedToken.json";

        //PaymentDemo
        public const string ClientId = "AcZ2w1FzoK4FurjtjHHjJMTQIo0eJuiHcDNVMojRiWlXxTMKwrl-BMTVWnuLD9_MmEEx7tZNTm8NbQ9n";
        public const string Secret = "";


        private const string PaypalBaseUrl = "https://api-m.sandbox.paypal.com"; //Live: https://api-m.paypal.com

        public static string PaypalOAuthUrl = $"{PaypalBaseUrl}/v1/oauth2/token";
        public static string PaypalGetOrderUrl = "https://api.sandbox.paypal.com/v2/checkout/orders/[order_id]";

    }
}
