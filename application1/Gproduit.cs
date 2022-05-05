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
    public partial class Gproduit : Form
    {

        string id;



        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Gproduit(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.con.Open();
            //affichage de tous les produits 
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from produit ";//requete contre les attaquez d'injections .

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
                        item.SubItems.Add(reader.GetString(4));
                        item.SubItems.Add(reader.GetString(5));

                        produits.Items.Add(item);





                    }
                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur");
            }

            //remplir le type depuit la base de donnée

            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd.CommandText = "SELECT type from produit ";
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    comboBox1.Items.Add(reader.GetString(0));

                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur type");
            }


        }

        private void produits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (produits.SelectedItems.Count == 0)
            {
              

            }
            else
            {


                MySqlCommand cmd = this.con.CreateCommand();
                cmd.CommandText = "SELECT * from produit where idProduit=@id";
                cmd.Parameters.AddWithValue("@id", produits.SelectedItems[0].Text.ToString());
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(0);
                        textBox2.Text = reader.GetString(1);
                        textBox3.Text = reader.GetString(2);
                        comboBox1.Text = reader.GetString(3);
                        textBox4.Text = reader.GetString(4);
                        textBox5.Text = reader.GetString(5);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)

        {
            

            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from produit where idProduit=@id"; //requete contre les attaquez d'injections .
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);


            try
            {
                MySqlDataReader reader = cmd1.ExecuteReader();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text== "")
                {
                    MessageBox.Show("verifier les donnes");
                    reader.Close();
                }
                else if (reader.HasRows == true)
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
                        Gproduit form3 = new Gproduit(this.id);
                        form3.Show();




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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("veuillez vous selectionnez un produit a modifier");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("vous vous vraiment modifier?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " update produit set  libelle=@libelle , prixachat=@prixachat , type=@type, prixvente=@prixvente , note=@note where idProduit=@id"; //requete contre les attaquez d'injections .
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@libelle", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@prixachat", textBox3.Text);
                    cmd1.Parameters.AddWithValue("@type", comboBox1.Text);
                    cmd1.Parameters.AddWithValue("@prixvente", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@note", textBox5.Text);


                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("modification avec succes");
                        this.Close();
                        Gproduit form3 = new Gproduit(this.id);
                        form3.Show();

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
                MessageBox.Show("veuiller selectionnez le produit a supprimer");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("vous vous vraiment supprimer ce produit?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " delete from produit  where idProduit=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("supprision avec succes");
                        this.Close();
                        Gproduit form3 = new Gproduit(this.id);
                        form3.Show();


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

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}