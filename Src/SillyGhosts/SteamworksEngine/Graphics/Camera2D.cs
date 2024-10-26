// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.Camera2D
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework;
using Steamworks.Engine.Common;


namespace Steamworks.Engine.Graphics
{
  public class Camera2D : ICamera2D
  {
    public RectangleF Viewport;
    public RectangleF CameraBounds;
    public Microsoft.Xna.Framework.Vector2 Position;
    public Microsoft.Xna.Framework.Vector2 Origin;
    public float Zoom = 1f;
    public float Rotation;

    public Camera2D(RectangleF viewport, RectangleF CameraBounds)
    {
      this.Origin = new Microsoft.Xna.Framework.Vector2(viewport.Width / 2f, viewport.Height / 2f);
      this.Viewport = viewport;
      this.CameraBounds = CameraBounds;
    }

    public Matrix GetViewMatrix(float ParralaxX, float ParralaxY)
    {
      return Matrix.CreateTranslation(new Vector3(-this.Position * new Microsoft.Xna.Framework.Vector2(ParralaxX, ParralaxY), 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.Origin, 0.0f)) * Matrix.CreateRotationZ(this.Rotation) * Matrix.CreateScale(this.Zoom, this.Zoom, 1f) * Matrix.CreateTranslation(new Vector3(this.Origin, 0.0f));
    }

    public void LookAt(float X, float Y)
    {
      X = (float) (int) X;
      Y = (float) (int) Y;
      this.Position = new Microsoft.Xna.Framework.Vector2(X - this.Viewport.Width / 2f, Y - this.Viewport.Height / 2f);
      if ((double) this.Position.X < (double) this.CameraBounds.Left)
        this.Position.X = this.CameraBounds.Left;
      if ((double) this.Position.Y < (double) this.CameraBounds.Top)
        this.Position.Y = this.CameraBounds.Top;
      if ((double) this.Position.X + (double) this.Viewport.Width > (double) this.CameraBounds.Right)
        this.Position.X = this.CameraBounds.Right - this.Viewport.Width;
      if ((double) this.Position.Y + (double) this.Viewport.Height <= (double) this.CameraBounds.Bottom)
        return;
      this.Position.Y = this.CameraBounds.Bottom - this.Viewport.Height;
    }
  }
}
