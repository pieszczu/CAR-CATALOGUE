using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;



namespace NewCarCatalogue
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;");
        
        public MainWindow()
        {
            InitializeComponent();
            wrzuc();
        }

        private void wrzuc()
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Brand FROM dbo.Cars", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Brand"].ToString();
                        COMBO1.Items.Add(value);
                    }
                    reader.Close();

                    

                    SqlCommand cmd3 = new SqlCommand("SELECT DISTINCT Color FROM dbo.Cars", sqlCon);
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        string value = reader3["Color"].ToString();
                        COMBO3.Items.Add(value);
                    }
                    reader3.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
                }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                // Czyszczenie zawartości COMBO2 przed dodaniem nowych modeli
                COMBO2.Items.Clear();

            // Pobranie wybranej marki z COMBO1
            string selectedBrand = COMBO1.SelectedItem.ToString();

            // Zapytanie SQL pobierające modele danej marki na podstawie wybranej marki z COMBO1
            SqlCommand cmd2 = new SqlCommand("SELECT DISTINCT Model FROM dbo.Cars WHERE Brand = @Brand", sqlCon);
            cmd2.Parameters.AddWithValue("@Brand", selectedBrand);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                string value = reader2["Model"].ToString();
                COMBO2.Items.Add(value);
            }
            reader2.Close();
                sqlCon.Close();
            }
        }
    }
        
}
