using RemoteEducationApplication.Client;
using System.Collections.ObjectModel;
using ExtensionLibrary.Collections.Extensions;

namespace RemoteEducationApplication.Extensions
{
    public static class ObservableCollectionExtension
    {
        #region ClientHandler

        /// <summary>
        /// Sorts the collection which contains clients.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="RemoteEducationApplication.Client.ClientHandler"/>.</typeparam>
        /// <param name="collection">The <see cref="System.Collections.ObjectModel.ObservableCollection{T}"/> instance.</param>
        public static void SortClients(this ObservableCollection<ClientHandler> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                for (int j = 0; j < collection.Count - i; j++)
                {
                    if (collection[j].ID > collection[j + 1].ID)
                        collection.MoveExtended(j, j + 1);

                    if (j == 0)
                        collection.MoveExtended(0, 0);
                }
            }
        }

        #endregion
    }
}
