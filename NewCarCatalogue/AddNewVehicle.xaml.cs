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
using System.Windows.Shapes;
using System.Data.SqlClient;

using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace NewCarCatalogue
{
    public partial class AddNewVehicle : Window
    {
        public AddNewVehicle()
        {
            InitializeComponent();
            windowLoaded();
        }
        
        private void windowLoaded()
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Brand FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Brand"].ToString();
                        addBrand.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Color FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Color"].ToString();
                        addColor.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Fuel FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Fuel"].ToString();
                        addFTypeBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Year FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Year"].ToString();
                        addYOProductionBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }  
            }
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                try
                {
                    object selected = addECapacityBox.SelectedItem;
                    addECapacityBox.Items.Clear();
                    if (selected != null)
                    {
                        addECapacityBox.SelectedItem = selected;
                    }
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Engine FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Engine"].ToString();
                        addECapacityBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void addBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                addModel.Items.Clear();

                string selectedBrand = addBrand.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedBrand))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Model FROM dbo.Cars2 WHERE Brand = @Brand", sqlCon);
                    cmd.Parameters.AddWithValue("@Brand", selectedBrand);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Model"].ToString();
                        addModel.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                }
            }
        }

        private void addModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void addFTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void addYOProductionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void addECapacityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void setPhotoSource(string source)
        {
        }

        private void addVehicle_Click(object sender, RoutedEventArgs e)
        {
            string newUserBrand = addBrand.Text;
            string newUserModel = addModel.Text;
            string newUserFuelType = addFTypeBox.Text;
            string newUserYear = addYOProductionBox.Text;
            string newUserEngine = addECapacityBox.Text;
            string newUserColor = addColor.Text;
            string newUserPhoto = source.Content.ToString();

            if (string.IsNullOrEmpty(newUserBrand) || string.IsNullOrEmpty(newUserModel) || string.IsNullOrEmpty(newUserFuelType) || string.IsNullOrEmpty(newUserYear) || string.IsNullOrEmpty(newUserEngine) ||string.IsNullOrEmpty(newUserColor) || string.IsNullOrEmpty(newUserPhoto))
            {
                if (string.IsNullOrEmpty(newUserBrand))
                {
                    MessageBox.Show("Add brand!");
                }
                if (string.IsNullOrEmpty(newUserModel))
                {
                    MessageBox.Show("Add model!");
                }
                if (string.IsNullOrEmpty(newUserFuelType))
                {
                    MessageBox.Show("Add Fuel Type!");
                }
                if (string.IsNullOrEmpty(newUserYear))
                {
                    MessageBox.Show("Add year!");
                }
                if (string.IsNullOrEmpty(newUserEngine))
                {
                    MessageBox.Show("Add Engine Capacity!");
                }
                if (string.IsNullOrEmpty(newUserColor))
                {
                    MessageBox.Show("Add color!");
                }
                if (string.IsNullOrEmpty(newUserPhoto))
                {
                    MessageBox.Show("Add photo!");
                }
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MAX(ID) FROM dbo.Cars2", sqlCon);
       
                    object result = cmd.ExecuteScalar();
                    sqlCon.Close();

                    sqlCon.Open();
                    SqlCommand cmdd = new SqlCommand("SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS EXIS FROM dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color", sqlCon);
                    cmdd.Parameters.AddWithValue("@Brand", newUserBrand);
                    cmdd.Parameters.AddWithValue("@Model", newUserModel);
                    cmdd.Parameters.AddWithValue("@Fuel", newUserFuelType);
                    cmdd.Parameters.AddWithValue("@Year", newUserYear);
                    cmdd.Parameters.AddWithValue("@Engine", newUserEngine);
                    cmdd.Parameters.AddWithValue("@Color", newUserColor);
                    
                    int resultt = (int)cmdd.ExecuteScalar();
                    
                    if (resultt == 1)
                    {
                        MessageBox.Show("Exists in database!");
                    }
                    else
                    {
                        if (result != DBNull.Value && result != null)
                        {
                            int id = (int)cmd.ExecuteScalar() + 1;
                            SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.Cars2 (ID, Brand, Model, Fuel, Year, Engine, Color, Photo) SELECT @id, @Brand, @Model, @Fuel, @Year, @Engine, @Color, BulkColumn FROM OPENROWSET(BULK N'" + newUserPhoto + "', SINGLE_BLOB) AS BLOB;", sqlCon);
                            cmd2.Parameters.AddWithValue("@Brand", newUserBrand);
                            cmd2.Parameters.AddWithValue("@Model", newUserModel);
                            cmd2.Parameters.AddWithValue("@Fuel", newUserFuelType);
                            cmd2.Parameters.AddWithValue("@Year", newUserYear);
                            cmd2.Parameters.AddWithValue("@Engine", newUserEngine);
                            cmd2.Parameters.AddWithValue("@Color", newUserColor);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.ExecuteNonQuery();
                            sqlCon.Close();
                            MessageBox.Show("Added!");
                        }
                        else
                        {
                            int id = 1;
                            SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.Cars2 (ID, Brand, Model, Fuel, Year, Engine, Color, Photo) SELECT @id, @Brand, @Model, @Fuel, @Year, @Engine, @Color, BulkColumn FROM OPENROWSET(BULK N'" + newUserPhoto + "', SINGLE_BLOB) AS BLOB;", sqlCon);
                            cmd2.Parameters.AddWithValue("@Brand", newUserBrand);
                            cmd2.Parameters.AddWithValue("@Model", newUserModel);
                            cmd2.Parameters.AddWithValue("@Fuel", newUserFuelType);
                            cmd2.Parameters.AddWithValue("@Year", newUserYear);
                            cmd2.Parameters.AddWithValue("@Engine", newUserEngine);
                            cmd2.Parameters.AddWithValue("@Color", newUserColor);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.ExecuteNonQuery();
                            sqlCon.Close();
                            MessageBox.Show("Added!");
                        }
                    } 
                }
            }
        }

        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki graficzne|*.jpg;*.png;*.bmp|Wszystkie pliki|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string sourceToPhotoa = openFileDialog.FileName;
                setPhotoSource(sourceToPhotoa);
                string nameOfFile = System.IO.Path.GetFileName(sourceToPhotoa);

                addPhoto.Content = nameOfFile;
                source.Content = sourceToPhotoa;
            }
        }

        private void addColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            addBrand.Text = "";
            addBrand.Text = "";
            addModel.Text = "";
            addFTypeBox.Text = "";
            addYOProductionBox.Text = "";
            addECapacityBox.Text = "";
            addColor.Text = "";
            addPhoto.Content = "choose photo";
            source.Content = "";
        }
    }
    }

