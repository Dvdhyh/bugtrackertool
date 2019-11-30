using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        MainWindow temp;

        public PageUser()
        {
            InitializeComponent();
            temp = ((MainWindow)Application.Current.MainWindow);
        }


        private void bRunQuery(object sender, RoutedEventArgs e)
        {
            temp.SqlRunQuery(txtUser.Text);
        }

        private void bInsert(object sender, RoutedEventArgs e)
        {
            temp.SqlAddUser("user1", "1234", 1, "g1");
        }
        private void bDelete(object sender, RoutedEventArgs e)
        {
            temp.SqlDeleteUser(txtUser.Text.ToString(), txtPass.Password.ToString());
        }

        private void bUpdate(object sender, RoutedEventArgs e)
        {
            temp.SqlRunQuery("SELECT * FROM user2");
        }

        private void bShowDB(object sender, RoutedEventArgs e)
        {
            temp.SqlRunQuery("SELECT NAME FROM master.sys.databases");
        }

        private void bShowT(object sender, RoutedEventArgs e)
        {
            temp.SqlRunQuery("SELECT DISTINCT TABLE_NAME FROM information_schema.TABLES");
        }
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            temp.SqlTest("SELECT COUNT(DISTINCT TABLE_NAME) FROM information_schema.TABLES WHERE TABLE_NAME = 'myUsers'");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            temp.SqlRunQuery("SELECT * FROM myUsers");
        }
    }
}
