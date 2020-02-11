using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Collections;

namespace MEDIATHEQUE
{
   


    public partial class Form1 : Form
    {    //variable pour stocker les medias
        public Media media;
        //variable pour stocker le fichier
        string fichier;

        string type_fichier;
        public static OleDbConnection con = new OleDbConnection();
        public static void seconnecter()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/media.accdb");
            con.Open();
        }
        StreamReader st;
        string url;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nom.Text != "")
            {
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.Multiselect = false;
                openFileDialog1.InitialDirectory = @"C:/";
                //choix suivant le type de fichier de media audio, texte, video ou photo
                switch (type.Text)
                {
                    case "TEXTE":
                        openFileDialog1.Filter = "Fichiers Textes (*.txt)| *.txt";

                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileInfo = new FileInfo(openFileDialog1.FileName);

                            url = fileInfo.Name;
                            if (File.Exists("C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + url))
                            {
                                MessageBox.Show("le fichier existe deja", "ATTENTION");
                            }
                            else
                            {
                                textBox4.Text = openFileDialog1.FileName;
                                type_fichier = "TEXTE";
                              
                                File.Copy(fileInfo.FullName, "C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + nom.Text + "_" + format.Text + "_" + taille.Text + ".txt");
                                MessageBox.Show("fichier copié avec success dans Media!!!");
                            }
                        }
                        break;

                    case "IMAGE":
                        openFileDialog1.Filter = "Fichiers JPEG *.JPEG| *.jpg";

                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileInfo = new FileInfo(openFileDialog1.FileName);
                            url = fileInfo.Name;
                            if (File.Exists("C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + url))
                            {
                                MessageBox.Show("le fichier existe deja", "ATTENTION");
                            }
                            else
                            {
                              
                                textBox4.Text = openFileDialog1.FileName;
                                type_fichier = "IMAGE";

                                // copie le fichier dans un repertoire
                                File.Copy(fileInfo.FullName, "C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + nom.Text + "_" + format.Text + "_" + taille.Text + ".jpg");
                                MessageBox.Show("fichier copié avec success dans Media!!!");
                            }
                        }
                        break;
                    case "AUDIO":
                        openFileDialog1.Filter = "Fichiers MP3 *.MP3| *.mp3";
                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileInfo = new FileInfo(openFileDialog1.FileName);
                            url = fileInfo.Name;
                            //on verifie que le fichier n est pas disponible dans le repertoire
                            if (File.Exists("C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + url))
                            {
                                MessageBox.Show("le fichier existe deja", "ATTENTION");
                            }
                            else
                            {
                                textBox4.Text = openFileDialog1.FileName;
                                type_fichier = "AUDIO";
                                // copie le fichier choisi dans un autre repertoire 
                                File.Copy(fileInfo.FullName, "C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + nom.Text + "_" + format.Text + "_" + taille.Text + ".mp3");
                                MessageBox.Show("fichier copié avec success dans Media!!!");
                            }
                        }
                        break;

                    case "VIDEO":
                        openFileDialog1.Filter = "Fichiers MP4 *.MP4| *.mp4";
                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var fileInfo = new FileInfo(openFileDialog1.FileName);
                            url = fileInfo.Name;
                            if (File.Exists("C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + url))
                            {
                                MessageBox.Show("le fichier existe deja", "ATTENTION");
                            }
                            else
                            {
                                textBox4.Text = openFileDialog1.FileName;
                                type_fichier = "VIDEO";

                                // copie le fichier choisi dans un autre repertoire 
                                File.Copy(fileInfo.FullName, "C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA" + "//" + nom.Text+"_" + format.Text+"_" + taille.Text + ".mp4");
                                MessageBox.Show("fichier copié avec success dans Media!!!");
                            }
                        }
                        break;
              
                }
            }
            else
            {
                MessageBox.Show("Entrer le nom du "+ type.Text +" !!!", "erreur");
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                //connecter a la base de donnees
                seconnecter();


                if (type.Text == "TEXTE")
                {
                    st = new StreamReader(textBox4.Text);
                    fichier = st.ReadToEnd();
                    st.Close();
                }
                else
                {
                    fichier = url;
                }

                media = new Media(nom.Text, format.Text, taille.Text, fichier, type_fichier);
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Texte VALUES(@nom, @format, @taille, @texte, @type);", con);
                cmd.Parameters.AddWithValue("@nom", media.Nom);
                cmd.Parameters.AddWithValue("@format", media.Format);
                cmd.Parameters.AddWithValue("@taille", media.Taille);
                cmd.Parameters.AddWithValue("@texte", media.Texte);
                cmd.Parameters.AddWithValue("@type", media.Type);

                cmd.ExecuteNonQuery();


                MessageBox.Show("Fichier enregistré", "SAUVEGARDE");
            }
            else
            {
                MessageBox.Show("Entrer le lien du texte!!!", "erreur");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            type.Text = "TEXTE";
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectoryInfo dossier = new DirectoryInfo("C:/Users/MICHEL/Documents/Visual Studio 2015/Projects/MEDIATHEQUE/MEDIA");
            FileInfo[] words = dossier.GetFiles();
          
            List<string> fiche = new List<string>();
            foreach(FileInfo fi in words)
            {
                fiche.Add(fi.Name);
            }
                          
            fiche.Sort();

         

            foreach (string fic in fiche)
            {
                MessageBox.Show(" " + fic);               
            }

            MessageBox.Show(" copie ");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nom.Text = "";
            format.Text = "";
            taille.Text = "";
            textBox4.Text = "";
        }
    }
}

