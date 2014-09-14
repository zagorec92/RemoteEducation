using Education.Model;
using RemoteEducation.DAL;
using RemoteEducation.DAL.Repositories;
using System.Net;

namespace RemoteEducationApplication.Helpers
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        public static void SaveServerInfo(IPAddress ipAddress)
        {
            using(EEducationDbContext context = new EEducationDbContext())
            {
                ServerInfoRepository serverInfoRepository = new ServerInfoRepository(context);
                ServerInfo serverInfo = new ServerInfo();
                serverInfo.IpAddress = ipAddress.ToString();

                if (serverInfoRepository.InsertOrUpdate(serverInfo))
                    serverInfoRepository.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLastIPAddress()
        {
            using(EEducationDbContext context = new EEducationDbContext())
            {
                ServerInfoRepository serverInfoRepository = new ServerInfoRepository(context);
                return serverInfoRepository.GetLastIPAddress();
            }
        }
    }
}
