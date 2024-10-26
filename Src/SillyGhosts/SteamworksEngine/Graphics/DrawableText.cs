// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.DrawableText
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class DrawableText : Entity, IDrawableText, IEntity, IUpdateable, IPositionable
  {
    public FontInfo Font;
    private string _text = "";
    private Color _textColor = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public DrawableText()
    {
    }

    public DrawableText(float X, float Y, FontInfo font)
    {
      this.X = X;
      this.Y = Y;
      this.Font = font;
    }

    public override void OnDraw(ISpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(this.Font, this.Text, this.Position, this.TextColor, this.Rotation, Vector2.Zero, this.Scale, FlipDirection.None, 0.0f, this.Alpha);
    }

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      base.Update(elapsedTime_s, totalTime_s);
    }

    public Color TextColor
    {
      get => this._textColor;
      set => this._textColor = value;
    }
  }
}
