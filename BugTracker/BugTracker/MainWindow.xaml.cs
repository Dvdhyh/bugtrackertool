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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public const string myCon = @"Data Source=DESKTOP-4RCDRN1;Initial Catalog=DBtest1;Integrated Security=True;Pooling=False";
        public SqlConnection sqlCon = new SqlConnection(myCon);
        public String query;
        public SqlCommand cmd;
        public SqlDataAdapter da;
        public System.Data.DataTable dt;

        string createUserTable = @"
CREATE TABLE [dbo].[myUsers] (
    [username]  VARCHAR (50) NOT NULL,
    [password]  VARCHAR (50) NOT NULL,
    [privilege] INT          DEFAULT ((1)) NOT NULL,
    [PartOfGroup]     VARCHAR (50) DEFAULT ('noGroup') NOT NULL,
    [OwnGroup]   VARCHAR (50) NOT NULL DEFAULT ('noGroup'),
    PRIMARY KEY CLUSTERED ([username] ASC)
)";

        public string currentUser;
        public int currentUserPrivilege = 1;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainCanvas.Content = new PageLogin();
            CreateDefaultTables();
        }

        //If default tables doesn't exist, create using SQL query
        private void CreateDefaultTables() {
            string mm = "SELECT DISTINCT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_NAME = 'myUsers'";
            DataTable tempDt = SqlRunQuery(mm);

            int tempInt = tempDt.Rows.Count;
            bool createDefaultUser = false;

            ////Check is table already exist in database
            if (tempInt == 0)
            {
                tempDt = SqlRunQuery(createUserTable);
                createDefaultUser = true;
            }
            else
            {
                //Check default user
                mm = "SELECT * FROM myUsers WHERE myUsers.username = 'admin'";
                tempDt = SqlRunQuery(mm);
                tempInt = tempDt.Rows.Count;
                if (tempInt == 0)
                {
                    createDefaultUser = true;
                }
            }

            if (createDefaultUser)
            {
                mm = "INSERT INTO myUsers (username, password, privilege) VALUES ('admin', '1234', 3)";
                tempDt = SqlRunQuery(mm);
                MessageBox.Show("default table created");
            }
        }

        public void Goto_LoginPage(object sender, RoutedEventArgs e)
        {
            MainCanvas.Content = new PageLogin();
            myDataGrid2.Visibility = Visibility.Collapsed;
        }

        public void Goto_UserPage(object sender, RoutedEventArgs e)
        {
            MainCanvas.Content = new PageUser();
            myDataGrid2.Visibility = Visibility.Visible;
        }

        public void Goto_ProfilePage(object sender, RoutedEventArgs e)
        {
            MainCanvas.Content = new PageProfile();
            myDataGrid2.Visibility = Visibility.Collapsed;
        }

        public DataTable SqlRunQuery(string myStr = "")
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                query = myStr;

                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                //Display data on this DataGrid tag
                myDataGrid2.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                sqlCon.Close();
            }

            return dt;
        }

        public void SqlTest(string myStr = "")
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                query = myStr;

                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                //Display data on this DataGrid tag
                myDataGrid2.ItemsSource = dt.DefaultView;

                string kk = string.Join(Environment.NewLine, dt.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
                MessageBox.Show(kk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                sqlCon.Close();
            }
        }

        public void SqlAddUser(string myUser, string myPass, int myP=1, string myGroup="noGroup", string myOwnGroup = "noGroup")
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                query = "INSERT INTO myUsers VALUES (@username, @password, @privilege, @PartOfGroup, @OwnGroup)";
                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", myUser);
                cmd.Parameters.AddWithValue("@password", myPass);
                cmd.Parameters.AddWithValue("@privilege", myP);
                cmd.Parameters.AddWithValue("@PartOfGroup", myGroup);
                cmd.Parameters.AddWithValue("@OwnGroup", myOwnGroup);

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                //Display data on this DataGrid tag
                myDataGrid2.ItemsSource = dt.DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public bool SqlCheckLogin(string myUser, string myPass)
        {
            bool isValid = false;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                query = "SELECT * FROM myUsers WHERE username=@username AND password=@password";
                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", myUser);
                cmd.Parameters.AddWithValue("@password", myPass);

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                int getRows = dt.Rows.Count;
                if (getRows > 0)
                {
                    isValid = true;
                    if (true) {
                        currentUser = myUser;
                        currentUserPrivilege = Int32.Parse(dt.Rows[0]["privilege"].ToString());
                        WelcomeBanner.Content = "Welcome, " + myUser;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlCon.Close();
            }

            return isValid;
        }

        public bool SqlCheckUsername(string myUser)
        {
            bool isValid = false;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                query = "SELECT * FROM myUsers WHERE username=@username";
                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", myUser);

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                int getRows = dt.Rows.Count;
                if (getRows > 0)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlCon.Close();
            }

            return isValid;
        }

        public void SqlDeleteUser(string myUser, string myPass)
        {
            try
            {

                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                query = "DELETE FROM myUsers WHERE username = @username AND password = @password";
                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", myUser);
                cmd.Parameters.AddWithValue("@password", myPass);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                //SqlRunQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            finally
            {
                sqlCon.Close();
            }
        }

        public void SqlUpdateUser(string myUser, string myPass, string myNewPass, string myPriv)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();


                query = "UPDATE myUsers SET password = @password2 AND privilege = @privilege WHERE username = @username AND password = @password";
                cmd = new SqlCommand(query, sqlCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@username", myUser);
                cmd.Parameters.AddWithValue("@password", myPass);
                cmd.Parameters.AddWithValue("@password2", myNewPass);
                cmd.Parameters.AddWithValue("@privilege", myPriv);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public void Goto_Testing(object sender, RoutedEventArgs e)
        {
            string te = SqlCheckLogin("user3", "123").ToString();
            MessageBox.Show(te);
        }


        public void Goto_Test1(object sender, RoutedEventArgs e)
        {
            SqlTest("SELECT * FROM myUsers");
        }

        public void Goto_UserPage1(object sender, RoutedEventArgs e)
        {
            SqlRunQuery("SELECT * FROM myUser");
        }

    }
}
