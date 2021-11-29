using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


namespace Ryan.Dialogue
{
    /// <summary>
    /// 對話系統
    /// 顯示對話框、對話內容打字效果
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("對話系統所需的介面物件")]
        public CanvasGroup groupDialogue;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.3f;
        [Header("對話按鍵")]
        public KeyCode dialogueKey = KeyCode.A;
        [Header("打字事件")]
        public UnityEvent onType;

        /// <summary>
        /// 開始對話
        /// </summary>
        /// <param name="data"></param>
        public void Dialogue(DataDialogue data)
        {
            StopAllCoroutines();
            StartCoroutine(switchDialogueGroup());       //啟動協同程序
            StartCoroutine(showDialogueContent(data));
        }
        /// <summary>
        /// 停止對話 : 關閉對話功能、介面淡出
        /// </summary>
        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(switchDialogueGroup(false));
        }
        /// <summary>
        /// 切換對話框群組物件
        /// </summary>
        /// <param name="fadeIn">是否淡入 : true 淡入 、 false 淡出 </param>
        private IEnumerator switchDialogueGroup(bool fadeIn = true)
        {
            //三元運算子
            //語法 : 布林值 ? true 結果 : false 結果 ;
            //透過布林值決定要增加的值， true 增加0.1  false 減少0.1
            float increace = fadeIn ? 0.1f : -0.1f; 

            for (int i = 0; i < 10; i++)                 //迴圈執行次數
            {
                groupDialogue.alpha += increace;             //群組元件 透明度 遞增
                yield return new WaitForSeconds(0.03f);  //等待時間
            }
        }
        /// <summary>
        /// 顯示對話內容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator showDialogueContent(DataDialogue data)
        {         
            textName.text = " ";             //清除對話者
            textName.text = data.nameDialogue; //更新對話者
            goTriangle.SetActive(false);     //隱藏 提示圖示

            string[] dialogueContents = { };    //儲存 對話內容 為空值
            #region 處理狀態與對話資料
            switch (data.stateNPCMission)
            {
                case StateNPCMission.beforeMission:
                    dialogueContents = data.beforeMission;
                    break;
                case StateNPCMission.missionning:
                    dialogueContents = data.missionning;
                    break;
                case StateNPCMission.afterMission:
                    dialogueContents = data.afterMission;
                    break;
            }
            #endregion
            //遍尋每一段對話
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = " ";      //清除對話內容
                //遍尋對話每一個字
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
                    onType.Invoke();
                    textContent.text += dialogueContents[j][i];
                    yield return new WaitForSeconds(dialogueInterval);
                }

                goTriangle.SetActive(true);   //隱藏 提示圖示
                //持續等待 輸入 對話的按鍵 null 等待一個影格的時間
                while (!Input.GetKeyDown(dialogueKey)) yield return null;                               
            }
            StartCoroutine(switchDialogueGroup(false));   //淡出
        }
    }
}


