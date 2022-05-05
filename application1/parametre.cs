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
    public partial class parametre : Form
    {
        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");
        string id;
        public parametre(string id )
        {
            InitializeComponent();
            this.id = id;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            textBox1.ReadOnly=true;
            if (textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox6.Text != "")
            {
                MySqlCommand cmd1 = this.con.CreateCommand();
                cmd1.CommandText = "update admin set nom=@nom,prenom=@prenom,email=@email,mdp=@mdp,telephone=@tel where id=@id";
                cmd1.Parameters.AddWithValue("@id", this.id);
                cmd1.Parameters.AddWithValue("@nom", textBox2.Text);
                cmd1.Parameters.AddWithValue("@prenom", textBox3.Text);
                cmd1.Parameters.AddWithValue("@email", textBox4.Text);
                cmd1.Parameters.AddWithValue("@mdp", textBox5.Text);
                cmd1.Parameters.AddWithValue("@tel", textBox6.Text);
                try
                {
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("modificationavec succes");
                }
                catch (MySqlException erreur)
                {
                    MessageBox.Show("erreur"+erreur);
                }



            }
            else
            {
                MessageBox.Show("donnée vide");
            }
            


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = true;
            textBox5.UseSystemPasswordChar = false;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox3.Visible = false;
            textBox5.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.con.Open();
            textBox1.ReadOnly= true;
            textBox1.Text = this.id;
            

            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from admin where id=@id";
            cmd.Parameters.AddWithValue("@id", this.id);
            try
            {
                MySqlDataReader reader=cmd.ExecuteReader();
             
                while (reader.Read())
                {
                    textBox2.Text = reader.GetString(1);
                    textBox3.Text = reader.GetString(2);
                    textBox4.Text = reader.GetString(3);
                    textBox5.Text = reader.GetString(5);
                    textBox6.Text = reader.GetString(5);
                    

                }
                reader.Close();
            }
            catch(MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }

        }
    }
}
