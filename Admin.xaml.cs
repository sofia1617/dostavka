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
using dostavka.sssDataSet1TableAdapters;

namespace dostavka
{
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            new RolesAdmin().Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AccountAdmin().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Payment_methodAdmin().Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new PostAdmin().Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new RestarauntAdmin().Show();
            Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new EmployeesAdmin().Show();
            Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            new UsersAdmin().Show();
            Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new ServiceAdmin().Show();
            Close();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            new ChequeAdmin().Show();
            Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            new ChequeServiceAdmin().Show();
            Close();
        }
    }
}
