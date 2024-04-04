// GameManager.GameLogic.GameSave

using GameManager.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;


// GameLogic namespace
#nullable disable
namespace GameManager.GameLogic
{

  // GameSave class
  public class GameSave
  {
    public List<float> Times = new List<float>();
    public bool[] Tutorial = new bool[8];
    private List<string> Buys;

    public float MusicVolume { get; private set; }

    public float SoundVolume { get; private set; }

    // SaveSound
    private void SaveSound()
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("volume.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.SoundVolume);
        binaryWriter.Write(this.MusicVolume);
        binaryWriter.Flush();//.Close();
        output.Flush();//.Close();
      }
      catch (IsolatedStorageException ex)
      {
        Debug.WriteLine("[ex] GameSave - SaveSound ex.: " + ex.Message);
      }
    }//GameSound

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
        binaryWriter.Dispose();//.Close();
        output.Dispose();//.Close();
      }
      catch (IsolatedStorageException ex)
      {
                Debug.WriteLine("[ex] GameSave - SaveMP ex.: " + ex.Message);
            }
    }

    // Load
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

                binaryReader.Dispose();//.Close();
                
                input.Dispose();//.Close();
      }
      catch (IsolatedStorageException ex)
      {
        Debug.WriteLine("[ex] GameSave - Load ex.: " + ex.Message);
      }
      try
      {
        IsolatedStorageFileStream input = storeForApplication.OpenFile("tutorial.sav", FileMode.Open);
        if (input.Length > 0L)
        {
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          for (int index = 0; index < 8; ++index)
            this.Tutorial[index] = binaryReader.ReadBoolean();
          
          binaryReader.Dispose();//.Close();
        }
        input.Dispose();//.Close();
      }
      catch (FileNotFoundException ex)
      {
        Debug.WriteLine("[i] GameSave - SaveSound - FileNotFound: " + ex.Message);
        storeForApplication.CreateFile("tutorial.sav");
      }
      catch (IsolatedStorageException ex2)
      {
         Debug.WriteLine("[ex] GameSave - SaveSound - IsolatedStorage ex.: " + ex2.Message);
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
                    binaryReader.Dispose();//.Close();
        }
        input.Dispose();//.Close();
      }
      catch (FileNotFoundException ex)
      {
        Debug.WriteLine("[i] GameSave - SaveSound - FileNotFoundException ex.: " + ex.Message);
        storeForApplication.CreateFile("times.sav");
      }
      catch (IsolatedStorageException ex2)
      {
         Debug.WriteLine("[ex] GameSave - SaveSound - IsolatedStorage ex.: " + ex2.Message);
      }
      
      try
      {
        if (storeForApplication.FileExists("unlocked.sav"))
        {
          IsolatedStorageFileStream input = storeForApplication.OpenFile("unlocked.sav", FileMode.Open);
          BinaryReader binaryReader = new BinaryReader((Stream) input);
          this.LevelsUnlocked = binaryReader.ReadInt32();
          binaryReader.Dispose();//.Close();
          input.Dispose();//.Close();
        }
        else
          this.SetLevelsUnlocked(5);
      }
      catch (IsolatedStorageException ex2)
      {
         Debug.WriteLine("[ex] GameSave - SaveSound - IsolatedStorage ex.: " + ex2.Message);
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
          binaryReader.Dispose();//.Close();
        }
        input.Dispose();//.Close();
      }
      catch (FileNotFoundException ex)
      {
        storeForApplication.CreateFile("mp.sav");
      }
      catch (IsolatedStorageException ex2)
      {
        Debug.WriteLine("[ex] GameSave - SaveSound - IsolatedStorage ex.: " + ex2.Message);
      }
    }//Load

    public int TapJoyPoints { get; set; }

    public int TapJoyPointsReduction { get; set; }

    public int LevelsUnlocked { get; set; }


    // GameSave constructor
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
    }//GameSave


    // HasBought
    public bool HasBought(string productId)
    {
      return this.Buys.Count<string>((Func<string, bool>) (p => p == productId)) == 1;
    }//HasBought


    // AddBought
    public void AddBought(string productId)
    {
      if (this.HasBought(productId))
        return;
      this.Buys.Add(productId);
      this.SaveMP();
    }//AddBought


    // SetLevelsUnlocked
    public void SetLevelsUnlocked(int _levelsUnlocked)
    {
      this.LevelsUnlocked = _levelsUnlocked;
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      try
      {
        IsolatedStorageFileStream output = storeForApplication.OpenFile("unlocked.sav", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        binaryWriter.Write(this.LevelsUnlocked);
        binaryWriter.Dispose();
        output.Dispose();
      }
      catch (IsolatedStorageException ex)
      {
        Debug.WriteLine("[ex] GameSave - SaveSound - IsolatedStorage ex.: " + ex.Message);
      }
    }//SetLevelsUnlocked


    // SetTime
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
        binaryWriter.Dispose();
        output.Dispose();
      }
      catch (IsolatedStorageException ex)
      {
        Debug.WriteLine("[ex] GameSave - SetTime - IsolatedStorage ex.: " + ex.Message);
      }
    }//SetTime


    // SetTapJoyPoints
    public void SetTapJoyPoints(int points)
    {
      if (points < this.TapJoyPoints)
        this.TapJoyPointsReduction += this.TapJoyPointsReduction - points;
      this.SpendPoints();
      this.TapJoyPoints = points;
      this.SaveMP();
    }//SetTapJoyPoints


    // GetTapJoyPoints
    public int GetTapJoyPoints()
    {
        return this.TapJoyPoints;
    }//GetTapJoyPoints

    // SpendPoints
    public void SpendPoints()
    {
       //
    }//SpendPoints


    // RefreshTapJoyPoints
    public void RefreshTapJoyPoints()
    {
        //
    }//RefreshTapJoyPoints


    // SetSoundVolume
    public void SetSoundVolume(float vol)
    {
      this.SoundVolume = vol;
      this.SaveSound();
    }//SetSoundVolume


    // SetMusicVolume
    public void SetMusicVolume(float vol)
    {
      this.MusicVolume = vol;
      this.SaveSound();
    }//SetMusicVolume

    // CurrentLevel
    public int CurrentLevel()
    {
      int index = 0;
      while (index < this.Times.Count && (double) this.Times[index] > 0.0)
        ++index;
      return index;
    }//CurrentLevel


    // GetTime
    public float GetTime(int level)
    {
        return level >= this.Times.Count
            ? -1f 
            : this.Times[level];
    }//GetTime


    // GetStarsNum
    public int GetStarsNum(int level, float time)
    {
      int starsNum = 1;
      if ((double) time < (double) GlobalMembers.GetTimeForStars(2, level))
        ++starsNum;
      if ((double) time < (double) GlobalMembers.GetTimeForStars(3, level))
        ++starsNum;
      return starsNum;
    }//GetStarsNum


    //GetSoundVolume
    public float GetSoundVolume()
    {
        return this.SoundVolume;
    }//GetSoundVolume

        public float GetMusicVolume()
    {
        return this.MusicVolume;
        }//GetMusicVolume

    // WasTutorial
    public bool WasTutorial(int i)
    {
        return this.Tutorial[i];
    }//WasTutorial


    // UsedTutorial
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
        binaryWriter.Dispose();
        output.Dispose();
      }
      catch (IsolatedStorageException ex)
      {
         Debug.WriteLine("[ex] UsedTutorial ex.: " + ex.Message);
      }
    }//UsedTutorial

  }//GameSave

}//GameManager.GameLogic
