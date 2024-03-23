using EstateAgency.DataBase;
using EstateAgency.Models;
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
using EstateAgency.DataBase;
using System.Windows.Shapes;
using System.Data.Entity.Migrations;

namespace EstateAgency
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateRealty.xaml
    /// </summary>
    public partial class AddOrUpdateRealty : Window
    {
        VinidiktovDay1Entities DB = new VinidiktovDay1Entities();
        public int ID = 0;
        public AddOrUpdateRealty()
        {
            InitializeComponent();
            StreetCB.ItemsSource = DB.Street.Select(a => a.NameStreet).ToList();
        }

        public AddOrUpdateRealty(Houses houses)
        {
            InitializeComponent();
            StreetCB.ItemsSource = DB.Street.Select(a => a.NameStreet).ToList();
            CityTB.Text = houses.Address_City;
            StreetCB.Text = DB.Street.Find(houses.Address_Street).ToString();
            AddressTB.Text = houses.Address_House;
            NumberTB.Text = houses.Address_Number;
            CoordinatShirotaTB.Text = houses.Coordinate_latitude;
            CoordinatDolgotaTB.Text = houses.Coordinate_longitude;
            TypeRealtyCB.SelectedIndex = 1;
            TotalAreaTB.Text = houses.TotalArea;
            FloorTB.Text = houses.TotalFloors;
            ID = houses.ID_House;
        }

        public AddOrUpdateRealty(Apartments apartments)
        {
            InitializeComponent();
            StreetCB.ItemsSource = DB.Street.Select(a => a.NameStreet).ToList();
            CityTB.Text = apartments.Address_City;
            StreetCB.Text = DB.Street.Find(apartments.Address_Street).ToString();
            AddressTB.Text = apartments.Address_House;
            NumberTB.Text = apartments.Address_Number;
            CoordinatShirotaTB.Text = apartments.Coordinate_latitude;
            CoordinatDolgotaTB.Text = apartments.Coordinate_longitude;
            TypeRealtyCB.SelectedIndex = 0;
            TotalAreaTB.Text = apartments.TotalArea;
            RoomTB.Text = apartments.Rooms.ToString();
            FloorTB.Text = apartments.Floor.ToString();
            ID = apartments.ID_Apartment;
        }

        public AddOrUpdateRealty(Lands lands)
        {
            InitializeComponent();
            StreetCB.ItemsSource = DB.Street.Select(a => a.NameStreet).ToList();
            CityTB.Text = lands.Address_City;
            StreetCB.Text = DB.Street.Find(lands.Address_Street).ToString();
            AddressTB.Text = lands.Address_House;
            NumberTB.Text = lands.Address_Number;
            CoordinatShirotaTB.Text = lands.Coordinate_latitude;
            CoordinatDolgotaTB.Text = lands.Coordinate_longitude;
            TypeRealtyCB.SelectedIndex = 2;
            TotalAreaTB.Text = lands.TotalArea;
            ID = lands.ID_Land;
        }

        private void SaveRealty_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (AddressTB.Text != string.Empty)
                {
                    if (NumberTB.Text != string.Empty)
                    {
                        if (TypeRealtyCB.Text != null)
                        {
                            if (TypeRealtyCB.SelectedIndex == 2)
                            {
                                Lands lands = new Lands();
                                if (ID != 0)
                                {
                                    lands.ID_Land = ID;
                                }
                                else
                                {
                                    lands.ID_Land = DB.Lands.Max(a => a.ID_Land) + 1;
                                }
                                lands.Address_City = CityTB.Text;
                                lands.ID_Land = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                                lands.Address_House = AddressTB.Text;
                                lands.Address_Number = NumberTB.Text;
                            lands.Address_Street = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                            lands.Coordinate_latitude = CoordinatShirotaTB.Text;
                                lands.Coordinate_longitude = CoordinatDolgotaTB.Text;
                                if (TotalAreaTB.Text != null)
                                    lands.TotalArea = TotalAreaTB.Text;
                                else
                                    lands.TotalArea = null;
                                DB.Lands.AddOrUpdate(lands);
                                DB.SaveChanges();
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            if (TypeRealtyCB.SelectedIndex == 1)
                            {
                                Houses house = new Houses();
                                if (ID != 0)
                                {
                                    house.ID_House = ID;
                                }
                                else
                                {
                                    house.ID_House = DB.Houses.Max(a => a.ID_House) + 1;
                                }
                                house.Address_City = CityTB.Text;
                                house.ID_House = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                                house.Address_House = AddressTB.Text;
                                house.Address_Street = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                                house.Address_Number = NumberTB.Text;
                                house.Coordinate_latitude = CoordinatShirotaTB.Text;
                                house.Coordinate_longitude = CoordinatDolgotaTB.Text;
                                if (TotalAreaTB.Text != null)
                                    house.TotalArea = TotalAreaTB.Text;
                                else
                                    house.TotalArea = null;
                                house.TotalFloors = FloorTB.Text;
                                DB.Houses.AddOrUpdate(house);
                                DB.SaveChanges();
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            if (TypeRealtyCB.SelectedIndex == 0)
                            {
                                Apartments apartments = new Apartments();
                                if (ID != 0)
                                {
                                    apartments.ID_Apartment = ID;
                                }
                                else
                                {
                                    apartments.ID_Apartment = DB.Apartments.Max(a => a.ID_Apartment) + 1;
                                }
                                apartments.Address_City = CityTB.Text;
                                apartments.ID_Apartment = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                                apartments.Address_House = AddressTB.Text;
                                apartments.Address_Number = NumberTB.Text;
                            apartments.Address_Street = DB.Street.First(a => a.NameStreet == StreetCB.SelectedItem.ToString()).ID_Street;
                            apartments.Coordinate_latitude = CoordinatShirotaTB.Text;
                                apartments.Coordinate_longitude = CoordinatDolgotaTB.Text;
                                if (TotalAreaTB.Text != null)
                                    apartments.TotalArea = TotalAreaTB.Text;
                                else
                                    apartments.TotalArea = null;
                                apartments.Rooms = Convert.ToInt32(RoomTB.Text);
                                apartments.Floor = Convert.ToInt32(FloorTB.Text);
                                DB.Apartments.AddOrUpdate(apartments);
                                DB.SaveChanges();
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Необходимо выбрать тип недвижимости");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите номер");
                    }
                }
                else
                {
                    MessageBox.Show("Введите дома");
                }
            ///}
            //catch
            //{
            //    MessageBox.Show("Необходимо заполнить данные");
            //}
        }

        private void CloseRealty_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void TypeRealtyCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TypeRealtyCB.SelectedIndex)
            {
                case 2:
                    TotalAreaTB.Visibility = Visibility.Visible;
                    TotalAreaBL.Visibility = Visibility.Visible;
                    FloorTB.Visibility = Visibility.Hidden;
                    FloorBL.Visibility = Visibility.Hidden;
                    RoomTB.Visibility = Visibility.Hidden;
                    RoomsBL.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    TotalAreaTB.Visibility = Visibility.Visible;
                    TotalAreaBL.Visibility = Visibility.Visible;
                    FloorBL.Visibility = Visibility.Visible;
                    FloorTB.Visibility = Visibility.Visible;
                    RoomTB.Visibility = Visibility.Hidden;
                    RoomsBL.Visibility = Visibility.Hidden;
                    break;
                case 0:
                    TotalAreaTB.Visibility = Visibility.Visible;
                    TotalAreaBL.Visibility = Visibility.Visible;
                    FloorTB.Visibility = Visibility.Visible;
                    FloorBL.Visibility = Visibility.Visible;
                    RoomTB.Visibility = Visibility.Visible;
                    RoomsBL.Visibility = Visibility.Visible;
                    break;

            }
        }
    }
}
