using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance; 

    private DialogConf[] dialogConfs;
    public DialogConf currDialogConf;
    //private GameObject dialogText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        instance.dialogConfs = Resources.LoadAll<DialogConf>("Dialog");
        //instance.dialogText = instance.transform.Find("提示文字_hint").gameObject;
    }
    /// <summary>
    /// 抓取對話數據
    /// </summary>
    /// <param name="dialogName"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public DialogConf GetDialog(string dialogName, int args)
    {
        for (int i = 0; i < instance.dialogConfs.Length; i++)
        {
            if (dialogName == instance.dialogConfs[i].name)
            {
                instance.dialogConfs[i].Args = args;
                return instance.dialogConfs[i];
            }
        }
        Debug.LogError("找不到對話");
        return null;
    }
    //[MenuItem("Tools/檢測對話數據配置")]
    //public static void CheckDialogConfs()
    //{
    //    DialogConf[] dialogConfs = Resources.LoadAll<DialogConf>("Conf");
    //    for (int i = 0; i < dialogConfs.Length; i++)
    //    {
    //        for (int j = 0; j < dialogConfs[i].dialogModels.Count; j++)
    //        {
    //            if (dialogConfs[i].dialogModels[j].nPCConf == null)
    //            {
    //                Debug.LogError(dialogConfs[i].name + "--中的第" + j + "條數據缺少數據");
    //            }
    //        }
    //    }
    //    Debug.Log("檢測完畢"); 
    //}

    public void HandleDialog(string contentName)
    {
        //if (instance.dialogText.activeSelf && Input.GetKeyDown(KeyCode.C))
        //{
        //    instance.dialogText.SetActive(false);
        //    StartDialog(contentName, Random.Range(1, 4));
        //}
        //else if (UI_Dialog.instance.isDialog == true && Input.GetKeyDown(KeyCode.C))
        //{
        //    if (!UI_Dialog.instance.isFinishText && !UI_Dialog.instance.cancelTyping)
        //    {
        //        UI_Dialog.instance.cancelTyping = true;
        //    }
        //    if (UI_Dialog.instance.isFinishText && !UI_Dialog.instance.cancelTyping)
        //    {
        //        for (int i = 0; i < currDialogConf.dialogModels[UI_Dialog.instance.currIndex].dialogEventModels.Count; i++)
        //        {
        //            UI_Dialog.instance.ParseDialogEvent(currDialogConf.dialogModels[UI_Dialog.instance.currIndex].dialogEventModels[i].dialogEvent, currDialogConf.dialogModels[UI_Dialog.instance.currIndex].dialogEventModels[i].Args);
        //        }
        //    }
        //}
    }
    /// <summary>
    /// 開始執行對話
    /// </summary>
    /// <param name="dialogName"></param>
    /// <param name="args"></param>
    public void StartDialog(string dialogName)
    {
        instance.currDialogConf = GetDialog(dialogName, 1);
        //NpcOnValueChanged();
        UI_Dialog.instance.currConf = instance.currDialogConf;
        UI_Dialog.instance.Start_Dialog(instance.currDialogConf, UI_Dialog.instance.currIndex);
    }

    /// <summary>
    /// //因應屬性去變動內容
    /// </summary>
    private void NpcOnValueChanged()
    {
        instance.currDialogConf.dialogModels.Clear();
        bool isfind = false;
        for (int i = 0; i < instance.currDialogConf.stateContent.Length; i++)
        {
            int temp = 0;
            if (!int.TryParse(instance.currDialogConf.stateContent[i], out temp) && isfind == true)
            {
                string[] strtemp = instance.currDialogConf.stateContent[i].Split(':');
                DialogModel dialogModel = new DialogModel();
                dialogModel.npcName = strtemp[0];
                dialogModel.npcContent = strtemp[1];

                DialogEventModel dialogEventModel = new DialogEventModel();
                int inttemp;
                if (i == instance.currDialogConf.stateContent.Length - 1)
                    dialogEventModel.dialogEvent = DialogEventEnum.ExitDialog;
                else if (!int.TryParse(instance.currDialogConf.stateContent[i+1], out inttemp))
                {
                    dialogEventModel.dialogEvent = DialogEventEnum.NextDialog;
                }
                else
                {
                    dialogEventModel.dialogEvent = DialogEventEnum.ExitDialog;
                }
                dialogModel.dialogEventModels.Add(dialogEventModel);

                instance.currDialogConf.dialogModels.Add(dialogModel);
            }
            else if (temp == instance.currDialogConf.Args)
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
