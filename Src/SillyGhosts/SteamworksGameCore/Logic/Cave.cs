// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Logic.Cave
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Steamworks.Games.Game.Core.Logic
{
  public class Cave
  {
    public int _caveID;
    public float _x;
    public float _y;
    public int _waitX;
    [XmlArray]
    public List<PassangerInfo> PassangerInfos = new List<PassangerInfo>();
    public float _passangerHeight;
    public int _waterLevel;

    [XmlAttribute]
    public int CaveID
    {
      get => this._caveID;
      set => this._caveID = value;
    }

    [XmlAttribute]
    public float X
    {
      get => this._x;
      set => this._x = value;
    }

    [XmlAttribute]
    public float Y
    {
      get => this._y;
      set => this._y = value;
    }

    [XmlAttribute]
    public int WaitX
    {
      get => this._waitX;
      set => this._waitX = value;
    }

    [XmlIgnore]
    public Vector2 Position => new Vector2((double) this.X, (double) this.Y);

    [XmlIgnore]
    public float PassangerHeight
    {
      get => this._passangerHeight;
      set => this._passangerHeight = value;
    }

    [XmlIgnore]
    public int WaterLevel
    {
      get => this._waterLevel;
      set => this._waterLevel = value;
    }

    [XmlIgnore]
    public bool HasPassangers => this.PassangerInfos.Count != 0;

    public Passanger ProducePassangerIfNeeded(float totaltime_s)
    {
      Passanger passanger = (Passanger) null;
      if (this.PassangerInfos.Count > 0)
      {
        PassangerInfo passangerInfo = this.FindPassangerInfo((double) totaltime_s);
        if (passangerInfo != null)
        {
          passanger = new Passanger();
          passanger.TargetCaveID = passangerInfo.TargetCaveID;
          passanger.SourceCaveID = this.CaveID;
          passanger.TargetPosition = new Vector2((double) this.WaitX, (double) this.Y - (double) this.PassangerHeight);
          passanger.Position = new Vector2((double) this.X, (double) this.Y - (double) this.PassangerHeight);
          passanger.Size = new Vector2((double) this.PassangerHeight, (double) this.PassangerHeight);
          passanger.Mass = 80f;
          passanger.Gravity = 500f;
          passanger.WaterDensity = 0.03f;
          passanger.WaterDragDensityDown = 0.03f;
          passanger.WaterDragDensityDown = 0.3f;
          passanger.AirDensity = 0.01f;
          passanger.WaterLevel = (float) this.WaterLevel;
          passanger.BoundingBox = new RectangleF(8f, 8f, 16f, 24f);
          passanger.StartTime = totaltime_s;
          passanger.BaseTravelTime = passangerInfo.BaseTravelTime;
          passanger.PassangerID = passangerInfo.PassangerID;
          this.PassangerInfos.RemoveAt(this.PassangerInfos.IndexOf(passangerInfo));
        }
      }
      return passanger;
    }

    private PassangerInfo FindPassangerInfo(double totaltime_ms)
    {
      PassangerInfo passangerInfo1 = (PassangerInfo) null;
      foreach (PassangerInfo passangerInfo2 in this.PassangerInfos)
      {
        if ((double) passangerInfo2.Time_ms < totaltime_ms)
          passangerInfo1 = passangerInfo2;
      }
      return passangerInfo1;
    }
  }
}
