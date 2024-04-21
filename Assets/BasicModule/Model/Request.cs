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
    [Category("ID"), Description("唯一ID")]
    public int ID { get; set; }
    [Category("ID"), Description("对应员工ID")]
    public int WorkerID { get; set; }
    [Category("Sentences"), Description("对话内容")]
    public List<string> ConversationSentences { get; set; } = new List<string>();

    [Category("Effect"), Description("健康度影响")]
    public double HealthEffect { get; set; }
    [Category("Effect"), Description("饱食度影响")]
    public double SatietyEffect { get; set; }
    [Category("Effect"), Description("San值影响")]
    public double SanityEffect { get; set; }
    [Category("Next"), Description("Yes选项下一任务ID")]
    public int NextID_Yes { get; set; }
    [Category("Next"), Description("No选项下一任务ID")]
    public int NextID_No { get; set; }
  }
}
