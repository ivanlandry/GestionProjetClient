using GestionProjetClient.Classes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            for (int i = 1; i <= 10; i++)
            {
                Projet project = new Projet(
                    $"P{i}",
                    $"Titre{i}",
                    $"2023-01-01", // Replace with actual date
                    $"Description{i}",
                    10000.0 * i,
                    5 + i,
                    100 + i,
                    $"Statut{i}",
                    5000.0 * i
                );

                projets.Add(project);
            }

            return projets;
        }

        // clients

        public void ajouterClient(Client client)
        {
            try {
                MySqlCommand command = new MySqlCommand("p_ajouter_client");
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

    }
}
