using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent
{
    public static class AuthenticationToken
    {
        public static void SaveToken(string token)
        {
            Properties.Settings.Default.accessToken = token;
        }

        public static string GetToken()
        {
            return Properties.Settings.Default.accessToken;
        }
    }
}
