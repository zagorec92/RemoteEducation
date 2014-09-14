using Education.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RemoteEducation.DAL.Repositories
{
    public class ServerInfoRepository : RepositoryBase<ServerInfo>
    {
        public ServerInfoRepository(EEducationDbContext context)
            : base(context) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IPAddress GetLastIPAddress()
        {
            ServerInfo serverInfo = base.GetAll().
                OrderByDescending(x => x.DateCreated).
                FirstOrDefault();

            return serverInfo != null ? 
                IPAddress.Parse(serverInfo.IpAddress) : null;
        }
    }
}
