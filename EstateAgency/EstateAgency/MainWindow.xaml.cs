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
using EstateAgency.DataBase;
using System.Windows.Shapes;
using EstateAgency.Models;

namespace EstateAgency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VinidiktovDay1Entities DB = new VinidiktovDay1Entities();
        List<Realty> realties = new List<Realty>();
        public MainWindow()
        {
            InitializeComponent();
            LoadDBRealty();
            InfoAgents.ItemsSource = DB.Agents.ToList();
            InfoClients.ItemsSource = DB.Clients.ToList();
        }

        private void AddAgent_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateRealtor add = new AddOrUpdateRealtor();
            add.Show();
            Close();
        }

        private void RedactAgent_Click(object sender, RoutedEventArgs e)
        {
            var select = (Agents)InfoAgents.SelectedItem;
            if (select == null)
            {
                MessageBox.Show("Необходимо выбрать строку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AddOrUpdateRealtor add = new AddOrUpdateRealtor(select.ID_Agent);
                add.Show();
                Close();
            }
        }

        private void DeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialogwindow = MessageBox.Show("Вы уверены, что хотите удалить данные?",
                    "Подтверждение действия!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogwindow == MessageBoxResult.Yes)
                {
                    var selectrow = (Agents)InfoAgents.SelectedItem;
                    if (selectrow != null)
                    {
                        var searchTable = DB.Agents.FirstOrDefault(x => x.ID_Agent == selectrow.ID_Agent);
                        DB.Agents.Remove(searchTable);
                        DB.SaveChanges();
                        DB = new VinidiktovDay1Entities();
                        InfoAgents.ItemsSource = DB.Agents.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Необходимо выбрать строку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch { }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateClient add = new AddOrUpdateClient();
            add.Show();
            Close();
        }

        private void RedactClient_Click(object sender, RoutedEventArgs e)
        {
            var select = (Clients)InfoClients.SelectedItem;
            if (select == null)
            {
                MessageBox.Show("Необходимо выбрать строку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AddOrUpdateClient add = new AddOrUpdateClient(select.ID_Client);
                add.Show();
                Close();
            }
        }

        private void DeleteCLient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialogwindow = MessageBox.Show("Вы уверены, что хотите удалить данные?",
                    "Подтверждение действия!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogwindow == MessageBoxResult.Yes)
                {
                    var selectrow = (Clients)InfoClients.SelectedItem;
                    if (selectrow != null)
                    {
                        var searchTable = DB.Clients.FirstOrDefault(x => x.ID_Client == selectrow.ID_Client);
                        DB.Clients.Remove(searchTable);
                        DB.SaveChanges();
                        DB = new VinidiktovDay1Entities();
                        InfoClients.ItemsSource = DB.Clients.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Необходимо выбрать строку", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch { }
        }

        private void SearchClientsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                InfoClients.ItemsSource = DB.Clients.ToList();
                if (SearchClientsTB.Text.Length == 0)
                {
                    InfoClients.ItemsSource = DB.Clients.ToList();
                }
                else
                {
                    string searchText = SearchClientsTB.Text;
                    var filteredRecords = DB.Clients.Where(r => r.FirstName.ToLower().StartsWith(searchText.ToLower()) |
                    r.MiddleName.ToLower().StartsWith(searchText.ToLower()) |
                    r.LastName.ToString().ToLower().StartsWith(searchText.ToLower()));
                    InfoClients.ItemsSource = filteredRecords.ToList();
                }
            }
            catch { MessageBox.Show("Проблемы поиска", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void SearchAgentsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                InfoAgents.ItemsSource = DB.Agents.ToList();
                if (SearchAgentsTB.Text.Length == 0)
                {
                    InfoAgents.ItemsSource = DB.Agents.ToList();
                }
                else
                {
                    string searchText = SearchAgentsTB.Text;
                    var filteredRecords = DB.Agents.Where(r => r.FirstName.ToLower().StartsWith(searchText.ToLower()) |
                    r.MiddleName.ToLower().StartsWith(searchText.ToLower()) |
                    r.LastName.ToLower().StartsWith(searchText.ToLower()));
                    InfoAgents.ItemsSource = filteredRecords.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Проблемы поиска", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadDBRealty()
        {
            var listRealty = new List<Realty>();
            var listHouses = DB.Houses.ToList();
            var listLands = DB.Lands.ToList();
            var listApart = DB.Apartments.ToList();
            for (int i = 0; i < DB.Houses.Count(); i++)//Преобразование под общий вид дома
            {
                Realty realty = new Realty();
                realty.Address_City = listHouses[i].Address_City;
                realty.Address_House = listHouses[i].Address_House;
                realty.Address_Number = listHouses[i].Address_Number;
                realty.Coordinate_latitude = listHouses[i].Coordinate_latitude.ToString();
                realty.Coordinate_longitude = listHouses[i].Coordinate_longitude.ToString();
                realty.Streets = listHouses[i].Street;
                realty.ID = listHouses[i].ID_House;
                realty.TypeRealty = "Дома";
                listRealty.Add(realty);
            }
            for (int i = 0; i < DB.Lands.Count(); i++)//Земля
            {
                Realty realty = new Realty();
                realty.Address_City = listLands[i].Address_City;
                realty.Address_House = listLands[i].Address_House;
                realty.Address_Number = listLands[i].Address_Number;
                realty.Coordinate_latitude = listLands[i].Coordinate_latitude.ToString();
                realty.Coordinate_longitude = listLands[i].Coordinate_longitude.ToString();
                realty.Streets = listLands[i].Street;
                realty.ID = listLands[i].ID_Land;
                realty.TypeRealty = "Земля";
                listRealty.Add(realty);
            }
            for (int i = 0; i < DB.Apartments.Count(); i++)//Квартиры
            {
                Realty realty = new Realty();
                realty.Address_City = listApart[i].Address_City;
                realty.Address_House = listApart[i].Address_House;
                realty.Address_Number = listApart[i].Address_Number;
                realty.Coordinate_latitude = listApart[i].Coordinate_latitude.ToString();
                realty.Coordinate_longitude = listApart[i].Coordinate_longitude.ToString();
                realty.Streets = listApart[i].Street;
                realty.ID = listApart[i].ID_Apartment;
                realty.TypeRealty = "Апартаменты";
                listRealty.Add(realty);
            }
            InfoRealty.ItemsSource = listRealty.ToList();
            realties = listRealty.ToList();
            FiltrAdressCB.ItemsSource = listRealty.Select(a => a.FullAddress).Distinct().ToList();
            FiltrRealtyCB.ItemsSource = listRealty.Select(a => a.TypeRealty).Distinct().ToList();
        }


        private void AddRealty_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateRealty add = new AddOrUpdateRealty();
            add.Show();
            Close();
        }

        private void RedactRealty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectRow = (Realty)InfoRealty.SelectedItem;
                if (selectRow != null)
                {
                    if (selectRow.TypeRealty == "Земля")
                    {
                        var searchElemt = DB.Lands.First(i => i.ID_Land == selectRow.ID);
                        var addOrupdate = new AddOrUpdateRealty(searchElemt);
                        addOrupdate.Show();
                        this.Close();
                    }
                    else
                    {
                        if (selectRow.TypeRealty == "Дом")
                        {
                            var searchElemt = DB.Houses.First(i => i.ID_House == selectRow.ID);
                            var addOrupdate = new AddOrUpdateRealty(searchElemt);
                            addOrupdate.Show();
                            this.Close();
                        }
                        else
                        {
                            if (selectRow.TypeRealty == "Апартаменты")
                            {
                                var searchElemt = DB.Apartments.First(i => i.ID_Apartment == selectRow.ID);
                                var addOrupdate = new AddOrUpdateRealty(searchElemt);
                                addOrupdate.Show();
                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Строка не выбрана!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DropHouse(int id)
        {
            DB.Houses.Remove(DB.Houses.Find(id));
            DB.SaveChanges();
        }

        public void DropLands(int id)
        {
            DB.Lands.Remove(DB.Lands.Find(id));
            DB.SaveChanges();
        }

        public void DropApartaments(int id)
        {
            DB.Apartments.Remove(DB.Apartments.Find(id));
            DB.SaveChanges();
        }

        private void DeleteRealty_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?"
                , "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selectRow = (Realty)InfoRealty.SelectedItem;
                if (selectRow != null)
                {
                    try
                    {
                        if (selectRow.TypeRealty == "Дома")
                            DropHouse(selectRow.ID);
                        if (selectRow.TypeRealty == "Земля")
                            DropLands(selectRow.ID);
                        if (selectRow.TypeRealty == "Апартаменты")
                            DropApartaments(selectRow.ID);
                        LoadDBRealty();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Строка не выбрана", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void SearchRealtyTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtered = realties.ToList();
            var resultFilter = filtered.Where(x => x.GetType().GetProperties().
            Any(p => p.GetValue(x, null)?.ToString().ToLower().
            Contains(SearchRealtyTB.Text) ?? false)).ToList();
            InfoRealty.ItemsSource = resultFilter;
        }

        private void FiltrRealtyCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoRealty.ItemsSource = realties.Where(v => v.TypeRealty == FiltrRealtyCB.SelectedItem.ToString()).ToList();
        }

        private void FiltrAdressCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoRealty.ItemsSource = realties.Where(v => v.FullAddress == FiltrAdressCB.SelectedItem.ToString()).ToList();
        }
    }
}
