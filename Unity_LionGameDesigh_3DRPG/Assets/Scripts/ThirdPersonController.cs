using UnityEngine;       // 引用Unity API (倉庫-資料與功能)
using UnityEngine.Video; // 引用 影片 API

// 修飾詞 類別  類別名稱 : 繼承類別
// MonoBehaviour : Unity 基底類別，要掛在物件上一定要繼承
// 繼承後會享有該類別的成員
// 在類別以及成員上方添加三條斜線會添加摘要
// 成用成員:欄位Field、屬性Property(變數)、方法Method、事件Event
/// <summary>
/// 第三人稱控制器
/// 移動、跳躍
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region 欄位Field
    // 儲存遊戲吃廖，例如:移動速度、跳躍高度等等...
    // 常用四大類型:整數 int、浮點數 float、字串 string、布林值 bool
    // 欄位語法:修飾詞 資料類型 欄位名稱 (指定 預設值) 結尾
    // 修飾詞: 
    // 1.公開 public  - 允許所有類別存取 - 顯示在屬性面板上 - 需要調整的資料設定為公開
    // 2.私人 private - 禁止所有類別存取 - 隱藏在屬性面板上 - 預設值
    // ※ Unity 以屬性面板資料為主
    // ※ 恢復程式預設值請按 ... > Reset
    // 欄位屬性 Attribute : 輔助欄位資料
    // 欄位屬性語法 : [屬性名稱(屬性值)]
    // Header 標題
    // Tooltip 提示:滑鼠停留在欄位名稱上會顯示彈出視窗
    // Range 範圍:可使用在數值類型資料上，例如 : int,float
    [Header("移動速度"),Tooltip("用來調整角色移動速度"),Range(0,500)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Range(0, 1000)]
    public int jumpheight = 100;
    [Header("檢查地面資料"), Tooltip("確認人物是否在地板上")]
    public bool OnTheGorund = false;
    public Vector3 CheckGroundMove;
    [Range(0, 3)]
    public float CheckGroundRadius = 0.2f;
    [Header("音效檔案")]
    public AudioClip JumpSound;
    public AudioClip LandingSound;
    [Header("動畫參數")]
    public string AnimatorPlayerWalk = "走路開關";
    public string AnimatorPlayerRun = "跑步開關";
    public string AnimatorPlayerHurt = "受傷觸發";
    public string AnimatorPlayerDie = "死亡開關";

    private AudioSource aud;
    private Rigidbody rid;
    private Animator ani;



    #region Unity 資料類型
    /**練習Unity 資料類型
    // 顏色 Color
    public Color color;
    public Color white = Color.white;                          //內建顏色
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f,0f,0.5f);             //自訂顏色RGB
    public Color color2 = new Color(0.5f, 0.5f, 0.5f,0.5f);    //自訂顏色RGBA

    //座標 Vector 2 - 4
    public Vector2 v2;
    public Vector2 v2uRight = Vector2.right;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2One = Vector2.one;
    public Vector2 V2Costom = new Vector2(10.5f, 21.5f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector4 v4 = new Vector4(1,2,3,4);

    // 按鍵 列舉資料 enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump= KeyCode.Space;

    // 遊戲資料類型
    public AudioClip sound;    // 音效 mp3 , ogg , wav
    public VideoClip video;    // 影片 mp4
    public Sprite sprite;      // 圖片 png , jpeg - 不支援 gif
    public Texture texture2D;  // 2D 圖片 png , jpeg
    public Material material;  // 材質球
    [Header("元件")]
    // 元件 Component : 屬性面板上可以摺疊的
    public Transform tra;
    public Animation anuOld;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    // 綠色蚯蚓
    // 1. 建議不要使用此名稱
    // 2. 使用過時API
    */
    #endregion

    #endregion

    #region 屬性Property

    #endregion

    #region 方法Method

    #endregion

    #region 事件Event
    // 特定時間點會執行的方法，程式的入口 Start 等於 Console Main
    #endregion
}
