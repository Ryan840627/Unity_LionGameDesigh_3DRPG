using UnityEngine;
using UnityEngine.UI;
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

        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(switchDialogueGroup());       //啟動協同程序
            StartCoroutine(showDialogueContent(data));
        }

        private IEnumerator switchDialogueGroup()
        {
            for (int i = 0; i < 10; i++)                 //迴圈執行次數
            {
                groupDialogue.alpha += 0.1f;             //群組元件 透明度 遞增
                yield return new WaitForSeconds(0.03f);  //等待時間
            }
        }

        private IEnumerator showDialogueContent(DataDialogue data)
        {
            textContent.text = " ";
            textName.text = " ";
            for (int i = 0; i < data.beforeMission[0].Length; i++)
            {
                textContent.text += data.beforeMission[0][i];
                yield return new WaitForSeconds(dialogueInterval);
            }
        }
    }
}


