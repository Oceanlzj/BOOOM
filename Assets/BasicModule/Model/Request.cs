using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;

namespace Assets.BasicModule.Model
{
  [Serializable]
  public class Request
  {
    public class Conversation
    {
      public string Line { get; set; }
      public bool IsTalk { get; set; } = false;

    }

    [Category("ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("ID"), Description("对应员工ID")]
    public int WorkerID { get; set; }
    [Category("Sentences"), Description("对话内容")]
    public List<Conversation> ConversationSentences { get; set; } = new List<Conversation>();

    [Category("Effect_Yes"), Description("健康度影响")]
    public double HealthEffect_Yes { get; set; }
    [Category("Effect_Yes"), Description("饱食度影响")]
    public double SatietyEffect_Yes { get; set; }
    [Category("Effect_Yes"), Description("San值影响")]
    public double SanityEffect_Yes { get; set; }

    [Category("Effect_NO"), Description("健康度影响")]
    public double HealthEffect_NO { get; set; } = 0;
    [Category("Effect_NO"), Description("饱食度影响")]
    public double SatietyEffect_NO { get; set; } = 0;
    [Category("Effect_NO"), Description("San值影响")]
    public double SanityEffect_NO { get; set; } = 0;

    [Category("Next"), Description("Yes选项下一任务ID")]
    public int NextID_Yes { get; set; }
    [Category("Next"), Description("No选项下一任务ID")]
    public int NextID_No { get; set; }
  }
}
