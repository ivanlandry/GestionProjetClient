using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.Classes
{
    class Projet
    {
        private string numero;
        private string titre;
        private string dateDebut;
        private string description;
        private double budget;
        private int nbEmploye;
        private int idClient;
        private string statut;
        private double totalSalaireAPayer;

        public Projet(string numero, string titre, string dateDebut, string description, double budget, int nbEmploye, int idClient, string statut, double totalSalaireAPayer)
        {
            this.Numero = numero;
            this.Titre = titre;
            this.DateDebut = dateDebut;
            this.Description = description;
            this.Budget = budget;
            this.NbEmploye = nbEmploye;
            this.IdClient = idClient;
            this.Statut = statut;
            this.TotalSalaireAPayer = totalSalaireAPayer;
        }

        public string Numero { get => numero; set => numero = value; }
        public string Titre { get => titre; set => titre = value; }
        public string DateDebut { get => dateDebut; set => dateDebut = value; }
        public string Description { get => description; set => description = value; }
        public double Budget { get => budget; set => budget = value; }
        public int NbEmploye { get => nbEmploye; set => nbEmploye = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public string Statut { get => statut; set => statut = value; }
        public double TotalSalaireAPayer { get => totalSalaireAPayer; set => totalSalaireAPayer = value; }
    }
}
