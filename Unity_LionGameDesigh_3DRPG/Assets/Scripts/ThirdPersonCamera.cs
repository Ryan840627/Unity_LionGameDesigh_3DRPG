using UnityEngine;

namespace Ryan
{
    /// <summary>
    /// 第三人稱攝影機系統
    /// 追蹤指定目標
    /// 並且可以左右、上下旋轉(限制)
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;
        [Header("追蹤速度"),Range(0,500)]
        public float speedTrack = 0.8f;
        [Header("旋轉左右速度"), Range(0, 500)]
        public float speedTurnHorizontal = 160;
        [Header("旋轉上下速度"), Range(0, 500)]
        public float speedTurnVertical = 160;
        #endregion

        #region 屬性
        /// <summary>
        /// 取得滑鼠水平、垂直座標
        /// </summary>
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region 方法
        /// <summary>
        /// 追蹤物標
        /// </summary>
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;                          //取得 目標 座標
            Vector3 posCamera = transform.position;                       //取得 攝影機 座標

            posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);   //攝影機座標 = 差值 (速度 * 一幀的時間 )

            transform.position = posCamera;                               //此物件的座標 = 攝影機座標
        }

        private void TurnCarema()
        {
            transform.Rotate(inputMouseY * Time.deltaTime * speedTurnVertical, inputMouseX * Time.deltaTime * speedTurnHorizontal, 0);
        }
        #endregion

        #region 事件
        // 在 Update 之後執行 處理攝影機追蹤行為
        private void LateUpdate()
        {
            TrackTarget();
        }
        private void Update()
        {
            TurnCarema();

        }
        #endregion


    }

}
