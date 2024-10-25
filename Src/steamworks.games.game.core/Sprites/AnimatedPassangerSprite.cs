// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Sprites.AnimatedPassangerSprite
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Games.Game.Core.Logic;

#nullable disable
namespace Steamworks.Games.Game.Core.Sprites
{
  public class AnimatedPassangerSprite : AnimatedSprite
  {
    public IPassanger PassangerData;
    private PassangerAnimationState AnimationState;
    private PassangerState lastState;

    public AnimatedPassangerSprite(TextureInfo textureInfo, IPassanger pass)
      : base(textureInfo)
    {
      this.PlayLoop("idle");
      this.PassangerData = pass;
      this.lastState = PassangerState.Idle;
      this.AnimationState = PassangerAnimationState.Idle;
      this.UpdatePosition();
      this.Width = this.PassangerData.Width;
      this.Height = this.PassangerData.Height;
    }

    private void UpdatePosition()
    {
      this.X = this.PassangerData.X;
      this.Y = this.PassangerData.Y;
    }

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      base.Update(elapsedTime_s, totalTime_s);
      this.UpdatePosition();
      this.UpdateAnimation();
    }

    private void UpdateAnimation()
    {
      this.SetAnimationStateFromData();
      this.SetNextAnimationState();
      this.PlayAnimation();
    }

    private void SetAnimationStateFromData()
    {
      if (this.PassangerData.State == this.lastState)
        return;
      switch (this.PassangerData.State)
      {
        case PassangerState.Idle:
          this.Stop();
          break;
        case PassangerState.WalkingLeft:
        case PassangerState.WalkingRight:
          this.AnimationState = !this.IsFullTurn ? PassangerAnimationState.WalkingBegin : PassangerAnimationState.TurningBegin;
          break;
        case PassangerState.Falling:
          this.Stop();
          this.AnimationState = PassangerAnimationState.PreFall;
          break;
      }
      this.lastState = this.PassangerData.State;
    }

    private void SetNextAnimationState()
    {
      if (this.IsPlaying)
        return;
      switch (this.AnimationState)
      {
        case PassangerAnimationState.WalkingBegin:
          this.AnimationState = PassangerAnimationState.Walking;
          break;
        case PassangerAnimationState.Walking:
          this.AnimationState = PassangerAnimationState.WalkingEnd;
          break;
        case PassangerAnimationState.WalkingEnd:
          this.AnimationState = PassangerAnimationState.Idle;
          break;
        case PassangerAnimationState.FallingBegin:
          this.AnimationState = PassangerAnimationState.Falling;
          break;
        case PassangerAnimationState.Falling:
          this.AnimationState = PassangerAnimationState.FallingEnd;
          break;
        case PassangerAnimationState.FallingEnd:
          this.AnimationState = PassangerAnimationState.Idle;
          break;
        case PassangerAnimationState.TurningBegin:
          this.AnimationState = PassangerAnimationState.TurningEnd;
          break;
        case PassangerAnimationState.TurningEnd:
          this.AnimationState = PassangerAnimationState.Walking;
          break;
        case PassangerAnimationState.PreFall:
          this.AnimationState = PassangerAnimationState.FallingBegin;
          break;
      }
    }

    private void PlayAnimation()
    {
      switch (this.AnimationState)
      {
        case PassangerAnimationState.Idle:
          this.PlayLoop("idle");
          this.SetFlip();
          break;
        case PassangerAnimationState.WalkingBegin:
          this.PlayOnce("turn_side");
          this.SetFlip();
          break;
        case PassangerAnimationState.Walking:
          this.PlayLoop("walking");
          break;
        case PassangerAnimationState.WalkingEnd:
          this.PlayOnce("turn_forward");
          break;
        case PassangerAnimationState.FallingBegin:
          this.PlayOnce("fall_begin");
          break;
        case PassangerAnimationState.Falling:
          this.PlayLoop("fall");
          break;
        case PassangerAnimationState.FallingEnd:
          this.PlayOnce("fall_end");
          break;
        case PassangerAnimationState.TurningBegin:
          this.PlayOnce("turn_forward");
          break;
        case PassangerAnimationState.TurningEnd:
          this.SetFlip();
          this.PlayOnce("turn_side");
          break;
      }
    }

    private void SetLastState() => this.lastState = this.PassangerData.State;

    private void SetFlip()
    {
      if (this.PassangerData.State == PassangerState.WalkingLeft)
        this.Flip = FlipDirection.None;
      else
        this.Flip = FlipDirection.X;
    }

    private void WalkingToIdle()
    {
      if (this.CurrentAnimationName == "walking" || this.CurrentAnimationName == "turn_side")
        this.PlayOnce("turn_forward");
      if (!(this.CurrentAnimationName == "turn_forward") || this.IsPlaying)
        return;
      this.PlayLoop("idle");
    }

    private void IdleToWalking()
    {
      if (this.CurrentAnimationName == "idle")
        this.PlayOnce("turn_side");
      if (!(this.CurrentAnimationName == "turn_side") || this.IsPlaying)
        return;
      this.PlayLoop("walking");
    }

    public bool IsFullTurn
    {
      get
      {
        return this.lastState == PassangerState.WalkingLeft || this.lastState == PassangerState.WalkingRight;
      }
    }
  }
}
