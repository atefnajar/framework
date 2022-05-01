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
    public partial class Form1 : Form
    {
      
        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT id from admin WHERE email=@email and mdp=@mdp";//requete contre les attaquez d'injections .
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@mdp", textBox2.Text);

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)
                {
                    MessageBox.Show("donnes incorect");

                }
                else
                {
                    while (reader.Read())
                    {

                        string id = reader.GetString(0);
                        Form2 form2 = new Form2(id);
                        form2.Show();
                        this.Hide();
                    }
                }
                }catch(MySqlException erreur)
            {
                MessageBox.Show("invalide");
            }
            
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.con.Open();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur de connexion"+erreur);
            }
        }
    }

   
}
