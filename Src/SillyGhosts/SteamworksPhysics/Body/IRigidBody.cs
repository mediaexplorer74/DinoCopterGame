// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Body.IRigidBody
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;
using Steamworks.Physics.Collisions;


namespace Steamworks.Physics.Body
{
  public interface IRigidBody
  {
    ICollisionDetector CurrentCollisionDetector { get; set; }

    RectangleF Bounds { get; }

    bool IsInWater { get; set; }

    bool FellInWater { get; set; }

    bool IsOnGroundOrWater { get; set; }

    bool CheckForPlatformCollisions { get; set; }

    bool IsCrashed { get; set; }

    bool Bumped { get; set; }

    RectangleF BoundingBox { get; set; }
  }
}
