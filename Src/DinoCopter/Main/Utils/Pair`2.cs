// GameManager.Utils.Pair`2

#nullable disable
namespace GameManager.Utils
{
  public class Pair<T, TU>
  {
    public Pair()
    {
    }

    public Pair(T first, TU second)
    {
      this.First = first;
      this.Second = second;
    }

    public T First { get; set; }

    public TU Second { get; set; }
  }
}
