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
    public sealed partial class PageListeClients : Page
    {
        private ObservableCollection<Client> listClients=null;
        public PageListeClients()
        {
            this.InitializeComponent();

            /* if (!Session.Statut)
                ajouterClient.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            else
                ajouterClient.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
           */
            this.listClients = Singleton.getInstance().getClients();
            gdvClients.ItemsSource = this.listClients;
        }

        private async void ajouterClient_Click(object sender, RoutedEventArgs e)
        {
            AjouterClientDialogue dialogue = new AjouterClientDialogue();

            dialogue.XamlRoot = rootClient.XamlRoot;
            dialogue.Title = "Nouveau client";
            dialogue.PrimaryButtonText = "Creer";
            dialogue.DefaultButton = ContentDialogButton.Primary;

            ContentDialogResult resultat = await dialogue.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {       
                    dialogue.Closing += Dialogue_Closing;

                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = rootClient.XamlRoot;
                    dialog.Title = "Ajout";
                    dialog.CloseButtonText = "OK";
                    dialog.Content = "ajout réussi!";

                    var result = await dialog.ShowAsync();

                this.Frame.Navigate(typeof(PageListeClients));
            }
        }

        private void Dialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = false;
        }

        private void gdvClients_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
