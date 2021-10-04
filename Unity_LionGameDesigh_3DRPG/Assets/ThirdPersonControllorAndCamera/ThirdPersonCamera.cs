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
        [Header("X軸上下旋轉限制 : 最小與最大值")]
        public Vector2 limitAngleX = new Vector2(-0.3f, 0.3f);
        [Header("攝影機在角色前方的上下旋轉限制 : 最小與最大值")]
        public Vector2 limitAngleFormForward = new Vector2(-0.2f, 0f);
        /// <summary>
        /// 攝影機前方座標
        /// </summary>
        private Vector3 posForward;
        /// <summary>
        /// 前方的長度
        /// </summary>
        private float lengthForward = 1;


        #endregion

        #region 屬性
        /// <summary>
        /// 取得滑鼠水平、垂直座標
        /// </summary>
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }

        public Vector3 PosForward 
        {
            get
            {
                posForward = transform.position + transform.forward * lengthForward;
                posForward.y = target.position.y;
                return posForward;
            }
        }
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

        private void LimitAngleXAndZFormTarget()
        {
            //print(transform.rotation);  //顯示攝影機角度設定
            Quaternion angle = transform.rotation;
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);
            angle.z = Mathf.Clamp(angle.z, limitAngleFormForward.x, limitAngleFormForward.y);
            transform.rotation = angle;
        }
        /// <summary>
        /// 凍結角度 Z 為0
        /// </summary>
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = 0;
            transform.eulerAngles = angle;
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
            LimitAngleXAndZFormTarget();
            FreezeAngleZ();
        }

        //在執行檔不會執行的事件
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0, 1, 0.3f);
            //前方座標 = 此物件座標 + 此物件前方 * 長度
            posForward = transform.position + transform.forward * lengthForward;
            //前方座標.y = 目標.座標.y(讓前方高度的座標相同)
            posForward.y = target.position.y;
            Gizmos.DrawSphere(posForward, 0.15f);
        }


        #endregion


    }

}
