﻿// Steamworks.Engine.XNA.XNAEngine

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;
using System;


namespace Steamworks.Engine.XNA
{
  public class XNAEngine : EngineBase
  {
    public XNAEngine(IDataLoader Loader, IGameSettings Settings)
      : base(Loader, Settings)
    {
    }

    public GraphicsDevice Device { get; set; }

    public override TextureInfo CreateTexture(Steamworks.Engine.Common.Color color, 
        float Width, float Height)
    {
      TextureInfo texture = new TextureInfo();
      texture.Texture = new Texture2D(this.Device, (int) Width, (int) Height);
      Microsoft.Xna.Framework.Color[] data = 
                new Microsoft.Xna.Framework.Color[texture.Texture.Width * texture.Texture.Height];
      for (int index = 0; index < data.Length; ++index)
        data[index] = (Microsoft.Xna.Framework.Color) color;
      texture.Texture.SetData<Microsoft.Xna.Framework.Color>(data);
      return texture;
    }

    public void LoadXNAResources(ContentManager contentManager)
    {
      (this.ResourceManagers.CurrentTextureManager as TextureManager).Load(contentManager);
      (this.ResourceManagers.CurrentFontManager as FontManager).Load(contentManager);
      (this.ResourceManagers.CurrentSoundManager as XNASoundManager).Load(contentManager);
    }

    protected override void CreateResources()
    {
      this.ResourceManagers = (Resources) new XNAResources();
    }

    public override void NavigateUrl(string url)
    {
        throw new NotImplementedException();
    }
  }
}
