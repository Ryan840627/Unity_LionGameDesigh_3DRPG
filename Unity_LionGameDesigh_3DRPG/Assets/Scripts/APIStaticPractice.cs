using UnityEngine;

/// <summary>
/// 靜態屬性與方法 API 課堂練習
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
    
    void Start()
    {
        int cameraCount  = Camera.allCamerasCount;
        print("所有攝影機數量 : " + cameraCount);

        Vector2 gravity = Physics2D.gravity;
        print("2D 的重力大小 : " + gravity);
        
        Physics2D.gravity = new Vector2(0, -20);
        Vector2 gravity2D = Physics2D.gravity;
        print("2D 的重力設定為Y = -20 : " + gravity2D);

        Time.timeScale = 0.5f;

        float pi= Mathf.PI;

        print("圓周率 : " + pi);

        int number = Mathf.FloorToInt(9.999f);
        print("對 9.999 去小數點 : " + number);

        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        float distace = Vector3.Distance(a,  b);
        print("取得兩點的距離 : " + distace);

        Application.OpenURL("https://unity.com/");
    }

    
    void Update()
    {
        bool anykey =  Input.anyKey;
        print("是否輸入任意鍵 : " + anykey);

        float timePass = Time.timeSinceLevelLoad;
        print("遊戲經過時間 : " + timePass);

        if (Input.GetKeyDown("space"))
        {
            print("是否按下按鍵(指定為空白鍵)");
        }

    }
}
