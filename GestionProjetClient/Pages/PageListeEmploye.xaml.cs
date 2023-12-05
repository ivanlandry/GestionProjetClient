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
using System.Collections.ObjectModel;
using GestionProjetClient.Classes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageListeEmploye : Page
    {
        private ObservableCollection<Employe> listEmployes = null;
        public PageListeEmploye()
        {
            this.InitializeComponent();
            if (!Session.Statut)
                ajouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
            else
                ajouterEmploye.Visibility = Microsoft.UI.Xaml.Visibility.Visible;

            this.listEmployes = Singleton.getInstance().getEmployes();
            gdvEmployes.ItemsSource = this.listEmployes;
        }

        private void ajouterEmploye_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gdvEmployes_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}