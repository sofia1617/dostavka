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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using dostavka;
using dostavka.sssDataSet1TableAdapters;

namespace dostavka
{
    /// <summary>
    /// Логика взаимодействия для RolesAdminWindow.xaml
    /// </summary>
    public partial class RolesAdmin: Window
    {
        RolesTableAdapter roles = new RolesTableAdapter();
        public RolesAdmin()
        {
            InitializeComponent();
            RolesGrid.ItemsSource = roles.GetData();
        }
        private void RoleCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(RoleTxb.Text))
            {
                roles.InsertQuery(RoleTxb.Text);
                RolesGrid.ItemsSource = roles.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void UpdateRoleBt_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainWindow.checkLet.IsMatch(RoleTxb.Text)))
            {
                object id = (RolesGrid.SelectedItem as DataRowView).Row[0];
                roles.UpdateQuery(RoleTxb.Text, Convert.ToInt32(id));
                RolesGrid.ItemsSource = roles.GetData();
            }

            if (MainWindow.checkLet.IsMatch(RoleTxb.Text))
            {
                object id = (RolesGrid.SelectedItem as DataRowView).Row[0];
                roles.UpdateQuery(RoleTxb.Text, Convert.ToInt32(id));
                RolesGrid.ItemsSource = roles.GetData();
            }

            else if (RolesGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void RoleDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (RolesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (RolesGrid.SelectedItem as DataRowView).Row[0];
                roles.DeleteQuery(Convert.ToInt32(id));
                RolesGrid.ItemsSource = roles.GetData();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void RolesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolesGrid.SelectedItem != null)
            {
                object cell = (RolesGrid.SelectedItem as DataRowView).Row[1];
                RoleTxb.Text = cell as string;
            }
            if (RolesGrid.SelectedItem == null)
            {
                RolesGrid.ItemsSource = roles.GetData();
            }
        }
    }
}
