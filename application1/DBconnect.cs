using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace application1
{
    class DBConnect
    {
        private MySqlConnection con;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {

         this.con = new MySqlConnection(@"data source=localhost;user id=root;password='';database=gestion de stock");

           
        }

        //open connection to database
        private bool OpenConnection()

        {
            return false;
        }
        

        //Close connection
        private int existe()
        {
            MySqlCommand cmd1 = this.con.CreateCommand();
            cmd1.CommandText = " select * from @table where @champ=@valeur";
            cmd1.Parameters.AddWithValue("@champ", chmaps);
            cmd1.Parameters.AddWithValue("@valeur", textBox1.Text);
            cmd1.Parameters.AddWithValue("@table", textBox1.Text);
            try {
                MySqlDataReader reader = cmd1.ExecuteReader();
               if (reader.HasRows == true){
                    return 1;
                }else
                {
                    return 0;
                }

            }catch( MySqlException erreur)
            {
               return -1;
            }
        }

        //Insert statement
        public void Insert()
        {
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string>[] Select()
        {
            return [1, 2];
        }

        //Count statement
        public int Count()
        {
            return 5;
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}






