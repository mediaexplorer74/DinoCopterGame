// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.DrawableRectangle
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class DrawableRectangle : Entity
  {
    public Color RectangleColor;
    private TextureInfo textureInfo;
    protected RectangleF _sourceRectangle;

    public DrawableRectangle(EngineBase Context, float Width, float Height, Color color)
    {
      this.RectangleColor = color;
      this.Width = Width;
      this.Height = Height;
      this.textureInfo = Context.CreateTexture(color, Width, Height);
    }

    public override void OnDraw(ISpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.textureInfo, this.Position, this.SourceRectangle, this.RectangleColor, this.Rotation, this.Center, this.Scale, FlipDirection.None, 0.0f, this.Alpha);
    }

    public virtual RectangleF SourceRectangle
    {
      get
      {
        return this._sourceRectangle != null ? this._sourceRectangle : new RectangleF(0.0f, 0.0f, this.Width, this.Height);
      }
      set => this._sourceRectangle = value;
    }
  }
}
