// Decompiled with JetBrains decompiler
// Type: steamworks.games.game.opengl.OpenGlPersister
// Assembly: Silly Gosts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD37877E-4464-40FF-B069-FD996B32AF74
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Silly Gosts.dll

using steamworks.games.game.core;
using System;
using System.IO;

#nullable disable
namespace steamworks.games.game.opengl
{
  public class OpenGlPersister : PersisterBase
  {
    public override void Save(byte[] bytesToSave, int saveByteCount, string SaveName)
    {
      File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveName), bytesToSave);
    }

    public override byte[] Load(string SaveName, int saveByteCount)
    {
      string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveName);
      return File.Exists(path) ? File.ReadAllBytes(path) : this.GetBytes(saveByteCount);
    }

    public OpenGlPersister()
      : base()
    {
    }
  }
}
