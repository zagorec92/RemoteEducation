using Education.Model.Entities;
using Education.Model.ETypeEntities;
using ExtensionLibrary.DataTypes.Helpers;
using System;
using System.Collections.Generic;
using ExtensionLibrary.Enums.Extensions;
using ExtensionLibrary.Enums.Helpers;

namespace Education.DAL
{
	public class MigrationHelper
	{
		#region Test

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="mapPairs"></param>
		/// <returns></returns>
		public static T[] CreateTestData<T>(string path, Dictionary<string, object> mapPairs = null)
			where T : Entity, new()
		{
			List<T> list = XmlHelper.MapXmlToObject<T>(path, mapPairs);
			list.ForEach(x => x.DateModified = DateTime.Now);

			return list.ToArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="mapPairs"></param>
		/// <returns></returns>
		public static T[] CreateTestDataForETypes<T>(string path, Dictionary<string, object> mapPairs = null)
			where T : EEntity, new()
		{
			List<T> list = XmlHelper.MapXmlToObject<T>(path, mapPairs);

			return list.ToArray();
		}

		#endregion
	}
}
