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
            //if (tbxNom.Text == "")
            //    tblNomErreur.Text = "Le nom est requis";
            //else
            //    tblNomErreur.Text = "";

            //if (tbxPrenom.Text == "")
            //    tblPrenomErreur.Text = "Le prenom est requis";
            //else tblPrenomErreur.Text = "";

            ////if (DateTimeOffset.Compare(tbxDateNaissance.Date, DateTimeOffset.Now) < 18)
            ////    tblDateNaissanceErreur.Text = "date de naissance invalide";
            ////else
            ////    tblDateNaissanceErreur.Text = "";

            //if (DateTimeOffset.Compare(tbxDateEmbauche.Date, DateTimeOffset.Now) < 0)
            //    tblDateEmbaucheErreur.Text = "date embaubhe invalide";
            //else
            //    tblDateEmbaucheErreur.Text = "";

            ////if (tbxDateNaissance.Text == "")
            ////    tblDateNaissanceErreur.Text = "La DateNaissance est requis";
            ////else tblDateNaissanceErreur.Text = "";

            ////if (tbxDateEmbauche.Text == "")
            ////    tblDateEmbaucheErreur.Text = "La tbxDateEmbauche est requis";
            ////else tblDateEmbaucheErreur.Text = "";

            //if (tbxEmail.Text == "")
            //    tblEmailErreur.Text = "La tbxEmail est requis";
            //else tblEmailErreur.Text = "";

            //if (tbxAdresse.Text == "")
            //    tblAdresseErreur.Text = "La tbxAdresse est requis";
            //else tblAdresseErreur.Text = "";

            //if (tbxTauxHoraire.Text == "")
            //    tblTauxHoraireErreur.Text = "La tbxTauxHoraire est requis";
            //else tblTauxHoraireErreur.Text = "";

            //if (tbxPhoto.Text == "")
            //    tblPhotoErreur.Text = "La tbxPhoto est requis";
            //else tblPhotoErreur.Text = "";

            //if (tbxStatut.Text == "")
            //    tblStatutErreur.Text = "La tbxStatut est requis";
            //else tblStatutErreur.Text = "";

            //if (tbxNbHeure.Text == "")
            //    tblNbHeureErreur.Text = "La NbHeure est requis";
            //else tblNbHeureErreur.Text = "";


            if (this.ContentDialog_PrimaryButtonClick != null)
            {

                //if (tblNomErreur.Text == "" && tblPrenomErreur.Text == "" && tblDateNaissanceErreur.Text == "" && tblDateEmbaucheErreur.Text == "" && tblEmailErreur.Text == "" && tblAdresseErreur.Text == "" && tblTauxHoraireErreur.Text == "" && tblPhotoErreur.Text == "" && tblStatutErreur.Text == "" && tblNbHeureErreur.Text == "")
                //{
                //    Employe employe = new Employe(null, tblNomErreur.Text, tblPrenomErreur.Text, Convert.ToDateTime(tblDateNaissanceErreur.Text), tblEmailErreur.Text, tblAdresseErreur.Text, Convert.ToDateTime(tblDateEmbaucheErreur.Text), Convert.ToDouble(tblTauxHoraireErreur.Text), tblPhotoErreur.Text, tblStatutErreur.Text,Convert.ToDouble(tblNbHeureErreur.Text));

                //    Singleton.getInstance().ajouterEmploye(employe);

                //}

                if (!string.IsNullOrWhiteSpace(tblDateNaissanceErreur.Text) && DateTime.TryParse(tblDateNaissanceErreur.Text, out DateTime dateNaissance) &&
                    !string.IsNullOrWhiteSpace(tblDateEmbaucheErreur.Text) && DateTime.TryParse(tblDateEmbaucheErreur.Text, out DateTime dateEmbauche) &&
                    !string.IsNullOrWhiteSpace(tblTauxHoraireErreur.Text) && double.TryParse(tblTauxHoraireErreur.Text, out double tauxHoraire) &&
                    !string.IsNullOrWhiteSpace(tblNbHeureErreur.Text) && double.TryParse(tblNbHeureErreur.Text, out double nbHeure))
                {
                    Employe employe = new Employe(null, tblNomErreur.Text, tblPrenomErreur.Text, tblDateNaissanceErreur.Text, tblEmailErreur.Text, tblAdresseErreur.Text, tblDateEmbaucheErreur.Text, tblTauxHoraireErreur.Text, tblPhotoErreur.Text, tblStatutErreur.Text, tblNbHeureErreur.Text);
                    Singleton.getInstance().ajouterEmploye(employe);
                }
                else
                {
                    this.Closing += AjouterEmployeDialogue_Closing;
                }
            }

        }
        private void AjouterEmployeDialogue_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            args.Cancel = true;

        }
    }
}
