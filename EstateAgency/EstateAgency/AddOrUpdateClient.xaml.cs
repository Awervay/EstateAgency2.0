using EstateAgency.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace EstateAgency
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateClient.xaml
    /// </summary>
    public partial class AddOrUpdateClient : Window
    {
        VinidiktovDay1Entities DB = new VinidiktovDay1Entities();
        public int transition = 0;
        public AddOrUpdateClient()
        {
            InitializeComponent();
        }
        public AddOrUpdateClient(int Transition)
        {
            InitializeComponent();
            transition = Transition;
            var cl = DB.Clients.Find(Transition);
            SurnameTB.Text = cl.FirstName;
            NameTB.Text = cl.MiddleName;
            SecondNameTB.Text = cl.LastName;
            PhoneTB.Text = cl.Phone;
            EmailTB.Text = cl.Email;
        }

        private void SaveClient_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTB.Text == string.Empty |
                NameTB.Text == string.Empty |
                SecondNameTB.Text == string.Empty)
            {
                MessageBox.Show("Заполните ФИО", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PhoneTB.Text == string.Empty && EmailTB.Text == string.Empty)
            {
                MessageBox.Show("Заполните телефон или почту", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Clients client = new Clients();
                if (transition != 0)
                {
                    client.ID_Client = transition;
                    client.FirstName = SurnameTB.Text;
                    client.MiddleName = NameTB.Text;
                    client.LastName = SecondNameTB.Text;
                    client.Phone = PhoneTB.Text;
                    client.Email = EmailTB.Text;
                    DB.Clients.AddOrUpdate(client);
                    DB.SaveChanges();
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    var lastId = DB.Agents.Max(f => (int?)f.ID_Agent) ?? 0;
                    client.ID_Client = lastId + 1;
                    client.FirstName = SurnameTB.Text;
                    client.MiddleName = NameTB.Text;
                    client.LastName = SecondNameTB.Text;
                    client.Phone = PhoneTB.Text;
                    client.Email = EmailTB.Text;
                    DB.Clients.AddOrUpdate(client);
                    DB.SaveChanges();
                    MainWindow main1 = new MainWindow();
                    main1.Show();
                    Close();
                }
            }
        }

        private void CloseClient_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void SurnameTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void SurnameTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
