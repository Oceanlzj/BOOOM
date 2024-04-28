using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.BasicModule.Model
{
  public static class FoodPropertyString
  {
    public static string FoodPropertyName(FoodProperty foodProperty)
    {
      string[] names = { "正常", "酸性", "机械", "辐射"};
      return names[(int)foodProperty];
    }
  }
  public enum FoodProperty
  {
    Normal = 0,
    Acid = 1,
    Mechanical = 2,
    RadioActive = 3//and more...


  }
}
