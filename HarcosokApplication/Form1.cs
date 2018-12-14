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
                this.Close();
            }
            
        }

        public void Databaseclose()
        {
            conn = new MySqlConnection("Server=localhost;Database=cs_harcosok;Uid=root;Password=;");
            conn.Close();
        }

    }
}
