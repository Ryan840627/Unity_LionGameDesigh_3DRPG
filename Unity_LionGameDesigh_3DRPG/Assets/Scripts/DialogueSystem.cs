using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ryan.Dialogue
{
    /// <summary>
    /// ��ܨt��
    /// ��ܹ�ܮءB��ܤ��e���r�ĪG
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("��ܨt�Ωһݪ���������")]
        public CanvasGroup groupDialogue;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("��ܶ��j"), Range(0, 10)]
        public float dialogueInterval = 0.3f;

        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(switchDialogueGroup());       //�Ұʨ�P�{��
            StartCoroutine(showDialogueContent(data));
        }

        private IEnumerator switchDialogueGroup()
        {
            for (int i = 0; i < 10; i++)                 //�j����榸��
            {
                groupDialogue.alpha += 0.1f;             //�s�դ��� �z���� ���W
                yield return new WaitForSeconds(0.03f);  //���ݮɶ�
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


