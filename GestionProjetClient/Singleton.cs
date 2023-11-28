using GestionProjetClient.Classes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Protection.PlayReady;
using Client = GestionProjetClient.Classes.Client;
using Session = GestionProjetClient.Classes.Session;

namespace GestionProjetClient
{
    class Singleton
    {
        ObservableCollection<Employe> employes;
        ObservableCollection<Client> clients;
        ObservableCollection<Projet> projets;

        static Singleton instance = null;
        Window window = null;
        NavigationViewItem navigationViewItemConnexion = null;

        static MySqlConnection con = null;
        public Singleton()
        {
            employes = new ObservableCollection<Employe>();
            clients = new ObservableCollection<Client>();
            projets = new ObservableCollection<Projet>();

            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq1;Uid=2204463;Pwd=2204463;");

        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }

        public Window getWindow()
        {
            return window;
        }

        public void setWindow(Window _window)
        {
            window = _window;
        }

        public NavigationViewItem getNavigationViewItemConnexion()
        {
            return navigationViewItemConnexion;
        }

        public void setNavigationViewItemConnexion(NavigationViewItem _navigationViewItemConnexion)
        {
            navigationViewItemConnexion = _navigationViewItemConnexion; 
        }

        // authentification

        public bool connexion(string nomUtilisateur, string motDePasse)
        {
            string username;
            bool statut = false;

            try
            {
                MySqlCommand commande = new MySqlCommand("pGetAdmin");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("_username", nomUtilisateur);
                commande.Parameters.AddWithValue("_password", motDePasse);
                con.Open();
                commande.Prepare();

                MySqlDataReader reader = commande.ExecuteReader();
           
                 reader.Read();

                username = reader.GetString("username");
                con.Close();

                statut = true;
                Session.startSession(username);
            }
            catch (MySqlException ex)
            {
                con.Close();
                statut = false;
            }

            return statut;
        }

        public void enregistrerAdmin(string nomUtilisateur,string motDePasse)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("pInsertAdmin");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("_username", nomUtilisateur);
                commande.Parameters.AddWithValue("_password", motDePasse);
                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                con.Close();
            }catch(MySqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

      
        public bool adminExist()
        {
            MySqlCommand commande = new MySqlCommand("pAdminExist");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            MySqlDataReader reader = commande.ExecuteReader();

            reader.Read();

            int resultat = Convert.ToInt32(reader["nb"]);

            con.Close();

            if (resultat == 0)
                return false;
            else
                return true;
        }

        // projets

        public ObservableCollection<Projet> getProjets()
        {
            MySqlCommand commande = new MySqlCommand("afficheListeProjet");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            projets.Clear();

            while (r.Read())
            {
                Projet projet = new Projet(
                    r["numeroProjet"].ToString(),
                    r["titre"].ToString(),
                    r["dateDebut"].ToString(),
                    r["description"].ToString(),
                    Convert.ToDouble(r["budget"]),
                    r["nbEmploye"].ToString(),
                    r["idClient"].ToString(),
                    r["statutProjet"].ToString(),
                    Convert.ToDouble(r["totalSalaire"])
                );
                
                projets.Add(projet);
            }
            con.Close();

            return projets;
        }

        public void ajouterProjet(Projet projet)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("p_ajouter_projet");
                command.Connection = con;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("_titreProjet",projet.Titre);
                command.Parameters.AddWithValue("_dateDebutProjet", projet.DateDebut);
                command.Parameters.AddWithValue("_budgetProjet", projet.Budget);
                command.Parameters.AddWithValue("_descriptionProjet", projet.Description);
                command.Parameters.AddWithValue("_nbEmployeProjet", projet.NbEmploye);
                command.Parameters.AddWithValue("_idClient", projet.IdClient);
                command.Parameters.AddWithValue("_totalSalaireAPayer", projet.TotalSalaireAPayer);

                con.Open();
                command.Prepare();
                command.ExecuteNonQuery();
                con.Close();

            }catch(MySqlException e)
            {
                Console.Write(e);
            }
        }

        // clients

        public void ajouterClient(Client client)
        {
            try {
                //MySqlCommand command = new MySqlCommand("p_ajouter_client");
                MySqlCommand command = new MySqlCommand("ajoutClient");
                command.Connection = con;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("_nomClient",client.Nom);
                command.Parameters.AddWithValue("_adresseClient", client.Adresse);
                command.Parameters.AddWithValue("_telClient", client.Telephone);
                command.Parameters.AddWithValue("_emailClient", client.Email);

                con.Open();
                command.Prepare();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch(MySqlException e)
            {
               Console.WriteLine(e);
            }
        }

        
        public ObservableCollection<Client> getClients()
        {
            MySqlCommand commande = new MySqlCommand("afficheListeClient");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            clients.Clear();
            while (r.Read())
            {
                Client client = new Client(Convert.ToInt32(r["idClient"]), r["nomClient"].ToString(), r["adresseClient"].ToString(), r["numeroTel"].ToString(), r["emailClient"].ToString());
                clients.Add(client);
            }
            con.Close();
            return clients;
        }

        //EMPLOYE

        public void ajouterEmploye(Employe employe)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("ajouterEmploye");
                command.Connection = con;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("_matriculeEmploye", employe.Matricule);

                command.Parameters.AddWithValue("_nomEmploye", employe.Nom);
                command.Parameters.AddWithValue("_prenomEmploye", employe.Prenom);
                command.Parameters.AddWithValue("_dateNaissance", employe.DateNaissance != DateTime.MinValue ? employe.DateNaissance.ToString("yyyy-MM-dd") : null);
                command.Parameters.AddWithValue("_emailEmploye", employe.Email);
                command.Parameters.AddWithValue("_adresseEmploye", employe.Adresse);
                command.Parameters.AddWithValue("_dateEmbauche", employe.DateEmbauche != DateTime.MinValue ? employe.DateEmbauche.ToString("yyyy-MM-dd") : null);
                command.Parameters.AddWithValue("_tauxHoraire", employe.TauxHoraire);
                command.Parameters.AddWithValue("_photo", employe.Photo);
                command.Parameters.AddWithValue("_statut", employe.Statut);
                command.Parameters.AddWithValue("_nbHeure", employe.NbHeure);

                con.Open();
                command.Prepare();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
                Debug.WriteLine(e.Message);
            }
        }


        public ObservableCollection<Employe> getEmployes()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            MySqlCommand commande = new MySqlCommand("afficheListeEmploye");
            commande.Connection = con;
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            //con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            employes.Clear();
            while (r.Read())
            {
                Employe employe = new Employe(r["matriculeEmploye"].ToString(), r["nomEmploye"].ToString(), r["prenomEmploye"].ToString(), Convert.ToString( r["dateNaissance"]), r["emailEmploye"].ToString(), r["adresseEmploye"].ToString(),Convert.ToString( r["dateEmbauche"]), Convert.ToString(r["tauxHoraire"]), r["photo"].ToString(), r["statut"].ToString(), Convert.ToString(r["nbHeure"]));
                employes.Add(employe);
            }
            con.Close();
            return employes;
           
        }

    }
}
