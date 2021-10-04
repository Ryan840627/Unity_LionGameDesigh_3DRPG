using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ryan
{
    /// <summary>
    /// 第三人稱控制器
    /// 移動、跳躍
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 欄位
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

        #endregion

        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float SoundValueRange { get => Random.Range(0.3f, 0.7f); }
       

        #region 方法
        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="moveSpeed">移動速度</param>
        private void Move(float moveSpeed)
        {
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
            //如果 尚未落地 並且 落地碰撞陣列大於 0 就 撥放一次音效
            if (!OnTheGround && hits.Length > 0) aud.PlayOneShot(LandingSound, SoundValueRange);
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
                aud.PlayOneShot(JumpSound, SoundValueRange);
            }
        }
        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnime()
        {
            ani.SetBool(AnimatorPlayerWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            // 設定是否在地板上 動畫參數
            ani.SetBool(AnimatorPlayerOnTheGround, OnTheGround);
            // 如果按下跳躍鍵 就設定 跳躍觸發參數
            // 判斷式 只有一行敘述(只有一個分號) 可以省略 大括號
            if (keyJump) ani.SetTrigger(AnimatorPlayerJump);
        }

        #endregion

        #region 事件
        private void Start()
        {
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            rig = gameObject.GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
        }
        private void Update()
        {
            UpdateAnime();
            Jump();
        }
        private void FixedUpdate()
        {
            Move(speed);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.5f, 0.5f);
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
   