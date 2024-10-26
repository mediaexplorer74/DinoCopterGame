// steamworks.games.game.opengl.OpenGlDataUtils

using steamworks.games.game.core;
using System;
using System.Diagnostics;
using System.IO;
//using Windows.Storage;


namespace steamworks.games.game.opengl
{
  public class OpenGlDataUtils : IDataUtils
  {
    public Stream OpenFile(string name)
    {
        //StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        Stream result = default;

        try
        {
            //RnD
            //result = (Stream)File.OpenRead(Path.Combine(localFolder.Path, name)); //?
            result = (Stream)File.OpenRead(Path.Combine("", name)); //?
        }
        catch (Exception ex)
        {
            Debug.WriteLine("[ex] OpenGlDataUtils-File.OpenRead bug: " + ex.Message);
        }

        return result;
    }
  }
}
