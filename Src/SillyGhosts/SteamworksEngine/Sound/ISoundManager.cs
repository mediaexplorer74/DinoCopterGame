// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Sound.ISoundManager
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll


namespace Steamworks.Engine.Sound
{
  public interface ISoundManager
  {
    bool MusicEnabled { get; set; }

    void PlayMusic(string Name);

    void StopMusic(string Name);

    void PlaySound(string Name, bool OnceAtTime);

    bool SoundEnabled { get; set; }

    void PlayLoop(string Name);

    void Stop(string Name);

    void Unmute();

    void Mute();

    bool Muted { get; set; }
  }
}
