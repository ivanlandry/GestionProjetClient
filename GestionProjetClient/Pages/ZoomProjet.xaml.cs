
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

            if (!Session.Statut)
            {
                modifier.IsEnabled = false;
                blockAjouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                modifier.IsEnabled = true;
                blockAjouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
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
                double salaireTotal = 0;

                // creation dynamique de grid pour la liste des employés
               

                int positionGrid = 1;
                for (int i = 0; i < listeEmployesDuProjet.Count; i++)
                {
                    int ct = 0;
                    foreach (KeyValuePair<string, string> kvp in listeEmployesDuProjet[i])
                    {
                        TextBlock txbValue = new TextBlock();
                        txbValue.Margin = new Thickness(0, 10, 10, 0);

                        if (ct == 5)
                            salaireTotal += Convert.ToDouble(kvp.Value);

                        txbValue.Text = (ct==5)? kvp.Value+"$":kvp.Value;
                        
                        Grid.SetRow(txbValue, positionGrid);
                        Grid.SetColumn(txbValue, ct);

                        gvdEmployesDuProjet.Children.Add(txbValue);
                        ct++;
                       
                    }
                    positionGrid++;

                }
                this.projet.TotalSalaireAPayer = salaireTotal;

                numero.Text = "Details du projet : " + this.projet.Numero;
                tbxTitre.Text = this.projet.Titre;
                tbxBudget.Text = this.projet.Budget.ToString();
                nbEmploye.Text = this.projet.NbEmploye;
                tbxDescription.Text = this.projet.Description;
                client.Text = this.projet.IdClient;
                cbbStatut.Text = this.projet.Statut;
                dateDebut.Text = this.projet.DateDebut;
                cbbStatut.SelectedValue = this.projet.Statut;
                totalSalaire.Text = this.projet.TotalSalaireAPayer.ToString()+"$";
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

        private async void modifier_Click(object sender, RoutedEventArgs e)
        {

            if (tbxDescription.Text == "")
            {
                tblDescriptionErreur.Text = " la description est requise"; 
            }
            else
            {
                tblDescriptionErreur.Text = "";
            }

            if (tbxTitre.Text == "")
            {
                tblTitreErreur.Text = " le titre est requis";
               
            }
            else
            {
                tblTitreErreur.Text = "";
            }


            if (tbxBudget.Text == "")
            {
                tblBudgetErreur.Text = "Le budget est requis";
               
            }
            else
            {
                if (!Validation.validerNombre(tbxBudget.Text))
                    tblBudgetErreur.Text = "Budget invalide";
                else
                    tblBudgetErreur.Text = "";
            }

            if(tblTitreErreur.Text=="" && tblBudgetErreur.Text=="" && tblDescriptionErreur.Text == "")
            {
                Singleton.getInstance().modifierProjet(this.projet.Numero,tbxTitre.Text,tbxDescription.Text,Convert.ToDouble(tbxBudget.Text),cbbStatut.SelectedValue.ToString());

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = rootZoomProjet.XamlRoot;
                dialog.Title = "Reponse";
                dialog.CloseButtonText = "OK";
                dialog.Content ="Modification réussit";

                var result = await dialog.ShowAsync();

                this.projet.Titre = tbxTitre.Text;
                this.projet.Description = tbxDescription.Text;
                this.projet.Budget =Convert.ToDouble(tbxBudget.Text);
                this.projet.Statut = cbbStatut.SelectedValue.ToString();
                this.Frame.Navigate(typeof(ZoomProjet), this.projet);
            }


        }
    }
}
