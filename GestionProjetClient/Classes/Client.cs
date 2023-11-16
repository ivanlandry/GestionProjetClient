using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    class Client { 
        private int identifiant;
        private string nom;
        private string prenom;
        private string telephone;
        private string email;

        public Client(int identifiant, string nom, string prenom, string telephone, string email)
        {
            this.Identifiant = identifiant;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Telephone = telephone;
            this.Email = email;
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
    }
}
