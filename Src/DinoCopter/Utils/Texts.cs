// GameManager.Utils.Texts

#nullable disable
namespace GameManager.Utils
{
  public static class Texts
  {
    public static string Play => nameof (Play);

    public static string Help => nameof (Help);

    public static string Options => nameof (Options);

    public static string About => nameof (About);

    public static string Back => nameof (Back);

    public static string AboutText
    {
      get
      {
        return "\r\n#1Dino Taxi\r\nVersion 1.0\r\nCreated by Fern's Blossom Studio\\n\r\nArt\r\nSebastian Woszczyk\\n\r\nProgramming\r\nAdam Szymanski\\n\r\nLevel Design\r\nKarol Odoner\\n\r\nSounds&Music\r\nJacek Dojwa\r\nPiotr Pacyna\\n\r\n";
      }
    }

    public static string HelpText => "\\\r\n\t\t\t\t  LoremIpsum\\\r\n\t\t\t\t  ";

    public static string Sound => nameof (Sound);

    public static string Music => nameof (Music);

    public static string On => nameof (On);

    public static string Off => nameof (Off);

    public static string Exit => nameof (Exit);

    public static string TryAgain => "Try again";

    public static string DoYouWantToTryAgain => "Do you want to try again?";

    public static string Yes => nameof (Yes);

    public static string No => nameof (No);

    public static string LevelCompleted => "Level completed";

    public static string LevelTime => "Level time: ";

    public static string Time => "Time:";

    public static string Menu => nameof (Menu);

    public static string Replay => nameof (Replay);

    public static string Next => nameof (Next);

    public static string TextDeathReasonHit => "You have crashed!";

    public static string TextDeathReasonEnergyEnd => "Energy run out.";

    public static string TextDeathReasonDrown => "Passenger has drown.";

    public static string TextDeathReasonPterodactylHit => "Pterodactyl has hit you.";

    public static string ProductLevels21To30Description
    {
      get
      {
        return "#1To unlock levels from 21th to 30th you have to buy them for 0.99USD (or similar amount in your local currency).";
      }
    }

    public static string Buy => nameof (Buy);

    public static string Restore => nameof (Restore);

    public static string PleaseWait => nameof (PleaseWait);

    public static string UnlockForFree => "Unlock for free";

    public static string UnlockForFreeDesc => "#1Or you can unlock for free:";

    public static string RestoreDesc
    {
      get
      {
        return "#1If you have already bought this on other device or you reinstalled game, please press restore button below.";
      }
    }

    public static string Unlock => nameof (Unlock);

    public static string TapjoyDesc
    {
      get => "#1To unlock levels for free you need 10 shells. To earn shells complete actions:";
    }

    public static string CheckACtions => "Check actions";

    public static string YouHave => "#1you have";

    public static string Shells => "#1shells";

    public static string NotEnoughShells => "#1you don't have enough shells.";

    public static string GetMore => "Get more";

    public static string UnlockMoreLevels
    {
      get
      {
        return "#1To unlock 5 more levels for free you need 1 shell. To earn shells complete actions:";
      }
    }

    public static string BuyFullVersion
    {
      get => "#1To remove ads and unlock levels you can buy full version.";
    }
  }
}
