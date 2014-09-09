using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SmartPayout
{
    public class DBConnect
    {
        CLogger logger = CLogger.Instance;

        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        private static DBConnect instance;

        public static DBConnect Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new DBConnect();
                }
                return instance;
            }
        }

        //Constructor
        private DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "sskfrontend";
            uid = "root";
            password = "GO816og#";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            conn = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        logger.UpdateLog("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        logger.UpdateLog("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                logger.UpdateLog(ex.Message);
                return false;
            }
        }

        public void UpdateInventoryAfterDispensed(String lastNoteValue, String trxId)
        {                   
            if (this.OpenConnection() == true)
            {
                new MySqlCommand("update ssp_inventory set current_payout_note = (current_payout_note - 1) where device_code = '01' and denom = '" + lastNoteValue + "'", conn).ExecuteNonQuery();                
                new MySqlCommand("insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" + lastNoteValue + "','dispense','-1','0','" + trxId + "')", conn).ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void UpdateInventoryAfterDispensed(List<ChannelData> data)
        {
            if (this.OpenConnection() == true)
            {
                foreach (ChannelData d in data)
                {
                    new MySqlCommand("update ssp_inventory set current_payout_note = '" + d.Level + "' where device_code = '01' and denom = '" + (d.Value / 100).ToString() + "'", conn).ExecuteNonQuery();                   
                }                                
                this.CloseConnection();
            }
        }

        public void UpdateInventoryAfterDispensed(Dictionary<Int32, String> data, String trxId)
        {
            if (this.OpenConnection() == true)
            {
                foreach (KeyValuePair<Int32, String> entry in data)
                {
                    new MySqlCommand("insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" + entry.Key + "','dispense','-"+entry.Value+"','0','" + trxId + "')", conn).ExecuteNonQuery();
                }                
                this.CloseConnection();
            }
        }

        public void UpdateInventoryAfterStacked(String lastNoteValue, String trxId)
        {            
            if (this.OpenConnection() == true)
            {
                new MySqlCommand("update ssp_inventory set current_cashbox_note = (current_cashbox_note + 1) where device_code = '01' and denom = '" + lastNoteValue + "'", conn).ExecuteNonQuery();                                
                new MySqlCommand("insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" + lastNoteValue + "','accept','0','1','" + trxId + "')", conn).ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void UpdateInventoryAfterStored(String lastNoteValue, String trxId)
        {            
            if (this.OpenConnection() == true)
            {
                new MySqlCommand("update ssp_inventory set current_payout_note = (current_payout_note + 1) where device_code = '01' and denom = '" + lastNoteValue + "'", conn).ExecuteNonQuery();                
                new MySqlCommand("insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" + lastNoteValue + "','accept','1','0','" + trxId +"')", conn).ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void UpdateInventoryAfterEmptyed(List<ChannelData> m_UnitDataList)
        {            
            if (this.OpenConnection() == true)
            {                
                foreach (ChannelData d in m_UnitDataList)
                {
                    SmartPayout.Instance.Payout.ChangeNoteRoute(d.Value, d.Currency, false);
                    new MySqlCommand("update ssp_inventory set current_cashbox_note = (current_cashbox_note+current_payout_note), current_routing = '0', "+
                        "current_payout_note = '" + d.Level + "' where device_code = '01' and denom = " + d.Value / 100, conn).ExecuteNonQuery();
                    new MySqlCommand("insert into ssp_history(device_code,denom,command,payout,cashbox,stan) values('01','" + d.Value/100 + "','empty','0',payout+cashbox,'')", conn).ExecuteNonQuery();                    
                }                                
                this.CloseConnection();
            }
        }

        public bool GetRouteSetting(int denom)
        {
            Int32 value = 0;
            if (this.OpenConnection() == true)
            {
                MySqlDataReader dataReader = new MySqlCommand("select current_routing from ssp_inventory where device_code = '01' and denom ='"+denom+"'", conn).ExecuteReader();
                while (dataReader.Read())
                {                    
                    value = (Int32)dataReader["current_routing"];                    
                }
                dataReader.Close();
                this.CloseConnection();                
            }
            if (value == 1)
            {
                return true;
            }
            return false;            
        }

        public String GetSmartPayoutPort()
        {
            String value = null;
            if (this.OpenConnection() == true)
            {
                MySqlDataReader dataReader = new MySqlCommand("select device_port from device where device_code = '01'", conn).ExecuteReader();
                while (dataReader.Read())
                {
                    value = (String)dataReader["device_port"];
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return value;  
        }
    }
}
