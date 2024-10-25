// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Sprites.BackgroundNear
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Graphics;
using Steamworks.Physics.Particles;

#nullable disable
namespace Steamworks.Games.Game.Core.Sprites
{
  public class BackgroundNear : Entity
  {
    private EngineBase _context;
    private CloudParticleSystem _cloudsSystem;
    private CloudParticleSystem _cloudsSystem2;
    private CloudParticleSystem _cloudsSystem3;
    protected IEntity _background_near;

    public BackgroundNear(EngineBase context, float width, float height, float parralax)
    {
      this._context = context;
      this._background_near = context.SpriteFactory.Get("background_close");
      this._background_near.Y = (float) ((double) height - (double) this._background_near.Height + 524.0) * parralax;
      this._background_near.X = (width - this._background_near.Width) * parralax;
      this.AttachChild(this._background_near);
      this._cloudsSystem = new CloudParticleSystem(width, height / 3f, context.ResourceManagers.CurrentTextureManager.Get("cloudsmall1"), 3);
      this._cloudsSystem2 = new CloudParticleSystem(width, height / 3f, context.ResourceManagers.CurrentTextureManager.Get("cloudsmall2"), 3);
      this._cloudsSystem3 = new CloudParticleSystem(width, height / 3f, context.ResourceManagers.CurrentTextureManager.Get("cloudsmall3"), 3);
    }

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      base.Update(elapsedTime_s, totalTime_s);
      this._cloudsSystem.AddParticles(elapsedTime_s);
      this._cloudsSystem.Update(elapsedTime_s, totalTime_s);
      this._cloudsSystem2.AddParticles(elapsedTime_s);
      this._cloudsSystem2.Update(elapsedTime_s, totalTime_s);
      this._cloudsSystem3.AddParticles(elapsedTime_s);
      this._cloudsSystem3.Update(elapsedTime_s, totalTime_s);
    }

    public override void Draw(ISpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this._cloudsSystem.Draw(spriteBatch);
      this._cloudsSystem2.Draw(spriteBatch);
      this._cloudsSystem3.Draw(spriteBatch);
    }
  }
}
