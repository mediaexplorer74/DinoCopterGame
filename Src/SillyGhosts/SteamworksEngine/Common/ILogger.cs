// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.ILogger
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Common
{
  public interface ILogger
  {
    void SetLogId(int[] ids);

    void WriteLine(int id, object message);

    void BeginGame();

    void BeginLevel(int i);

    void EndLevel(int score);

    void EndGame();

    void Alert(string msg);
  }
}
