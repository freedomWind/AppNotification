using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPush : MonoBehaviour
{
    public static LocalPush Ins;
    INotification localNotify;
    void SetLocalNotification(INotification notification)
    {
        localNotify = notification;
    }
    private void Awake()
    {
        Ins = this;
        GameObject.DontDestroyOnLoad(this);
#if UNITY_ANDROID
        SetLocalNotification(new AndriodPush());
#elif UNITY_IOS
                SetLocalNotification(new IphonePush());
#endif
        localNotify?.CleanNotification();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            localNotify.NotificationMessage("雨凇mono:10秒推送", System.DateTime.Now.AddSeconds(10), false);
            localNotify.NotificationMessage("雨凇mono:每天中午12点推送", 12, true);
        }
        else
        {
            localNotify.CleanNotification();
        }
    }

}
