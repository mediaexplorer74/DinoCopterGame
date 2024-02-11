// GameManager.GameLogic.GameSave

using GameManager.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;

#nullable disable
namespace GameManager.GameLogic
{
  public class GameSave
  {
    public List<float> Times = new List<float>();
    public bool[] Tutorial = new bool[8];
    private List<string> Buys;

    public float MusicVolume { get; private set; }

    public float SoundVolume { get; private set; }

    private void SaveSound()
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("volume.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.SoundVolume);
        binaryWriter.Write(this.MusicVolume);
        binaryWriter.Close();
        output.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
    }

    private void SaveMP()
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("mp.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.TapJoyPoints);
        binaryWriter.Write(this.TapJoyPointsReduction);
        int count = this.Buys.Count;
        binaryWriter.Write(count);
        for (int index = 0; index < count; ++index)
        {
          binaryWriter.Write(this.Buys[index].Length);
          binaryWriter.Write(this.Buys[index]);
        }
        binaryWriter.Close();
        output.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
    }

    private void Load()
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      if (!storeForApplication.FileExists("volume.sav"))
      {
        this.SoundVolume = 0.7f;
        this.MusicVolume = 0.5f;
        this.SaveSound();
      }
      try
      {
        IsolatedStorageFileStream input = storeForApplication.OpenFile("volume.sav", FileMode.OpenOrCreate);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        this.SoundVolume = binaryReader.ReadSingle();
        this.MusicVolume = binaryReader.ReadSingle();
        binaryReader.Close();
        input.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
      try
      {
        IsolatedStorageFileStream input = storeForApplication.OpenFile("tutorial.sav", FileMode.Open);
        if (input.Length > 0L)
        {
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          for (int index = 0; index < 8; ++index)
            this.Tutorial[index] = binaryReader.ReadBoolean();
          binaryReader.Close();
        }
        input.Close();
      }
      catch (FileNotFoundException ex)
      {
        storeForApplication.CreateFile("tutorial.sav");
      }
      catch (IsolatedStorageException ex)
      {
      }
      try
      {
        IsolatedStorageFileStream input = storeForApplication.OpenFile("times.sav", FileMode.Open);
        if (input.Length > 0L)
        {
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          int num1 = binaryReader.ReadInt32();
          for (int index = 0; index < this.Times.Count; ++index)
            this.Times[index] = 0.0f;
          for (int index = 0; index < num1; ++index)
          {
            float num2 = binaryReader.ReadSingle();
            this.Times.Insert(index, num2);
          }
          binaryReader.Close();
        }
        input.Close();
      }
      catch (FileNotFoundException ex)
      {
        storeForApplication.CreateFile("times.sav");
      }
      catch (IsolatedStorageException ex)
      {
      }
      try
      {
        if (storeForApplication.FileExists("unlocked.sav"))
        {
          IsolatedStorageFileStream input = storeForApplication.OpenFile("unlocked.sav", FileMode.Open);
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          this.LevelsUnlocked = binaryReader.ReadInt32();
          binaryReader.Close();
          input.Close();
        }
        else
          this.SetLevelsUnlocked(5);
      }
      catch (IsolatedStorageException ex)
      {
      }
      try
      {
        IsolatedStorageFileStream input = storeForApplication.OpenFile("mp.sav", FileMode.OpenOrCreate);
        if (input.Length > 0L)
        {
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          this.TapJoyPoints = binaryReader.ReadInt32();
          this.TapJoyPointsReduction = binaryReader.ReadInt32();
          int num = binaryReader.ReadInt32();
          for (int index = 0; index < num; ++index)
          {
            binaryReader.ReadInt32();
            this.Buys.Add(binaryReader.ReadString());
          }
          binaryReader.Close();
        }
        input.Close();
      }
      catch (FileNotFoundException ex)
      {
        storeForApplication.CreateFile("mp.sav");
      }
      catch (IsolatedStorageException ex)
      {
      }
    }

    public int TapJoyPoints { get; set; }

    public int TapJoyPointsReduction { get; set; }

    public int LevelsUnlocked { get; set; }

    public GameSave()
    {
      this.SoundVolume = 0.0f;
      this.MusicVolume = 0.0f;
      this.TapJoyPoints = 0;
      this.TapJoyPointsReduction = 0;
      this.Times = new List<float>(30);
      this.Buys = new List<string>();
      this.Tutorial = new bool[8];
      for (int index = 0; index < 8; ++index)
        this.Tutorial[index] = false;
      this.Load();
    }

    public bool HasBought(string productId)
    {
      return this.Buys.Count<string>((Func<string, bool>) (p => p == productId)) == 1;
    }

    public void AddBought(string productId)
    {
      if (this.HasBought(productId))
        return;
      this.Buys.Add(productId);
      this.SaveMP();
    }

    public void SetLevelsUnlocked(int _levelsUnlocked)
    {
      this.LevelsUnlocked = _levelsUnlocked;
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("unlocked.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.LevelsUnlocked);
        binaryWriter.Close();
        output.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
    }

    public void SetTime(int level, float time)
    {
      while (this.Times.Count <= level)
        this.Times.Add(0.0f);
      this.Times[level] = (double) this.Times[level] <= 0.0 ? time : Math.Min(time, this.Times[level]);
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("times.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        int count = this.Times.Count;
        binaryWriter.Write(count);
        for (int index = 0; index < count; ++index)
        {
          float time1 = this.Times[index];
          binaryWriter.Write(time1);
        }
        binaryWriter.Close();
        output.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
    }

    public void SetTapJoyPoints(int points)
    {
      if (points < this.TapJoyPoints)
        this.TapJoyPointsReduction += this.TapJoyPointsReduction - points;
      this.SpendPoints();
      this.TapJoyPoints = points;
      this.SaveMP();
    }

    public int GetTapJoyPoints() => this.TapJoyPoints;

    public void SpendPoints()
    {
    }

    public void RefreshTapJoyPoints()
    {
    }

    public void SetSoundVolume(float vol)
    {
      this.SoundVolume = vol;
      this.SaveSound();
    }

    public void SetMusicVolume(float vol)
    {
      this.MusicVolume = vol;
      this.SaveSound();
    }

    public int CurrentLevel()
    {
      int index = 0;
      while (index < this.Times.Count && (double) this.Times[index] > 0.0)
        ++index;
      return index;
    }

    public float GetTime(int level) => level >= this.Times.Count ? -1f : this.Times[level];

    public int GetStarsNum(int level, float time)
    {
      int starsNum = 1;
      if ((double) time < (double) GlobalMembers.GetTimeForStars(2, level))
        ++starsNum;
      if ((double) time < (double) GlobalMembers.GetTimeForStars(3, level))
        ++starsNum;
      return starsNum;
    }

    public float GetSoundVolume() => this.SoundVolume;

    public float GetMusicVolume() => this.MusicVolume;

    public bool WasTutorial(int i) => this.Tutorial[i];

    public void UsedTutorial(int i)
    {
      this.Tutorial[i] = true;
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("tutorial.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        for (int index = 0; index < 8; ++index)
          binaryWriter.Write(this.Tutorial[index]);
        binaryWriter.Close();
        output.Close();
      }
      catch (IsolatedStorageException ex)
      {
      }
    }
  }
}
