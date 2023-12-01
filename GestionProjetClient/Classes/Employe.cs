using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    internal class Employe : INotifyPropertyChanged
    {
        //private string matricule;
        //private string nom;
        //private string prenom;
        //private DateTime dateNaissance;
        //private DateTime dateEmbauche;
        //private string email;
        //private string adresse;
        //private double tauxHoraire;
        //private string photo;
        //private string statut;
        //private double nbHeure;

         string matricule;
         string nom;
         string prenom;
         DateTime dateNaissance;
         DateTime dateEmbauche;
         string email;
         string adresse;
         double tauxHoraire;
         string photo;
         string statut;
         double nbHeure;

        public Employe(string matricule, string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, string tauxHoraire, string photo, string statut, string nbHeure)
        {
            this.Matricule = matricule;
            this.Nom = nom;
            this.Prenom = prenom;
            this.DateNaissance = DateTime.TryParse(dateNaissance, out DateTime parsedDateNaissance) ? parsedDateNaissance : DateTime.MinValue;
            //this.DateNaissance = dateNaissance;
            this.Email = email;
            this.Adresse = adresse;
            this.DateEmbauche = DateTime.TryParse(dateEmbauche, out DateTime parsedDateEmbauche) ? parsedDateEmbauche : DateTime.MinValue;
            //this.DateEmbauche = dateEmbauche;
            this.TauxHoraire = double.TryParse(tauxHoraire, out double parsedTauxHoraire) ? parsedTauxHoraire : 0.0;
            this.Photo = photo;
            this.Statut = statut;
            this.NbHeure = double.TryParse(nbHeure, out double parsedNbHeure) ? parsedNbHeure : 0.0;

        }

        public string Matricule { get => matricule; set => matricule = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public DateTime DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public double TauxHoraire { get => tauxHoraire; set => tauxHoraire = value; }
        public string Photo { get => photo; set => photo = value; }
        public string Statut { get => statut; set => statut = value; }
        public double NbHeure { get => nbHeure; set => nbHeure = value; }

        private bool tauxHoraireValide()
        {
            if (this.tauxHoraire < 15)
                return false;
            else
                return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public override string ToString()
        {
            return $"{Matricule}-{Nom}";
        }
        public void notify([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
