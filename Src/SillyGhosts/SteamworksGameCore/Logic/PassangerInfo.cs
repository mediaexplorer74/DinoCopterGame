// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.PassangerInfo
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Games.Game.Core.Logic
{
  public class PassangerInfo
  {
    [XmlAttribute]
    public int PassangerType;
    [XmlAttribute]
    public float Time_ms;
    [XmlAttribute]
    public int TargetCaveID;
    [XmlAttribute]
    public float BaseTravelTime;
    [XmlAttribute]
    public int PassangerID;
  }
}
