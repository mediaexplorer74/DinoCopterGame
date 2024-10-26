// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.GameDifficulty
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll


namespace Steamworks.Games.Game.Core.Logic
{
  public class GameDifficulty
  {
    public static GameDifficulty Easy = new GameDifficulty(2f, 0.7f, GameDifficultyNames.Easy);
    public static GameDifficulty Normal = new GameDifficulty(1.1f, 0.6f, GameDifficultyNames.Normal);
    public static GameDifficulty Hard = new GameDifficulty(0.7f, 0.4f, GameDifficultyNames.Hard);
    public static GameDifficulty Base = new GameDifficulty(1f, 0.5f, GameDifficultyNames.Base);
    public int LevelID;
    private float TravelDifficultyFactor;
    private float ShowDifficultyFactor;
    public GameDifficultyNames Name;

    public GameDifficulty(
      float TravelDifficultyFactor,
      float ShowDifficultyFactor,
      GameDifficultyNames Name)
    {
      this.TravelDifficultyFactor = TravelDifficultyFactor;
      this.ShowDifficultyFactor = ShowDifficultyFactor;
      this.Name = Name;
    }

    public float GetTravelTime(float baseTravelTime_s)
    {
      return this.TravelDifficultyFactor * baseTravelTime_s;
    }

    public float GetTravelTime3(float baseTravelTime_s) => baseTravelTime_s * 3f;

    public void ChangeTimes(CaveLevel Level)
    {
      foreach (Cave cave in Level.Caves)
      {
        foreach (PassangerInfo passangerInfo in cave.PassangerInfos)
        {
          passangerInfo.BaseTravelTime = this.GetTravelTime(passangerInfo.BaseTravelTime);
          passangerInfo.Time_ms = this.GetShowTime(passangerInfo.Time_ms);
        }
      }
    }

    private float GetShowTime(float time_ms) => time_ms * this.ShowDifficultyFactor;
  }
}
