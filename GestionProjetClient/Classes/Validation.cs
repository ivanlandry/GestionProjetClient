using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    internal static class Validation
    {
        public static bool validerEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }

        
        public static bool validerNombre(string nombre)
        {
            try
            {
                double nb = Convert.ToDouble(nombre);
                return true;

            }catch(Exception e)
            {
                return false;
            }
        }
        public static bool validerTelephone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            try
            {
                string phonePattern = @"^\d{10}$";
                Regex regex = new Regex(phonePattern);

                return regex.IsMatch(phoneNumber);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
