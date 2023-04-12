using System;
using System.Collections.Generic;
using System.Data;
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
using dostavka.sssDataSet1TableAdapters;
using dostavka;


namespace dostavka
{
    /// <summary>
    /// Логика взаимодействия для AccountAdminWindow.xaml
    /// </summary>
    public partial class AccountAdmin : Window
    {
        AccountTableAdapter account = new AccountTableAdapter();
        RolesTableAdapter roles = new RolesTableAdapter();

        public AccountAdmin()
        {
            InitializeComponent();
            AccountGrid.ItemsSource = account.GetData();

            AccountComboBox.ItemsSource = roles.GetData();
            AccountComboBox.DisplayMemberPath = "Role_name";
            AccountComboBox.SelectedValuePath = "id_Role";
        }
        private void AccountCreateBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(AccountLoginTxb.Text) &
                MainWindow.checkLet.IsMatch(AccountPasswordTxb.Text) &
                AccountComboBox.SelectedIndex >= 0)
            {
                account.InsertQuery(AccountLoginTxb.Text, AccountPasswordTxb.Text, Convert.ToInt32(AccountComboBox.Text));
                AccountGrid.ItemsSource = account.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void UpdateAccountBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(AccountLoginTxb.Text) &
                MainWindow.checkLet.IsMatch(AccountPasswordTxb.Text) &
                AccountComboBox.SelectedIndex >= 0)
            {
                object id = (AccountGrid.SelectedItem as DataRowView).Row[0];
                account.UpdateQuery(AccountLoginTxb.Text, AccountPasswordTxb.Text, Convert.ToInt32(AccountComboBox.SelectedValue), Convert.ToInt32(id));
                AccountGrid.ItemsSource = account.GetData();
            }

            else if (AccountGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void AccountDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (AccountGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (AccountGrid.SelectedItem as DataRowView).Row[0];
                account.DeleteQuery(Convert.ToInt32(id));
                AccountGrid.ItemsSource = account.GetData();
            }
        }

        private void AccountBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void AccountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountGrid.SelectedItem != null)
            {
                object login = (AccountGrid.SelectedItem as DataRowView).Row[1];
                AccountLoginTxb.Text = login as string;
                object pass = (AccountGrid.SelectedItem as DataRowView).Row[2];
                AccountPasswordTxb.Text = pass as string;
                object id_Role = (AccountGrid.SelectedItem as DataRowView).Row[3];
                AccountComboBox.SelectedValue = id_Role;
            }
            if (AccountGrid.SelectedItem == null)
            {
                AccountGrid.ItemsSource = account.GetData();
            }
        }
    }
}
