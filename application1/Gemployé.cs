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
    public partial class Gemployé : Form
    {
        string id;



        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Gemployé(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.con.Open();
          
            //affichage de tous les employes 
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from admin ";

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

                        employes.Items.Add(item);





                    }
                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from administration where codeAdministration=@code ";
            cmd.Parameters.AddWithValue("@code", textBox5.Text);
            try
            {

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows == false)
                {
                    panel7.Visible = true;
                    label8.Visible = true;
                    label4.Visible = false;
                    reader.Close();
                }
                else
                {
                    panel7.Visible = false;
                    label7.Visible = true;
                    panel6.Visible = true;
                    employes.Visible = true;
                    reader.Close();

                }
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("" + erreur);
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from admin where id=@id";
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);


            try
            {
                MySqlDataReader reader = cmd1.ExecuteReader();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("verifier les donnes");
                    reader.Close();
                }
                else if (reader.HasRows == true)
                {

                    MessageBox.Show("employe existe dans la base veillez verifier ");

                    reader.Close();
                }

                else
                {
                    reader.Close();

                    MySqlCommand cmd = this.con.CreateCommand();

                    cmd.CommandText = " insert into admin values(@id,@nom,@prenom,@mail,@pass,@tel)";
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@nom", textBox2.Text);
                    cmd.Parameters.AddWithValue("@prenom", textBox3.Text);
                    cmd.Parameters.AddWithValue("@mail", textBox4.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox7.Text);
                    cmd.Parameters.AddWithValue("@tel", textBox6.Text);


                    try
                    {

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ajout avec succes");
                        this.Close();
                        Gemployé form7 = new Gemployé(this.id);
                        form7.Show();




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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("veuillez vous selectionnez un employé a modifier");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("vous vous vraiment modifier?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " update admin set  nom=@nom , prenom=@prenom , email=@mail,mdp=@mdp,telephone=@tel where id=@id"; //requete contre les attaquez d'injections .
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@nom", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@prenom", textBox3.Text);

                    cmd1.Parameters.AddWithValue("@mail", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@tel", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@mdp", textBox7.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("modification avec succes");
                        this.Close();
                        Gemployé form7 = new Gemployé(this.id);
                        form7.Show();

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
                MessageBox.Show("veuiller selectionnez l'employé a supprimer");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("vous vous vraiment supprimer cet employé?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " delete from employe  where id=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("supprission avec succes");
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

        private void employes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employes.SelectedItems.Count == 0)
            {


            }
            else
            {


                MySqlCommand cmd = this.con.CreateCommand();
                cmd.CommandText = "SELECT * from admin where id=@id";
                cmd.Parameters.AddWithValue("@id", employes.SelectedItems[0].Text.ToString());
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(0);
                        textBox2.Text = reader.GetString(1);
                        textBox3.Text = reader.GetString(2);
                        textBox4.Text = reader.GetString(3);
                        textBox6.Text = reader.GetString(4);
                        textBox7.Text = reader.GetString(5);


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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            textBox5.UseSystemPasswordChar = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            textBox5.UseSystemPasswordChar = true;
        }
    }
    }

