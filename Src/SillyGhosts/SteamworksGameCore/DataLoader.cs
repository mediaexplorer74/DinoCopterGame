// Decompiled with JetBrains decompiler
// Type: steamworks.games.game.core.DataLoader
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Graphics;
using Steamworks.Engine.Sound;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Games.Game.Core.Logic;
using Steamworks.Shared.Map;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace steamworks.games.game.core
{
  public class DataLoader : ICaveCabDataLoader, IDataLoader
  {
    private IDataUtils DataUtils;

    public DataLoader(IDataUtils dataUtils) => this.DataUtils = dataUtils;

    public TextureInfo[] LoadTextures()
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (TextureInfo[]));
      Stream stream1 = this.DataUtils.OpenFile("assets1\\sprites.xml");
      Stream stream2 = stream1;
      TextureInfo[] textureInfoArray = (TextureInfo[]) xmlSerializer.Deserialize(stream2);
      stream1.Flush(); 
      stream1.Dispose();//Close();
      return textureInfoArray;
    }

    public int GetLevelsCount() => 20;

    private string GetLevelName(int CurrentLevelIndex)
    {
      return "level_" + CurrentLevelIndex.ToString() + ".cav";
    }

    private string GetMapName(int CurrentLevelIndex)
    {
      return "level_" + CurrentLevelIndex.ToString() + ".tmx";
    }

    public CaveLevel LoadLevel(int levelNumber, GameDifficulty CurrentDifficulty)
    {
      CaveLevel Level = new XmlSerializer(typeof (CaveLevel))
                .Deserialize(this.DataUtils.OpenFile(
                    "assets1\\levels\\" + this.GetLevelName(levelNumber))) as CaveLevel;
      string mapName = this.GetMapName(levelNumber);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (TMXMapData));
      Level.CaveMap = xmlSerializer.Deserialize(
          this.DataUtils.OpenFile(
          "assets1\\maps\\" + mapName)) as TMXMapData;
      CurrentDifficulty.ChangeTimes(Level);
      return Level;
    }

    public SoundInfo[] LoadSounds()
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (SoundInfo[]));
      Stream stream1 = this.DataUtils.OpenFile("assets1\\sounds.xml");
      Stream stream2 = stream1;
      SoundInfo[] soundInfoArray = (SoundInfo[]) xmlSerializer.Deserialize(stream2);
      stream1.Flush();
      stream1.Dispose();//Close();
      return soundInfoArray;
    }

    public FontInfo[] LoadFonts()
    {
      FontInfo[] fontInfoArray = new FontInfo[3];
      FontInfo fontInfo1 = new FontInfo();
      fontInfo1.Name = "smallfont";
      fontInfoArray[0] = fontInfo1;
      FontInfo fontInfo2 = new FontInfo();
      fontInfo2.Name = "mediumfont";
      fontInfoArray[1] = fontInfo2;
      FontInfo fontInfo3 = new FontInfo();
      fontInfo3.Name = "bigfont";
      fontInfoArray[2] = fontInfo3;
      return fontInfoArray;
    }
  }
}
