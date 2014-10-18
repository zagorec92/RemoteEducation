using RemoteEducationApplication.Client;
using System.Collections.ObjectModel;

namespace RemoteEducationApplication.Extensions
{
    public static class ObservableCollectionExtension
    {
        #region Generic

        /// <summary>
        /// Moves item from current index to another.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="collection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance on which move is executed.</param>
        /// <param name="oldIndex">Current index.</param>
        /// <param name="newIndex">New index.</param>
        public static void MoveExtended<T>(this ObservableCollection<T> collection, int oldIndex, int newIndex)
        {
            T item = collection[oldIndex];
            collection.Remove(item);
            collection.Insert(newIndex, item);
        }

        /// <summary>
        /// Removes all items except the first one from existing collection and adds thme to another collection.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="addCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance in which items will be added.</param>
        /// <param name="removeCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance from which items will be removed.</param>
        public static void TakeExceptFirst<T>(this ObservableCollection<T> addCollection,
            ObservableCollection<T> removeCollection)
        {
            int length = removeCollection.Count - 1;

            for (int i = length; i > 0; i--)
            {
                addCollection.Add(removeCollection[i]);
                removeCollection.RemoveAt(i);
            }
        }

        /// <summary>
        /// Removes all items from existing collection and adds them to another collection.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="System.Object"/></typeparam>
        /// <param name="addCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance in which items will be added.</param>
        /// <param name="removeCollection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> 
        /// instance from which items will be removed.</param>
        public static void TakeAll<T>(this ObservableCollection<T> addCollection,
            ObservableCollection<T> removeCollection)
        {
            int length = removeCollection.Count - 1;

            for (int i = length; i > -1; i--)
            {
                addCollection.Add(removeCollection[i]);
                removeCollection.RemoveAt(i);
            }
        }

        #endregion

        #region ClientHandler

        /// <summary>
        /// Sorts the collection which contains clients.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="RemoteEducationApplication.Client.ClientHandler"/>.</typeparam>
        /// <param name="collection">The <see cref="System.Collections.ObjectModel.ObservableCollection"/> instance </param>
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
