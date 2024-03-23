using EstateAgency.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для AddOrUpdateRealtor.xaml
    /// </summary>
    public partial class AddOrUpdateRealtor : Window
    {
        VinidiktovDay1Entities DB = new VinidiktovDay1Entities();
        public int transition = 0;
        public AddOrUpdateRealtor()
        {
            InitializeComponent();
        }
        public AddOrUpdateRealtor(int Transition)
        {
            InitializeComponent();
            transition = Transition;
            var ag = DB.Agents.Find(Transition);
            SurnameTB.Text = ag.FirstName;
            NameTB.Text = ag.MiddleName;
            SecondNameTB.Text = ag.LastName;
            CountDealTB.Text = ag.DealShare.ToString();
        }

        private void SaveRealtor_Click(object sender, RoutedEventArgs e)
        {
            if(SurnameTB.Text == string.Empty |
                NameTB.Text == string.Empty |
                SecondNameTB.Text == string.Empty)
            {
                MessageBox.Show("Заполните ФИО", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (CountDealTB.Text == string.Empty)
            {
                MessageBox.Show("Заполните долю риелтора", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(Convert.ToInt32(CountDealTB.Text) < 0 || Convert.ToInt32(CountDealTB.Text) > 100)
            {
                MessageBox.Show("Не может быть меньше 0% и превышать 100%", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Agents agent = new Agents();
                if (transition != 0)
                {
                    agent.ID_Agent = transition;
                    agent.FirstName = SurnameTB.Text;
                    agent.MiddleName = NameTB.Text;
                    agent.LastName = SecondNameTB.Text;
                    agent.DealShare = Convert.ToInt32(CountDealTB.Text);
                    DB.Agents.AddOrUpdate(agent);
                    DB.SaveChanges();
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    var lastId = DB.Agents.Max(f => (int?)f.ID_Agent) ?? 0;
                    agent.ID_Agent = lastId + 1;
                    agent.FirstName = SurnameTB.Text;
                    agent.MiddleName = NameTB.Text;
                    agent.LastName = SecondNameTB.Text;
                    agent.DealShare = Convert.ToInt32(CountDealTB.Text);
                    DB.Agents.AddOrUpdate(agent);
                    DB.SaveChanges();
                    MainWindow main1 = new MainWindow();
                    main1.Show();
                    Close();
                }
            }
        }

        private void CloseRealtor_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
        /// <summary>
        /// Запрет на ввод букв
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountDealTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// Запрет на ввод чисел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
