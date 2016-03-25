using Microsoft.SqlServer.Management.Smo;
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


namespace DataBaseAssignmentWPF
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            DataBaseManager DBM = new DataBaseManager();
            rtbLog.AppendText("- Connecting to Local Server... \n\n");
            List<Database> databases = DBM.GetDataBases();
            rtbLog.AppendText("- Displaying Databases... \n\n");

            lbDBs.DisplayMemberPath = "Name";
            foreach (Database data in databases)
            {
                lbDBs.Items.Add(data);
            }
        }

        private void backup_Click(object sender, RoutedEventArgs e)
        {
            var db = lbDBs.SelectedItem as Database;
            rtbLog.AppendText("- Creating backup of database " + db.Name  +"... \n\n");
            DataBaseManager DBM = new DataBaseManager();
            DBM.BackupDataBase(db);
            rtbLog.AppendText("- Backup of database " + db.Name + " complete. \n\n");

        }

        private void rtbLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtbLog.ScrollToEnd();
        }
    }
}
