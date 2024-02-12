// GameManager.Spawn

using GameManager.GraphicsSystem;
using GameManager.Utils;

#nullable disable
namespace GameManager
{
  public class Spawn
  {
    public Point Pos { get; set; }

    public Point Platform { get; set; }

    public int Station { get; set; }

    public int Right { get; set; }

    public int Left { get; set; }

    public Game1 Game { get; set; }

    public Sprite Passenger { get; set; }

    public float DisappearTime { get; set; }

    public bool IsPassangerAlive { get; set; }

    public int PassengersSpawned { get; set; }

    public int MaxPassengersSpawned { get; set; }

    public Spawn() => this.Game = (Game1) null;

    public Spawn(Point pos, Game1 game)
    {
      this.Pos = pos;
      this.Game = game;
      this.DisappearTime = -15f;
      this.IsPassangerAlive = false;
      this.MaxPassengersSpawned = -1;
      this.PassengersSpawned = 0;
      this.Left = (int) pos.X;
      this.Right = (int) pos.X;
      Point platformSize = this.Game.GetPlatformSize((int) pos.X, (int) pos.Y);
      this.Left = (int) platformSize.X;
      this.Right = (int) platformSize.Y;
      for (int left = this.Left; left <= this.Right; ++left)
      {
        if (this.Game.GetPlatformId((int) game.Fg[left][(int) pos.Y]) >= 0)
          this.Station = game.GetPlatformId((int) game.Fg[left][(int) pos.Y]);
        if (this.Game.IsLandindPlatform((int) this.Game.Fg[left][(int) pos.Y - 1]))
          this.Platform = new Point((float) left, pos.Y - 1f);
      }
    }

    public void Update(float elapsedTime)
    {
      if (this.Game == null)
        return;
      this.spawn();
    }

    public int GetStation() => this.Game == null ? -1 : this.Station;

    public Point GetPlatform() => this.Game == null ? new Point() : new Point(this.Platform);

    public Point GetPos() => this.Game == null ? new Point() : new Point(this.Pos);

    public int GetLeft() => this.Left;

    public int GetRight() => this.Right;

    public Sprite GetPassenger() => this.Passenger;

    public bool IsOnStation(Sprite s)
    {
      if (this.Game == null)
        return false;
      Player player = s as Player;
      return player.IsOnGround() && (double) player.GetForce().Len() < 0.004999999888241291 && (double) (int) s.GetPos().Y == (double) this.Platform.Y + 1.0 && (double) s.GetPos().X + (double) s.GetWidth() / 2.0 >= (double) this.Left && (double) s.GetPos().X + (double) s.GetWidth() / 2.0 <= (double) (this.Right + 1);
    }

    public void SetMaxPassengersSpawned(int value) => this.MaxPassengersSpawned = value;

    public bool spawn()
    {
      bool flag = !(this.Passenger is GameManager.Passenger passenger) || passenger.GetState() == GlobalMembers.PassengerState.PassengerStateHide || passenger.GetState() == GlobalMembers.PassengerState.PassengerStateSink;
      if (flag && this.IsPassangerAlive)
        this.DisappearTime = this.Game.GetGameTime();
      this.IsPassangerAlive = !flag;
      if (!this.IsPassangerAlive && (double) this.Game.GetGameTime() - (double) this.DisappearTime > 15.0 && (double) this.Station * 5.0 < (double) this.Game.GetGameTime() && (this.MaxPassengersSpawned < 0 || this.PassengersSpawned < this.MaxPassengersSpawned) && (double) this.Pos.Y > (double) this.Game.GetWaterLevel() + 0.5)
      {
        this.Passenger = GameManager.Passenger.CreatePassenger(new Point(this.Pos.X, this.Pos.Y), Util.Random.Next(3), this.Station);
        this.Game.AddSprite(this.Passenger, 4);
        ++this.PassengersSpawned;
      }
      return false;
    }

    public bool CanTakePassenger()
    {
      return this.Game != null && (double) this.Pos.Y > (double) this.Game.GetWaterLevel() + 0.5;
    }
  }
}
