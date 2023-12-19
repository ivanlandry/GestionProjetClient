using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using GestionProjetClient.Classes;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.dialogues
{
    public sealed partial class AjouterProjetDialogue : ContentDialog
    {
        private bool close=false;

        private List<string> listClients = new List<string>();
        public AjouterProjetDialogue()
        {
            this.InitializeComponent();

            foreach (var item in Singleton.getInstance().getClients())
            {
                this.listClients.Add(item.ToString());
            } 
            dateDebut.Date = DateTime.Now;

            tbxClient.ItemsSource = this.listClients;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            bool erreur = false;

            if (tbxDescription.Text == "")
            {
                tblDescriptionErreur.Text = " la description est requise";
                erreur = true;
            }
                
            else
            {
                tblDescriptionErreur.Text = "";
            }
                

            if (tbxTitre.Text == "")
            {
                tblTitreErreur.Text = " le titre est requis"; 
                erreur = true;
            }
                
            else
            {
                tblTitreErreur.Text = "";
            }
                

            if (tbxBudget.Text=="")
            {
                tblBudgetErreur.Text = "Le budget est requis"; 
                erreur = true;
            }
                
            else
            {
                if (!Validation.validerNombre(tbxBudget.Text))
                    tblBudgetErreur.Text = "Budget invalide";
                else
                    tblBudgetErreur.Text = "";
            }

            if (DateTimeOffset.Compare(dateDebut.Date,DateTimeOffset.Now)<0)
            {
                tblDateDebutErreur.Text = "Date invalide"; 
                erreur =true;
            }
                
            else
            {
                tblDateDebutErreur.Text = "";
            }
                

            if (tbxClient.Text == "Aucun résultat" || tbxClient.Text=="")
            {
                tblClientErreur.Text = "Client invalide"; 
                erreur=true;
            }
            else
            {
                tblClientErreur.Text = "";
            }
                

            if (this.ContentDialog_PrimaryButtonClick != null)
            {
                if(!erreur)
                {
                    string[] tabIdClient = tbxClient.Text.Split('-');
                
                    string[] tabDateDebut = dateDebut.Date.ToString().Split(' ');
             
                    Projet projet = new Projet("0",tbxTitre.Text, tabDateDebut[0], tbxDescription.Text,Convert.ToDouble(tbxBudget.Text),cbbNbEmploye.SelectedItem.ToString(), tabIdClient[0], "en cours",0);

                    Singleton.getInstance().ajouterProjet(projet);
                    this.close = false;
                }
                else
                {
                    this.close = true;
                }
                this.Closing += AjouterProjetDialogue_Closing;
            }

        }

        private void AjouterProjetDialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = this.close;
        }

        private void client_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<string> suggestions = new List<string>();

            foreach (var item in listClients)
            {
                if (item.Contains(tbxClient.Text))
                    suggestions.Add(item);
            }

            if (suggestions.Count == 0)
                suggestions.Add("Aucun résultat");

            tbxClient.ItemsSource = suggestions;
        }

        private void client_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

      

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.close = false;
            this.Closing += AjouterProjetDialogue_Closing;
        }
    }
}
