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
    /// Логика взаимодействия для PostAdminWindow.xaml
    /// </summary>
    public partial class PostAdmin : Window
    {
        PostTableAdapter post = new PostTableAdapter();
        public PostAdmin()
        {
            InitializeComponent();
            PostGrid.ItemsSource = post.GetData();
        }

        private void PostCreate_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(PostTxb.Text))
            {
                post.InsertQuery(PostTxb.Text);
                PostGrid.ItemsSource = post.GetData();
                
            }
            else
            {
                MessageBox.Show("Ошибка");
                //post.InsertQuery(PostTxb.Text);
                //PostGrid.ItemsSource = post.GetData();
            }
        }

        private void PostUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.checkLet.IsMatch(PostTxb.Text))
            {
                object id = (PostGrid.SelectedItem as DataRowView).Row[0];
                post.UpdateQuery(PostTxb.Text, Convert.ToInt32(id));
                PostGrid.ItemsSource = post.GetData();
            }

            if (PostTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (PostGrid.SelectedItem != null)
            {
                MessageBox.Show("Введены некоректные данные");
                //object id = (PostGrid.SelectedItem as DataRowView).Row[0];
                //post.UpdateQuery(PostTxb.Text, Convert.ToInt32(id));
                //PostGrid.ItemsSource = post.GetData();
            }
        }

        private void PostDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (PostGrid.SelectedItem as DataRowView).Row[0];
                post.DeleteQuery(Convert.ToInt32(id));
                PostGrid.ItemsSource = post.GetData();
            }
        }

        private void PostBackBt_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            Close();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                object cell = (PostGrid.SelectedItem as DataRowView).Row[1];
                PostTxb.Text = cell as string;
            }
            if (PostGrid.SelectedItem == null)
            {
                PostGrid.ItemsSource = post.GetData();
            }
        }

        private void Postimport_Click(object sender, RoutedEventArgs e)
        {
            List<employess> forImport = JsonConverter.DeserializeObject<List<employess>>();
            foreach (var item in forImport)
            {
                post.InsertQuery(item.Post_name);
            }
            PostGrid.ItemsSource = null;
            PostGrid.ItemsSource = post.GetData();
        }
    }
    internal class employess 
    {
        public string Post_name { get; set; }
    }
}
