using dostavka.sssDataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml.Linq;

namespace dostavka
{
    /// <summary>
    /// Логика взаимодействия для KassaUserWindow.xaml
    /// </summary>
    public partial class KassaUser : Window
    {
        Cheque_serviceTableAdapter CS = new Cheque_serviceTableAdapter();
        ChequeTableAdapter cheque = new ChequeTableAdapter();
        ServiceeTableAdapter service = new ServiceeTableAdapter();
        int Summa = 0;
        public KassaUser()
        {
            InitializeComponent();

            ChequeServiceGrid.ItemsSource = CS.GetData();
            TalonGrid.ItemsSource = cheque.GetData();
            ServiceGrid.ItemsSource = service.GetData();

            TalonComboBox.ItemsSource = cheque.GetData();
            TalonComboBox.DisplayMemberPath = "id_Cheque";
            TalonComboBox.SelectedValuePath = "id_Cheque";
        }

        private void BackBt_Click(object sender, RoutedEventArgs e)
        {
            new User().Show();
            Close();
        }
        private void UpdateChequeBt_Click(object sender, RoutedEventArgs e)
        {
            if ((TalonComboBox.SelectedValue == null) || (ServiceGrid.SelectedItem == null))
      
            {
                MessageBox.Show("Ошибка");
            }
            

            else
            {

                int idT = Convert.ToInt32(TalonComboBox.SelectedValue);
                int idS = Convert.ToInt32((ServiceGrid.SelectedItem as DataRowView)[0]);
                var cost = (ServiceGrid.SelectedItem as DataRowView).Row[2];
                int cost2 = (Convert.ToInt32(cost));
                Summa += cost2;
                CS.InsertQuery(idT, idS);
                ChequeServiceGrid.ItemsSource = CS.GetData();
                var poluch = Convert.ToInt32(Polucheno.Text);
                var cdacha = poluch - Summa;
                var idCheka = Convert.ToInt32(idT);
                string txt = "\nКассовый чек #" + idCheka + "\n" + "\nИтого к оплате: " + Summa + "\nВнесено: " + Polucheno.Text + "\nСдача: " + cdacha; ;


            }
        }

        private void CloseChequeBt_Click(object sender, RoutedEventArgs e)
        {

            if (MainWindow.checkLet.IsMatch(Polucheno.Text))
            {
                MessageBox.Show("Ошибка");
            }
            if (ServiceGrid.SelectedItem == null)
            {
                MessageBox.Show("Ошибка");
            }

            else
            {
                int idT = Convert.ToInt32(TalonComboBox.SelectedValue);
                int idS = Convert.ToInt32((ServiceGrid.SelectedItem as DataRowView)[0]);
                var name = (ServiceGrid.SelectedItem as DataRowView).Row[1];
                var cost = (ServiceGrid.SelectedItem as DataRowView).Row[2];
                int cost2 = (Convert.ToInt32(cost));
                Summa += cost2;
                CS.InsertQuery(idT, idS);
                ChequeServiceGrid.ItemsSource = CS.GetData();
                object id = (TalonComboBox.Text);
                var poluch = Convert.ToInt32(Polucheno.Text);
                var cdacha = poluch - Summa;


                var idCheka = Convert.ToInt32(idT);
                string txt = "\tДоставка" + "\nКассовый чек #" + idCheka + "\n" + "\nИтого к оплате: " + Summa + "\nВнесено: " + Polucheno.Text + "\nСдача: " + cdacha; ;
             
                File.AppendAllText("C:\\Users\\PC\\Desktop\\Чек #" + idCheka + ".txt", txt + "\nВыбранная услуга: " + name + "-" + cost2 + "\n");
                Close();
            }
        }
    }
}
