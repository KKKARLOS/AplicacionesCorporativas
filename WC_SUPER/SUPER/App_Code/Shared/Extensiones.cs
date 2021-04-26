using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Descripción breve de Extensiones
/// </summary>
public static class Extensiones
{
    public static DataTable CopyGenericToDataTable<T>(this IEnumerable<T> items)
    {
        var properties = typeof(T).GetProperties();
        var dt = new DataTable();

        foreach (var prop in properties)
        {
            dt.Columns.Add(prop.Name);
        }

        foreach (var item in items)
        {
            var row = dt.NewRow();

            foreach (var prop in properties)
            {
                var itemValue = prop.GetValue(item, new object[] { });
                row[prop.Name] = itemValue;
            }

            dt.Rows.Add(row);
        }

        return dt;
    }

    public static object ToType<T>(this object obj, T type)
    {

        //create instance of T type object:
        var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

        //loop through the properties of the object you want to covert:          
        foreach (PropertyInfo pi in obj.GetType().GetProperties())
        {
            try
            {

                //get the value of property and try 
                //to assign it to the property of T type object:
                tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                          pi.GetValue(obj, null), null);
            }
            catch { }
        }

        //return the T type object:         
        return tmp;
    }

    public static object ToNonAnonymousList<T>(this List<T> list, Type t)
    {

        //define system Type representing List of objects of T type:
        var genericType = typeof(List<>).MakeGenericType(t);

        //create an object instance of defined type:
        var l = Activator.CreateInstance(genericType);

        //get method Add from from the list:
        MethodInfo addMethod = l.GetType().GetMethod("Add");

        //loop through the calling list:
        foreach (T item in list)
        {

            //convert each object of the list into T object 
            //by calling extension ToType<T>()
            //Add this object to newly created list:
            addMethod.Invoke(l, new object[] { item.ToType(t) });
        }

        //return List of T objects:
        return l;
    }
}
