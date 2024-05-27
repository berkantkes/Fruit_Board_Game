using System;
using System.Collections.Generic;

[Serializable]
public class CollectionWrapper<T>
{
    public List<T> Items;

    public CollectionWrapper(List<T> items)
    {
        Items = items;
    }
}
