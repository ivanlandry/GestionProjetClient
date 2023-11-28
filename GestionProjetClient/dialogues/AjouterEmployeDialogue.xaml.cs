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
using MySql.Data.MySqlClient;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.dialogues
{
    public sealed partial class AjouterEmployeDialogue : ContentDialog
    {
        public AjouterEmployeDialogue()
        {
            this.InitializeComponent();
            tbxDateNaissance.Date = DateTime.Now;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            bool erreur = false;
            //if (tbxMatricule.Text == "")
            //{
            //    tblMatriculeErreur.Text = "kdkd";
            //    erreur = true;
            //}
            //else
            //    tblMatriculeErreur.Text = "";
            

            if (tbxNom.Text == "")
            {
                tblNomErreur.Text = "Le nom est requis";
                 erreur = true;
            }
            else
                tblNomErreur.Text = "";

            if (tbxPrenom.Text == "")
            {
                tblPrenomErreur.Text = "Le prenom est requis";
            erreur = true;

            }
            else tblPrenomErreur.Text = "";

            //if (tbxDateNaissance.Date)
            //    tblDateNaissanceErreur.Text = "date de naissance invalide";
            //else
            //    tblDateNaissanceErreur.Text = "";

            //if (DateTimeOffset.Compare(tbxDateEmbauche.Date, DateTimeOffset.Now) < 0)
            //    tblDateEmbaucheErreur.Text = "date embauche invalide";
            //else
            //    tblDateEmbaucheErreur.Text = "";

            if (tbxEmail.Text == "")
            {
                tblEmailErreur.Text = "La tbxEmail est requis";
                erreur = true;
            }
            else tblEmailErreur.Text = "";

            if (tbxAdresse.Text == "")
            {
                tblAdresseErreur.Text = "La tbxAdresse est requis";
                erreur = true;
            }
            else tblAdresseErreur.Text = "";

            if (tbxTauxHoraire.Text == "")
            {
                tblTauxHoraireErreur.Text = "La tbxTauxHoraire est requis";
                erreur = true;
            }
            else tblTauxHoraireErreur.Text = "";

            if (tbxPhoto.Text == "")
            {
                tblPhotoErreur.Text = "La tbxPhoto est requis";
                erreur = true;
            }
            else tblPhotoErreur.Text = "";

            if (tbxStatut.Text == "")
            {
                tblStatutErreur.Text = "La tbxStatut est requis";
                erreur = true;
            }
            else tblStatutErreur.Text = "";

            if (tbxNbHeure.Text == "")
            {
                tblNbHeureErreur.Text = "La NbHeure est requis";
                erreur = true;
            }
            else tblNbHeureErreur.Text = "";


            if (this.ContentDialog_PrimaryButtonClick != null)
            {
                try
                {
                    if (!erreur)
                    {
                        Employe employe = new Employe("hg",tbxNom.Text, tbxPrenom.Text, Convert.ToString(tbxDateNaissance), tbxEmail.Text, tbxAdresse.Text, Convert.ToString(tbxDateEmbauche), tbxTauxHoraire.Text, tbxPhoto.Text, tbxStatut.Text, tbxNbHeure.Text);
                        Singleton.getInstance().ajouterEmploye(employe);
                    }
                    else
                    {
                        this.Closing += AjouterEmployeDialogue_Closing;
                    }
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e);
                }

            }

        }
        private void AjouterEmployeDialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = true;

        }
    }
}
