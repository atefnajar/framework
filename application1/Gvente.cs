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
    public partial class Gvente : Form
    {
        string id;



        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Gvente(string id)
        {
            this.id = id;

            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            this.con.Open();

            //affichage de tous les employes 
            MySqlCommand cmd = this.con.CreateCommand();
            cmd.CommandText = "SELECT * from vente ";

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
                        item.SubItems.Add(reader.GetString(6));
                        item.SubItems.Add(reader.GetString(7));
                        item.SubItems.Add(reader.GetString(8));
                        item.SubItems.Add(reader.GetString(9));

                        ventes.Items.Add(item);





                    }
                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur");
            }

            //remplir les employe
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd.CommandText = "SELECT id from admin ";
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    comboBox2.Items.Add(reader.GetString(0));

                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur type");
            }

            //remplir les clients
            MySqlCommand cmd2 = this.con.CreateCommand();
            cmd2.CommandText = "SELECT id from client ";
            try
            {
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {

                    comboBox3.Items.Add(reader.GetString(0));

                }
                reader.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur type");
            }
            //remplir les produits
            MySqlCommand cmd3 = this.con.CreateCommand();
            cmd3.CommandText = "SELECT idProduit from produit ";
            try
            {
                MySqlDataReader reader = cmd3.ExecuteReader();
                while (reader.Read())
                {

                    comboBox4.Items.Add(reader.GetString(0));

                }
                reader.Close();

            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("erreur type");
            }




        

        }

        private void button1_Click(object sender, EventArgs e)
        {
         

                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " select * from vente where idVente=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);


                    try
                    {
                        MySqlDataReader reader = cmd1.ExecuteReader();
                        if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.SelectedIndex<=0  || comboBox2.SelectedIndex <= 0  || comboBox3.SelectedIndex <= 0  || comboBox4.SelectedIndex <= 0)
                        {
                            MessageBox.Show("verifier les donnes");
                            reader.Close();
                        }
                        else if (reader.HasRows == true)
                        {

                            MessageBox.Show("vente existe dans la base veillez verifier ");

                            reader.Close();
                        }

                        else
                        {
                            reader.Close();

                            MySqlCommand cmd = this.con.CreateCommand();




                            cmd.CommandText = " insert into vente values(@id,@date,@qte,@pa,@pv,@mouvement,@profit,@admin,@client,@produit)";
                            cmd.Parameters.AddWithValue("@id", textBox1.Text);
                            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                            cmd.Parameters.AddWithValue("@qte", textBox2.Text);
                            cmd.Parameters.AddWithValue("@pa", textBox3.Text);
                            cmd.Parameters.AddWithValue("@pv", textBox4.Text);
                            cmd.Parameters.AddWithValue("@mouvement", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@profit", textBox5.Text);
                            cmd.Parameters.AddWithValue("@admin", comboBox2.Text);
                            cmd.Parameters.AddWithValue("@client", comboBox3.Text);
                            cmd.Parameters.AddWithValue("@produit", comboBox4.Text);


                            try
                            {

                                cmd.ExecuteNonQuery();
                                MessageBox.Show("ajout avec succes");
                                this.Close();
                                Gvente form8 = new Gvente(this.id);
                                form8.Show();




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
                
                
         
           


        

        private void ventes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ventes.SelectedItems.Count == 0)
            {


            }
            else
            {

                MySqlCommand cmd = this.con.CreateCommand();
                cmd.CommandText = "SELECT * from vente where idVente=@id";
                cmd.Parameters.AddWithValue("@id", ventes.SelectedItems[0].Text.ToString());
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(0);
                        dateTimePicker1.Value = Convert.ToDateTime(reader.GetString(1));
                        textBox2.Text = reader.GetString(2);
                        textBox3.Text = reader.GetString(3);
                        textBox4.Text = reader.GetString(4);
                        textBox5.Text = reader.GetString(5);
                        comboBox1.Text = reader.GetString(6);
                        comboBox2.Text = reader.GetString(7);
                        comboBox3.Text = reader.GetString(8);
                        comboBox4.Text = reader.GetString(9);




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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox1.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("veuillez vous selectionnez une vente a modifier");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("vous vous vraiment modifier?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " update vente set  dateVente=@date , prixAchat=@pa , quantiteVente=@qte,prixVente=@pv,mouvement=@mouvement,m_vente=@mvente,admin=@admin,client=@client,produit=@produit where idVente=@id"; //requete contre les attaquez d'injections .
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                    cmd1.Parameters.AddWithValue("@qte", textBox2.Text);

                    cmd1.Parameters.AddWithValue("@pa", textBox3.Text);
                    cmd1.Parameters.AddWithValue("@pv", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@mouvement", comboBox1.Text);
                    cmd1.Parameters.AddWithValue("@mvente", textBox5.Text);
                    cmd1.Parameters.AddWithValue("@admin", comboBox2.Text);
                    cmd1.Parameters.AddWithValue("@client", comboBox3.Text);
                    cmd1.Parameters.AddWithValue("@produit", comboBox4.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("modification avec succes");
                        this.Close();
                        Gvente form8 = new Gvente(this.id);
                        form8.Show();

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
                MessageBox.Show("veuiller selectionnez la vente  a supprimer");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("vous vous vraiment supprimer cette vente ?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " delete from vente  where idVente=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("supprission avec succes");
                        this.Close();
                        Gvente form8 = new Gvente(this.id);
                        form8.Show();


                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("" + erreur);
                    }
                }

                else
                {
                    MessageBox.Show("cette vente n'existe pas dans la base veillez selectionnez ");
                }
            }

        }
    }
    }

