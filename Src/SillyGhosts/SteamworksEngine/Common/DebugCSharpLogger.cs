// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.DebugCSharpLogger
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Steamworks.Engine.Common
{
  public class DebugCSharpLogger : ILogger
  {
    private List<int> ids;

    public DebugCSharpLogger(int[] ids) => this.ids = ((IEnumerable<int>) ids).ToList<int>();

    public void SetLogId(int[] ids) => this.ids = ((IEnumerable<int>) ids).ToList<int>();

    public void WriteLine(int id, object message) => this.ids.Contains(id);

    public void BeginGame()
    {
    }

    public void BeginLevel(int i)
    {
    }

    public void EndLevel(int score)
    {
    }

    public void EndGame()
    {
    }

    public void Alert(string msg)
    {
    }
  }
}
