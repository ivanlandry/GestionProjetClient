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

           /* if (!Session.Statut)
                ajouterProjet.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            else
                ajouterProjet.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
           */
            this.listProjets = Singleton.getInstance().getProjets();
            gdvProjets.ItemsSource = this.listProjets;
     
        }

        private void gdvProjets_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void ajouterProjet_Click(object sender, RoutedEventArgs e)
        {
           AjouterProjetDialogue dialogue = new AjouterProjetDialogue();

            dialogue.XamlRoot = rootProjet.XamlRoot;
            dialogue.Title = "Nouveau un projet";
            dialogue.PrimaryButtonText = "Creer";
            dialogue.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialogue.ShowAsync();
        }
    }
}
