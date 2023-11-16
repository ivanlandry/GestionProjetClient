using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    static class Session
    {
        private static bool statut;

        public static bool Statut { get => statut; set => statut = value; }

        public static void startSession()
        {
            Session.statut = true;
        }

        public static void EndSession()
        {
            Session.statut = false;
        }
    }
}
