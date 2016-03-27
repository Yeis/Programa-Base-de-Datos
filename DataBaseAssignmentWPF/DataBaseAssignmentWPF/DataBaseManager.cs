﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Text.RegularExpressions;

namespace DataBaseAssignmentWPF
{
    //Capa de Negocios para operaciones relacionadas a las bases de datos
    public class DataBaseManager
    {
        //Metodo que nos trae todas las bases de datos del local server 
        public List<Database> GetDataBases()
        {
            List<Database> dbs = new List<Database>();

            Server myServer = new Server(@"(local)");
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();
            
            foreach (Database myDB in myServer.Databases)
            {
                dbs.Add(myDB);
            }

            if (myServer.ConnectionContext.IsOpen)
            {
                myServer.ConnectionContext.Disconnect();
               
            }
            
            return dbs;
        }
        //Metodo que crea una base de datos con un nombre como parametro (cero configuraciones)
        public void CrearDataBase(string nombre)
        {
            try
            {
                Server myserver = new Server(@"(local)");
                myserver.ConnectionContext.LoginSecure = true;
                myserver.ConnectionContext.Connect();
                Database db = new Database(myserver, nombre);
                db.Create();

                if (myserver.ConnectionContext.IsOpen)
                    myserver.ConnectionContext.Disconnect();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<String> GetAllBackups()
        {
            List<String> Backups = new List<String>();
            
            try
            {
               
                Server myserver = new Server(@"(local)");
                myserver.ConnectionContext.LoginSecure = true;
                myserver.ConnectionContext.Connect();

                string[] files = Directory.GetFiles(myserver.BackupDirectory,"*.bak",SearchOption.AllDirectories);

                foreach (Database item in myserver.Databases)
                {

                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].Contains(item.Name))
                        {
                            //  Backups.Add(files[i].Substring(71) + "Belongs to: " + item.Name);
                           Backups.Add(files[i].Substring(files[i].LastIndexOf("Backup\\")+7) +  " Belongs to " + item.Name);
                            string temp = files[i].LastIndexOf("Backup\\").ToString(); 
                        }

                    }
                }

                if (myserver.ConnectionContext.IsOpen)
                    myserver.ConnectionContext.Disconnect();
            }
            catch (Exception)
            {

                throw;
            }
            

            return Backups;
        }

        //Me todo que hace un Backup de una base de datos seleccionada
        public void BackupDataBase(Database db)
        {
            Backup buFull = new Backup();
            buFull.Action = BackupActionType.Database;
            buFull.Database = db.Name;
            BackupDeviceItem BDI = new BackupDeviceItem();
            BDI.DeviceType = DeviceType.File;
            string dbTime = db.Name + "-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day 
                + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            BDI.Name = @".\" + dbTime + ".bak";
            //buFull.Incremental = true;
            //buFull.Devices.AddDevice(", DeviceType.File);
            buFull.Devices.Add(BDI);

            buFull.BackupSetName = db.Name + " Backup";
            buFull.BackupSetDescription = db.Name + " - Full Backup";

            Server myServer = new Server(@"(local)");
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();

            buFull.SqlBackup(myServer);

            if (myServer.ConnectionContext.IsOpen)
                myServer.ConnectionContext.Disconnect();

        }
    }
}
