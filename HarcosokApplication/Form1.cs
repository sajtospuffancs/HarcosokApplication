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

        public Form1()
        {
            InitializeComponent();
            Databaseconn();
            Tablecreate();





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


    }
}
