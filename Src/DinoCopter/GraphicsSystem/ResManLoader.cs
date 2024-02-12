// GameManager.GraphicsSystem.ResManLoader

using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace GameManager.GraphicsSystem
{
  public class ResManLoader
  {
    public readonly Dictionary<string, bool> ToLoad;

    public int ToLoadNum { get; private set; }

    public int Loaded { get; private set; }

    public int Loading { get; private set; }

    public Dictionary<string, Paintable> Resources { get; private set; }

    public ResManLoader()
    {
      this.ToLoadNum = 0;
      this.Loaded = 0;
      this.Loading = 0;
      this.Resources = new Dictionary<string, Paintable>();
      this.ToLoad = new Dictionary<string, bool>();
    }

    public void AddToLoad(string resourceId)
    {
      if (resourceId.Length < 0 || this.ToLoad.ContainsKey(resourceId))
        return;
      this.ToLoad.Add(resourceId, false);
    }

    public void AddToLoad(Paintable p)
    {
      if (p == null || p.ResourceId.Length <= 0)
        return;
      this.AddToLoad(p.ResourceId);
    }

    public void ReleaseResourceMemory()
    {
      foreach (KeyValuePair<string, Paintable> resource in this.Resources)
      {
        if (resource.Value.PType != PType.Unloaded)
        {
          resource.Value.PType = PType.Unloaded;
          resource.Value.PImage.Texture = (Texture2D) null;
        }
      }
    }

    public void LoadSizes()
    {
      foreach (KeyValuePair<Rh, string> image in GlobalMembers.Images)
      {
        Texture2D texture2D = DispManager.Content.Load<Texture2D>(string.Format("{0}/{1}",
            (object) Util.FilePrefix, (object) image.Value));

        Paintable paintable = new Paintable(PType.Unloaded)
        {
          ResourceId = image.Value,
          PImage = {
            KeepPixelData = false
          }
        };

        this.Resources[image.Value] = paintable;
        paintable.PImage.W = (float) texture2D.Width;
        paintable.PImage.H = (float) texture2D.Height;
        paintable.PImage.TextureWidth = 1;

        while (paintable.PImage.TextureWidth < texture2D.Width)
          paintable.PImage.TextureWidth <<= 1;

        paintable.PImage.TextureHeight = 1;
        
        while (paintable.PImage.TextureHeight < texture2D.Height)
          paintable.PImage.TextureHeight <<= 1;
      }
    }

    public void LoadResources(bool releasePrevious)
    {
      if (releasePrevious)
      {
        foreach (KeyValuePair<string, Paintable> resource in this.Resources)
        {
          if (resource.Value.PType != PType.Unloaded && !this.ToLoad.ContainsKey(resource.Key))
          {
            resource.Value.PType = PType.Unloaded;
            resource.Value.PImage.Texture = (Texture2D) null;
          }
        }
      }
      this.ToLoadNum = this.ToLoad.Count;

      if (this.ToLoadNum == 0)
        return;
      foreach (string key in this.ToLoad.Keys)
      {
        Paintable paintable = this.Resources[key];
        if (paintable == null)
        {
          paintable = new Paintable();
          this.Resources[key] = paintable;
        }
        if (paintable.PType == PType.Unloaded)
        {
          string assetName = string.Format("{0}/{1}", (object) Util.FilePrefix, (object) key);
          Texture2D texture = DispManager.Content.Load<Texture2D>(assetName);
          paintable.InitImage(texture.Width, texture.Height, texture, false);
          paintable.PType = PType.Image;
        }
      }
      this.ToLoad.Clear();
    }
  }
}
