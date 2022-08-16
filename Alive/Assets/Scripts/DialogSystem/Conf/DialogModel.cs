using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DialogOverEventEnum
{
    [LabelText("無")]
    None,
    [LabelText("查理睡覺故事")]
    CharlieGoBed,
    [LabelText("黑色過場")]
    BlackTransitions,
}
public enum DialogEventEnum
{
    [LabelText("下一句對話")]
    NextDialog,
    [LabelText("退出對話")]
    ExitDialog,
    [LabelText("跳轉對話")]
    JumpDialog,
}
[Serializable]
public class DialogModel 
{
    //[HideLabel]
    //[OnValueChanged("NpcOnValueChanged")]
    //[Required("必須添加數據")]
    //public NPCConf nPCConf;
    public DialogModel()
    {
        dialogEventModels = new List<DialogEventModel>();
        dialogPlayerSelects = new List<DialogPlayerSelect>();
    }

    [HorizontalGroup,HideLabel,ReadOnly,LabelWidth(5)]
    public string npcName;
    [HorizontalGroup,HideLabel,ReadOnly,MultiLineProperty(4)]
    public string npcContent;
    [LabelText("對話後事件")]
    public List<DialogEventModel> dialogEventModels;
    [LabelText("玩家對話選擇")]
    public List<DialogPlayerSelect> dialogPlayerSelects;
}
/// <summary>
/// 對話事件數據
/// </summary>
[Serializable]
public class DialogEventModel
{
    [HideLabel, HorizontalGroup("事件"), LabelWidth(20)]
    public DialogEventEnum dialogEvent;

    [HideLabel, HorizontalGroup("事件")]
    public string Args;

    [HideLabel, HorizontalGroup("對話後事件"), LabelWidth(20)]
    public DialogOverEventEnum dialogOverEvent;

    [HideLabel, HorizontalGroup("對話後事件")]
    public string DialogOverArgs;
}
/// <summary>
/// 玩家選擇
/// </summary>
[Serializable]
public class DialogPlayerSelect
{
    [LabelText("選項文字")]
    public string Content;
    [LabelText("事件")]
    public List<DialogEventModel> dialogEventModels;
}