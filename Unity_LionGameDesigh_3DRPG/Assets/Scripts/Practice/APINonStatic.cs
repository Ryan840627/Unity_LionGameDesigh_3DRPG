using UnityEngine;

/// <summary>
/// 認識API : 非靜態 Non Static
/// </summary>
public class APINonStatic : MonoBehaviour
{
    public Transform tral; //修飾詞 要存取非靜態類別 欄位名稱
    public Camera cam;
    public Light lig;

    private void Start()
    {
        #region 非靜態屬性
        //與靜態差異
        // 1. 需要實體物件
        // 2. 取得實體物件 - 定義欄位並將要存取的物件存入欄位
        // 3. 遊戲物件(階層面板下都是)、元件(屬性面板下)必須存在場景內
        //取得 Get
        //語法 : 欄位名稱，非靜態類別
        print("攝影機座標 : " + tral.position);
        print("攝影機深度 : " + cam.depth);

        //設定
        //語法 : 欄位名稱，非靜態屬性 指定 值
        tral.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region 非靜態方法
        //呼叫
        //語法 : 
        //欄位名稱，非靜態方法名稱(對應引數);
        lig.Reset();
        #endregion
    }
}
