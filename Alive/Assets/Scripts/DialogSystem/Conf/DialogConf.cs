using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "對話配置",menuName = "角色對話/新增對話數據")]
public class DialogConf : ScriptableObject
{
    [LabelText("對話文檔")]
    [OnValueChanged("NpcOnTextChanged")]
    [Required("必須添加數據")]
    public TextAsset textContent;

    [LabelText("是否有分支")]
    public bool isbranch = false;

    [OnValueChanged("NpcOnValueChanged")]
    [LabelText("參數")]
    public int Args = -1;

    [LabelText("對話數據")]
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<DialogModel> dialogModels;

    [HideInInspector]
    public string[] stateContent;
    /// <summary>
    /// NPC配置文件修改時候調用的方法
    /// </summary>
    private void NpcOnTextChanged()
    {
        stateContent = textContent.text.Split('\n');
    }
    /// <summary>
    /// NPC配置文件修改時候調用的方法
    /// </summary>
    private void NpcOnValueChanged()
    {
        dialogModels.Clear();
        bool isfind = false;
        for (int i = 0; i < stateContent.Length; i++)
        {
            int temp = 0;
            if (!int.TryParse(stateContent[i], out temp) && isfind == true)
            {
                string[] strtemp = stateContent[i].Split(':');
                DialogModel dialogModel = new DialogModel();
                dialogModel.npcName = strtemp[0];
                dialogModel.npcContent = strtemp[1];

                DialogEventModel dialogEventModel = new DialogEventModel();
                int inttemp;
                if (i == stateContent.Length - 1)
                {
                    dialogEventModel.dialogEvent = DialogEventEnum.ExitDialog;
                }
                else if (!int.TryParse(stateContent[i + 1], out inttemp))
                {
                    dialogEventModel.dialogEvent = DialogEventEnum.NextDialog;
                }
                else
                {
                    dialogEventModel.dialogEvent = DialogEventEnum.ExitDialog;
                }
                dialogModel.dialogEventModels.Add(dialogEventModel);

                dialogModels.Add(dialogModel);
            }
            else if (temp == Args)
            {
                isfind = true;
            }
            else
            {
                isfind = false;
            }
        }
    }
}
