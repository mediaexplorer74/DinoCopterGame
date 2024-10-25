// Decompiled with JetBrains decompiler
// Type: Steamworks.Engine.Sound.XNASoundManager
// Assembly: Steamworks.Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 191680A8-837B-439B-9D59-78E90C7D63A4
// Assembly location: C:\Users\Admin\Desktop\RE\SillyGhosts\Steamworks.Engine.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Steamworks.Engine.Common;
using System;
using System.Diagnostics;

#nullable disable
namespace Steamworks.Engine.Sound
{
  public class XNASoundManager : Manager<SoundInfo>, ISoundManager
  {
    private SoundInfo CurrentMusic;

    public bool Muted { get; set; }

    public bool MusicEnabled { get; set; }

    public bool SoundEnabled { get; set; }

    public void Load(ContentManager manager)
    {
      foreach (string key in this.Dict.Keys)
      {
        if (this.Dict[key].IsMusic)
        {
            try
            {
                //this.Dict[key].Song = manager.Load<Song>(this.Dict[key].FilePath);
                this.Dict[key].Song = manager.Load<Song>(this.Dict[key].FilePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] XNASoundManager problem: " + ex.Message);
            }
        }
        else
        {
            this.Dict[key].Sound = manager.Load<SoundEffect>(this.Dict[key].FilePath);
        }
      }
    }

    public void PlayMusic(string Name)
    {
      if (!this.MusicEnabled)
        return;
      SoundInfo soundInfo = this.Get(Name);
      if (soundInfo.IsPlaying)
        return;
      this.CurrentMusic = soundInfo;
      MediaPlayer.IsRepeating = true;
      if (!this.Muted)
        MediaPlayer.Play(this.CurrentMusic.Song);
      this.CurrentMusic.IsPlaying = true;
    }

    public void StopMusic(string Name)
    {
      if (!this.MusicEnabled)
        return;
      MediaPlayer.Stop();
      if (this.CurrentMusic != null)
        this.CurrentMusic.IsPlaying = false;
      this.CurrentMusic = (SoundInfo) null;
    }

    public void PlaySound(string Name, bool OnceAtTime)
    {
      if (!this.SoundEnabled)
        return;
      this.Play(Name, OnceAtTime);
    }

    private void Play(string Name, bool OnceAtTime) => this.Play(this.Get(Name), false);

    private void Play(SoundInfo sound, bool Loop)
    {
      if (sound.SoundInstance == null)
      {
        sound.SoundInstance = sound.Sound.CreateInstance();
        sound.SoundInstance.IsLooped = Loop;
      }
      if (sound.SoundInstance.State != SoundState.Stopped || this.Muted)
        return;
      sound.SoundInstance.Play();
    }

    public void PlayLoop(string Name)
    {
      if (!this.SoundEnabled)
        return;
      this.Play(this.Get(Name), true);
    }

    public void Stop(string Name)
    {
      if (!this.SoundEnabled)
        return;
      SoundInfo soundInfo = this.Get(Name);
      if (soundInfo.SoundInstance == null)
        return;
      soundInfo.SoundInstance.Stop();
    }

    public void Unmute()
    {
      this.Muted = false;
      if (this.CurrentMusic == null || !this.CurrentMusic.IsPlaying)
        return;
      MediaPlayer.Play(this.CurrentMusic.Song);
    }

    public void Mute()
    {
      this.Muted = true;
      if (this.CurrentMusic != null && this.CurrentMusic.IsPlaying)
        MediaPlayer.Stop();
      foreach (string key in this.Dict.Keys)
      {
        if (this.Dict[key].SoundInstance != null && this.Dict[key].SoundInstance.State == SoundState.Playing)
          this.Dict[key].SoundInstance.Stop();
      }
    }
  }
}
