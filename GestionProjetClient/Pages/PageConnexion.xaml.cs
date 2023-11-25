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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageConnexion : Page
    {
        private bool adminExist = false;
        private NavigationViewItem navigationViewItemConnexion=null;
        public PageConnexion()
        {
            this.InitializeComponent();
            this.navigationViewItemConnexion = Singleton.getInstance().getNavigationViewItemConnexion();

            if (Singleton.getInstance().adminExist())
            {
                Singleton.getInstance().getNavigationViewItemConnexion().Content = "se connecter";
                titre.Text = "Se connecter";
                btnConnexion.Content = " Se connecter";
                this.adminExist = true;
            }
            else
            {
                Singleton.getInstance().getNavigationViewItemConnexion().Content = "s'enregistrer";
                titre.Text = "S'enregistrer";
                btnConnexion.Content = " S'enregistrer";
                this.adminExist = false;
            }


        }

        private async void  btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            string nomUtilisateur = tbxNomUtilisateur.Text;
            string motDePasse = tbxMotDePasse.Password;

           

            if (nomUtilisateur == "")
            {
                tblNomUtilisateurErreur.Text = "Entrer un nom d'utilisateur";
            }
            else
            {
                if (!this.adminExist)
                {
                    if (nomUtilisateur.Length < 5)
                    {
                        tblNomUtilisateurErreur.Text = "minimun 5 caractères";
                    }
                    else
                    {
                        tblNomUtilisateurErreur.Text = "";
                    }
                }
                else
                {
                    tblNomUtilisateurErreur.Text = "";
                }
            }

            if (motDePasse == "")
            {
                tblMotDePasseErreur.Text = "Entrer un mot de passe";
            }
            else
            {
                if (!this.adminExist)
                {
                    if (motDePasse.Length < 8)
                    {
                        tblMotDePasseErreur.Text = "minimun 8 caractères";
                    }
                    else
                    {
                        tblMotDePasseErreur.Text = "";
                    }
                }
                else
                {
                    tblMotDePasseErreur.Text = "";
                }
            }

            if (tblNomUtilisateurErreur.Text == "" && tblMotDePasseErreur.Text == "")
            {
                
                if (this.adminExist)
                {
                    bool result = Singleton.getInstance().connexion(nomUtilisateur, motDePasse);

                    if (result)
                    {
                        this.navigationViewItemConnexion.Content = $"Se deconnecter({Session.NomUtilisateur})";
                        this.navigationViewItemConnexion.Icon = new SymbolIcon(Symbol.Switch);

                        this.Frame.Navigate(typeof(PageListeProjets));
                    }
                    else
                    {
                        tblNomUtilisateurErreur.Text = " nom d'utilisateur ou mot de passe incorrect";
                    }
                }
                else { 

                    Singleton.getInstance().enregistrerAdmin(nomUtilisateur, motDePasse);
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = rootConnexion.XamlRoot;
                    dialog.Title = "Login";
                    dialog.CloseButtonText = "OK";
                    dialog.Content = "compte créé!";

                    var result = await dialog.ShowAsync();
                    this.Frame.Navigate(typeof(PageConnexion));
                }
            }
        }
    }
}

