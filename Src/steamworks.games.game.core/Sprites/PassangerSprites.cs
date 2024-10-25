// Decompiled with JetBrains decompiler
// Type: Steamworks.Games.Game.Core.Sprites.PassangerSprites
// Assembly: steamworks.games.game.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 566BA5BF-24DF-44A2-AEB8-7F32FAFED412
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\steamworks.games.game.core.dll

using Steamworks.Engine;
using Steamworks.Engine.Graphics;
using Steamworks.Games.Game.Core.Interfaces;
using Steamworks.Games.Game.Core.Logic;
using System.Collections.Generic;

#nullable disable
namespace Steamworks.Games.Game.Core.Sprites
{
  public class PassangerSprites : Entity
  {
    private List<AnimatedPassangerSprite> Passangers = new List<AnimatedPassangerSprite>();
    private EngineBase Context;
    private List<IEntity> FadingPassangers;
    private CaveCabCore Core;
    public bool HasGhostAppeared;
    public bool HasGhostDisappeared;

    public PassangerSprites(EngineBase Context, CaveCabCore Core)
    {
      this.Context = Context;
      this.Core = Core;
      this.FadingPassangers = new List<IEntity>();
    }

    public override void Update(float elapsedTime_s, float totalTime_s)
    {
      this.HasGhostAppeared = false;
      this.HasGhostDisappeared = false;
      this.CreatePassangers(this.Core.WaitingPassangers);
      this.CreatePassangers(this.Core.LeftPassangers);
      foreach (Entity passanger in this.Passangers)
        passanger.Update(elapsedTime_s, totalTime_s);
      this.FadeOutAndDeletePassangers();
    }

    private void CreatePassangers(List<IPassanger> PassangersData)
    {
      foreach (IPassanger pass in PassangersData)
      {
        bool flag = false;
        foreach (AnimatedPassangerSprite passanger in this.Passangers)
        {
          if (passanger.PassangerData == pass)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.CreatePassanger(pass);
      }
    }

    private void FadeOutAndDeletePassangers()
    {
      AnimatedPassangerSprite animatedPassangerSprite = (AnimatedPassangerSprite) null;
      foreach (AnimatedPassangerSprite passanger in this.Passangers)
      {
        IPassanger passangerData = passanger.PassangerData;
        if (this.Core.WaitingPassangers.IndexOf(passangerData) < 0 && this.Core.LeftPassangers.IndexOf(passangerData) < 0)
          animatedPassangerSprite = passanger;
      }
      if (animatedPassangerSprite == null)
        return;
      if (this.FadingPassangers.IndexOf((IEntity) animatedPassangerSprite) < 0)
      {
        animatedPassangerSprite.FadeAlpha(1f, 0.0f, 0.2f);
        this.HasGhostDisappeared = true;
        this.FadingPassangers.Add((IEntity) animatedPassangerSprite);
      }
      if (!animatedPassangerSprite.AlphaFadeComplete)
        return;
      this.Passangers.RemoveAt(this.Passangers.IndexOf(animatedPassangerSprite));
      this.FadingPassangers.RemoveAt(this.FadingPassangers.IndexOf((IEntity) animatedPassangerSprite));
    }

    private IAnimatedEntity CreatePassanger(IPassanger pass)
    {
      AnimatedPassangerSprite passanger = new AnimatedPassangerSprite(this.Context.ResourceManagers.CurrentTextureManager.Get("ghost1"), pass);
      passanger.FadeAlpha(0.0f, 1f, 0.2f);
      this.HasGhostAppeared = true;
      this.Passangers.Add(passanger);
      return (IAnimatedEntity) passanger;
    }

    public override void Draw(ISpriteBatch spriteBatch)
    {
      foreach (Entity passanger in this.Passangers)
        passanger.Draw(spriteBatch);
    }
  }
}
