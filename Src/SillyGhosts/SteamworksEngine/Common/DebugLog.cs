// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.DebugLog
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Common
{
  public static class DebugLog
  {
    private static ILogger logger = (ILogger) new DebugCSharpLogger(new int[2]
    {
      0,
      4
    });

    public static void WriteLine(int id, object message) => DebugLog.logger.WriteLine(id, message);

    public static void BeginGame() => DebugLog.logger.BeginGame();

    public static void BeginLevel(int i) => DebugLog.logger.BeginLevel(i);

    public static void EndLevel(int score) => DebugLog.logger.EndLevel(score);

    public static void EndGame() => DebugLog.logger.EndGame();

    public static void Alert(string msg) => DebugLog.logger.Alert(msg);
  }
}
