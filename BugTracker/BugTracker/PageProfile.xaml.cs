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
    /// Interaction logic for PageProfile.xaml
    /// </summary>
    public partial class PageProfile : Page
    {
        MainWindow temp;

        public PageProfile()
        {
            InitializeComponent();
            temp = ((MainWindow)Application.Current.MainWindow);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterUsername.Text = temp.currentUser;
            PrivilegeLevel.Text = temp.currentUserPrivilege.ToString();
            if (PrivilegeLevel.Text != "3") {
                RegisterUsername.IsReadOnly = true;
                PrivilegeLevel.IsReadOnly = true;
            }
        }


        private void ClickUpdateSubmit(object sender, RoutedEventArgs e)
        {
            string myUser = RegisterUsername.Text.ToString();
            string myPass1 = password1.Password.ToString();
            string myPass2 = password2.Password.ToString();
            string myPrivilege = PrivilegeLevel.Text.ToString();
            if (temp.currentUser == myUser)
            {
                if (myPass1 == myPass2 && myPass1 != "")
                {
                    temp.SqlUpdateUser(myUser, myPass1, myPass2, "1");

                    temp.Goto_LoginPage(temp, e);
                }
                else
                {
                    MessageBox.Show("Please check you have entered the password correctly");
                }
            }
            else if (temp.currentUserPrivilege == 3)
            {
                if (temp.SqlCheckUsername(RegisterUsername.Text.ToString())) {
                    if (myPrivilege != "1") { 
                    
                    }
                    else if (myPass1 == myPass2 && myPass1 != "")
                    {
                        temp.SqlUpdateUser(myUser, myPass1, myPass2, "1");

                        temp.Goto_LoginPage(temp, e);
                    }
                }
            }
            else { 
                MessageBox.Show("Please check that values are entered correctly");
            }
        }

        private void ClickPageMoveToMain(object sender, RoutedEventArgs e)
        {
            //temp.MainCanvas.Content = new PageProfile();
            temp.Goto_UserPage(temp, e);
        }


    }
}
