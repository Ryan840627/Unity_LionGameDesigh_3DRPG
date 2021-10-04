using UnityEngine;

namespace Ryan
{
    /// <summary>
    /// �ĤT�H����v���t��
    /// �l�ܫ��w�ؼ�
    /// �åB�i�H���k�B�W�U����(����)
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        #region ���
        [Header("�ؼЪ���")]
        public Transform target;
        [Header("�l�ܳt��"),Range(0,500)]
        public float speedTrack = 0.8f;
        [Header("���४�k�t��"), Range(0, 500)]
        public float speedTurnHorizontal = 160;
        [Header("����W�U�t��"), Range(0, 500)]
        public float speedTurnVertical = 160;
        #endregion

        #region �ݩ�
        /// <summary>
        /// ���o�ƹ������B�����y��
        /// </summary>
        public float inputMouseX { get => Input.GetAxis("Mouse X"); }
        public float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region ��k
        /// <summary>
        /// �l�ܪ���
        /// </summary>
        private void TrackTarget()
        {
            Vector3 posTarget = target.position;                          //���o �ؼ� �y��
            Vector3 posCamera = transform.position;                       //���o ��v�� �y��

            posCamera = Vector3.Lerp(posCamera, posTarget, speedTrack * Time.deltaTime);   //��v���y�� = �t�� (�t�� * �@�V���ɶ� )

            transform.position = posCamera;                               //�����󪺮y�� = ��v���y��
        }

        private void TurnCarema()
        {
            transform.Rotate(inputMouseY * Time.deltaTime * speedTurnVertical, inputMouseX * Time.deltaTime * speedTurnHorizontal, 0);
        }
        #endregion

        #region �ƥ�
        // �b Update ������� �B�z��v���l�ܦ欰
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
