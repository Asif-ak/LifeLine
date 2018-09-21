using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LifeLine_WebAPi.WrapperCLasses
{
    public class HelperClass
    {
        internal static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
