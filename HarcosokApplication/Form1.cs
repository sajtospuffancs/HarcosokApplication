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

namespace HarcosokApplication
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        List<string> harcosnevek = new List<string>();

        public Form1()
        {
            InitializeComponent();

            Databaseconn();
            Tablecreate();
            neveklekeres();




            FormClosed += (sender, e) => Databaseclose();
        }

        public void Databaseconn()
        {
            try {
                conn = new MySqlConnection("Server=localhost;Database=cs_harcosok;Uid=root;Password=;");
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Nincs megfelelő adatbázis! A program leáll!");
                throw;
            }
            
        }
        public void Databaseclose()
        {
            conn = new MySqlConnection("Server=localhost;Database=cs_harcosok;Uid=root;Password=;");
            conn.Close();
        }
        public void Tablecreate()
        {
            var command1 = conn.CreateCommand();
            command1.CommandText = @"
            CREATE TABLE IF NOT EXISTS harcosok (
            id INTEGER PRIMARY KEY AUTO_INCREMENT,
            nev VARCHAR(255) UNIQUE,
            letrehozas DATE NOT NULL          
            );";
            command1.ExecuteNonQuery();
            var command2 = conn.CreateCommand();
            command2.CommandText = @"
            CREATE TABLE IF NOT EXISTS kepessegek (
            id INTEGER PRIMARY KEY AUTO_INCREMENT,
            nev TEXT ,
            leiras TEXT ,
            harcos_id INTEGER
            );";
            command2.ExecuteNonQuery();
            var command3 = conn.CreateCommand();
            command3.CommandText = @"
            ALTER TABLE kepessegek
            ADD FOREIGN KEY (harcos_id) REFERENCES harcosok(id);
            ";
            command3.ExecuteNonQuery();
        }

        private void letrehozas_Button_Click(object sender, EventArgs e)
        {
            string harcosnev = harcosNeveTextBox.Text;
            bool bennevan = true;
            foreach(string t in harcosnevek)
            { 
                if (harcosnev != t)
                {
                    bennevan = false;
                }

            }
            if (bennevan == true)
            {
                MessageBox.Show("Ez a név már foglalt! Kérlek válassz újat!");
            }
            else
            {
                
            }
            

        }

        public void neveklekeres()
        {
            var command1 = conn.CreateCommand();
            command1.CommandText = "SELECT nev FROM harcosok";
            MySqlDataReader nevolvasas = command1.ExecuteReader();
            try
            {
                if (nevolvasas.HasRows){
                    while (nevolvasas.Read())
                    {
                        string s = nevolvasas.GetString(0);
                        harcosnevek.Add(s);
                    }
                }
                else
                {
                    MessageBox.Show("Nincsenek sorok a táblában!");
                }
            }
            catch
            {
                MessageBox.Show("Hiba van a kiolvasásban!");
            }
            nevolvasas.Close();
            foreach (string t in harcosnevek)
            {
                harcosokListBox.Items.Add(t);
            }
        }


    }
}
