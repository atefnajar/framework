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
    public partial class Gclients : Form

    {
        string id;



        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Gclients(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.con.Open();
            //affichage de tous les clients 
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from client ";

            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader.GetString(0));

                        item.SubItems.Add(reader.GetString(1));
                        item.SubItems.Add(reader.GetString(2));
                        item.SubItems.Add(reader.GetString(3));

                        clients.Items.Add(item);





                    }
                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from client where id=@id"; 
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);


            try
            {
                MySqlDataReader reader = cmd1.ExecuteReader();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" )
                {
                    MessageBox.Show("verifier les donnes");
                    reader.Close();
                }
                else if (reader.HasRows == true)
                {

                    MessageBox.Show("client existe dans la base veillez verifier ");

                    reader.Close();
                }

                else
                {
                    reader.Close();

                    MySqlCommand cmd = this.con.CreateCommand();

                    cmd.CommandText = " insert into client values(@id,@nom,@prenom,@mail)"; 
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@nom", textBox2.Text);
                    cmd.Parameters.AddWithValue("@prenom", textBox3.Text);
                    cmd.Parameters.AddWithValue("@mail", textBox4.Text);


                    try
                    {

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ajout avec succes");
                        this.Close();
                        Gclients form6 = new Gclients(this.id);
                        form6.Show();




                    }
                    catch (MySqlException erreur)

                    {
                        MessageBox.Show("erreur" + erreur);

                    }
                }

            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("veuillez vous selectionnez un client a modifier");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("vous vous vraiment modifier?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " update client set  nom=@nom , prenom=@prenom , mail=@mail where id=@id"; //requete contre les attaquez d'injections .
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@nom", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@prenom", textBox3.Text);
                
                    cmd1.Parameters.AddWithValue("@mail", textBox4.Text);
                


                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("modification avec succes");
                        this.Close();
                        Gclients form6 = new Gclients(this.id);
                        form6.Show();

                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("" + erreur);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("veuiller selectionnez le client a supprimer");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("vous vous vraiment supprimer ce client?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " delete from client  where id=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("supprision avec succes");
                        this.Close();
                        Gclients form6 = new Gclients(this.id);
                        form6.Show();


                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("" + erreur);
                    }
                }

                else
                {
                    MessageBox.Show("ce produit n'existe pas dans la base veillez selectionnez ");
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();
        }

        private void clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clients.SelectedItems.Count == 0)
            {


            }
            else
            {


                MySqlCommand cmd = this.con.CreateCommand();
                cmd.CommandText = "SELECT * from client where id=@id";
                cmd.Parameters.AddWithValue("@id", clients.SelectedItems[0].Text.ToString());
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(0);
                        textBox2.Text = reader.GetString(1);
                        textBox3.Text = reader.GetString(2);
                        textBox4.Text = reader.GetString(3);
                    
                        textBox1.ReadOnly = true;

                    }
                    reader.Close();
                }
                catch (MySqlException erreur)
                {
                    MessageBox.Show("erreur" + erreur);
                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
