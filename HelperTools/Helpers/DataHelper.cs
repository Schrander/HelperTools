using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace HelperTools.Helpers
{
	public static class DataHelper
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> list)
		{
			DataTable currentDT = CreateTable<T>();

			Type type = typeof(T);

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
			foreach (T item in list)
			{
				DataRow row = currentDT.NewRow();
				foreach (PropertyDescriptor prop in properties)
				{

					if (prop.PropertyType == typeof(decimal?) || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(long?))
						row[prop.Name] = prop.GetValue(item) ?? 0;

					else if (prop.PropertyType == typeof(Guid?))
						row[prop.Name] = prop.GetValue(item) ?? new Guid();

					else if (prop.PropertyType == typeof(DateTime?))
						if (prop.GetValue(item) == null)
							row[prop.Name] = prop.GetValue(item) ?? DateTime.Now;
						else
							row[prop.Name] = prop.GetValue(item);

				}
				currentDT.Rows.Add(row);
			}
			return currentDT;
		}

		private static DataTable CreateTable<T>()
		{
			Type entType = typeof(T);
			DataTable tbl = new DataTable();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
			foreach (PropertyDescriptor prop in properties)
			{
				if (prop.PropertyType == typeof(decimal?))
					tbl.Columns.Add(prop.Name, typeof(decimal));
				else if (prop.PropertyType == typeof(int?))
					tbl.Columns.Add(prop.Name, typeof(int));
				else if (prop.PropertyType == typeof(long?))
					tbl.Columns.Add(prop.Name, typeof(long));
				else if (prop.PropertyType == typeof(Guid?))
					tbl.Columns.Add(prop.Name, typeof(Guid));
				else if (prop.PropertyType == typeof(DateTime?))
					tbl.Columns.Add(prop.Name, typeof(DateTime));
				else
					tbl.Columns.Add(prop.Name, prop.PropertyType);
			}
			return tbl;
		}

		public static T DataRowToObject<T>(this DataRow dr) where T : new()
		{
			Type t = typeof(T);
			T result = new T();

			foreach (DataColumn dc in dr.Table.Columns)
			{
				try
				{
					t.InvokeMember(dc.ColumnName, BindingFlags.SetProperty, null, result, new object[] { dr[dc] });
				}
				catch (Exception ex)
				{
					//Usually you are getting here because a column doesn't exist or it is null
					if (ex.ToString() != null) { }
				}
			}

			return result;
		}
	}
}