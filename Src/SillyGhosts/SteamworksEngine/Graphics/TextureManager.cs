// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.TextureManager
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class TextureManager : Manager<TextureInfo>, ITextureManager
  {
    public void Load(ContentManager manager)
    {
      foreach (string key in this.Dict.Keys)
        this.Dict[key].Texture = manager.Load<Texture2D>(this.Dict[key].FilePath);
    }
  }
}
