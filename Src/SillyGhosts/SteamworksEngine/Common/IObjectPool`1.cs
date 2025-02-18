﻿// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.IObjectPool`1
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Common
{
  public interface IObjectPool<T> where T : new()
  {
    T New();

    void Return(T item);

    void Clear();
  }
}
