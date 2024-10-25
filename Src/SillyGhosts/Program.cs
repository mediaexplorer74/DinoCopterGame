// Decompiled with JetBrains decompiler
// Type: steamworks.games.game.opengl.Program
// Assembly: Silly Gosts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD37877E-4464-40FF-B069-FD996B32AF74
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Silly Gosts.dll

using steamworks.games.game.core;
using Steamworks.Games.Game.Core.Interfaces;
using System;

#nullable disable
namespace steamworks.games.game.opengl
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (SillyGhostGame sillyGhostGame = 
                new SillyGhostGame((IDataUtils)new OpenGlDataUtils(), 
                (IPersister)new OpenGlPersister()))
                sillyGhostGame.Run();
        }
    }
}
