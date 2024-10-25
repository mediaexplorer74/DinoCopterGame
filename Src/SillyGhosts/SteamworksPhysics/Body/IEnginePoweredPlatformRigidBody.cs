// Decompiled with JetBrains decompiler
// Type: Steamworks.Physics.Body.IEnginePoweredPlatformRigidBody
// Assembly: Steamworks.Physics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4471951E-A007-4E28-848D-F46F3B05AC18
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Physics.dll

#nullable disable
namespace Steamworks.Physics.Body
{
  public interface IEnginePoweredPlatformRigidBody : IRigidBody
  {
    bool IsEngineRunning { get; }
  }
}
