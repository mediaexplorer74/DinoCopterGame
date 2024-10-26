// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.Sprite
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class Sprite : Entity
  {
    private TextureInfo _textureInfo;
    private Color _color = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
    protected RectangleF _sourceRectangle;

    public TextureInfo SpriteTextureInfo
    {
      get => this._textureInfo;
      set
      {
        this._textureInfo = value;
        if (value.Texture == null)
          return;
        if (!this.WidthSet)
          this.Width = (float) value.Texture.Width * this.Scale;
        if (this.HeightSet)
          return;
        this.Height = (float) value.Texture.Height * this.Scale;
      }
    }

    public Color EntityColor
    {
      get => this._color;
      set => this._color = value;
    }

    public Sprite()
    {
    }

    ~Sprite() => this.Dispose(false);

    public Sprite(TextureInfo textureInfo)
    {
      this.SpriteTextureInfo = textureInfo;
      this.Width = (float) textureInfo.SizeX;
      this.Height = (float) textureInfo.SizeY;
    }

    public override void OnDraw(ISpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.SpriteTextureInfo, this.Position.Plus(this.Center), this.SourceRectangle, this.EntityColor, this.Rotation, this.Center, this.Scale, this.Flip, 0.0f, this.Alpha);
    }

    public virtual RectangleF SourceRectangle
    {
      get
      {
        return this._sourceRectangle != null ? this._sourceRectangle : this.SpriteTextureInfo.SourceRectangle;
      }
      set => this._sourceRectangle = value;
    }
  }
}
