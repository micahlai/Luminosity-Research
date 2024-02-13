using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activityMonitor : MonoBehaviour
{
    float timer;
    float minuteTimer;
    public bool timerStart;

    // Start is called before the first frame update
    void Start()
    {
        timerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            timer += Time.deltaTime;
            minuteTimer += Time.deltaTime;
            if (minuteTimer >= 60)
            {

                minuteTimer = 0;
                FindObjectOfType<PlayfabManager>().getActivityData();

            }
        }
    }
    public void updateData(List<activityData> activityData)
    {
        var date = System.DateTime.Now;
        var startDate = new System.DateTime(2024, 2, 13);
        activityData activity = new activityData(Mathf.RoundToInt(timer / 60), date.Subtract(startDate).Days);
        bool dayExists = false;
        for (int i = 0; i < activityData.ToArray().Length; i++)
        {
            if (activityData[i].day == activity.day)
            {
                dayExists = true;
                activityData[i].minutesOnline += activity.minutesOnline;
            }
        }
        if (!dayExists)
        {
            activityData.Add(activity);
        }
        FindObjectOfType<PlayfabManager>().sendActivityData(activityData);

    }
    public void createData()
    {
        minuteTimer = 0;
        var date = System.DateTime.Now;
        var startDate = new System.DateTime(2024, 2, 13);
        activityData activity = new activityData(Mathf.RoundToInt(timer / 60), date.Subtract(startDate).Days);
        List<activityData> activityDatas = new List<activityData>();
        activityDatas.Add(activity);
        FindObjectOfType<PlayfabManager>().sendActivityData(activityDatas);
    }
    [System.Serializable]
    public class activityData
    {
        public int minutesOnline;
        public int day;

        public activityData(int _minutesOnline, int _day)
        {
            minutesOnline = _minutesOnline;
            day = _day;
        }
    }
}
