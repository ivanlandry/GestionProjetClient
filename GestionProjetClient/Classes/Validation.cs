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
        public static bool validerDateNaissance(DateTimeOffset date)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset age18 = now.AddYears(-18);
            DateTimeOffset ageRetraite = now.AddYears(-65);

            return (date <= age18 && date >= ageRetraite);
        }

        public static bool validerDateEmbauche(DateTimeOffset date)
        {
            try
            {
                DateTimeOffset now = DateTimeOffset.Now;
                DateTimeOffset embaucheDate = now.AddDays(0);


                return (date <= embaucheDate);
            }
            catch
            {
                return false;
            }
        }

        public static bool validerTauxHoraire(double tauxHoraire)
        {
            try
            {
                const double tauxMax = 500;
                const double tauxMin = 15;

                return (tauxHoraire >= tauxMin && tauxHoraire <= tauxMax && !double.IsNaN(tauxHoraire));
            }
            catch
            {
                return false;
            }
        }
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
