// Decompiled with JetBrains decompiler
// Type: GameManager.GameElements.Tree
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3F40C3B-C6E1-4F5E-A59A-127A12A38B73
// Assembly location: C:\Users\Admin\Desktop\RE\DinoCopter\GameManager.dll

using GameManager.GraphicsSystem;
using GameManager.Utils;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.GameElements
{
  internal class Tree : Sprite
  {
    public GlobalMembers.TreeState State { get; private set; }

    public float StateChangeTime { get; private set; }

    private Tree(Point pos)
      : base(7, pos)
    {
      this.SetState(GlobalMembers.TreeState.TreeStateHasFruit);
    }

    private void SetState(GlobalMembers.TreeState newState)
    {
      this.State = newState;
      this.StateChangeTime = GlobalMembers.Game.GetGameTime();
      if (this.State == GlobalMembers.TreeState.TreeStateDontHaveFruit)
        this.SetPaintable(Paintable.Copy(GlobalMembers.Game.TreeHitAnim));
      else
        this.SetPaintable(Paintable.Copy(GlobalMembers.Game.TreeStandAnim));
    }

    public static Sprite CreateTree(Point pos)
    {
      Tree tree1 = new Tree(pos);
      Sprite tree2 = (Sprite) tree1;
      tree1.Ref = tree2;
      return tree2;
    }

    public bool OnStoneHit()
    {
      if (this.State != GlobalMembers.TreeState.TreeStateHasFruit)
        return false;
      this.SetState(GlobalMembers.TreeState.TreeStateDontHaveFruit);
      Sprite fruit = Fruit.CreateFruit(this.GetPos());
      this.Parent.AddSprite(fruit, 2);
      fruit.SetPos(this.GetPos() + new Point((float) (((double) this.GetWidth() - (double) fruit.GetWidth()) / 2.0), this.GetHeight()));
      fruit.SetSpeed(new Point(0.0f, 3f));
      if (!GlobalMembers.Game.HasLose)
        GlobalMembers.Manager.PlaySound(GlobalMembers.SfxTreeHit);
      return true;
    }

    public override void Render(SpriteBatch spriteBatch) => base.Render(spriteBatch);

    public override void Update(float time)
    {
      base.Update(time);
      if (this.State != GlobalMembers.TreeState.TreeStateDontHaveFruit || (double) GlobalMembers.Game.GetGameTime() - (double) this.StateChangeTime < 12.0)
        return;
      this.SetState(GlobalMembers.TreeState.TreeStateHasFruit);
    }

    public override void OnAdd() => base.OnAdd();

    public GlobalMembers.TreeState GetState() => this.State;
  }
}
