using UnityEngine;
using UnityEngine.Events;

namespace Ryan.Dialogue 
{
    /// <summary>
    /// NPC系統
    /// 偵測目標是否進入對話範圍
    /// 並且開啟對話系統
    /// </summary>
    public class NPC : MonoBehaviour
    {
        #region 欄位、屬性
        [Header("對話資料")]
        public DataDialogue dataDialogue;
        [Header("相關資訊"),Range(0,10)]
        public float checkPlayerRadius = 1.5f;
        public GameObject goTip;
        private Transform target;
        [Header("面向速度"),Range(0, 10)]
        public float speedLookat = 3f;

        [Header("對話系統")]
        public DialogueSystem dialogueSystem;
        [Header("完成任務事件")]
        public UnityEvent onFinish;

        private bool startDialogueKey { get => Input.GetKeyDown(KeyCode.E); }
        /// <summary>
        /// 目前任務數量
        /// </summary>
        private int countCurrent;

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkPlayerRadius);
        }

        private bool checkPlayer()
        {
            Collider[] hits =  Physics.OverlapSphere(transform.position, checkPlayerRadius,1 << 6);
            if (hits.Length > 0) target = hits[0].transform;
            return hits.Length > 0;
        }
        private void LookatPlayer()
        {
            if (checkPlayer())
            {
                Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookat);
            }
        }
        /// <summary>
        /// 初始設定
        /// 狀態恢復為任務前
        /// </summary>
        private void Initialized()
        {
            dataDialogue.stateNPCMission = StateNPCMission.beforeMission;
        }
        /// <summary>
        /// 玩家進入範圍內 並解 按下指定按鍵 請對話系統執行 開始對話
        /// 玩家退出範圍外 停止對話
        /// 判斷狀態 : 任務前 任務中 任務後
        /// </summary>
        private void StartDialogue()
        {
            if (checkPlayer() && startDialogueKey)
            {
                dialogueSystem.Dialogue(dataDialogue);
                // 判斷 如果NPC 任務前 就把狀態改為 任務中
                if (dataDialogue.stateNPCMission == StateNPCMission.beforeMission) dataDialogue.stateNPCMission = StateNPCMission.missionning;
            }
            else if (!checkPlayer()) dialogueSystem.StopDialogue();
        }

        

        /// <summary>
        /// 更新任務需求數量
        /// 任務目標物件得到或死亡後處理
        /// </summary>
        public void UpdateMissionCount()
        {
            countCurrent++;

            //目前狀態 等於 需求數量 狀態 等於 完成任務
            if (countCurrent == dataDialogue.countNeed)
            {
                dataDialogue.stateNPCMission = StateNPCMission.afterMission;
                onFinish.Invoke();
            }
        }

        private void Awake()
        {
            Initialized();
        }
        private void Update()
        {
            goTip.SetActive(checkPlayer());
            LookatPlayer();
            StartDialogue();
        }
    }
}


