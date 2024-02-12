// GameManager.GameLogic.GameSerialized

using GameManager.GameElements;
using GameManager.GraphicsSystem;
using GameManager.Utils;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GameLogic
{
  public class GameSerialized
  {
    public static bool IsSerialized;
    public int LostReason;
    public bool FirstCrash;

    public bool FirstLoad { get; set; }

    public bool HasLose { get; set; }

    public bool HasWon { get; set; }

    public int PassengersDelivered { get; set; }

    public int PassengersToDeliver { get; set; }

    public float Energy { get; set; }

    public float GameTime { get; set; }

    public float WaterLevel { get; set; }

    public float WaterLevelRise { get; set; }

    public float LoseTime { get; set; }

    public Point MoveDir { get; set; }

    public int Stones { get; set; }

    public int Level { get; set; }

    public bool DropStone { get; set; }

    public List<Point> StonePos { get; set; }

    public List<bool> StoneOnGround { get; set; }

    public List<GlobalMembers.StoneState> StoneState { get; set; }

    public List<Point> FruitPos { get; set; }

    public List<bool> FruitOnGround { get; set; }

    public List<int> FruitType { get; set; }

    public Point PlayerPos { get; set; }

    public float CopterAnimPhase { get; set; }

    public float WhirlAnimPhase { get; set; }

    public bool OnGround { get; set; }

    public float WaterFloatEnergyStart { get; set; }

    public float WaterFloatStart { get; set; }

    public bool FloatsOnWater { get; set; }

    public int PassengersIn { get; set; }

    public int MaxPassengers { get; set; }

    public List<GlobalMembers.ChaserState> ChaserState { get; set; }

    public List<float> ChaserStateChangeTime { get; set; }

    public List<int> ChaserWereHit { get; set; }

    public List<bool> ThereIsPassenger { get; set; }

    public List<Point> PassengerPos { get; set; }

    public List<GlobalMembers.PassengerState> PassengerState { get; set; }

    public List<float> StateChangeTime { get; set; }

    public List<int> TargetStation { get; set; }

    public List<int> FromStation { get; set; }

    public List<int> PassengerType { get; set; }

    public List<float> FloatStart { get; set; }

    public List<bool> IsPassangerAlive { get; set; }

    public List<int> PassengersSpawned { get; set; }

    public List<int> MaxPassengersSpawned { get; set; }

    public GameSerialized()
    {
    }

    public GameSerialized(Game currentGame)
    {
      this.GameTime = currentGame.GameTime;
      this.FirstLoad = currentGame.FirstLoad;
      this.HasLose = currentGame.HasLose;
      this.HasWon = currentGame.HasWon;
      this.PassengersDelivered = currentGame.PassengersDelivered;
      this.PassengersToDeliver = currentGame.PassengersToDeliver;
      this.Level = currentGame.Level;
      this.Energy = currentGame.Energy;
      this.GameTime = currentGame.GameTime;
      this.WaterLevel = currentGame.WaterLevel;
      this.WaterLevelRise = currentGame.WaterLevelRise;
      this.LoseTime = currentGame.LoseTime;
      this.MoveDir = currentGame.MoveDir;
      this.Stones = currentGame.Stones;
      this.Level = currentGame.Level;
      this.DropStone = currentGame.DropStone;
      this.LostReason = currentGame.LostReason;
      int count = currentGame.GetSpriteLayer(2).Count;
      this.StonePos = new List<Point>(count);
      this.StoneOnGround = new List<bool>(count);
      this.StoneState = new List<GlobalMembers.StoneState>(count);
      this.FruitPos = new List<Point>(count);
      this.FruitOnGround = new List<bool>(count);
      this.FruitType = new List<int>(count);
      foreach (Sprite sprite in currentGame.GetSpriteLayer(2))
      {
        if (sprite.TypeId == 6)
        {
          this.StonePos.Add(sprite.GetPos());
          this.StoneOnGround.Add((sprite as Stone).OnGround);
          this.StoneState.Add((sprite as Stone).State);
        }
        if (sprite.TypeId == 8)
        {
          this.FruitPos.Add(sprite.GetPos());
          this.FruitOnGround.Add((sprite as Fruit).OnGround);
          this.FruitType.Add((sprite as Fruit).Type);
        }
      }
      this.ChaserState = new List<GlobalMembers.ChaserState>();
      this.ChaserStateChangeTime = new List<float>();
      this.ChaserWereHit = new List<int>();
      foreach (Sprite sprite in currentGame.GetSpriteLayer(3))
      {
        if (sprite.TypeId == 3)
        {
          Chaser chaser = sprite as Chaser;
          this.ChaserState.Add(chaser.State);
          this.ChaserStateChangeTime.Add(chaser.StateChangeTime);
          this.ChaserWereHit.Add(chaser.WereHit);
        }
      }
      this.PassengerPos = new List<Point>(10);
      this.PassengerState = new List<GlobalMembers.PassengerState>(10);
      this.StateChangeTime = new List<float>(10);
      this.TargetStation = new List<int>(10);
      this.FromStation = new List<int>(10);
      this.PassengerType = new List<int>(10);
      this.FloatStart = new List<float>(10);
      this.IsPassangerAlive = new List<bool>(10);
      this.PassengersSpawned = new List<int>(10);
      this.MaxPassengersSpawned = new List<int>(10);
      this.ThereIsPassenger = new List<bool>(10);
      for (int index = 0; index < 10; ++index)
      {
        if (currentGame.Spawns[index].Passenger != null)
        {
          this.ThereIsPassenger.Insert(index, true);
          this.IsPassangerAlive.Insert(index, currentGame.Spawns[index].IsPassangerAlive);
          this.PassengersSpawned.Insert(index, currentGame.Spawns[index].PassengersSpawned);
          this.MaxPassengersSpawned.Insert(index, currentGame.Spawns[index].MaxPassengersSpawned);
          this.PassengerPos.Insert(index, currentGame.Spawns[index].Passenger.GetPos());
          this.PassengerState.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).State);
          this.StateChangeTime.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).StateChangeTime);
          this.TargetStation.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).TargetStation);
          this.FromStation.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).FromStation);
          this.PassengerType.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).PassengerType);
          this.FloatStart.Insert(index, (currentGame.Spawns[index].Passenger as Passenger).FloatStart);
        }
        else
          this.ThereIsPassenger.Insert(index, false);
      }
      this.PlayerPos = currentGame.Player.GetPos();
      Player player = currentGame.Player as Player;
      this.FirstCrash = player.FirstCrash;
      this.CopterAnimPhase = player.CopterAnimPhase;
      this.WhirlAnimPhase = player.WhirlAnimPhase;
      this.OnGround = player.OnGround;
      this.WaterFloatEnergyStart = player.WaterFloatEnergyStart;
      this.WaterFloatStart = player.WaterFloatStart;
      this.FloatsOnWater = player.FloatsOnWater;
      this.PassengersIn = player.PassengersIn;
      this.MaxPassengers = player.MaxPassengers;
    }
  }
}
