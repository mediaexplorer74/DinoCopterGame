// GameManager.GraphicsSystem.BMFont

using GameManager.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class BMFont
  {
    private readonly int[] _charMapping;
    private readonly int _maxCharId;
    private readonly SpriteFont _spriteFont;
    private int[][] _bmfChars;
    public Paintable Img = new Paintable();

    public BMFont(SpriteFont spriteFont)
    {
      this._spriteFont = spriteFont;
      this.FontColor = Microsoft.Xna.Framework.Color.White;
      this._maxCharId = 0;
      this._charMapping = new int[this._maxCharId + 1];
      for (int index = 0; index < this._maxCharId + 1; ++index)
        this._charMapping[index] = -1;
      for (int index = 0; index < 0; ++index)
        this._charMapping[this._bmfChars[index][0]] = index;
    }

    public Microsoft.Xna.Framework.Color FontColor { get; set; }

    public void SetColor(float r, float g, float b, float a)
    {
      this.FontColor = new Microsoft.Xna.Framework.Color(r, g, b, a);
    }

    public float GetCharWidth(char c)
    {
      return this._spriteFont.MeasureString(string.Concat((object) c)).X;
    }

    public float GetStringWidth(string s) => this._spriteFont.MeasureString(s).X;

    public float GetHeight() => this._spriteFont.MeasureString("abc").Y;

    public Vector2 GetStringMeasure(string s) => this._spriteFont.MeasureString(s);

    public void Write(string s, float x, float y, int anchor, SpriteBatch spriteBatch)
    {
      x += GlobalMembers.Manager.TranslatePos.X;
      y += GlobalMembers.Manager.TranslatePos.Y;
      if ((32 & anchor) == 32)
        x -= this.GetStringWidth(s);
      if ((16 & anchor) == 16)
        x -= this.GetStringWidth(s) / 2f;
      if ((4 & anchor) == 4)
        y -= this.GetHeight() - 1f;
      if ((2 & anchor) == 2)
        y -= this.GetHeight() / 2f;
      x = (float) (int) ((double) x + 0.5);
      y = (float) (int) ((double) y + 0.5);

      spriteBatch.DrawString(this._spriteFont, s, 
          new Vector2(x, GlobalMembers.ScreenHeight - y - this.GetStringMeasure(s).Y),
          this.FontColor);
    }
  }
}
