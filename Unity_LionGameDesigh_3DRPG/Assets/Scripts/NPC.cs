using UnityEngine;

namespace Ryan.Dialogue 
{
    /// <summary>
    /// NPC�t��
    /// �����ؼЬO�_�i�J��ܽd��
    /// �åB�}�ҹ�ܨt��
    /// </summary>
    public class NPC : MonoBehaviour
    {
        #region ���B�ݩ�
        [Header("��ܸ��")]
        public DataDialogue dataDialogue;
        [Header("������T"),Range(0,10)]
        public float checkPlayerRadius = 1.5f;
        public GameObject goTip;
        private Transform target;
        [Header("���V�t��"),Range(0, 10)]
        public float speedLookat = 3f;

        [Header("��ܨt��")]
        public DialogueSystem dialogueSystem;

        private bool startDialogueKey { get => Input.GetKeyDown(KeyCode.E); }
        /// <summary>
        /// �ثe���ȼƶq
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
        /// ���a�i�J�d�� �ø� ���U���w���� �й�ܨt�ΰ��� �}�l���
        /// ���a�h�X�d��~ ������
        /// </summary>
        private void StartDialogue()
        {
            if (checkPlayer() && startDialogueKey)
            {
                dialogueSystem.Dialogue(dataDialogue);
            }
            else if (!checkPlayer()) dialogueSystem.StopDialogue();
        }

        

        /// <summary>
        /// ��s���ȻݨD�ƶq
        /// ���ȥؼЪ���o��Φ��`��B�z
        /// </summary>
        public void UpdateMissionCount()
        {
            countCurrent++;

            //�ثe���A ���� �ݨD�ƶq ���A ���� ��������
            if (countCurrent == dataDialogue.countNeed) dataDialogue.stateNPCMission = StateNPCMission.afterMission;
        }


        private void Update()
        {
            goTip.SetActive(checkPlayer());
            LookatPlayer();
            StartDialogue();
        }
    }
}


