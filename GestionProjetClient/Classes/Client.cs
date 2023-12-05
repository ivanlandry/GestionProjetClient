using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    internal class Client : INotifyPropertyChanged
    {
        //private int identifiant;
        //private string nom;
        //private string telephone;
        //private string email;
        //private string adresse;

         int identifiant;
         string nom;
         string telephone;
         string email;
         string adresse;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{Identifiant}-{Nom}";

        }

        public void notify([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
