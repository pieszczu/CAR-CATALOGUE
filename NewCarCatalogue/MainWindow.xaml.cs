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

using System.IO;
using System.Windows.Media.Imaging;


namespace NewCarCatalogue
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            windowLoaded();
            showBTN.IsEnabled = false;
            deleteBTN.IsEnabled = false;
            editBTN.IsEnabled = false;
        }
        private void selected()
        {
            selectedBrand = brandBox.SelectedItem?.ToString();
            selectedModel = modelBox.SelectedItem?.ToString();
            selectedFuel = fTypeBox.SelectedItem?.ToString();
            selectedYear = yOProductionBox.SelectedItem?.ToString();
            selectedEngine = eCapacityBox.SelectedItem?.ToString();
            selectedColor = colorBox.SelectedItem?.ToString();
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
                        brandBox.Items.Add(value);
                    }
                    reader.Close();
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
                modelBox.Items.Clear();
                selected();
                
                if (!string.IsNullOrEmpty(selectedBrand))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Model FROM dbo.Cars2 WHERE Brand = @Brand", sqlCon);
                    cmd.Parameters.AddWithValue("@Brand", selectedBrand);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Model"].ToString();
                        modelBox.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                    brandBox.IsEnabled = false;
                }
            }
        }

        private void COMBO2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                fTypeBox.Items.Clear();

                string selectedBrand = brandBox.SelectedItem?.ToString();
                string selectedModel = modelBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedBrand))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Fuel FROM dbo.Cars2 WHERE Model = @Model", sqlCon);
                    cmd.Parameters.AddWithValue("@Model", selectedModel);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string value = reader["Fuel"].ToString();
                        fTypeBox.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                    modelBox.IsEnabled = false;
                }
            }
        }
        
        private void COMBO3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showBTN.IsEnabled = true;
        }

        void reset()
        {
            brandBox.IsEnabled = true;
            brandBox.SelectedIndex = -1;
            modelBox.IsEnabled = true;
            modelBox.SelectedIndex = -1;
            colorBox.IsEnabled = true;
            colorBox.SelectedIndex = -1;
            imagee.Source = null;
            showBTN.IsEnabled = false;
            deleteBTN.IsEnabled = false;
            editBTN.IsEnabled = false;
            fTypeBox.IsEnabled = true;
            yOProductionBox.IsEnabled = true;
            eCapacityBox.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        public static string selectedBrand { get; set; }
        public static string selectedModel { get; set; }
        public static string selectedColor { get; set; }
        public static string selectedYear { get; set; }
        public static string selectedEngine { get; set; }
        public static string selectedFuel { get; set; }

        public static string query = "SELECT Photo FROM dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color";

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                selected();
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
            showBTN.IsEnabled = false;
            deleteBTN.IsEnabled = true;
            editBTN.IsEnabled = true;
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            editCar editCar = new editCar();
            editCar.Show();
            Window.GetWindow(this).Close();
        }

        private void ANV_Click(object sender, RoutedEventArgs e)
        {
            AddNewVehicle addNewVehicle = new AddNewVehicle();
            addNewVehicle.Show();
            Window.GetWindow(this).Close();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "DeleteVehicle", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
                {
                    string UserBrand = brandBox.Text;
                    string UserModel = modelBox.Text;
                    string UserFuel = fTypeBox.Text;
                    string UserYear = yOProductionBox.Text;
                    string UserEngine = eCapacityBox.Text;
                    string UserColor = colorBox.Text;
                    
                    sqlCon.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Cars2 WHERE Brand = @Brand AND Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine AND Color = @Color", sqlCon);
                    cmd.Parameters.AddWithValue("@Brand", UserBrand);
                    cmd.Parameters.AddWithValue("@Model", UserModel);
                    cmd.Parameters.AddWithValue("@Fuel", UserFuel);
                    cmd.Parameters.AddWithValue("@Year", UserYear);
                    cmd.Parameters.AddWithValue("@Engine", UserEngine);
                    cmd.Parameters.AddWithValue("@Color", UserColor);
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
                MessageBox.Show("Deleted!");
                reset();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Window.GetWindow(this).Close();
            }
        }

        private void FTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                yOProductionBox.Items.Clear();

                string selectedModel = modelBox.SelectedItem?.ToString();
                string selectedFuel = fTypeBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedModel))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Year FROM dbo.Cars2 WHERE Model = @Model AND Fuel = @Fuel", sqlCon);
                    cmd.Parameters.AddWithValue("@Model", selectedModel);
                    cmd.Parameters.AddWithValue("@Fuel", selectedFuel);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string value = reader["Year"].ToString();
                        yOProductionBox.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                    fTypeBox.IsEnabled = false;
                }
            }
        }

        private void YOProductionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                eCapacityBox.Items.Clear();

                string selectedModel = modelBox.SelectedItem?.ToString();
                string selectedFuel = fTypeBox.SelectedItem?.ToString();
                string selectedYear = yOProductionBox.SelectedItem?.ToString();
 
                if (!string.IsNullOrEmpty(selectedModel))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Engine FROM dbo.Cars2 WHERE Model = @Model AND Fuel = @Fuel AND Year = @Year", sqlCon);
                    cmd.Parameters.AddWithValue("@Model", selectedModel);
                    cmd.Parameters.AddWithValue("@Fuel", selectedFuel);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string value = reader["Engine"].ToString();
                        eCapacityBox.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                    yOProductionBox.IsEnabled = false;
                }
            }
        }

        private void ECapacityBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-A95GOD6B; Initial Catalog=NCC; Integrated Security=True;"))
            {
                sqlCon.Open();
                colorBox.Items.Clear();

                string selectedModel = modelBox.SelectedItem?.ToString();
                string selectedFuel = fTypeBox.SelectedItem?.ToString();
                string selectedYear = yOProductionBox.SelectedItem?.ToString();
                string selectedEngine = eCapacityBox.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedEngine))
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT Color FROM dbo.Cars2 WHERE Model = @Model AND Fuel = @Fuel AND Year = @Year AND Engine = @Engine", sqlCon);
                    cmd.Parameters.AddWithValue("@Model", selectedModel);
                    cmd.Parameters.AddWithValue("@Fuel", selectedFuel);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);
                    cmd.Parameters.AddWithValue("@Engine", selectedEngine);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string value = reader["Color"].ToString();
                        colorBox.Items.Add(value);
                    }
                    reader.Close();
                    sqlCon.Close();
                    eCapacityBox.IsEnabled = false;
                }
            }
        }
    }
        
}
