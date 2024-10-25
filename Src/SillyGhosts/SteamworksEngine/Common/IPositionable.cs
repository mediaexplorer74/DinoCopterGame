// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.IPositionable
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

#nullable disable
namespace Steamworks.Engine.Common
{
  public interface IPositionable
  {
    float X { get; set; }

    float Y { get; set; }

    float Width { get; set; }

    float Height { get; set; }

    float Rotation { get; set; }
  }
}
