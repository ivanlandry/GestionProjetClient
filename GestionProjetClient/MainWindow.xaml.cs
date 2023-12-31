using GestionProjetClient.Classes;
using GestionProjetClient.Pages;
using GestionProjetClient.Modification;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using GestionProjetClient.dialogues;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.InitializeComponent();
            SingletomFen.getInstance().Fenetre = this;
            Singleton.getInstance().setWindow(this);
            Singleton.getInstance().setNavigationViewItemConnexion(connexion);

            if (Singleton.getInstance().adminExist())
                Singleton.getInstance().getNavigationViewItemConnexion().Content = "se connecter";
            else
                Singleton.getInstance().getNavigationViewItemConnexion().Content = "s'enregistrer";

            mainFrame.Navigate(typeof(PageListeProjets));
        }


        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;

            switch (item.Name)
            {
                case "btnBack":
                    if (mainFrame.CanGoBack)
                    {
                        mainFrame.GoBack();
                    }
                    break;
                case "listeProjet":
                    mainFrame.Navigate(typeof(PageListeProjets));
                    break;
                case "listeClient":
                    mainFrame.Navigate(typeof(PageListeClients));
                    break;
                case "listeEmploye":
                    mainFrame.Navigate(typeof(PageListeEmploye));
                    break;
                case "modificationEmploye":
                    mainFrame.Navigate(typeof(PageZoomEmploye));
                    break;
                case "modificationClient":
                    mainFrame.Navigate(typeof(PageZoomClients));
                    break;
                case "modificationProjet":
                    mainFrame.Navigate(typeof(ZoomProjet));
                    break;
                case "connexion":
                    if (Classes.Session.Statut)
                    {
                        connexion.Content = "Se connecter";
                        connexion.Icon = new SymbolIcon(Symbol.OtherUser);
                        Classes.Session.EndSession();
                    }
                    mainFrame.Navigate(typeof(PageConnexion));
                    break;
            }
        }
    }
}
