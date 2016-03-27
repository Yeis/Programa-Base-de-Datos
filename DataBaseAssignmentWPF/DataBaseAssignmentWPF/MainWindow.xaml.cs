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
        DataBaseManager DBM = new DataBaseManager();
        List<Database> databases;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
         
            rtbLog.AppendText("- Connecting to Local Server... \n\n");
            databases = DBM.GetDataBases();
            rtbLog.AppendText("- Displaying Databases... \n\n");

            ListDatabases();
            
        }
        public void ListDatabases()
        {
            lbDBs.DisplayMemberPath = "Name";
            lbDBs.Items.Clear();
            foreach (Database data in databases)
            {
                lbDBs.Items.Add(data);
            }
        }
    
        private void backup_Click(object sender, RoutedEventArgs e)
        {
            var db = lbDBs.SelectedItem as Database;
            rtbLog.AppendText("- Creating backup of database " + db.Name  +"... \n\n");
            DBM.BackupDataBase(db);
            rtbLog.AppendText("- Backup of database " + db.Name + " complete. \n\n");

        }

        private void rtbLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtbLog.ScrollToEnd();
        }

        private void buttoncrear_Click(object sender, RoutedEventArgs e)
        {
            //Crear una base de datos 
            rtbLog.AppendText("- Creating Database... \n\n");
            if (tb_BaseDatos.Text != "")
            {
                DBM.CrearDataBase(tb_BaseDatos.Text);
                databases = DBM.GetDataBases();
                ListDatabases();
                rtbLog.AppendText("- Database"+tb_BaseDatos.Text+" created \n\n");
                tb_BaseDatos.Text = "";
            }
            else
            {
                rtbLog.AppendText("- Invalid Input \n\n");
            }
      
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //listamos todos los backups existentes en el log 
            List<String> BDIs = DBM.GetAllBackups();
            foreach (String i in BDIs)
            {
                rtbLog.AppendText(i + "\n\n");
            }
        }
    }
}
