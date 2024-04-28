using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  public class GameEventManager
  {
    public class GameEvent
    {
      public int ID { get; set; }
      public int Piroity { get; set; }
      public int NextID { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public void Raise()
      {
        GameEventManager.Instance.Events.Add(this);
      }

      public void Clear()
      {
        GameEventManager.Instance.Events.Remove(this);
      }

    }

    private static Lazy<GameEventManager> gameEventManager = new Lazy<GameEventManager>(() => new GameEventManager());
    public static GameEventManager Instance
    {
      get {
        gameEventManager.Value.Events.Sort((x,y) => x.Piroity.CompareTo(y.Piroity));
        return gameEventManager.Value; 
      }
    }

    public List<GameEvent> Events { get; set; }


    private GameEventManager()
    {
      Events = new List<GameEvent>();

    }
  }
}

