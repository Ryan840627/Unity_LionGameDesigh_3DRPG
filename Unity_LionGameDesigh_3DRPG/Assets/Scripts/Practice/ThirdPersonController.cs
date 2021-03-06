using UnityEngine;       // 引用Unity API (倉庫-資料與功能)
using UnityEngine.Video; // 引用 影片 API

namespace Camera_Practice
{
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
        [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(0, 500)]
        public float speed = 10.5f;
        [Header("跳躍高度"), Range(0, 1000)]
        public int jumpheight = 250;
        [Header("檢查地面資料"), Tooltip("確認人物是否在地板上")]
        public bool OnTheGround;
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
        public string AnimatorPlayerJump = "跳躍觸發";
        public string AnimatorPlayerOnTheGround = "是否在地板上";

        [Header("玩家遊戲物件")]
        public GameObject playerObject;
        private AudioSource aud;
        private Rigidbody rig;
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
        /** 屬性練習
        // 屬性不會顯示在面板上
        // 儲存資料，與欄位相同
        // 差異在於 : 可以設定存取權限 Get set
        // 屬性語法 : 修飾詞 資料類型 屬性名稱 { 取:存: }
        public int ReadAndWrite { get; set; }
        // 唯讀屬性 : 只能取得 get
        public int read { get;}
        // 唯讀屬性 : 透過 get 設定預設值，關鍵字return為傳回值
        public int readValue 
        {
            get
            {
                return 100;
            }
        }
        // 唯寫屬性 : 禁止，必須要有get
        // public int write { set; }
        // value 指的是指定的值
        private int _hp;
        public int hp 
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            } 
        }
        

        public int MyProperty { get; set; }
        */
        // C#6.0 存取值 可以使用Lambda => 運算子
        // 語法 : get => (程式區塊) - 單行可省略大括號
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }


        #endregion

        #region 練習方法Method
        /*
        // 定義與實作較複雜程式的區塊、功能
        // 方法語法:修飾詞 傳回資料類型 方法名稱 (參數1, ...參數N) {程式區塊}
        // 常用傳回類型:無傳回  void - 此方法沒有傳回資料
        // 格式化: ctrl + K D
        // 自訂方法:
        // 自訂方法需要被呼叫才會執行方法內的程式
        // 名稱顏色為淡黃色 - 沒有被呼叫
        // 名稱顏色為亮黃色 - 有被呼叫
        private void Test()
        {
            print("我是自訂方法");
        }
        private int ReturnJump()
        {
            return 999;
        }

        // 參數語法:資料類型 參數名稱 指定 預設值
        // 有預設值的參數可以不輸入引數，選填式參數
        // ※選填式參數只能放在()右邊
        private void Skill(int damage, string effect = "灰塵特效",string sound = "呼呼呼")
        {
            print("參數版本 - 傷害值: " + damage);
            print("參數版本 - 技能特效:"+ effect);
            print("參數版本 - 聲音特效:" + sound);
        }

        // 對照組:不使用參數
        // 降低維護與擴充性
        private void skill100()
        {
            print("傷害值: " + 100);
            print("技能特效:");
        }
        private void skill150()
        {
            print("傷害值: " + 150);
            print("技能特效:");
        }
        private void skill200()
        {
            print("傷害值: " + 200);
            print("技能特效:");
        }

        // ※非必要但很重要
        // BMI = 體重 / 身高 * 身高
        /// <summary>
        /// 計算BMI方法
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private float BMI(float weight, float height, string name = "測試")
        {
            print(name + " 的BMI");
            return weight / (height * height);
        }
        */
        #endregion


        #region 方法
        // 折疊 Ctrl M O
        // 展開 Ctrl M L
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="moveSpeed">移動速度</param>
        private void Move(float moveSpeed)
        {
            // 取消Animator Apply Motion(使用動畫內位移資訊來進行移動) 
            // 剛體.加速度 = 三圍向量(參數);  - 加速度用來控制剛體三個軸向的移動速度
            // 前方 * 輸入值 * 移動速度
            // 使用前後左右軸向運動並且保持原本地心引力
            rig.velocity = Vector3.forward * MoveInput("Vertical") * moveSpeed +
                           Vector3.right * MoveInput("Horizontal") * moveSpeed +
                           Vector3.up * rig.velocity.y;
        }
        /// <summary>
        /// 移動按鍵輸入
        /// </summary>
        /// <param name="axisName">要取得的軸向名稱</param>
        /// <returns>移動按鍵值</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        /// <summary>
        /// 檢查地板
        /// </summary>
        /// <returns>是否碰到地板</returns>
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * CheckGroundMove.x +
                transform.up * CheckGroundMove.y +
                transform.forward * CheckGroundMove.z
                , CheckGroundRadius, 1 << 3);
            // print("球體碰到的第一個物件 : " + hits[0].name);
            OnTheGround = hits.Length > 0;

            // 傳回 碰撞陣列數量>0 ，只要碰到指定圖層物件就代表在地面上
            return hits.Length > 0;
        }
        /// <summary>
        /// 跳躍
        /// </summary>
        private void Jump()
        {
            //並且&&
            //如果在 地面上 並且按下 空白鍵 就 跳躍
            if (CheckGround() && keyJump)
            {
                rig.AddForce(transform.up * jumpheight);
            }

        }
        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnime()
        {
            /** 練習與走路動畫說明
            // 預期成果
            // 按下前或後時 將布林值設為 true
            // 沒有按時 將布林值設為 false
            // Input
            // if (選擇條件)
            // != ， == 比較運算子 (選擇條件)

            // 當玩家往前或後移動時 true
            // 沒有按下前或後時 false
            // 垂直值 不等於 0 就代表 true
            // 垂直值 等於 0 就代表 false

            // 前後不等於 0 或 左右 不等於 0 都是走路
            // || 或者
            */

            ani.SetBool(AnimatorPlayerWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            // 設定是否在地板上 動畫參數
            ani.SetBool(AnimatorPlayerOnTheGround, OnTheGround);
            // 如果按下跳躍鍵 就設定 跳躍觸發參數
            // 判斷式 只有一行敘述(只有一個分號) 可以省略 大括號
            if (keyJump) ani.SetTrigger(AnimatorPlayerJump);
        }

        #endregion




        #region 事件Event
        // 特定時間點會執行的方法，程式的入口 Start 等於 Console Main
        // 開始事件 : 遊戲開始時執行一次
        private void Start()
        {

            #region 輸出方法
            // 輸出方法
            //print("HELLOWORLD!");

            //Debug.Log("一般訊息");
            //Debug.LogWarning("警告訊息");
            //Debug.LogError("錯誤訊息");
            #endregion

            #region 屬性練習
            /*屬性練習
            // 欄位與屬性 取得Get 、設定Set
            print("欄位資料  - 移動速度 : " + speed);
            print("屬性資料  - 讀寫屬性 : " + ReadAndWrite);
            speed = 30.5f;
            ReadAndWrite = 60;
            print("修改後的資料");
            print("欄位資料  - 移動速度 : " + speed);
            print("屬性資料  - 讀寫屬性 : " + ReadAndWrite);
            // 唯讀屬性
            // read = 7 ; // 唯讀屬性不能設定Set
            print("唯讀屬性 : " + read);
            print("唯讀屬性  - 有預設值 : " + readValue);

            // 屬性存取練習
            print("HP : " + _hp);
            hp = 100;
            print("HP : " + _hp);
            */
            #endregion

            #region 方法練習
            /*
            // 呼叫自訂方法語法: 方法名稱();
            Test();
            Test();
            // 呼叫有傳回值的方法
            // 1.區域變數指定傳回值 - 區域變數警能在此結構{大括號}內存取
            int j = ReturnJump();
            print("跳躍值: " + j);
            // 2.將傳回方法當成值使用
            print("跳躍值，當值使用: " + (ReturnJump() + 1));

            skill100();
            skill150();
            skill200();
            // 呼叫有參數方法時，必須輸入對應的引數
            Skill(100);
            Skill(999,"爆炸特效");
            // 有多個選填式參數時可使用指名參數語法:  參數名稱:值
            Skill(500, sound:"咻咻咻") ;

            print(BMI(68, 1.723f, "我"));
            */
            #endregion

            //要取得腳本的遊戲物件可以使用關鍵字 gameObject

            //取得元件方式
            // 1. 物件欄位名稱，取得元件(類型(元件類型)) 當作 元件類型
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            // 2.此腳本遊戲物件，取得元件<泛型>();
            rig = gameObject.GetComponent<Rigidbody>();
            // 3. 取得元件<泛型>();
            // 類別可以使用繼承類別(父類別)的成員、公開或保護 欄位、屬性及方法
            ani = GetComponent<Animator>();

        }

        // 更新事件 : 一秒約執行 60 次，60 FPS -Frame Per Second
        // 處理持續性的運動，移動物件，監聽玩家輸入按鍵

        private void Update()
        {
            UpdateAnime();
            Jump();
        }
        //固定更新事件 : 固定0.02秒執行一次  -  50FPS
        //處理物理行為，例如 : Rigibody API
        private void FixedUpdate()
        {
            Move(speed);
        }

        //繪製圖示事件 : 
        // 在Unity Editor 內繪製圖示輔助開發，發布後會自動隱藏
        private void OnDrawGizmos()
        {
            // 1. 指定顏色
            // 2. 繪製圖營
            Gizmos.color = new Color(1, 0.5f, 0.5f, 0.5f);
            // transform 與此腳本在停一屬性面板的 Transform 元件
            Gizmos.DrawSphere(
                transform.position +
                transform.right * CheckGroundMove.x +
                transform.up * CheckGroundMove.y +
                transform.forward * CheckGroundMove.z
                , CheckGroundRadius);
        }
        #endregion
    }
}

