using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace application1
{
    public partial class principal : Form
    {
        string  id;
        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public principal( string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.con.Open();
            DateTime thisDay = DateTime.Today;
            textBox2.Text = thisDay.ToString();
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT prenom,nom from admin WHERE id = @id ";//requete contre les attaquez d'injections .
            cmd.Parameters.AddWithValue("@id", this.id);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString(0)+" "+reader.GetString(1);
                }
            }
            reader.Close();
            //nombre clients
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = "SELECT id from client  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox3.Text = nb.ToString();
                reader1.Close();
            }catch(MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }


            //nombre fournisseur


            MySqlCommand cmd2 = this.con.CreateCommand();
            cmd2.CommandText = "SELECT idFournisseur from fournisseur  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd2.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox4.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }


            
            //nombre d employee

            MySqlCommand cmd3 = this.con.CreateCommand();
            cmd3.CommandText = "SELECT id from admin  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd3.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox5.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("atef" + erreur);
            }


            //nombre achats

            MySqlCommand cmd4 = this.con.CreateCommand();
            cmd4.CommandText = "SELECT idAchat from achat  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd4.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox6.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }
            //nombre vente

            MySqlCommand cmd5 = this.con.CreateCommand();
            cmd5.CommandText = "SELECT idVente from vente  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd5.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox7.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }
            //nombre produit
            MySqlCommand cmd6 = this.con.CreateCommand();
            cmd6.CommandText = "SELECT  idProduit from produit ";
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd6.ExecuteReader();
                while (reader1.Read())
                {

                    nb++;
                }
                textBox8.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }


            //vente nom payé
            MySqlCommand cmd7 = this.con.CreateCommand();
            cmd7.CommandText = "SELECT mouvement from vente  ";//requete contre les attaquez d'injections .
            try
            {
                var nb = 0;
                MySqlDataReader reader1 = cmd7.ExecuteReader();
                while (reader1.Read())
                {
                    if (reader1.GetString(0) == "non payé")
                    {
                        nb++;
                    }
                }
                vnp.Text = nb.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }


            //caisse
            MySqlCommand cmd8 = this.con.CreateCommand();
            cmd8.CommandText = "SELECT prixVente from vente  ";
            try
            {
                MySqlDataReader reader1 = cmd8.ExecuteReader();
                decimal total = 0;
                var ch = "";
              
                decimal ch1 = 0;


                while (reader1.Read())
                {
                   ch=reader1.GetString(0);
                    ch1 = Decimal.Parse(ch);
                    total = total + ch1;
                    ch = "";

                 
                }
                
                c.Text = total.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }



            //caisse
            MySqlCommand cmd9 = this.con.CreateCommand();
            cmd7.CommandText = "SELECT m_vente from vente  ";
            try
            {
                decimal total = 0;
                var ch = "";
                decimal ch1 = 0;
                MySqlDataReader reader1 = cmd8.ExecuteReader();
                while (reader1.Read())
                {
                    ch = reader1.GetString(0);
                    ch1 = Decimal.Parse(ch);
                    total = total + ch1;
                    ch = "";


                }

                b.Text = total.ToString();
                reader1.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }



        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gvente vente = new Gvente(this.id);
            vente.Show();
            this.Hide();
                
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gfournisseur fournisseur = new Gfournisseur(this.id);
            fournisseur.Show();
            this.Hide();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            login form1 = new login();
            form1.Show();
           

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gproduit produit = new Gproduit(this.id);
            produit.Show();
            this.Hide();


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Gfournisseur fournisseur = new Gfournisseur(this.id);
            fournisseur.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
            login form1 = new login();
            form1.Show();


        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Gproduit produit = new Gproduit(this.id);
            produit.Show();
            this.Hide();

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Gclients client = new Gclients(this.id);
            client.Show();
            this.Hide();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gclients client = new Gclients(this.id);
            client.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Gemployé employe = new Gemployé(this.id);
            employe.Show();
            this.Hide();
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gemployé employe = new Gemployé(this.id);
            employe.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Gvente vente = new Gvente(this.id);
            vente.Show();
            this.Hide();

        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gachat achat = new Gachat(this.id);
            achat.Show();
            this.Hide();

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Gachat achat = new Gachat(this.id);
            achat.Show();
            this.Hide();
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            parametre para = new parametre(this.id);
            para.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            parametre para = new parametre(this.id);
            para.Show();
            this.Hide();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}
