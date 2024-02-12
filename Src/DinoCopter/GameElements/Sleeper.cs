// GameManager.GameElements.Sleeper

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace GameManager.GameElements
{
  public class Sleeper : Sprite
  {
    public static SoundEffectInstance SfxSnoringInstance;

    public bool Right { get; private set; }

    public Sprite Wind { get; private set; }

    public SoundSource Snore { get; private set; }

    private Sleeper(Point pos, bool right)
      : base(5, pos)
    {
      Sleeper.SfxSnoringInstance = GlobalMembers.SfxSnoring.CreateInstance();
      Sleeper.SfxSnoringInstance.Volume = 0.0f;
      Sleeper.SfxSnoringInstance.IsLooped = true;
      Sleeper.SfxSnoringInstance.Play();
      this.Right = right;
      this.SetPaintable(GlobalMembers.Game.SleeperInAnim);
      this.Snore = new SoundSource(Sleeper.SfxSnoringInstance, new Point(this.Pos + this.GetSize() / 2f), 1f, 4f, 8f);
    }

    public virtual void SetAnim()
    {
      this.Wind.SetTypeId(Convert.ToBoolean((int) ((double) GlobalMembers.Game.GetGameTime() / 3.0) % 2) == !this.Right ? 100 : 101);
      this.SetPaintable(Paintable.Copy((int) ((double) GlobalMembers.Game.GetGameTime() / 3.0) % 2 == 0 ? GlobalMembers.Game.SleeperInAnim : GlobalMembers.Game.SleeperOutAnim));
      this.Img.SetAnimationDuration(3f);
    }

    public static Sprite CreateSleeper(Point pos, bool right)
    {
      Sleeper sleeper1 = new Sleeper(pos, right);
      Sprite sleeper2 = (Sprite) sleeper1;
      sleeper1.Ref = sleeper2;
      return sleeper2;
    }

    public override void Update(float time)
    {
      base.Update(time);
      if ((int) ((double) GlobalMembers.Game.GetGameTime() / 3.0) == (int) (((double) GlobalMembers.Game.GetGameTime() - (double) time) / 3.0))
        return;
      this.SetAnim();
    }

    public override void OnCollision(Sprite s)
    {
    }

    public override void OnAdd()
    {
      this.Wind = Sprite.CreateSprite(this.Right ? 101 : 100, this.GetPos() + new Point(this.GetWidth(), 0.0f));
      this.Parent.AddSprite(this.Wind, 7);
      this.Wind.SetPaintable(Paintable.CreateFilledRect(GlobalMembers.TileSize.X * 3f, GlobalMembers.TileSize.Y * 2f, Util.ConvertIntToColor(0U)));
      if (!this.Right)
        this.Wind.SetPos(this.GetPos() + new Point(-this.Wind.GetWidth(), 0.0f));
      this.SetAnim();
      GlobalMembers.Game.SoundSources.Add(this.Snore);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      this.Img.Mirror = this.Right;
      base.Render(spriteBatch);
      this.Img.Mirror = false;
      GlobalMembers.Game.EnterTutorial(6);
    }

    public override void OnRemove()
    {
      GlobalMembers.Game.SoundSources.Remove(this.Snore);
      Sleeper.SfxSnoringInstance.Stop();
    }
  }
}
