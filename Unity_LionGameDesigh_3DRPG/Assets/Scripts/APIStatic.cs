using UnityEngine;

/// <summary>
/// 認識API : 靜態Static
/// </summary>
public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        #region 靜態屬性
        //與非靜態差異
        //1.不需要實體物件
        //取得
        //語法
        //類別名稱，靜態屬性
        float r = Random.value;
        print("取得靜態屬性隨機值，隨機值 : " + r);

        //設定Set
        //語法:
        //類別名稱，靜態屬性 指定 值
        // 只要看到 Read Only 就不能設定
        Cursor.visible = true;
        // Random.value = 99.9f; 唯獨屬性不能設定
        #endregion

        #region 靜態方法
        // 呼叫、參數、傳回值
        // 簽章 : 參數、傳回值
        //語法:
        //類別名稱，靜態方法(對應引數)
        float range =  Random.Range(10.5f, 20.9f);
        print("隨機範圍 10.5 ~ 20.9 : " + range);

        //API 說明很重要 : 使用整數時不包含最大值
        int rangeint = Random.Range(1, 3);
        print("隨機範圍 1 ~ 3 : " + rangeint);
        #endregion
    }

    private void Update()
    {
        #region 靜態屬性
        // print("經過多久 :" + Time.timeSinceLevelLoad);
        #endregion

        #region 靜態方法
        float h = Input.GetAxis("Horizontal");
        print("水平值 : " + h);
        #endregion
    }
}
