using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog instance;
    private GameObject dialogSystem;
    private Text nameText;
    private Text contentText;
    //private Transform Options;
    //private GameObject prefab_OptionItem;


    public DialogConf currConf;
    public int currIndex;
    //public bool cancelTyping = false;
    //public bool isFinishText = false;
    //public bool isDialog = false;
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
        instance.dialogSystem = instance.transform.Find("Dialog").gameObject;
        instance.nameText = instance.transform.Find("Dialog/NameText").GetComponent<Text>();
        instance.contentText = instance.transform.Find("Dialog/ContentText").GetComponent<Text>();
        //content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        //instance.Options = instance.transform.Find("Options");
        //instance.prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
    }
    public void Start_Dialog(DialogConf conf, int index)
    {
        //instance.isDialog = true;
        instance.dialogSystem.SetActive(true);
        //開始說話
        StartCoroutine(DoMainTextEF(conf, index));
        
        ////NPC事件
        //for (int i = 0; i < model.dialogEventModels.Count; i++)
        //{
        //    ParseDialogEvent(model.dialogEventModels[i].dialogEvent, model.dialogEventModels[i].Args);
        //}
        #region
        //刪除已有的玩家選項
        //for (int i = 0; i < Options.childCount; i++)
        //{
        //    Destroy(Options.GetChild(i).gameObject);
        //}
        //for (int i = 0; i < model.dialogPlayerSelects.Count; i++)
        //{
        //    UI_Options_Item item = GameObject.Instantiate<GameObject>(prefab_OptionItem, Options).GetComponent<UI_Options_Item>();
        //    item.Init(model.dialogPlayerSelects[i]);
        //}
        #endregion
    }

    public void ParseDialogEvent(DialogEventEnum dialogEvent, string args)
    {
        switch (dialogEvent)
        {
            case DialogEventEnum.NextDialog:
                NextDialogEvent();
                break;
            case DialogEventEnum.ExitDialog:
                ExitDialogEvent();
                break;
            case DialogEventEnum.JumpDialog:
                JumpDialogEvent(int.Parse(args));
                break;
            default:
                break;
        }
    }
    public void ParseDialogEvent(DialogOverEventEnum dialogEvent, string args)
    {
        switch (dialogEvent)
        {
            case DialogOverEventEnum.None:
                return;
            case DialogOverEventEnum.CharlieGoBed:
                //EventsManager.instance.CharlieGoBed();
                break;
            case DialogOverEventEnum.BlackTransitions:
               // EventsManager.instance.BlackTransitions(null);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 觸發下一段對話
    /// </summary>
    private void NextDialogEvent()
    {
        instance.currIndex++;
        instance.Start_Dialog(currConf, currIndex);
    }
    /// <summary>
    /// 觸發跳轉對話
    /// </summary>
    private void JumpDialogEvent(int index)
    {
        instance.currIndex = index;
        instance.Start_Dialog(currConf, currIndex);
    }
    /// <summary>
    /// 觸發退出對話
    /// </summary>
    public void ExitDialogEvent()
    {
        //instance.isDialog = false;
        instance.dialogSystem.SetActive(false);
        //EventsManager.instance.CharlieGoBed();
        for (int i = 0; i < DialogManager.instance.currDialogConf.dialogModels[currIndex].dialogEventModels.Count; i++)
        {
            ParseDialogEvent(DialogManager.instance.currDialogConf.dialogModels[currIndex].dialogEventModels[i].dialogOverEvent, DialogManager.instance.currDialogConf.dialogModels[currIndex].dialogEventModels[i].DialogOverArgs);
        }
        instance.currIndex = 0;
    }

    IEnumerator DoMainTextEF(DialogConf conf, int index)
    {

        // 字符数量决定了 conteng的高 每23个字符增加25的高
        //float addHeight = txt.Length / 23 + 1;
        //content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);
        //instance.isFinishText = false;
        DialogModel model;
        model = conf.dialogModels[index];
        instance.nameText.text = model.npcName + ":";
        instance.contentText.text = model.npcContent;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < model.dialogEventModels.Count; i++)
        {
            ParseDialogEvent(model.dialogEventModels[i].dialogEvent, model.dialogEventModels[i].Args);
        }
        //for (int i = 0; i < txt.Length; i++)
        //{
        //    currStr += txt[i];
        //    yield return new WaitForSeconds(0.08f);
        //    mainText.text = currStr;
        //    // 每满23个字，下移一个距离 25
        //    //if (i>23*3&&i % 23 == 0)
        //    //{
        //    //    content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y+25);
        //    //}
        //}
        //instance.cancelTyping = false;
        //instance.isFinishText = true;
    }
   
}
