using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JPush;
using System;
using LocalNotification = UnityEngine.iOS.LocalNotification;

public interface INotification
{
    void NotificationMessage(string message, int hour, bool isRepeatDay);
    void NotificationMessage(string message, System.DateTime newDate, bool isRepeatDay);
    void CleanNotification();
}

public class AndriodPush :INotification
{
    public void NotificationMessage(string message, int hour, bool isRepeatDay)
    { }
    public void NotificationMessage(string message, System.DateTime newDate, bool isRepeatDay)
    { }
    public void CleanNotification()
    { }
}
public class IphonePush :INotification
{
    public void NotificationMessage(string message, int hour, bool isRepeatDay)
    {
        int year = System.DateTime.Now.Year;
        int month = System.DateTime.Now.Month;
        int day = System.DateTime.Now.Day;
        System.DateTime newdate = new DateTime(year, month, day, hour, 0, 0);
        NotificationMessage(message, newdate, isRepeatDay);
    }
    public void NotificationMessage(string message, System.DateTime newDate, bool isRepeatDay)
    {
        if (newDate > DateTime.Now)
        {
            LocalNotification localNotification = new LocalNotification();
            localNotification.fireDate = newDate;
            localNotification.alertBody = message;
            localNotification.applicationIconBadgeNumber = 1;
            localNotification.hasAction = true;
            if (isRepeatDay)
            {
                localNotification.repeatCalendar = UnityEngine.iOS.CalendarIdentifier.ChineseCalendar;
                localNotification.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
            }
        }
    }
    public void CleanNotification()
    {
        LocalNotification l = new LocalNotification();
        l.applicationIconBadgeNumber = -1;
        UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow(l);
        LocalPush.Ins.StartCoroutine(delayOneFrame());       
    }
    IEnumerator delayOneFrame()
    {
        yield return null;
        UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
        UnityEngine.iOS.NotificationServices.ClearLocalNotifications();
    }

}

