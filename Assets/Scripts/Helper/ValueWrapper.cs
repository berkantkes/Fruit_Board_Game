using System;

[Serializable]
public class ValueWrapper<T>
{
    public T Value;

    public ValueWrapper(T value)
    {
        Value = value;
    }
}
