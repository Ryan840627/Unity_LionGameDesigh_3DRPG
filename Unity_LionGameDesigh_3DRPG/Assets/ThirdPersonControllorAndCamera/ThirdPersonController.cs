using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ryan
{
    /// <summary>
    /// �ĤT�H�ٱ��
    /// ���ʡB���D
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ���
        [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(0, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Range(0, 1000)]
        public int jumpheight = 250;
        [Header("�ˬd�a�����"), Tooltip("�T�{�H���O�_�b�a�O�W")]
        public bool OnTheGround;
        public Vector3 CheckGroundMove;
        [Range(0, 3)]
        public float CheckGroundRadius = 0.2f;
        [Header("�����ɮ�")]
        public AudioClip JumpSound;
        public AudioClip LandingSound;
        [Header("�ʵe�Ѽ�")]
        public string AnimatorPlayerWalk = "�����}��";
        public string AnimatorPlayerRun = "�]�B�}��";
        public string AnimatorPlayerHurt = "����Ĳ�o";
        public string AnimatorPlayerDie = "���`�}��";
        public string AnimatorPlayerJump = "���DĲ�o";
        public string AnimatorPlayerOnTheGround = "�O�_�b�a�O�W";
        [Header("���V�t��"), Range(0, 50)]
        public float speedLookAt = 2;
        #endregion
        #region ��� �p�H

        [Header("���a�C������")]
        public GameObject playerObject;
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;

        /// <summary>
        /// ��v�����O
        /// </summary>
        private ThirdPersonCamera thirdPersonCamera;
        #endregion

        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        private float SoundValueRange { get => Random.Range(0.3f, 0.7f); }
       

        #region ��k
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="moveSpeed">���ʳt��</param>
        private void Move(float moveSpeed)
        {
            //Vector.forward �@�ɮy�� �� �e��(����)
            //transform.forward ������ �� �e��(�ϰ�)
            rig.velocity = transform.forward * MoveInput("Vertical") * moveSpeed +
                           transform.right * MoveInput("Horizontal") * moveSpeed +
                           Vector3.up * rig.velocity.y;
        }
        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="axisName">�n���o���b�V�W��</param>
        /// <returns>���ʫ����</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        /// <returns>�O�_�I��a�O</returns>
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(
                transform.position +
                transform.right * CheckGroundMove.x +
                transform.up * CheckGroundMove.y +
                transform.forward * CheckGroundMove.z
                , CheckGroundRadius, 1 << 3);
            //�p�G �|�����a �åB ���a�I���}�C�j�� 0 �N ����@������
            if (!OnTheGround && hits.Length > 0) aud.PlayOneShot(LandingSound, SoundValueRange);
            OnTheGround = hits.Length > 0;
            // �Ǧ^ �I���}�C�ƶq>0 �A�u�n�I����w�ϼh����N�N��b�a���W
            return hits.Length > 0;
        }
        /// <summary>
        /// ���D
        /// </summary>
        private void Jump()
        {
            //�åB&&
            //�p�G�b �a���W �åB���U �ť��� �N ���D
            if (CheckGround() && keyJump)
            {
                rig.AddForce(transform.up * jumpheight);
                aud.PlayOneShot(JumpSound, SoundValueRange);
            }
        }
        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAnime()
        {
            ani.SetBool(AnimatorPlayerWalk, MoveInput("Vertical") != 0 || MoveInput("Horizontal") != 0);
            // �]�w�O�_�b�a�O�W �ʵe�Ѽ�
            ani.SetBool(AnimatorPlayerOnTheGround, OnTheGround);
            // �p�G���U���D�� �N�]�w ���DĲ�o�Ѽ�
            // �P�_�� �u���@��ԭz(�u���@�Ӥ���) �i�H�ٲ� �j�A��
            if (keyJump) ani.SetTrigger(AnimatorPlayerJump);
        }

        private void LookAtForward()
        {
            //�����b�V ������� �� �j�� 0.1 �N�B�z ���V
            if (Mathf.Abs(MoveInput("Vertical"))>0.1f)
            {
                // ���o�e�訤�� = �|��.���ۨ���(�e��y�� - �����y��)
                Quaternion angle = Quaternion.LookRotation(thirdPersonCamera.PosForward - transform.position);
                // �����󪺨��� = �|��.�t��
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, 0.5f*speedLookAt);
            }
            
        }

        #endregion
        
        #region �ƥ�
        private void Start()
        {
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            rig = gameObject.GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
            //��v�����O = �z�L�����M�䪫��<�x��>();
            //FindObjectOfType ���n��b Update ���ϥη|�y���j�q�į�t��
            thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();
        }
        private void Update()
        {
            UpdateAnime();
            Jump();
            LookAtForward();
        }
        private void FixedUpdate()
        {
            Move(speed);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawSphere(
                transform.position +
                transform.right * CheckGroundMove.x +
                transform.up * CheckGroundMove.y +
                transform.forward * CheckGroundMove.z
                , CheckGroundRadius);
        }
        #endregion
    }
}
   