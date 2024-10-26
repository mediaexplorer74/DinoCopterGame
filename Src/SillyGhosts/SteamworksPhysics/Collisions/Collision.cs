// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Collisions.Collision
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;


namespace Steamworks.Physics.Collisions
{
  public struct Collision
  {
    public CollisionDirection Direction;
    public Vector2 Intersection;

    public static Collision None
    {
      get
      {
        return new Collision()
        {
          Intersection = Vector2.Zero,
          Direction = CollisionDirection.None
        };
      }
    }

    public override bool Equals(object obj)
    {
      return obj is Collision collision && collision.Intersection.Equals((object) this.Intersection) && ((Collision) obj).Direction == this.Direction;
    }
  }
}
