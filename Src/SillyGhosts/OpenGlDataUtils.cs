// Decompiled with JetBrains decompiler
// Type: steamworks.games.game.opengl.OpenGlDataUtils
// Assembly: Silly Gosts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD37877E-4464-40FF-B069-FD996B32AF74
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Silly Gosts.dll

using steamworks.games.game.core;
using System;
using System.IO;

#nullable disable
namespace steamworks.games.game.opengl
{
  public class OpenGlDataUtils : IDataUtils
  {
    public Stream OpenFile(string name)
    {
      return (Stream) File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name));
    }
  }
}
