using Education.Model;
using System.Linq;
using System.Net;

namespace Education.DAL.Repositories
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
