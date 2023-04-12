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
using dostavka;
using dostavka.sssDataSet1TableAdapters;

namespace dostavka
{
    /// <summary>
    /// Логика взаимодействия для DepartmentAdminWindow.xaml
    /// </summary>
    public partial class RestarauntAdmin : Window
    {
        RestaurantTableAdapter department = new RestaurantTableAdapter();
        public RestarauntAdmin()
        {
            InitializeComponent();
            DepartmentGrid.ItemsSource = department.GetData();
        }

        private void DepartmentCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(DepartmentTxb.Text) &
                MainWindow.checkLet.IsMatch(DepartmentFloorTxb.Text))
            {
                
                department.InsertQuery(DepartmentTxb.Text, DepartmentFloorTxb.Text);
                DepartmentGrid.ItemsSource = department.GetData();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void DepartmentUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainWindow.checkLet.IsMatch(DepartmentTxb.Text) &
                MainWindow.checkLet.IsMatch(DepartmentFloorTxb.Text)))
            {
                object id = (DepartmentGrid.SelectedItem as DataRowView).Row[0];
                department.UpdateQuery(DepartmentTxb.Text, DepartmentFloorTxb.Text, Convert.ToInt32(id));
                DepartmentGrid.ItemsSource = department.GetData();
            }

            if (MainWindow.checkLet.IsMatch(DepartmentTxb.Text) &
                MainWindow.checkLet.IsMatch(DepartmentFloorTxb.Text))
            {
                object id = (DepartmentGrid.SelectedItem as DataRowView).Row[0];
                department.UpdateQuery(DepartmentTxb.Text, DepartmentFloorTxb.Text, Convert.ToInt32(id));
                DepartmentGrid.ItemsSource = department.GetData();
            }

            else if (DepartmentGrid.SelectedItem != null && (MainWindow.checkLet.IsMatch(DepartmentTxb.Text)))
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void DepartmentDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (DepartmentGrid.SelectedItem as DataRowView).Row[0];
                department.DeleteQuery(Convert.ToInt32(id));
                DepartmentGrid.ItemsSource = department.GetData();
            }
        }

        private void DepartmentBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void DepartmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentGrid.SelectedItem != null)
            {
                object cell = (DepartmentGrid.SelectedItem as DataRowView).Row[1];
                DepartmentTxb.Text = cell as string;
                object floor = (DepartmentGrid.SelectedItem as DataRowView).Row[2];
                DepartmentFloorTxb.Text = floor as string;
            }
        }
    }
}
