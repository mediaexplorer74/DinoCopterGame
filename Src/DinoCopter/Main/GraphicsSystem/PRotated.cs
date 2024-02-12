// GameManager.GraphicsSystem.PRotated

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class PRotated
  {
    public Paintable Img { get; set; }

    public float Angle { get; set; }

    public float AxisX { get; set; }

    public float AxisY { get; set; }

    public void Draw(float x, float y, SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Img.GetTexture(), new Vector2(x, y), new Rectangle?(), Color.White, this.Angle, new Vector2(this.AxisX, this.AxisY), 1f, SpriteEffects.None, 0.0f);
    }
  }
}
