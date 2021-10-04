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
        [Header("X�b�W�U���୭�� : �̤p�P�̤j��")]
        public Vector2 limitAngleX = new Vector2(-0.3f, 0.3f);
        [Header("��v���b����e�誺�W�U���୭�� : �̤p�P�̤j��")]
        public Vector2 limitAngleFormForward = new Vector2(-0.2f, 0f);
        /// <summary>
        /// ��v���e��y��
        /// </summary>
        private Vector3 posForward;
        /// <summary>
        /// �e�誺����
        /// </summary>
        private float lengthForward = 1;


        #endregion

        #region �ݩ�
        /// <summary>
        /// ���o�ƹ������B�����y��
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

        private void LimitAngleXAndZFormTarget()
        {
            //print(transform.rotation);  //�����v�����׳]�w
            Quaternion angle = transform.rotation;
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);
            angle.z = Mathf.Clamp(angle.z, limitAngleFormForward.x, limitAngleFormForward.y);
            transform.rotation = angle;
        }
        /// <summary>
        /// �ᵲ���� Z ��0
        /// </summary>
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = 0;
            transform.eulerAngles = angle;
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
            LimitAngleXAndZFormTarget();
            FreezeAngleZ();
        }

        //�b�����ɤ��|���檺�ƥ�
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.2f, 0, 1, 0.3f);
            //�e��y�� = ������y�� + ������e�� * ����
            posForward = transform.position + transform.forward * lengthForward;
            //�e��y��.y = �ؼ�.�y��.y(���e�谪�ת��y�ЬۦP)
            posForward.y = target.position.y;
            Gizmos.DrawSphere(posForward, 0.15f);
        }


        #endregion


    }

}
