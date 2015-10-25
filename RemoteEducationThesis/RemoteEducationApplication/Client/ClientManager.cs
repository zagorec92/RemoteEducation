using System.Collections.ObjectModel;
using System.Net.Sockets;
using ExtensionLibrary.Collections.Extensions;
using ExtensionLibrary.Enums.Extensions;

namespace Education.Application.Client
{
	public static class ClientManager
	{
		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public static ObservableCollection<ClientHandler> Clients { get; set; }

		#endregion

		#region DataExchange

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static string GetUserIdentification(NetworkStream stream)
		{
			int length = stream.ReadByte();
			byte[] buffer = new byte[length];

			stream.Read(buffer, 0, length);
			return buffer.GetString();
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientHandler"></param>
		/// <returns></returns>
		public static bool IsClientConnected(this ClientHandler clientHandler)
		{
			if (clientHandler.IsClientConnected)
				return true;
			else
				throw new SocketException(SocketError.ConnectionAborted.GetValue());
		}
	}
}
