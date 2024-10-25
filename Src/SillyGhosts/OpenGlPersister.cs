// steamworks.games.game.opengl.OpenGlPersister

using SharpDX;
using steamworks.games.game.core;
using System;
using System.Diagnostics;
using System.IO;
using Windows.Storage;

#nullable disable
namespace steamworks.games.game.opengl
{
  public class OpenGlPersister : PersisterBase
  {
    public override void Save(byte[] bytesToSave, int saveByteCount, string SaveName)
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        try
        {
            //RnD
            //AppDomain.CurrentDomain.BaseDirectory
            File.WriteAllBytes(Path.Combine(localFolder.Path, SaveName), bytesToSave);
        }
        catch (Exception ex)
        {
           Debug.WriteLine("[ex] Save bug: " + ex.Message);
        }
    }//Save


    // Load
    public override byte[] Load(string SaveName, int saveByteCount)
    {
       StorageFolder localFolder = ApplicationData.Current.LocalFolder;
       //RnD
       //AppDomain.CurrentDomain.BaseDirectory
       string path = Path.Combine(localFolder.Path, SaveName);

       byte[] result = default;
       
       try 
       {
          result = File.Exists(path) ? File.ReadAllBytes(path) : this.GetBytes(saveByteCount);
       }
       catch (Exception ex)
       {
          Debug.WriteLine("[ex] Load bug: " + ex.Message);
       }
       return result; 
    }//Load

    public OpenGlPersister() : base()
    {
       //
    }
  }
}
