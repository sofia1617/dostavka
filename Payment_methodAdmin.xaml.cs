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
    /// Логика взаимодействия для Payment_methodAdminWindow.xaml
    /// </summary>
    public partial class Payment_methodAdmin : Window
    {
        Payment_methodTableAdapter payment = new Payment_methodTableAdapter();
        public Payment_methodAdmin()
        {
            InitializeComponent();
            PaymentGrid.ItemsSource = payment.GetData();
        }
        private void PaymentCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(PaymentTxb.Text))
            {
                payment.InsertQuery(PaymentTxb.Text);
                PaymentGrid.ItemsSource = payment.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
                //payment.InsertQuery(PaymentTxb.Text);
                //PaymentGrid.ItemsSource = payment.GetData();
            }
        }

        private void PaymentUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(PaymentTxb.Text))
            {
                object id = (PaymentGrid.SelectedItem as DataRowView).Row[0];
                payment.UpdateQuery(PaymentTxb.Text, Convert.ToInt32(id));
                PaymentGrid.ItemsSource = payment.GetData();
            }


            else if (PaymentGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void PaymentDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (PaymentGrid.SelectedItem as DataRowView).Row[0];
                payment.DeleteQuery(Convert.ToInt32(id));
                PaymentGrid.ItemsSource = payment.GetData();
            }
        }

        private void PaymentBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void PaymentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaymentGrid.SelectedItem != null)
            {
                object cell = (PaymentGrid.SelectedItem as DataRowView).Row[1];
                PaymentTxb.Text = cell as string;
            }
            else if (PaymentGrid.SelectedItem != null)
            {
                PaymentGrid.ItemsSource = payment.GetData();
            }
        }

        private void PaymentimportBt_Click(object sender, RoutedEventArgs e)
        {
            List<payment> forImport = JsonConverter.DeserializeObject<List<payment>>();
            foreach (var item in forImport)
            {
                payment.InsertQuery(item.Payment_method_name);
            }
            PaymentGrid.ItemsSource = null;
            PaymentGrid.ItemsSource = payment.GetData();
        }
    }
    internal class payment
    {
        public string Payment_method_name { get; set; }
    }
}

