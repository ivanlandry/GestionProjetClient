using GestionProjetClient.Classes;
using GestionProjetClient.dialogues;
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
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageListeProjets : Page
    {
        private ObservableCollection<Projet> listProjets=null;
        public PageListeProjets()
        {
            this.InitializeComponent();

            if (!Session.Statut)
                ajouterProjet.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            else
                ajouterProjet.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
           
            this.listProjets = Singleton.getInstance().getProjets();
            gdvProjets.ItemsSource = this.listProjets;
     
        }

       
        private void gdvProjets_ItemClick(object sender, ItemClickEventArgs e)
        { 
            this.Frame.Navigate(typeof(ZoomProjet), e.ClickedItem as Projet);
        }

        private async void ajouterProjet_Click(object sender, RoutedEventArgs e)
        {
           AjouterProjetDialogue dialogue = new AjouterProjetDialogue();

            dialogue.XamlRoot = rootProjet.XamlRoot;
            dialogue.Title = "Nouveau un projet";
            dialogue.PrimaryButtonText = "Creer";
            dialogue.CloseButtonText = "fermer";
            dialogue.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialogue.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = rootProjet.XamlRoot;
                dialog.Title = "Ajout";
                dialog.CloseButtonText = "OK";
                dialog.Content = "ajout réussi!";

                var result = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(PageListeProjets));
            }

        }

        private async void btSauvegardeFichier_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();

                // Configurations du FileSavePicker
                var hWnd = IntPtr.Zero; // Par défaut, aucune fenêtre associée
                if (SingletomFen.getInstance().Fenetre != null)
                {
                    hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletomFen.getInstance().Fenetre);
                    WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
                }
                picker.SuggestedFileName = "projets";
                picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".csv" });

                // Sélection du fichier de sauvegarde
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                if (monFichier != null)
                {
                    StringBuilder csvContent = new StringBuilder();

                    // Entête CSV avec les noms des colonnes
                    csvContent.AppendLine("Numero;Titre;DateDebut;Description;Budget;NbEmploye;IdClient;Statut;TotalSalaireAPayer");

                    // Récupération des projets et ajout de leurs informations au fichier CSV
                    foreach (Projet projet in Singleton.getInstance().getProjets())
                    {
                        csvContent.AppendLine(projet.stringCSV());
                    }

                    // Écriture des données dans le fichier CSV
                    await Windows.Storage.FileIO.WriteTextAsync(monFichier, csvContent.ToString(), Windows.Storage.Streams.UnicodeEncoding.Utf8);

                    // Affichage d'un message de succès
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = rootProjet.XamlRoot;
                    dialog.Title = "Information";
                    dialog.CloseButtonText = "OK";
                    dialog.Content = "Fichier sauvegardé avec succès";
                    var result = await dialog.ShowAsync();
                }
               
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
            }

        }

        private void btSauvegardeFichier_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
