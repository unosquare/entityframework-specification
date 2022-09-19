namespace Unosquare.EntityFramework.Specification.Common.Primitive;

public class KeyWithCount<T>
{
    public T Key { get; set; }

    public int Count { get; set; }
}