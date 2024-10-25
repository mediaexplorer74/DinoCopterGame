// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Common.MathUtils
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using System;

#nullable disable
namespace Steamworks.Engine.Common
{
  public static class MathUtils
  {
    public static double RAD_TO_DEG = 180.0 / Math.PI;
    public static double DEG_TO_RAD = Math.PI / 180.0;

    public static double radToDeg(double pRad) => MathUtils.RAD_TO_DEG * pRad;

    public static double DegToRad(double pDeg) => MathUtils.DEG_TO_RAD * pDeg;

    public static float NormalizeAngle(float angle)
    {
      angle %= 6.28318548f;
      return (double) angle < 0.0 ? angle + 6.28318548f : angle;
    }
  }
}
