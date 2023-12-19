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
using Microsoft.VisualBasic;
using Org.BouncyCastle.Tls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.dialogues
{
    public sealed partial class AjouterEmployeDialogue : ContentDialog
    {
        string resultatRadioButton = "";
        private bool close = false;

        public AjouterEmployeDialogue()
        {
            this.InitializeComponent();
            tbxDateNaissance.Date = DateTime.Now;
            tbxDateEmbauche.Date = DateTime.Now;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            bool erreur = false;
            //String rastatut = "";

            if (tbxNom.Text == "")
            {
                tblNomErreur.Text = "Le nom est requis";
                erreur = true;
            }
            else
            {
                tblNomErreur.Text = "";
            }

            if (tbxPrenom.Text == "")
            {
                tblPrenomErreur.Text = "Le prenom est requis";
                erreur = true;

            }
            else
            {
                tblPrenomErreur.Text = "";
            }

            if (!Validation.validerDateNaissance(tbxDateNaissance.Date))
                tblDateNaissanceErreur.Text = "date de naissance invalide";
            else
                tblDateNaissanceErreur.Text = "";




            if (!Validation.validerDateEmbauche(tbxDateEmbauche.Date))
                tblDateEmbaucheErreur.Text = "date embauche invalide";
            else
                tblDateEmbaucheErreur.Text = "";


            //if (tbxEmail.Text == "")
            //{
            //    tblEmailErreur.Text = "l'email est requis";
            //    erreur = true;
            //}
            //else
            //{
                if (!Validation.validerEmail(tbxEmail.Text) || tbxEmail.Text == "")
                {
                    tblEmailErreur.Text = "Adresse email invalide";
                    erreur = true;
                }
                else
                    tblEmailErreur.Text = "";
            
            //}

            if (tbxAdresse.Text == "")
            {
                tblAdresseErreur.Text = "La tbxAdresse est requis";
                erreur = true;
            }
            else
            { 
                tblAdresseErreur.Text = ""; 
            }

            //if (tbxTauxHoraire.Text == "")
            //{
            //    tblTauxHoraireErreur.Text = "La tbxTauxHoraire est requis";
            //    erreur = true;
            //}
            //else
            //{
                if (tbxTauxHoraire.Text == "" || !Validation.validerTauxHoraire(Convert.ToDouble(tbxTauxHoraire.Text)))
                {
                    tblTauxHoraireErreur.Text = "Les taux horaire est invalide";
                    erreur = true;
                }
                else
                    tblTauxHoraireErreur.Text = "";
            //    tblTauxHoraireErreur.Text = "Les taux horaire est invalide";
            //}

            if (tbxPhoto.Text == "")
            {
                tblPhotoErreur.Text = "La tbxPhoto est requis";
                erreur = true;
            }
            else { tblPhotoErreur.Text = ""; }

            if(resultatRadioButton =="")

            if (tbxNbHeure.Text == "")
            {
                tblNbHeureErreur.Text = "La NbHeure est requis";
                erreur = true;
            }
            else { tblNbHeureErreur.Text = ""; }

            string statut = string.Empty;

            if (this.ContentDialog_PrimaryButtonClick != null)
            {
                try
                {
                    if (!erreur)
                    {
                        Employe employe = new Employe("hg", tbxNom.Text, tbxPrenom.Text, tbxDateNaissance.Date.ToString("yyyy-MM-dd"), tbxEmail.Text, tbxAdresse.Text, tbxDateEmbauche.Date.ToString("yyyy-MM-dd"), tbxTauxHoraire.Text, tbxPhoto.Text, resultatRadioButton, tbxNbHeure.Text);
                        Singleton.getInstance().ajouterEmploye(employe);
                        this.close = false;
                    }
                    else
                    {
                        this.close = true;
                    }
                    this.Closing += AjouterEmployeDialogue_Closing;
                }
                catch (MySqlException e)
                {
                    Console.WriteLine(e);
                }

            }

        }

        private void AjouterEmployeDialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = this.close;

        }

        private void rastatut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(rastatut.SelectedItem != null)
            {
                resultatRadioButton = rastatut.SelectedItem.ToString();
            }
        }

        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {

        }


        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.close = false;
            this.Closing += AjouterEmployeDialogue_Closing;
        }
    }
}
