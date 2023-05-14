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
    public partial class editCar : Window
    {
        public editCar()
        {
            InitializeComponent();
            Window_Loaded();
            photo();
            
            editModel.SelectedItem = MainWindow.selectedModel;
            editColor.SelectedItem = MainWindow.selectedColor;

            displayString();
        }
        private void displayString()
        {
            string displayBrand = MainWindow.selectedBrand;
            editBrand.SelectedItem = displayBrand;
            string displayModel = MainWindow.selectedModel;
            editModel.SelectedItem = displayModel;
            string displayFuel = MainWindow.selectedFuel;
            editFTypeBox.SelectedItem = displayFuel;
            string displayYear = MainWindow.selectedYear;
            editYOProductionBox.SelectedItem = displayYear;
            string displayEngine = MainWindow.selectedEngine;
            editECapacityBox.SelectedItem = displayEngine;
            string displayColor = MainWindow.selectedColor;
            editColor.SelectedItem = displayColor;
        }
        private void photo()
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                string query = MainWindow.query;
                string selectedBrand = MainWindow.selectedBrand;
                string selectedModel = MainWindow.selectedModel;
                string selectedColor = MainWindow.selectedColor;
                string selectedYear = MainWindow.selectedYear;
                string selectedEngine = MainWindow.selectedEngine;
                string selectedFuel = MainWindow.selectedFuel;

                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Brand", selectedBrand);
                    command.Parameters.AddWithValue("@Model", selectedModel);
                    command.Parameters.AddWithValue("@Year", selectedYear);
                    command.Parameters.AddWithValue("@Engine", selectedEngine);
                    command.Parameters.AddWithValue("@Fuel", selectedFuel);
                    command.Parameters.AddWithValue("@Color", selectedColor);

                    byte[] imageBytes = (byte[])command.ExecuteScalar();

                    using (MemoryStream stream = new MemoryStream(imageBytes))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = stream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        Image image = new Image();
                        imagee.Source = bitmapImage;
                    }
                }
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Window_Loaded()
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
                        editBrand.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }  
        }

        private void editBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                editModel.Items.Clear();

                string selectedBrand = editBrand.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedBrand))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Model FROM dbo.Cars2 WHERE Brand = @Brand", sqlCon);
                    cmd.Parameters.AddWithValue("@Brand", selectedBrand);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Model"].ToString();
                        editModel.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                }
            }
        }

        private void editModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                object selected = editFTypeBox.SelectedItem;
                editFTypeBox.Items.Clear();
                if (selected != null)
                {
                    editFTypeBox.SelectedItem = selected;
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Fuel FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Fuel"].ToString();
                        editFTypeBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }     
        }

        private void editFTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                object selected = editYOProductionBox.SelectedItem;
                editYOProductionBox.Items.Clear();
                if (selected != null)
                {
                    editYOProductionBox.SelectedItem = selected;
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Year FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Year"].ToString();
                        editYOProductionBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void editYOProductionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                object selected = editECapacityBox.SelectedItem;
                editECapacityBox.Items.Clear();
                if (selected != null)
                {
                    editECapacityBox.SelectedItem = selected;
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Engine FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Engine"].ToString();
                        editECapacityBox.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }


        private void ECapacityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                editColor.Items.Clear();
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Color FROM dbo.Cars2", sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Color"].ToString();
                        editColor.Items.Add(value);
                    }
                    reader.Close();
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        

        private void setPhotoSource(string source)
        {
        }

        private void ChangePhotoBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki graficzne|*.jpg;*.png;*.bmp|Wszystkie pliki|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string sourceToPhoto = openFileDialog.FileName;
                setPhotoSource(sourceToPhoto);
                string nameOfFile = System.IO.Path.GetFileName(sourceToPhoto);

                ChangePhotoBTN.Content = nameOfFile;
                source.Content = sourceToPhoto;
                imagee.Visibility = System.Windows.Visibility.Hidden;
            }
            ChangePhotoBTN.IsEnabled = false;
        }

        private void ChangeBTN_Click(object sender, RoutedEventArgs e)
        {
            string editUserBrand = editBrand.Text;
            string editUserModel = editModel.Text;
            string editUserFuel = editFTypeBox.Text;
            string editUserYear = editYOProductionBox.Text;
            string editUserEngine = editECapacityBox.Text;
            string editUserColor = editColor.Text;
            string newUserPhoto = source.Content.ToString();

            if (string.IsNullOrEmpty(editUserBrand) || string.IsNullOrEmpty(editUserModel) || string.IsNullOrEmpty(editUserFuel) || string.IsNullOrEmpty(editUserYear) || string.IsNullOrEmpty(editUserEngine) ||string.IsNullOrEmpty(editUserColor))
            {
                if (string.IsNullOrEmpty(editUserBrand))
                {
                    MessageBox.Show("Add brand!");
                }
                if (string.IsNullOrEmpty(editUserModel))
                {
                    MessageBox.Show("Add model!");
                }
                if (string.IsNullOrEmpty(editUserFuel))
                {
                    MessageBox.Show("Add fuel!");
                }
                if (string.IsNullOrEmpty(editUserYear))
                {
                    MessageBox.Show("Add year!");
                }
                if (string.IsNullOrEmpty(editUserEngine))
                {
                    MessageBox.Show("Add engine!");
                }
                if (string.IsNullOrEmpty(editUserColor))
                {
                    MessageBox.Show("Add color!");
                }
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS EXIS FROM dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color", sqlCon);
                    cmd.Parameters.AddWithValue("@Brand", editUserBrand);
                    cmd.Parameters.AddWithValue("@Model", editUserModel);
                    cmd.Parameters.AddWithValue("@Fuel", editUserFuel);
                    cmd.Parameters.AddWithValue("@Year", editUserYear);
                    cmd.Parameters.AddWithValue("@Engine", editUserEngine);
                    cmd.Parameters.AddWithValue("@Color", editUserColor);

                    int result = (int)cmd.ExecuteScalar();
                    sqlCon.Close();

                    if (result == 1 && ChangePhotoBTN.IsEnabled==true)
                    {
                        MessageBox.Show("Exists in database!");
                    }
                    else
                    {
                        
                        if(ChangePhotoBTN.IsEnabled == false)
                        {
                            string selectedUserBrand = MainWindow.selectedBrand;
                            string selectedUserModel = MainWindow.selectedModel;
                            string selectedUserYear = MainWindow.selectedYear;
                            string selectedUserEngine = MainWindow.selectedEngine;
                            string selectedUserFuel = MainWindow.selectedFuel;
                            string selectedUserColor = MainWindow.selectedColor;

                            sqlCon.Open();
                            SqlCommand cmdS = new SqlCommand("SELECT id from dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color", sqlCon);
                            cmdS.Parameters.AddWithValue("@Brand", selectedUserBrand);
                            cmdS.Parameters.AddWithValue("@Model", selectedUserModel);
                            cmdS.Parameters.AddWithValue("@Fuel", selectedUserFuel);
                            cmdS.Parameters.AddWithValue("@Year", selectedUserYear);
                            cmdS.Parameters.AddWithValue("@Engine", selectedUserEngine);
                            cmdS.Parameters.AddWithValue("@Color", selectedUserColor);

                            int id = (int)cmdS.ExecuteScalar();
                            sqlCon.Close();

                            sqlCon.Open();
                            SqlCommand cmd2 = new SqlCommand("UPDATE dbo.Cars2 SET Brand = @Brand, Model = @Model, Fuel = @Fuel, Year = @Year, Engine = @Engine, Color = @Color, Photo = (SELECT BulkColumn FROM OPENROWSET(BULK N'" + newUserPhoto + "', SINGLE_BLOB) AS BLOB) WHERE id = @id", sqlCon);
                            cmd2.Parameters.AddWithValue("@Brand", editUserBrand);
                            cmd2.Parameters.AddWithValue("@Model", editUserModel);
                            cmd2.Parameters.AddWithValue("@Fuel", editUserFuel);
                            cmd2.Parameters.AddWithValue("@Year", editUserYear);
                            cmd2.Parameters.AddWithValue("@Engine", editUserEngine);
                            cmd2.Parameters.AddWithValue("@Color", editUserColor);
                            cmd2.Parameters.AddWithValue("@id", id);
                            cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            string selectedUserBrand = MainWindow.selectedBrand;
                            string selectedUserModel = MainWindow.selectedModel;
                            string selectedUserYear = MainWindow.selectedYear;
                            string selectedUserEngine = MainWindow.selectedEngine;
                            string selectedUserFuel = MainWindow.selectedFuel;
                            string selectedUserColor = MainWindow.selectedColor;
                            sqlCon.Open();
                            SqlCommand cmdS = new SqlCommand("SELECT id from dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color", sqlCon);
                            cmdS.Parameters.AddWithValue("@Brand", selectedUserBrand);
                            cmdS.Parameters.AddWithValue("@Model", selectedUserModel);
                            cmdS.Parameters.AddWithValue("@Fuel", selectedUserFuel);
                            cmdS.Parameters.AddWithValue("@Year", selectedUserYear);
                            cmdS.Parameters.AddWithValue("@Engine", selectedUserEngine);
                            cmdS.Parameters.AddWithValue("@Color", selectedUserColor);
                            int id = (int)cmdS.ExecuteScalar();
                            sqlCon.Close();

                            sqlCon.Open();
                            SqlCommand cmd3 = new SqlCommand("UPDATE dbo.Cars2 SET Brand = @Brand, Model = @Model, Fuel = @Fuel, Year = @Year, Engine = @Engine, Color = @Color WHERE id = @id; ", sqlCon);
                            cmd3.Parameters.AddWithValue("@Brand", editUserBrand);
                            cmd3.Parameters.AddWithValue("@Model", editUserModel);
                            cmd3.Parameters.AddWithValue("@Fuel", editUserFuel);
                            cmd3.Parameters.AddWithValue("@Year", editUserYear);
                            cmd3.Parameters.AddWithValue("@Engine", editUserEngine);
                            cmd3.Parameters.AddWithValue("@Color", editUserColor);
                            cmd3.Parameters.AddWithValue("@id", id);
                            cmd3.ExecuteNonQuery();
                        }
                        MessageBox.Show("Changed!");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Window.GetWindow(this).Close();
                        sqlCon.Close();
                    }
                }
            }
        }
    }
    }

