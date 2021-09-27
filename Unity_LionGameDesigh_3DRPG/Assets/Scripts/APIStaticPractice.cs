using UnityEngine;

/// <summary>
/// 靜態屬性與方法 API 課堂練習
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
    
    void Start()
    {
        //print("所有攝影機數量 : " + Camera.allCamerasCount);   // 1
        //print("2D 的重力大小 : " + Physics2D.gravity);         //  0 , -9.8
        //print("圓周率 : " + Mathf.PI);                        //  3.14159
        //Physics2D.gravity = new Vector2(0, -20);
        //Time.timeScale = 0.5f;
        //print("對 9.999 去小數點 : " + Mathf.FloorToInt(9.999f));  9
        //Vector3 a = new Vector3(1, 1, 1);
        //Vector3 b = new Vector3(22, 22, 22);
        //print("取得兩點的距離 : " + Vector3.Distance(a, b));

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
        //print("是否輸入任意鍵 : " + Input.anyKey);
        //print("遊戲經過時間 : " + Time.time);
        print("是否按下按鍵(指定為空白鍵) : " + Input.GetKeyDown(KeyCode.Space));
        bool anykey =  Input.anyKey;
        print("是否輸入任意鍵 : " + anykey);

        float timePass = Time.timeSinceLevelLoad;
        print("遊戲經過時間 : " + timePass);
        
    }
}
