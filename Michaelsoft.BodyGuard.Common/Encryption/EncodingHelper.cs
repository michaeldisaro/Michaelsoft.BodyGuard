using System;

namespace Michaelsoft.BodyGuard.Common.Encryption
{
    public static class EncodingHelper
    {

        public static string ToSafeUrlBase64(byte[] input)
        {
            return Convert.ToBase64String(input)
                          .TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        public static byte[] FromSafeUrlBase64(string base64)
        {
            var incoming = base64
                           .Replace('_', '/').Replace('-', '+');
            switch (base64.Length % 4)
            {
                case 2:
                    incoming += "==";
                    break;
                case 3:
                    incoming += "=";
                    break;
            }
            return Convert.FromBase64String(incoming);
        }

    }
}