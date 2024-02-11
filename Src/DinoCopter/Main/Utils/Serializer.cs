// GameManager.Utils.Serializer

using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace GameManager.Utils
{
  public static class Serializer
  {
    public static string Serialize<T>(T data)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractSerializer(typeof (T)).WriteObject((Stream) memoryStream, (object) data);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        return new StreamReader((Stream) memoryStream).ReadToEnd();
      }
    }

    public static T Deserialize<T>(string xml)
    {
      using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
        return (T) new DataContractSerializer(typeof (T)).ReadObject((Stream) memoryStream);
    }

    public static void Store<T>(string filename, T obj)
    {
      using (IsolatedStorageFileStream storageFileStream = IsolatedStorageFile.GetUserStoreForApplication().OpenFile(filename, FileMode.Create))
        new DataContractSerializer(typeof (T)).WriteObject((Stream) storageFileStream, (object) obj);
    }

    public static T Retrieve<T>(string filename)
    {
      T obj = default (T);
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      if (storeForApplication.FileExists(filename))
      {
        using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(filename, FileMode.Open))
          obj = (T) new DataContractSerializer(typeof (T)).ReadObject((Stream) storageFileStream);
        storeForApplication.DeleteFile(filename);
      }
      return obj;
    }

    public static void DeleteFile(string filename)
    {
      IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
      if (!storeForApplication.FileExists(filename))
        return;
      storeForApplication.DeleteFile(filename);
    }
  }
}
