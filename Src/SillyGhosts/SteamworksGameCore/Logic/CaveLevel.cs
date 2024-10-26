// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.CaveLevel
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Shared.Map;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Steamworks.Games.Game.Core.Logic
{
  public class CaveLevel
  {
    [XmlAttribute]
    public int Index;
    [XmlAttribute]
    public string Name;
    [XmlArray]
    public List<Cave> Caves;
    [XmlAttribute]
    public int StartX;
    [XmlAttribute]
    public int StartY;
    [XmlAttribute]
    public bool Rain;
    [XmlAttribute]
    public bool Ice;
    [XmlAttribute]
    public bool Wind;
    [XmlAttribute]
    public bool TestOnly;
    [XmlIgnore]
    public TMXMapData CaveMap;
  }
}
