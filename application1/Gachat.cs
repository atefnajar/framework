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
    public partial class Gachat : Form
    {
        string id;



        MySqlConnection con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

        public Gachat(string id)
        {this.id= id;   
            InitializeComponent();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.con.Open();
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from achat where idAchat=@id";
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);


            try
            {
                MySqlDataReader reader = cmd1.ExecuteReader();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" )
                {
                    MessageBox.Show("verifier les donnes");
                    reader.Close();
                }
                else if (reader.HasRows == true)
                {

                    MessageBox.Show("achat existe dans la base veillez verifier ");

                    reader.Close();
                }

                else
                {
                    reader.Close();

                    MySqlCommand cmd = this.con.CreateCommand();

                    cmd.CommandText = " insert into vente values(@id,@qte,@produit,@frs,@admin,@note)";
                    cmd.Parameters.AddWithValue("@id", textBox1.Text);
                    
                    cmd.Parameters.AddWithValue("@qte", textBox2.Text);
                    cmd.Parameters.AddWithValue("@produit", textBox3.Text);
                    cmd.Parameters.AddWithValue("@frs", textBox4.Text);
                 
                    cmd.Parameters.AddWithValue("@admin", textBox5.Text);
                    cmd.Parameters.AddWithValue("@note", textBox6.Text);
            


                    try
                    {

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("ajout avec succes");
                        this.Close();
                        Gachat form9 = new Gachat(this.id);
                        form9.Show();




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
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("veuillez vous selectionnez une achat a modifier");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("vous vous vraiment modifier?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " update achat set  quantite=@qte , produit=@produit , fournisseur=@frs,admin=@admin,note=@note where idVente=@id"; 
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@qte", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@produit", textBox3.Text);
                    cmd1.Parameters.AddWithValue("@frs", textBox4.Text);
                    cmd1.Parameters.AddWithValue("@admin", textBox5.Text);
                    cmd1.Parameters.AddWithValue("@note", textBox6.Text);
                



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("modification avec succes");
                        this.Close();
                        Gachat form9 = new Gachat(this.id);
                        form9.Show();

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
                MessageBox.Show("veuiller selectionnez l'achat   a supprimer");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("vous vous vraiment supprimer cette achat ?", "Verification", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    MySqlCommand cmd1 = this.con.CreateCommand();
                    cmd1.CommandText = " delete from achat  where idAchat=@id";
                    cmd1.Parameters.AddWithValue("@id", textBox1.Text);



                    try
                    {
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("supprission avec succes");
                        this.Close();
                        Gachat form9 = new Gachat(this.id);
                        form9.Show();


                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("" + erreur);
                    }
                }

                else
                {
                    MessageBox.Show("cette achat n'existe pas dans la base veillez selectionnez ");
                }
            }


        }

        private void achats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (achats.SelectedItems.Count == 0)
            {


            }
            else
            {

                MySqlCommand cmd = this.con.CreateCommand();
                cmd.CommandText = "SELECT * from achat where idAchat=@id";
                cmd.Parameters.AddWithValue("@id", achats.SelectedItems[0].Text.ToString());
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(0);
                        textBox2.Text = reader.GetString(1);
                        textBox3.Text = reader.GetString(2);
                        textBox4.Text = reader.GetString(3);
                        textBox5.Text = reader.GetString(4);
                        textBox6.Text = reader.GetString(5);
                       




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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            principal form2 = new principal(this.id);
            form2.Show();
        }
    }
}
