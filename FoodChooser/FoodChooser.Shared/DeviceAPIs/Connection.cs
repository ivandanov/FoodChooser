using System;
using System.Collections.Generic;
using System.Text;
using Windows.Networking;
using Windows.Networking.Connectivity;

namespace FoodChooser.DeviceAPIs
{
    public class Connection
    {
        public static string GetConnection()
        {
            string connectionProfileInfo = string.Empty;
            try
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

                if (InternetConnectionProfile == null)
                {
                    connectionProfileInfo = "Not connected to Internet";
                }
                else
                {
                    connectionProfileInfo = InternetConnectionProfile.ProfileName;
                    //NotifyUser("Internet connection profile = " + connectionProfileInfo);
                }
            }
            catch (Exception ex)
            {
                //NotifyUser("Unexpected exception occurred: " + ex.ToString());
            }
            return connectionProfileInfo;
        }
    }
}
