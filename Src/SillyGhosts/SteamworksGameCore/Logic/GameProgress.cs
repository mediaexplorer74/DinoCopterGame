// Steamworks.Games.Game.Core.Logic.GameProgress

using Steamworks.Games.Game.Core.Interfaces;


namespace Steamworks.Games.Game.Core.Logic
{
  public class GameProgress
  {
    private const int _difficultiesCount = 4;
    private const string _gameProgressFile = "GameProgress";
    private const string _tutorialPlayedFile = "Tutorial";
    private const int _tutorialByteCount = 3;
    private byte[] _tutorialBytes = new byte[3];
    private const int _levelCount = 20;
    private const int _saveByteCount = 80;
    private int[][] _scores = new int[4][];
    private IPersister _persister;

    public GameProgress(IPersister persister)
    {
      this._persister = persister;
      this.LoadScores();
      this.LoadTutorial();
    }

    public void HintShowed(Hint hint)
    {
      this._tutorialBytes[(int) hint] = (byte) 1;
      this.SaveTutorial();
    }

    public bool WasHintShowed(Hint hint) => this._tutorialBytes[(int) hint] == (byte) 1;

    private void LoadTutorial() => this._tutorialBytes = this._persister.Load("Tutorial", 3);

    private void SaveTutorial() => this._persister.Save(this._tutorialBytes, 3, "Tutorial");

    public int GetLevelScore(int LevelID, int GameDifficultyNameID)
    {
      return this._scores[GameDifficultyNameID][LevelID] == (int) this._persister.DefaultValue
                ? 0 
                : this._scores[GameDifficultyNameID][LevelID];
    }

    public void SetLevelScore(int LevelID, int GameDifficultyNameID, int Score)
    {
      this._scores[GameDifficultyNameID][LevelID] = (int) (byte) Score;
      this.SaveScores();
    }

    public bool IsLevelLocked(int LevelID, int GameDifficultyNameID)
    {
      return LevelID != 0 && this.GetLevelScore(LevelID - 1, GameDifficultyNameID) <= 0;
    }

    private void LoadScores()
    {
      byte[] numArray = this._persister.Load(nameof (GameProgress), 80);
      for (int index1 = 0; index1 < 4; ++index1)
      {
        this._scores[index1] = new int[20];
        if (numArray.Length != 0)
        {
          for (int index2 = 0; index2 < 20; ++index2)
            this._scores[index1][index2] = (int) numArray[index1 * 20 + index2];
        }
      }
    }

    private void SaveScores()
    {
      byte[] bytesToSave = new byte[80];
      for (int index1 = 0; index1 < 4; ++index1)
      {
        for (int index2 = 0; index2 < 20; ++index2)
          bytesToSave[index1 * 20 + index2] = (byte) this._scores[index1][index2];
      }
      this._persister.Save(bytesToSave, 80, nameof (GameProgress));
    }
  }
}
