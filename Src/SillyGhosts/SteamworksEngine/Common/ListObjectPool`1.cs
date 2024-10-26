// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.ListObjectPool`1
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using System.Collections.Generic;


namespace Steamworks.Engine.Common
{
  public class ListObjectPool<T> : IObjectPool<T> where T : new()
  {
    private List<T> items = new List<T>();
    private int Active;

    public T New()
    {
      if (this.Active < this.items.Count)
      {
        T obj = this.items[this.Active];
        ++this.Active;
        return obj;
      }
      T obj1 = new T();
      this.items.Add(obj1);
      ++this.Active;
      return obj1;
    }

    public void Return(T item)
    {
      this.items.Remove(item);
      --this.Active;
      this.items.Insert(this.Active, item);
    }

    public void Clear() => this.Active = 0;
  }
}
