// GameManager.GameElements.Fruit

using GameManager.GameLogic;
using GameManager.GraphicsSystem;
using GameManager.Utils;

#nullable disable
namespace GameManager.GameElements
{
  internal class Fruit : Sprite
  {
    public bool OnGround { get; set; }

    public int Type { get; set; }

    private Fruit(Point pos)
      : base(8, pos)
    {
      this.Type = Util.Random.Next(5);
      this.SetPaintable(Paintable.Copy(GlobalMembers.Game.Food[this.Type]));
    }

    public static Sprite CreateFruit(Point pos)
    {
      Fruit fruit1 = new Fruit(pos);
      Sprite fruit2 = (Sprite) fruit1;
      fruit1.Ref = fruit2;
      return fruit2;
    }

    public override void OnCollision(Sprite s)
    {
      if (s.GetTypeId() != -2 && s.GetTypeId() != 10)
        return;
      Collision collision = Collision.CollisionRectangleRectangle(this.GetPos(), this.GetPos() + this.GetSize(), s.GetPos(), s.GetPos() + s.GetSize(), this.GetPos() - this.GetOldPos() - s.GetPos() + s.GetOldPos());
      if ((double) this.Speed.Dot(collision.Normal) < 0.0)
      {
        this.Speed = new Point();
        Point point = this.GetPos() - this.GetOldPos();
        this.SetPos(this.GetPos() - collision.Normal * point.Dot(collision.Normal));
      }
      if ((double) collision.Normal.Y <= 0.0)
        return;
      this.OnGround = true;
    }

    public override void Update(float time)
    {
      this.OnGround = false;
      Fruit fruit = this;
      fruit.Speed = fruit.Speed + GlobalMembers.Game.GetGravity(this.Ref) * time;
      base.Update(time);
      this.CheckCollisions(1);
    }
  }
}
