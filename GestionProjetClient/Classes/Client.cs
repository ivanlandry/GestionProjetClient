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
        private string telephone;
        private string email;
        private string adresse;

        public Client(int identifiant, string nom, string adresse, string telephone, string email)
        {
            this.Identifiant = identifiant;
            this.Nom = nom;
            
            this.Telephone = telephone;
            this.Email = email;
            this.Adresse = adresse;
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }

        public override string ToString()
        {
            return $"{Nom}({Identifiant})";

        }
    }
}
