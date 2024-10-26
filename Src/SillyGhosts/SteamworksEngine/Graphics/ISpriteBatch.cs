// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.ISpriteBatch
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public interface ISpriteBatch
  {
    void BeginOpaqueCam(ICamera2D camera, float ParallaxX, float ParallaxY);

    void BeginAlphaCam(ICamera2D camera, float ParallaxX, float ParallaxY);

    void BeginAlpha();

    void BeginOpaque();

    void End();

    void Draw(
      TextureInfo texture,
      Vector2 position,
      RectangleF sourceRectangle,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      FlipDirection flip,
      float layerDepth,
      float Alpha);

    void DrawString(
      FontInfo spriteFont,
      string text,
      Vector2 position,
      Color color,
      float rotation,
      Vector2 origin,
      float scale,
      FlipDirection flip,
      float layerDepth,
      float Alpha);

    float Scale { get; set; }
  }
}
