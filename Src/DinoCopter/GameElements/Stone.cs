// GameManager.Stone

using GameManager.GameLogic;
using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager
{
  internal class Stone : Sprite
  {
    public bool OnGround { get; set; }

    public GlobalMembers.StoneState State { get; set; }

    private Stone(Point pos)
      : base(6, pos)
    {
      this.OnGround = false;
      this.State = GlobalMembers.StoneState.StoneStateNormal;
      this.SetPaintable(GlobalMembers.Game.StoneAnim);
    }

    public static Sprite CreateStone(Point pos)
    {
      Stone stone1 = new Stone(pos);
      Sprite stone2 = (Sprite) stone1;
      stone1.Ref = stone2;
      return stone2;
    }

    public override void OnAdd()
    {
    }

    public override void OnCollision(Sprite s)
    {
      if (s.GetTypeId() == 1 && this.State != GlobalMembers.StoneState.StoneStateInChopper)
      {
        Player player = (Player) GlobalMembers.Game.GetPlayer();
        if (player.IsOnGround() && this.OnGround && player.AcceptMorePassengers())
        {
          player.PassengerIn();
          this.State = GlobalMembers.StoneState.StoneStateInChopper;
          GlobalMembers.Game.IncreaseStones();
        }
      }
      if (s.GetTypeId() == -2 || s.GetTypeId() == 10)
      {
        Collision collision = Collision.CollisionRectangleRectangle(this.GetPos(), this.GetPos() + this.GetSize(), s.GetPos(), s.GetPos() + s.GetSize(), this.GetPos() - this.GetOldPos() - s.GetPos() + s.GetOldPos());
        if ((double) this.Speed.Dot(collision.Normal) < 0.0)
        {
          this.Speed = new Point();
          Point point = this.GetPos() - this.GetOldPos();
          this.SetPos(this.GetPos() - collision.Normal * point.Dot(collision.Normal));
        }
        if ((double) collision.Normal.Y > 0.0)
          this.OnGround = true;
      }
      if (this.OnGround || this.State != GlobalMembers.StoneState.StoneStateNormal)
        return;
      if (s.GetTypeId() == 3 && ((Chaser) s).OnStoneHit())
        this.Speed.Y *= -0.5f;
      if (s.GetTypeId() == 4 && ((Pterodactyl) s).OnStoneHit())
        this.Speed.Y *= -0.5f;
      if (s.GetTypeId() != 7 || !((Tree) s).OnStoneHit())
        return;
      this.Speed.Y *= -0.5f;
    }

    public override void Update(float time)
    {
      this.OnGround = false;
      if ((KeyHelper.KeysState[AbsKey.Ok] == GameManager.Utils.State.Down || GlobalMembers.Game.ShouldDropStone()) && this.State == GlobalMembers.StoneState.StoneStateInChopper)
      {
        ((Player) GlobalMembers.Game.GetPlayer()).PassengerOut();
        this.State = GlobalMembers.StoneState.StoneStateNormal;
        this.Speed = GlobalMembers.Game.GetPlayer().GetSpeed();
        GlobalMembers.Game.DecreaseStones();
      }
      if (this.State == GlobalMembers.StoneState.StoneStateNormal)
      {
        Stone stone = this;
        stone.Speed = stone.Speed + GlobalMembers.Game.GetGravity(this.Ref) * time;
        base.Update(time);
        this.CheckCollisions(1);
        this.CheckCollisions(5);
        this.CheckCollisions(3);
        this.CheckCollisions(2);
      }
      else
      {
        base.Update(time);
        Sprite player = GlobalMembers.Game.GetPlayer();
        this.SetPos(player.GetPos() + new Point((float) (((double) player.GetWidth() - (double) this.GetWidth()) / 2.0), 0.0f));
      }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
      if (this.State != GlobalMembers.StoneState.StoneStateNormal)
        return;
      base.Render(spriteBatch);
    }
  }
}
