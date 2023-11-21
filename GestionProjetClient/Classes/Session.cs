using GestionProjetClient.Pages;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    static class Session
    {
     
        private static bool statut = false;
        private static string nomUtilisateur;
      

        public static bool Statut { get => statut; set => statut = value; }
        public static string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }

        public static void startSession(string _nomUtilisateur)
        {
            Session.nomUtilisateur = _nomUtilisateur;
            Session.statut = true;
        }
        public static void EndSession()
        {
            Session.NomUtilisateur = null;
            Session.Statut = false;
        }

    }
}
