using GestionProjetClient.Classes;
using GestionProjetClient.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.Modification
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageZoomClients : Page
    {
        public PageZoomClients()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {

                Client client = e.Parameter as Client;
                tbxIdentifientModifier.Text = client.Identifiant.ToString();
                tbxNomModifier.Text = client.Nom.ToString();
                tbxAdresseModifier.Text = client.Adresse.ToString();
                tbxtelephoneModifier.Text = client.Telephone.ToString();
                tbxEmailModifier.Text = client.Email.ToString();

            }
        }
        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool erreur = false;
                bool pasValide = false;

                if (tbxNomModifier.Text == "")
                {
                    tblNomModifierErreur.Text = "le nom est requis";
                    erreur = true;
                }
                else
                {
                    tblNomModifierErreur.Text = "";
                }


                if (tbxAdresseModifier.Text == "")
                {
                    tblAdresseModifierErreur.Text = "l'adrese est requis";
                    erreur = true;
                }
                else
                {
                    tblAdresseModifierErreur.Text = "";
                }

                if (tbxtelephoneModifier.Text == "")
                {
                    tblTelephoneErreurModifier.Text = "le telephone est requis";
                    erreur = true;
                }
                else
                {
                    if (Validation.validerTelephone(tbxtelephoneModifier.Text))
                    {
                        tblTelephoneErreurModifier.Text = "";
                    }
                    else
                    {
                        tblTelephoneErreurModifier.Text = "Numéro invalide invalide";
                        pasValide = true;
                    }
                        
                }

                if (tbxEmailModifier.Text == "")
                {
                    tblEmailErreurModifier.Text = "l'email est requis";
                    erreur = true;
                }
                else
                {
                    if (Validation.validerEmail(tbxEmailModifier.Text))
                    {
                        tblEmailErreurModifier.Text = "";
                    }
                    else
                    {
                        tblEmailErreurModifier.Text = "Adresse email invalide";
                        pasValide=true;
                    }
                        
                }
                if (this.btModifier_Click != null)
                {
                    if (!erreur && !pasValide)
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
                            //Console.WriteLine("ds«d");
                            tblTexte.Text = "bouton Oui";
                            Client client = new Client(Convert.ToInt32(tbxIdentifientModifier.Text), tbxNomModifier.Text, tbxAdresseModifier.Text, tbxtelephoneModifier.Text, tbxEmailModifier.Text);

                            if (Convert.ToInt32(tbxIdentifientModifier.Text) == client.Identifiant)
                            {
                                Singleton.getInstance().modifierClient(client);

                                dialog.XamlRoot = mainpanel.XamlRoot;
                                dialog.Title = "Information";
                                dialog.CloseButtonText = "OK";
                                dialog.Content = "Produit Modifier avec success";

                                var result = await dialog.ShowAsync();

                                this.Frame.Navigate(typeof(PageListeClients));
                            }

                        }
                        else
                        {
                            tblTexte.Text = "ANNULATION";

                            //int id = Singleton.getInstance().getDernierClient();
                            dialog.XamlRoot = mainpanel.XamlRoot;
                            dialog.Title = "Information";
                            dialog.CloseButtonText = "OK";
                            dialog.Content = "Modification ANNULER";

                            var result = await dialog.ShowAsync();
                        }
                    }
                    else { }
                }
                        
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
