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
        public static bool validerStatut(string statut,DateTimeOffset dateEmbauche)
        {
            try
            {
                DateTimeOffset now = DateTimeOffset.Now;
                int dateStatutPermanent = (now - dateEmbauche).Days/365;
                string statutEmp = "Permanent";
                if(statut != statutEmp && dateStatutPermanent >= 3 || statut == statutEmp && dateStatutPermanent < 3)

                return false;
                return true;
            }
            catch 
            {
                return false;
            }
            
        }
        public static bool validerDateNaissance(DateTimeOffset date)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            int dateage18 = (now - date).Days/365;
            int dateageRetraite = (now - date).Days / 365;
            //DateTimeOffset age18ans ;

            //DateTimeOffset age18 = now.AddYears(-18);
            //DateTimeOffset ageRetraite = now.AddYears(-65);

            if (dateage18 >=18 && dateageRetraite <=65)
            return true;
            return false;
        }

        public static bool validerDateEmbauche(DateTimeOffset date)
        {
            try
            {
                DateTimeOffset now = DateTimeOffset.Now;
                //DateTimeOffset embaucheDate = now.AddDays(0);
                int embaucheDate = (now - date).Days+1;

                if(embaucheDate >=1)
                return true;
                return false;
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
