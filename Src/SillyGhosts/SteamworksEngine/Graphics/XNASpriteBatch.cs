// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.XNASpriteBatch
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class XNASpriteBatch : ISpriteBatch
  {
    private SpriteBatch XnaBatch;

    public float Scale { get; set; } = 1f;

    public XNASpriteBatch(SpriteBatch xnaBatch) => this.XnaBatch = xnaBatch;

    public void BeginOpaqueCam(ICamera2D camera, float ParallaxX, float ParallaxY)
    {
      this.XnaBatch.Begin(blendState: BlendState.Opaque, transformMatrix: new Matrix?(camera.GetViewMatrix(ParallaxX * this.Scale, ParallaxY * this.Scale)));
    }

    public void BeginAlphaCam(ICamera2D camera, float ParallaxX, float ParallaxY)
    {
      this.XnaBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: new Matrix?(camera.GetViewMatrix(ParallaxX * this.Scale, ParallaxY * this.Scale)));
    }

    public void BeginAlpha() => this.XnaBatch.Begin(blendState: BlendState.AlphaBlend);

    public void End() => this.XnaBatch.End();

    public void Draw(
      TextureInfo textureInfo,
      Steamworks.Engine.Common.Vector2 position,
      RectangleF sourceRectangle,
      Steamworks.Engine.Common.Color color,
      float rotation,
      Steamworks.Engine.Common.Vector2 origin,
      float scale,
      FlipDirection flip,
      float layerDepth,
      float Alpha)
    {
      SpriteEffects spriteEffects = XNASpriteBatch.GetSpriteEffects(flip);
      Rectangle? sourceRectangle1 = new Rectangle?();
      if (sourceRectangle != null)
        sourceRectangle1 = new Rectangle?(new Rectangle((int) sourceRectangle.X, (int) sourceRectangle.Y, (int) sourceRectangle.Width, (int) sourceRectangle.Height));
      this.XnaBatch.Draw(textureInfo.Texture, (Microsoft.Xna.Framework.Vector2) position * this.Scale, sourceRectangle1, (Microsoft.Xna.Framework.Color) color * Alpha, rotation, (Microsoft.Xna.Framework.Vector2) origin, scale * this.Scale, spriteEffects, layerDepth);
    }

    private static SpriteEffects GetSpriteEffects(FlipDirection flip)
    {
      SpriteEffects spriteEffects;
      switch (flip)
      {
        case FlipDirection.X:
          spriteEffects = SpriteEffects.FlipHorizontally;
          break;
        case FlipDirection.Y:
          spriteEffects = SpriteEffects.FlipVertically;
          break;
        default:
          spriteEffects = SpriteEffects.None;
          break;
      }
      return spriteEffects;
    }

    public void DrawString(
      FontInfo fontInfo,
      string text,
      Steamworks.Engine.Common.Vector2 position,
      Steamworks.Engine.Common.Color color,
      float rotation,
      Steamworks.Engine.Common.Vector2 origin,
      float scale,
      FlipDirection flip,
      float layerDepth,
      float Alpha)
    {
      SpriteEffects spriteEffects = XNASpriteBatch.GetSpriteEffects(flip);
      this.XnaBatch.DrawString(fontInfo.Font, text, (Microsoft.Xna.Framework.Vector2) position * this.Scale, (Microsoft.Xna.Framework.Color) color * Alpha, rotation, (Microsoft.Xna.Framework.Vector2) origin, scale * this.Scale, spriteEffects, layerDepth);
    }

    public void BeginOpaque() => this.XnaBatch.Begin(blendState: BlendState.Opaque);
  }
}
