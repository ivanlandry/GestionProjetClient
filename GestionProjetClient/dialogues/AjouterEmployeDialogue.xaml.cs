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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GestionProjetClient.dialogues
{
    public sealed partial class AjouterEmployeDialogue : ContentDialog
    {
        public AjouterEmployeDialogue()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (tbxNom.Text == "")
                tblNomErreur.Text = "Le nom est requis";
            else
                tblNomErreur.Text = "";

            //if (tbxPrenom.Text == "")
            //    tblPrenomErreur.Text = "Le prenom est requis";
            //else
            //    tblPrenomErreur.Text = "";

            

            

        }
    }
}
