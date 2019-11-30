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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        MainWindow temp;

        public PageLogin()
        {
            InitializeComponent();
            temp = ((MainWindow)Application.Current.MainWindow);
        }

        private void ClickSignIn(object sender, RoutedEventArgs e)
        {
            if (temp.SqlCheckLogin(TextUsername.Text.ToString(), TextPass.Password.ToString()))
            {
                temp.Goto_UserPage(temp, e);
            }
            else
            {
                MessageBox.Show("USERNAME or PASSWORD is incorrect");
            }
        }

        private void ClickRegister(object sender, RoutedEventArgs e)
        {
            temp.MainCanvas.Content = new PageRegister();
        }
    }
}
