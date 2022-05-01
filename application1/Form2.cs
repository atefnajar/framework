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
    public partial class Form2 : Form
    {
        string  id;
        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Form2( string id)
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
                MessageBox.Show("" + erreur);
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








        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
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
            Form3 produit = new Form3();
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

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();


        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
