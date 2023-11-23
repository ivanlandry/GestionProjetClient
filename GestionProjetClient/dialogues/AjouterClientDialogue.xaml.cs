using GestionProjetClient.Classes;
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

namespace GestionProjetClient.dialogues
{
    public sealed partial class AjouterClientDialogue :ContentDialog
    {
   
        public AjouterClientDialogue()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            if (tbxNom.Text == "")
                tblNomErreur.Text = "le nom est requis";
            else
                tblNomErreur.Text = "";

            if (tbxAdresse.Text == "")
                tblAdresseErreur.Text = "l'adrese est requis";
            else
                tblAdresseErreur.Text = "";

            if (tbxtelephone.Text == "")
                tblTelephoneErreur.Text = "le telephone est requis";
            else
            {
                if (Validation.validerTelephone(tbxtelephone.Text))
                {
                    tblTelephoneErreur.Text = "";
                }else
                    tblTelephoneErreur.Text = "Num�ro invalide invalide";
            }

            if (tbxEmail.Text == "")
                tblEmailErreur.Text = "l'email est requis";
            else
            {
                if (Validation.validerEmail(tbxEmail.Text))
                {
                    tblEmailErreur.Text = "";
                }else
                    tblEmailErreur.Text = "Adresse email invalide";
            }
                

            if(this.ContentDialog_PrimaryButtonClick != null)
            {
                if (tblAdresseErreur.Text == "" && tblEmailErreur.Text == "" && tblNomErreur.Text == "" && tblTelephoneErreur.Text == "")
                {
                    Client client = new Client(0,tbxNom.Text,tbxAdresse.Text,tbxtelephone.Text,tbxEmail.Text);

                    Singleton.getInstance().ajouterClient(client);

                    
                }
                else
                {
                    this.Closing += AjouterClientDialogue_Closing;
                }
            }
        }
        private void AjouterClientDialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = true;
        }
    }
}