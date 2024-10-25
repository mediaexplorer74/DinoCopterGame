// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Body.PositionableMediator
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

using Steamworks.Engine.Common;

#nullable disable
namespace Steamworks.Physics.Body
{
  public class PositionableMediator
  {
    private IPositionable Source;
    private IPositionable Target;

    public PositionableMediator(IPositionable source, IPositionable target)
    {
      this.Source = source;
      this.Target = target;
    }

    public void UpdateTarget()
    {
      this.Target.X = this.Source.X;
      this.Target.Y = this.Source.Y;
      this.Target.Rotation = this.Source.Rotation;
    }
  }
}
