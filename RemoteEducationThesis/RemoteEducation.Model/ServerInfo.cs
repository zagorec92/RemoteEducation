namespace Education.Model
{
    public class ServerInfo : EntityBase
    {
        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the host name.
        /// </summary>
        public string Hostname { get; set; }
    }
}
