using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections.Generic;
/// <summary>
/// Summary description for ListDto
/// </summary>
public sealed class ListDto<T> : List<T>
{

    public ListDto()
    { }

    public ListDto(IEnumerable<T> collection)
        : base(collection)
    { }

    public ListDto(int capacity)
        : base(capacity)
    { }

    private SearchListDtoArg[] _args;

    public SearchListDtoArg[] Argumentos
    {
        get { return _args; }
    }

    public List<T> ToList()
    {
        return new List<T>(base.ToArray());
    }
    public IList<T> ToIList()
    {
        return new List<T>(base.ToArray());
    }

    public bool Exists(SearchListDtoArg[] args)
    {
        _args = args;
        return base.Exists(findObject);
    }
    public bool Exists(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return Exists(args.ToArray());
    }
    public bool Exists(List<SearchListDtoArg> args)
    {
        return Exists(args.ToArray());
    }

    public T Find(SearchListDtoArg[] args)
    {
        _args = args;
        return base.Find(findObject);
    }
    public T Find(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return Find(args.ToArray());
    }
    public T Find(List<SearchListDtoArg> args)
    {
        return Find(args.ToArray());
    }

    public int FindIndex(SearchListDtoArg[] args)
    {
        _args = args;
        return base.FindIndex(findObject);
    }
    public int FindIndex(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindIndex(args.ToArray());
    }
    public int FindIndex(List<SearchListDtoArg> args)
    {
        return FindIndex(args.ToArray());
    }

    public int FindIndex(SearchListDtoArg[] args, int startIndex)
    {
        _args = args;
        return base.FindIndex(startIndex, findObject);
    }
    public int FindIndex(SearchListDtoArg arg, int startIndex)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindIndex(args.ToArray(), startIndex);
    }
    public int FindIndex(List<SearchListDtoArg> args, int startIndex)
    {
        return FindIndex(args.ToArray(), startIndex);
    }

    public int FindIndex(SearchListDtoArg[] args, int startIndex, int count)
    {
        _args = args;
        return base.FindIndex(startIndex, count, findObject);
    }
    public int FindIndex(SearchListDtoArg arg, int startIndex, int count)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindIndex(args.ToArray(), startIndex, count);
    }
    public int FindIndex(List<SearchListDtoArg> args, int startIndex, int count)
    {
        return FindIndex(args.ToArray(), startIndex, count);
    }

    public T FindLast(SearchListDtoArg[] args)
    {
        _args = args;
        return base.FindLast(findObject);
    }
    public T FindLast(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindLast(args.ToArray());
    }
    public T FindLast(List<SearchListDtoArg> args)
    {
        return FindLast(args.ToArray());
    }

    public int FindLastIndex(SearchListDtoArg[] args, int startIndex)
    {
        _args = args;
        return base.FindLastIndex(startIndex, findObject);
    }
    public int FindLastIndex(SearchListDtoArg arg, int startIndex)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindLastIndex(args.ToArray(), startIndex);
    }
    public int FindLastIndex(List<SearchListDtoArg> args, int startIndex)
    {
        return FindLastIndex(args.ToArray(), startIndex);
    }

    public int FindLastIndex(SearchListDtoArg[] args, int startIndex, int count)
    {
        _args = args;
        return base.FindLastIndex(startIndex, count, findObject);
    }
    public int FindLastIndex(SearchListDtoArg arg, int startIndex, int count)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindLastIndex(args.ToArray(), startIndex, count);
    }
    public int FindLastIndex(List<SearchListDtoArg> args, int startIndex, int count)
    {
        return FindLastIndex(args.ToArray(), startIndex, count);
    }

    public bool TrueForAll(SearchListDtoArg[] args)
    {
        _args = args;
        return base.TrueForAll(findObject);
    }
    public bool TrueForAll(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return TrueForAll(args.ToArray());
    }
    public bool TrueForAll(List<SearchListDtoArg> args)
    {
        return TrueForAll(args.ToArray());
    }

    public ListDto<T> FindAll(SearchListDtoArg arg)
    {
        ListDto<SearchListDtoArg> args = new ListDto<SearchListDtoArg>();
        args.Add(arg);
        return FindAll(args.ToArray());
    }
    public ListDto<T> FindAll(List<SearchListDtoArg> args)
    {
        return FindAll(args.ToArray());
    }
    public ListDto<T> FindAll(SearchListDtoArg[] args)
    {
        _args = args;
        T[] items = base.FindAll(findObject).ToArray();
        return new ListDto<T>(items);
    }

    private bool findObject(T item)
    {
        if (_args == null)
            return false;

        int numArgs = _args.Length;

        if (numArgs == 0)
            return false;

        int numSuccesses = 0;

        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item, true);
        object valueProp;
        PropertyDescriptor propDesc;
        foreach (SearchListDtoArg var in _args)
        {
            propDesc = properties.Find(var.NameProperty, true);
            if (propDesc != null)
            {
                valueProp = propDesc.GetValue(item);
                if (valueProp != null)
                {
                    if (valueProp.ToString().Equals(var.Value))
                    {
                        numSuccesses += 1;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(var.Value))
                    {
                        numSuccesses += 1;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        if (numArgs.Equals(numSuccesses))
            return true;
        else
            return false;
    }


}

public sealed class SearchListDtoArg
{
    private string property;
    private string _value;

    public SearchListDtoArg()
    { }

    public SearchListDtoArg(string property, string value)
    {
        this.property = property;
        this._value = value;
    }

    /// <summary>
    /// Nombre de la propiedad donde se buscara.
    /// </summary>
    public string NameProperty
    {
        get { return property; }
        set { property = value; }
    }

    /// <summary>
    /// Valor a buscar dentro de la Propiedad. 
    /// </summary>
    public string Value
    {
        get { return _value; }
        set { value = _value; }
    }
}
