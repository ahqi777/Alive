using UnityEngine;
using System.Collections;

public class SimpleVisualGestureListener : MonoBehaviour, VisualGestureListenerInterface
{
    public bool GestureCompleted(long userId, int userIndex, string gesture, float confidence)
    {
        if (gesture == "Freeze") //如果举起右手姿态完成
        {
            print("我投降...");
            Debug.Log("我投降...");
        }
        return true;
        //throw new System.NotImplementedException();
    }
    //检测动作姿态过程
    public void GestureInProgress(long userId, int userIndex, string gesture, float progress)
    {

    }
    void Start()
    {

    }
    void Update()
    {

    }

}
