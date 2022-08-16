using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DialogOverEventEnum
{
    [LabelText("�L")]
    None,
    [LabelText("�d�z��ı�G��")]
    CharlieGoBed,
    [LabelText("�¦�L��")]
    BlackTransitions,
}
public enum DialogEventEnum
{
    [LabelText("�U�@�y���")]
    NextDialog,
    [LabelText("�h�X���")]
    ExitDialog,
    [LabelText("������")]
    JumpDialog,
}
[Serializable]
public class DialogModel 
{
    //[HideLabel]
    //[OnValueChanged("NpcOnValueChanged")]
    //[Required("�����K�[�ƾ�")]
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
    [LabelText("��ܫ�ƥ�")]
    public List<DialogEventModel> dialogEventModels;
    [LabelText("���a��ܿ��")]
    public List<DialogPlayerSelect> dialogPlayerSelects;
}
/// <summary>
/// ��ܨƥ�ƾ�
/// </summary>
[Serializable]
public class DialogEventModel
{
    [HideLabel, HorizontalGroup("�ƥ�"), LabelWidth(20)]
    public DialogEventEnum dialogEvent;

    [HideLabel, HorizontalGroup("�ƥ�")]
    public string Args;

    [HideLabel, HorizontalGroup("��ܫ�ƥ�"), LabelWidth(20)]
    public DialogOverEventEnum dialogOverEvent;

    [HideLabel, HorizontalGroup("��ܫ�ƥ�")]
    public string DialogOverArgs;
}
/// <summary>
/// ���a���
/// </summary>
[Serializable]
public class DialogPlayerSelect
{
    [LabelText("�ﶵ��r")]
    public string Content;
    [LabelText("�ƥ�")]
    public List<DialogEventModel> dialogEventModels;
}