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
    public partial class login : Form
    {


        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");








        public login()
        {
            InitializeComponent();
            
            
          

        }

       


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            try
            {
       


        MySqlCommand cmd = this.con.CreateCommand();

                    cmd.CommandText = "SELECT id from admin WHERE email=@email and mdp=@mdp";//requete contre les attaquez d'injections .
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@mdp", textBox2.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows == false)
                    {
                        MessageBox.Show("donnes incorect");
                    reader.Close();
                    label3.Visible = true;
                    textBox1.Clear();
                    textBox2.Clear();


                    }
                    else
                    {
                        while (reader.Read())
                        {

                            string id = reader.GetString(0);
                            principal form2 = new principal(id);
                            form2.Show();
                            this.Hide();
                        }
                    }
               
            }
            catch(MySqlException erreur)
            {
                MessageBox.Show("erreur" + erreur);
            }
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur de connexion"+erreur);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
            textBox2.UseSystemPasswordChar = false;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox2.Visible = true;
            textBox2.UseSystemPasswordChar = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("veuillez contactez l'administration pour  avoir vos cordonnées avec votre identité");
        }
    }

   
}
