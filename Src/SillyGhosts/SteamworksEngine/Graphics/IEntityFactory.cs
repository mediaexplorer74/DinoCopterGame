﻿// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Graphics.IEntityFactory
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Graphics
{
  public interface IEntityFactory
  {
    IEntity Get(string Name);

    ProgressBar GetProgressBar(string Name);

    Button GetButton(string Name);

    ToggleButton GetToggleButton(string Name);

    IAnimatedEntity GetAnimated(string Name);

    IDrawableText GetText(string text, string FontName);
  }
}
