using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для ChequeAdminWindow.xaml
    /// </summary>
    public partial class ChequeAdmin : Window
    {
        ChequeTableAdapter cheque = new ChequeTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        Payment_methodTableAdapter payment = new Payment_methodTableAdapter();
        public ChequeAdmin()
        {
            InitializeComponent();

            ChequeGrid.ItemsSource = cheque.GetData();

            ChequeUsersComboBox.ItemsSource = users.GetData();
            ChequeUsersComboBox.DisplayMemberPath = "Surname_user";
            ChequeUsersComboBox.SelectedValuePath = "id_User";

            ChequePaymentComboBox.ItemsSource = payment.GetData();
            ChequePaymentComboBox.DisplayMemberPath = "Payment_method_name";
            ChequePaymentComboBox.SelectedValuePath = "id_Payment_method";

        }

        private void ChequeCreateBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(ChequeUsersComboBox.Text) &
                (ChequePaymentComboBox.SelectedIndex >= 0))
            {
                cheque.InsertQuery(Convert.ToInt32(ChequeUsersComboBox.Text), Convert.ToInt32(ChequePaymentComboBox.Text));
                ChequeGrid.ItemsSource = cheque.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void ChequeAccountBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(ChequeUsersComboBox.Text) &
                (ChequePaymentComboBox.SelectedIndex >= 0))
            {
                object id = (ChequeGrid.SelectedItem as DataRowView).Row[0];
                cheque.UpdateQuery(Convert.ToInt32(ChequeUsersComboBox.Text), Convert.ToInt32(ChequePaymentComboBox.Text), Convert.ToInt32(id));
                ChequeGrid.ItemsSource = cheque.GetData();
            }
            else if (ChequeGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void ChequeDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (ChequeGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (ChequeGrid.SelectedItem as DataRowView).Row[0];
                cheque.DeleteQuery(Convert.ToInt32(id));
                ChequeGrid.ItemsSource = cheque.GetData();
            }
        }

        private void ChequeBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void ChequeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChequeGrid.SelectedItem != null)
            {
                object id_User = (ChequeGrid.SelectedItem as DataRowView).Row[1];
                ChequeUsersComboBox.SelectedValue = id_User;

                object id_Payment = (ChequeGrid.SelectedItem as DataRowView).Row[2];
                ChequePaymentComboBox.SelectedValue = id_Payment;

            }
            if (ChequeGrid.SelectedItem == null)
            {
                ChequeGrid.ItemsSource = cheque.GetData();
            }
        }
    }
}
