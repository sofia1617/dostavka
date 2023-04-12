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
    public partial class EmployeesAdmin : Window
    {
        RestaurantTableAdapter cabinet = new RestaurantTableAdapter();
        PostTableAdapter post = new PostTableAdapter();
        EmployeesTableAdapter doctor = new EmployeesTableAdapter();
        public EmployeesAdmin()
        {
            InitializeComponent();

            DoctorGrid.ItemsSource = doctor.GetData();

            DoctorPostComboBox.ItemsSource = post.GetData();
            DoctorPostComboBox.DisplayMemberPath = "Post_name";
            DoctorPostComboBox.SelectedValuePath = "id_Post";

            resComboBox.ItemsSource = cabinet.GetData();
            resComboBox.DisplayMemberPath = "Restaurant_name";
            resComboBox.SelectedValuePath = "id_Restaurant";
        }

        private void DoctorCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(DoctorSurnameTxb.Text) &
                MainWindow.checkLet.IsMatch(DoctorNameTxb.Text) &
                MainWindow.checkLet.IsMatch(DoctorMidlleNameTxb.Text) &
                (DoctorPostComboBox.SelectedIndex >= 0) &
                (resComboBox.SelectedIndex >= 0)
                )
            {
                doctor.InsertQuery(DoctorSurnameTxb.Text, DoctorNameTxb.Text, DoctorMidlleNameTxb.Text,
                Convert.ToInt32(DoctorPostComboBox.Text), Convert.ToInt32(resComboBox.Text));
                DoctorGrid.ItemsSource = doctor.GetData();
            }
            else
            {
                MessageBox.Show("ОШибка");
            }
        }

        private void DoctorUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(DoctorSurnameTxb.Text) &
                MainWindow.checkLet.IsMatch(DoctorNameTxb.Text) &
                MainWindow.checkLet.IsMatch(DoctorMidlleNameTxb.Text) &
                (DoctorPostComboBox.SelectedIndex >= 0) &
                (resComboBox.SelectedIndex >= 0)
                )
            {
                object id = (DoctorGrid.SelectedItem as DataRowView).Row[0];
                doctor.UpdateQuery(DoctorSurnameTxb.Text, DoctorNameTxb.Text, DoctorMidlleNameTxb.Text, Convert.ToInt32(DoctorPostComboBox.Text), Convert.ToInt32(resComboBox.Text), Convert.ToInt32(id));
                DoctorGrid.ItemsSource = doctor.GetData();
                
            }

            else if (DoctorGrid.SelectedItem != null)
            {
                MessageBox.Show("Ошибка");
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void DoctorDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (DoctorGrid.SelectedItem as DataRowView).Row[0];
                doctor.DeleteQuery(Convert.ToInt32(id));
                DoctorGrid.ItemsSource = doctor.GetData();
            }
        }

        private void DoctorBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void DoctorGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorGrid.SelectedItem != null)
            {
                object surname = (DoctorGrid.SelectedItem as DataRowView).Row[1];
                DoctorSurnameTxb.Text = surname as string;

                object name = (DoctorGrid.SelectedItem as DataRowView).Row[2];
                DoctorNameTxb.Text = name as string;

                object midlleName = (DoctorGrid.SelectedItem as DataRowView).Row[3];
                DoctorMidlleNameTxb.Text = midlleName as string;

                object id_Post = (DoctorGrid.SelectedItem as DataRowView).Row[4];
                DoctorPostComboBox.SelectedValue = id_Post;

                object id_Cabinet = (DoctorGrid.SelectedItem as DataRowView).Row[5];
                resComboBox.SelectedValue = id_Cabinet;

            }
            if (DoctorGrid.SelectedItem == null)
            {
                DoctorGrid.ItemsSource = doctor.GetData();
            }
        }
    }
}
