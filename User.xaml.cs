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

namespace dostavka
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class User : Window
    {
        ChequeTableAdapter cheque = new ChequeTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        Payment_methodTableAdapter payment = new Payment_methodTableAdapter();
        public User()
        {
            InitializeComponent();

            TalonGrid.ItemsSource = cheque.GetData();
            UsersGrid.ItemsSource = users.GetData();
            
            TalonUsersComboBox.ItemsSource = users.GetData();
            TalonUsersComboBox.DisplayMemberPath = "Surname_user";
            TalonUsersComboBox.SelectedValuePath = "id_User";

            TalonPaymentComboBox.ItemsSource = payment.GetData();
            TalonPaymentComboBox.DisplayMemberPath = "Payment_method_name";
            TalonPaymentComboBox.SelectedValuePath = "id_Payment_method";
        }

        private void TalonCreateBt_Click(object sender, RoutedEventArgs e)
        {
            if (TalonUsersComboBox.SelectedIndex >= 0 &
                TalonPaymentComboBox.SelectedIndex >= 0)
            {
                cheque.InsertQuery(Convert.ToInt32(TalonUsersComboBox.Text), Convert.ToInt32(TalonPaymentComboBox.Text));
                TalonGrid.ItemsSource = cheque.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void TalonAccountBt_Click(object sender, RoutedEventArgs e)
        {
            if (TalonUsersComboBox.SelectedIndex >= 0 &
                TalonPaymentComboBox.SelectedIndex >= 0)
            {
                object id = (TalonGrid.SelectedItem as DataRowView).Row[0];
                cheque.UpdateQuery(Convert.ToInt32(TalonUsersComboBox.Text), Convert.ToInt32(TalonPaymentComboBox.Text), Convert.ToInt32(id));
                TalonGrid.ItemsSource = cheque.GetData();
            }
            else if (TalonGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }


        private void TalonDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (TalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (TalonGrid.SelectedItem as DataRowView).Row[0];
                cheque.DeleteQuery(Convert.ToInt32(id));
                TalonGrid.ItemsSource = cheque.GetData();
            }
        }

        private void TalonGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TalonGrid.SelectedItem != null)
            {
                object id_User = (TalonGrid.SelectedItem as DataRowView).Row[1];
                TalonUsersComboBox.SelectedValue = id_User;

                object id_Payment = (TalonGrid.SelectedItem as DataRowView).Row[2];
                TalonPaymentComboBox.SelectedValue = id_Payment;

            }
            if (TalonGrid.SelectedItem == null)
            {
                TalonGrid.ItemsSource = cheque.GetData();
            }
        }

        private void KassaBt_Click(object sender, RoutedEventArgs e)
        {
            new KassaUser().Show();
            Close();
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
