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
    public partial class Form3 : Form
    {


       


        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from produit ";//requete contre les attaquez d'injections .
            try
            {
                this.con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader.GetString(0));

                        item.SubItems.Add(reader.GetString(1));
                        item.SubItems.Add(reader.GetString(2));
                        item.SubItems.Add(reader.GetString(3));
                        item.SubItems.Add(reader.GetString(4));
                        produits.Items.Add(item);





                    }
                }
                reader.Close();
            }catch(MySqlException erreur)
            {
                MessageBox.Show("erreur");
            }
         



        }

        private void produits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void button1_Click(object sender, EventArgs e)

        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("l id ne doit pas etre vide");
            }
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from produit where idProduit=@id"; //requete contre les attaquez d'injections .
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);


            try
            {
                MySqlDataReader reader = cmd1.ExecuteReader();

                if (reader.HasRows == true)
                {

                    MessageBox.Show("produit existe dans la base veillez verifier ");

                    reader.Close();
                }
               
                else
                {
                    reader.Close();
                
                    MySqlCommand cmd = this.con.CreateCommand();

                    cmd.CommandText = " insert into produit values(@id,@libelle,@pu,@type,@pv,@note)"; //requete contre les attaquez d'injections .
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@libelle", textBox2.Text);
                    cmd.Parameters.AddWithValue("@pu", textBox3.Text);
                    cmd.Parameters.AddWithValue("@type", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@pv", textBox4.Text);
                    cmd.Parameters.AddWithValue("@note", textBox5.Text);

                    try
                    {

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ajout avec succes");
                        this.Close();
                         Form3 form3 = new Form3();
                        form3.Show();

                       


                    }
                    catch (MySqlException erreur)

                    {
                        MessageBox.Show("erreur" + erreur);

                    }
                }

            }catch(MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();

            
        }
    }
}
