using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
        [Header("��ܫ���")]
        public KeyCode dialogueKey = KeyCode.A;
        [Header("���r�ƥ�")]
        public UnityEvent onType;

        /// <summary>
        /// �}�l���
        /// </summary>
        /// <param name="data"></param>
        public void Dialogue(DataDialogue data)
        {
            StopAllCoroutines();
            StartCoroutine(switchDialogueGroup());       //�Ұʨ�P�{��
            StartCoroutine(showDialogueContent(data));
        }
        /// <summary>
        /// ������ : ������ܥ\��B�����H�X
        /// </summary>
        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(switchDialogueGroup(false));
        }
        /// <summary>
        /// ������ܮظs�ժ���
        /// </summary>
        /// <param name="fadeIn">�O�_�H�J : true �H�J �B false �H�X </param>
        private IEnumerator switchDialogueGroup(bool fadeIn = true)
        {
            //�T���B��l
            //�y�k : ���L�� ? true ���G : false ���G ;
            //�z�L���L�ȨM�w�n�W�[���ȡA true �W�[0.1  false ���0.1
            float increace = fadeIn ? 0.1f : -0.1f; 

            for (int i = 0; i < 10; i++)                 //�j����榸��
            {
                groupDialogue.alpha += increace;             //�s�դ��� �z���� ���W
                yield return new WaitForSeconds(0.03f);  //���ݮɶ�
            }
        }
        /// <summary>
        /// ��ܹ�ܤ��e
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IEnumerator showDialogueContent(DataDialogue data)
        {         
            textName.text = " ";             //�M����ܪ�
            textName.text = data.nameDialogue; //��s��ܪ�
            goTriangle.SetActive(false);     //���� ���ܹϥ�

            string[] dialogueContents = { };    //�x�s ��ܤ��e ���ŭ�
            #region �B�z���A�P��ܸ��
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
            //�M�M�C�@�q���
            for (int j = 0; j < dialogueContents.Length; j++)
            {
                textContent.text = " ";      //�M����ܤ��e
                //�M�M��ܨC�@�Ӧr
                for (int i = 0; i < dialogueContents[j].Length; i++)
                {
                    onType.Invoke();
                    textContent.text += dialogueContents[j][i];
                    yield return new WaitForSeconds(dialogueInterval);
                }

                goTriangle.SetActive(true);   //���� ���ܹϥ�
                //���򵥫� ��J ��ܪ����� null ���ݤ@�Ӽv�檺�ɶ�
                while (!Input.GetKeyDown(dialogueKey)) yield return null;                               
            }
            StartCoroutine(switchDialogueGroup(false));   //�H�X
        }
    }
}


