using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PageRegister.xaml
    /// </summary>
    public partial class PageRegister : Page
    {
        MainWindow temp;

        public PageRegister()
        {
            InitializeComponent();
            temp = ((MainWindow)Application.Current.MainWindow);
        }

        private void ClickRegisterSubmit(object sender, RoutedEventArgs e)
        {
            string myUser = RegisterUsername.Text.ToString();
            string myPass1 = password1.Password.ToString();
            string myPass2 = password2.Password.ToString();
            Regex r = new Regex("[a-zA-Z0-9]+");

            //Check username only has numbers and letters and passwords match
            if (r.IsMatch(myUser) && myPass1 == myPass2 && myPass1 != "")
            {
                temp.SqlAddUser(myUser, myPass1);

                temp.Goto_LoginPage(temp, e);
            }
            else
            {
                MessageBox.Show("Please enter a valid USERNAME or PASSWORD");
            }
        }

        private void ClickPageMoveToLogin(object sender, RoutedEventArgs e)
        {
            temp.Goto_LoginPage(temp, e);
        }
    }
}
