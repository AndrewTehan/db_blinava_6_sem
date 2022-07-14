using System;
using System.Data.SqlTypes;
using System.IO;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedType(Format.UserDefined, IsFixedLength = false, MaxByteSize = 100, Name = "Route")]
public struct Route : INullable, IBinarySerialize
{
    private const string rt = "Minsk,Moscow";
    public string route;

    private bool _null;


    public override string ToString()
    {
        return string.Join("-->>", route.Split(','));
    }

    public bool IsNull
    {
        get
        {
            // Введите здесь код
            return _null;
        }
    }

    public static Route Null
    {
        get
        {
            var h = new Route
            {
                _null = true
            };
            return h;
        }
    }

    public static Route Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        var u = new Route
        {
            route = string.Join("-->>", s.Value.Split(','))
        };
        return u;
    }

    public void Read(BinaryReader r)
    {
        route = route??  rt;
    }

    public void Write(BinaryWriter w)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(route);
        w.Write(bytes);
    }
}