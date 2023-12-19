using GestionProjetClient.Classes;
using GestionProjetClient.Pages;
using GestionProjetClient;
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
using Microsoft.UI.Xaml.Media.Imaging;
using MySqlX.XDevAPI;
using Session = GestionProjetClient.Classes.Session;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Modification
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageZoomEmploye : Page
    {
        string resultatRadioButton = "";
        ObservableCollection<Employe> employes = Singleton.getInstance().getEmployes();
        
        public PageZoomEmploye()
        {
            this.InitializeComponent();

            if (!Session.Statut)
            {
                tbxNomModifier.IsEnabled = true;
                tbxPrenomModifier.IsEnabled = true;
                tbxDateNaissance.IsEnabled = true;
                tbxDateEmbauche.IsEnabled = true;
                tbxEmail.IsEnabled = true;
                tbxAdresse.IsEnabled = true;
                tbxTauxHoraire.IsEnabled = true;
                tbxPhoto.IsEnabled = true;
                rastatut.IsEnabled = true;
                tbxNbHeure.IsEnabled = true;

                btModifier.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                btModifier.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            if (e.Parameter is not null)
            {

                Employe employe = e.Parameter as Employe;
                
                    tbxMatriculeModifier.Text   = employe.Matricule.ToString();
                    tbxNomModifier.Text         = employe.Nom.ToString();
                    tbxPrenomModifier.Text      = employe.Prenom.ToString();
                    tbxDateNaissance.Date       = employe.DateNaissance;
                    tbxDateEmbauche.Date        = employe.DateEmbauche;
                    tbxEmail.Text               = employe.Email.ToString();
                    tbxAdresse.Text             = employe.Adresse.ToString();
                    tbxTauxHoraire.Text         = employe.TauxHoraire.ToString();
                    tbxPhoto.Text               = employe.Photo.ToString();
                    resultatRadioButton         = employe.Statut.ToString();
                    tbxNbHeure.Text             = employe.NbHeure.ToString();

                    tblStatutType.Text          = resultatRadioButton;
                    ImageEmploye.Source         = new BitmapImage(new Uri(employe.Photo));


            }
        }


        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            bool erreur = false;
            
                if (rastatut.SelectedItem == null || !Validation.validerStatut(resultatRadioButton, tbxDateEmbauche.Date))
                {
                    tblStatutErreur.Text = "Statut invalide en fonction de la date embauche";
                    erreur = true;
                }
                else
                {
                    resultatRadioButton = rastatut.SelectedItem.ToString();
                }

            if (!erreur)
            {
                try
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = mainpanel.XamlRoot;
                    dialog.Title = "Modifier";
                    dialog.PrimaryButtonText = "Oui";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    dialog.Content = "On modifie?";

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    if (resultat == ContentDialogResult.Primary)
                    {
                        tblTexte.Text = "bouton Oui";


                        Employe employe = new Employe(tbxMatriculeModifier.Text, tbxNomModifier.Text, tbxPrenomModifier.Text, tbxDateNaissance.Date.ToString("yyyy-MM-dd"), tbxEmail.Text, tbxAdresse.Text, tbxDateEmbauche.Date.ToString("yyyy-MM-dd"), tbxTauxHoraire.Text, tbxPhoto.Text, resultatRadioButton, tbxNbHeure.Text);

                        if (tbxMatriculeModifier.Text.Equals(employe.Matricule, StringComparison.OrdinalIgnoreCase))
                        {

                            Singleton.getInstance().modifierEmploye(employe);

                            dialog.XamlRoot = mainpanel.XamlRoot;
                            dialog.Title = "Information";
                            dialog.CloseButtonText = "OK";
                            dialog.Content = "Produit Modifier avec success";

                            var result = await dialog.ShowAsync();

                            this.Frame.Navigate(typeof(PageListeEmploye));
                        }

                    }
                    else
                    {
                        tblTexte.Text = "ANNULATION";

                        dialog.XamlRoot = mainpanel.XamlRoot;
                        dialog.Title = "Information";
                        dialog.CloseButtonText = "OK";
                        dialog.Content = "Modification ANNULER";

                        var result = await dialog.ShowAsync();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void rastatut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rastatut.SelectedItem != null)
            {
                resultatRadioButton = rastatut.SelectedItem.ToString();
            }

        }
    }
}
