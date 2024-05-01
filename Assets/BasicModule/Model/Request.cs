using System;
using System.Collections.Generic;
using System.ComponentModel;

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

    [Category("Sentences"), Description("后对话内容——YES")]
    public List<string> ConversationSentences_Yes { get; set; } = new List<string>();
    [Category("Sentences"), Description("后对话内容——NO")]
    public List<string> ConversationSentences_NO { get; set; } = new List<string>();

    [Category("Effect_Yes"), Description("健康度影响")]
    public double HealthEffect_Yes { get; set; } = 0;
    [Category("Effect_Yes"), Description("饱食度影响")]
    public double SatietyEffect_Yes { get; set; } = 0;
    [Category("Effect_Yes"), Description("San值影响")]
    public double SanityEffect_Yes { get; set; } = 0;    
    [Category("Effect_Yes"), Description("Yes选项营养液减少量")]
    public double NSRemoveCounr_Yes { get; set; } = 0;    
    [Category("Effect_Yes"), Description("Yes选项CG 无则-1")]
    public int CGID_Yes { get; set; } = 0;

    [Category("Effect_NO"), Description("健康度影响")]
    public double HealthEffect_NO { get; set; } = 0;
    [Category("Effect_NO"), Description("饱食度影响")]
    public double SatietyEffect_NO { get; set; } = 0;
    [Category("Effect_NO"), Description("San值影响")]
    public double SanityEffect_NO { get; set; } = 0;    
    [Category("Effect_NO"), Description("NO选项营养液减少量")]
    public double NSRemoveCounr_NO { get; set; } = 0;    
    [Category("Effect_NO"), Description("NO选项选项CG 无则-1")]
    public double CGID_NO { get; set; } = 0;

    [Category("Wait"), Description("等待触发轮数")]
    public int RoundBeforeTrigger { get; set; }



    [Category("Next"), Description("Yes选项下一任务ID")]
    public int NextID_Yes { get; set; }
    [Category("Next"), Description("No选项下一任务ID")]
    public int NextID_No { get; set; }




  }
}
