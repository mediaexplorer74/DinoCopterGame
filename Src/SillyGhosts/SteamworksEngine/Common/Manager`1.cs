// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.Manager`1
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Shared.Graphics;
using System;
using System.Collections.Generic;


namespace Steamworks.Engine.Common
{
  public class Manager<T> where T : ResourceInfo
  {
    protected Dictionary<string, T> Dict = new Dictionary<string, T>();

    public void Init(T[] infos)
    {
      for (int index = 0; index < infos.Length; ++index)
        this.Dict.Add(infos[index].Name.ToLower(), infos[index]);
    }

    public T Get(string key)
    {
      return this.Dict.ContainsKey(key.ToLower()) ? this.Dict[key.ToLower()] : throw new Exception("Brak resource " + key);
    }
  }
}
