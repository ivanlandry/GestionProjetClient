using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    class Employe
    {
        private string matricule;
        private string nom;
        private string prenom;
        private string dateNaissance;
        private string dateEmbauche;
        private string email;
        private string adresse;
        private double tauxHoraire;
        private string photo;
        private string statut;
        private double nbHeure;

        public Employe(string matricule, string nom, string prenom, string dateNaissance, string dateEmbauche, string email, string adresse, double tauxHoraire, string photo, string statut, double nbHeure)
        {
            this.Matricule = matricule;
            this.Nom = nom;
            this.Prenom = prenom;
            this.DateNaissance = dateNaissance;
            this.DateEmbauche = dateEmbauche;
            this.Email = email;
            this.Adresse = adresse;
            this.TauxHoraire = tauxHoraire;
            this.Photo = photo;
            this.Statut = statut;
            this.NbHeure = nbHeure;
            
        }

        public string Matricule { get => matricule; set => matricule = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public string DateEmbauche { get => dateEmbauche; set => dateEmbauche = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
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
    }
}
