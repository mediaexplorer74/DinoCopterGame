// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.IEntity
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Steamworks.Engine.Common;

#nullable disable
namespace Steamworks.Engine.Graphics
{
  public interface IEntity : IUpdateable, IPositionable
  {
    void Draw(ISpriteBatch spriteBatch);

    void FadeAlpha(float from, float to, float totaltime_s);

    void FadeIn(float totaltime_s);

    void FadeOut(float totaltime_s);

    bool IsTouched(Vector2 Position);

    void AttachChild(IEntity child);

    void DetachChild(IEntity child);

    bool AlphaFadeComplete { get; }

    Vector2 Center { get; set; }

    FlipDirection Flip { get; set; }

    float Alpha { get; set; }

    object Tag { get; set; }

    bool HasParent { get; }

    void SetParent(IEntity entity);

    float Scale { get; set; }

    void CenterX(float ScreenWidth);
  }
}
