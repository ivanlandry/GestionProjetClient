
using GestionProjetClient.Classes;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomProjet : Page
    {
        private Projet projet=null;
        private List<string> listEmployes= new List<string>(); 
        public ZoomProjet()
        {
            this.InitializeComponent();

            if (Session.Statut)
            {
                titre.IsEnabled = true;
                budget.IsEnabled = true;
                nbEmploye.IsEnabled = true;
                description.IsEnabled = true;
                client.IsEnabled = true;
                statut.IsEnabled = true;
                totalSalaire.IsEnabled = true;
                dateDebut.IsEnabled = true;

                blockAjouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                blockAjouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
      

            foreach (var item in Singleton.getInstance().getEmployes())
            {
                this.listEmployes.Add(item.ToStringTextBox());
            }
            tbxEmploye.ItemsSource = this.listEmployes;
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            
            if(e.Parameter is not null)
            {
                this.projet = e.Parameter as Projet;

                List<Array> listeEmployesDuProjet=Singleton.getInstance().afficherProjet(this.projet.Numero);

                // creation dynamique de grid pour la liste des employés
                for(int i = 1; i <= listeEmployesDuProjet.Count; i++)
                {
                    RowDefinition rowDef = new RowDefinition();
                    gvdEmployesDuProjet.RowDefinitions.Add(rowDef);
                }

                for(int i = 0; i < listeEmployesDuProjet.Count; i++)
                {
                    int ct = 0;
                    foreach (KeyValuePair<string, string> kvp in listeEmployesDuProjet[i])
                    {
                        TextBlock txbValue = new TextBlock();
                        txbValue.Margin = new Thickness(0, 10, 10, 0);

                        if (i == 0)
                        {
                            txbValue.Text = kvp.Key;
                            txbValue.FontWeight = FontWeights.Bold;
                          
                        }
                        else
                        {
                            txbValue.Text = kvp.Value;
                        }
                        Grid.SetRow(txbValue, i);
                        Grid.SetColumn(txbValue, ct);

                        ct++;

                        gvdEmployesDuProjet.Children.Add(txbValue);
                    }
                
                }

                numero.Text = "Details du projet : " + this.projet.Numero;
                titre.Text = this.projet.Titre;
                budget.Text = this.projet.Budget.ToString();
                nbEmploye.Text = this.projet.NbEmploye;
                description.Text = this.projet.Description;
                client.Text = this.projet.IdClient;
                statut.Text = this.projet.Statut;
                totalSalaire.Text = this.projet.TotalSalaireAPayer.ToString();
            }
        }

        private void tbxEmploye_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<string> suggestions = new List<string>();

            foreach (var item in this.listEmployes)
            {
                if (item.Contains(tbxEmploye.Text))
                    suggestions.Add(item);
            }

            if (suggestions.Count == 0)
                suggestions.Add("Aucun résultat");

            tbxEmploye.ItemsSource = suggestions;
        }

        private async void btnAjouterEmploye_Click(object sender, RoutedEventArgs e)
        {
            if (tbxEmploye.Text=="" || tbxEmploye.Text=="Aucun resultat")
            {
                tbxEmployeErreur.Text = "Choisir un employé";
            }
            else
            {
                tbxEmployeErreur.Text = "";
            }

            string[] tabEmploye = tbxEmploye.Text.ToString().Split(',');
            string matriculeEmploye = tabEmploye[0];
            double nbHeure=0;

            if (nbHeureTravaille.Text == "")
            {
                nbHeureTravailleErreur.Text = "Entrer un nombre";
            }
            else
            {
                if (Validation.validerNombre(nbHeureTravaille.Text))
                {
                    nbHeure = Convert.ToDouble(nbHeureTravaille.Text);
                    if(nbHeure>=0)
                        nbHeureTravailleErreur.Text = "";
                    else
                        nbHeureTravailleErreur.Text = "Le nombre d'heure ne peut etre négatif";
                }
                else
                {
                    nbHeureTravailleErreur.Text = "Nombre invalide";
                }
            }

            if(nbHeureTravailleErreur.Text=="" && tbxEmployeErreur.Text == "")
            {
                string resultat = Singleton.getInstance().assignerProjetEmploye(this.projet.Numero, matriculeEmploye, nbHeure);

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = rootZoomProjet.XamlRoot;
                dialog.Title = "Reponse";
                dialog.CloseButtonText = "OK";
                dialog.Content = (resultat == "") ? "Ajout réussit" : resultat;

                var result = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(ZoomProjet),this.projet);
            }
        }
    }
}
