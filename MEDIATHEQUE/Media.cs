using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDIATHEQUE
{
    public class Media
    {
        private string nom;
        private string format;
        private string taille;
        private string texte;

        private string type;
        public string Nom{
            get{ return nom;}
            set{ nom = value;}
            }

        public string Texte
        {
            get { return texte; }
            set { texte = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string Format
        {
            get { return format;}
            set { format = value;}
        }

        public string Taille
        {
            get { return taille; }
            set { taille = value; }
        }


        public Media(string nom, string format, string taille, string texte, string type)
        {
            this.nom = nom;
            this.format = format;
            this.taille = taille;
            this.texte = texte;
            this.type = type;
        }

    }
}
