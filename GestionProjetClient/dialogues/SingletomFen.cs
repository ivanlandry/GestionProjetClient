using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjetClient.dialogues
{
    internal class SingletomFen
    {
        static SingletomFen instance = null;
        Window fenetre;
        public SingletomFen() { }

        public Window Fenetre { get => fenetre; set => fenetre = value; }

        public static SingletomFen getInstance()
        {
            if (instance == null)
                instance = new SingletomFen();
            return instance;
        }

    }
}
