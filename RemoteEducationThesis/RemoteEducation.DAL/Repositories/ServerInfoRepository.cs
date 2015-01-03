using Education.Model;
using System.Linq;
using System.Net;

namespace Education.DAL.Repositories
{
    public class ServerInfoRepository : RepositoryBase<ServerInfo>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Education.DAL.Repositories.ServerInfoRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="Education.DAL.EEducationDbContext"/> instance.</param>
        public ServerInfoRepository(EEducationDbContext context)
            : base(context) { }

        /// <summary>
        /// Gets the last IP address.
        /// </summary>
        /// <returns>The <see cref="Systemm.Net.IPAddress"/> instance if exists, null otherwise.</returns>
        public IPAddress GetLastIPAddress()
        {
            ServerInfo serverInfo = base.GetAll()
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefault();

            return serverInfo != null ? 
                IPAddress.Parse(serverInfo.IpAddress) : null;
        }
    }
}
