// Decompiled with JetBrains decompiler
// Type: Steamworks.Shared.Logger
// Assembly: Steamworks.Shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A135BB0-A5B3-40D7-BB59-980AD29572AB
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Shared.dll

using System.Diagnostics;
using System.Text;

#nullable disable
namespace Steamworks.Shared
{
  public class Logger
  {
    private string Source;

    public Logger(params object[] msg)
    {
      if (!Debugger.IsAttached)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < msg.Length; ++index)
      {
        stringBuilder.Append(msg[index].ToString());
        stringBuilder.Append(" ");
      }
      this.Source = stringBuilder.ToString();
    }

    public void Info(params object[] msg)
    {
      if (!Debugger.IsAttached)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < msg.Length; ++index)
      {
        stringBuilder.Append(msg[index].ToString());
        stringBuilder.Append(" ");
      }
      stringBuilder.ToString();
    }

    public void Debug(params object[] msg)
    {
      if (!Debugger.IsAttached)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < msg.Length; ++index)
      {
        stringBuilder.Append(msg[index].ToString());
        stringBuilder.Append(" ");
      }
      stringBuilder.ToString();
    }

    public void Error(params object[] msg)
    {
      if (!Debugger.IsAttached)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < msg.Length; ++index)
      {
        stringBuilder.Append(msg[index].ToString());
        stringBuilder.Append(" ");
      }
      stringBuilder.ToString();
    }
  }
}
