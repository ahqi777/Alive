using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "��ܰt�m",menuName = "������/�s�W��ܼƾ�")]
public class DialogConf : ScriptableObject
{
    [LabelText("��ܤ���")]
    [OnValueChanged("NpcOnTextChanged")]
    [Required("�����K�[�ƾ�")]
    public TextAsset textContent;

    [LabelText("�O�_������")]
    public bool isbranch = false;

    [OnValueChanged("NpcOnValueChanged")]
    [LabelText("�Ѽ�")]
    public int Args = -1;

    [LabelText("��ܼƾ�")]
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<DialogModel> dialogModels;

    [HideInInspector]
    public string[] stateContent;
    /// <summary>
    /// NPC�t�m���ק�ɭԽեΪ���k
    /// </summary>
    private void NpcOnTextChanged()
    {
        stateContent = textContent.text.Split('\n');
    }
    /// <summary>
    /// NPC�t�m���ק�ɭԽեΪ���k
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
