using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Ryan.Enemy 
{ 
    /// <summary>
    /// �ĤH�欰
    /// �ĤH���A : ���ݡB�����B�l�ܡB�����B���ˡB���`
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        #region ���}���
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��: �l�ܻP����")]
        [Range(0,7)]
        public float rangeAttack = 5f;
        [Range(0, 20)]
        public float rangeTrack = 15f;
        [Header("�����H�����")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H�����")]
        public Vector2 v2RandomWalk = new Vector2(3f, 7f);
        [Header("�����ɶ�"), Range(0, 5)]
        public float timeAttack = 2.5f;
        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("��������ǰe�ˮ`�ɶ�"), Range(0, 5)]
        public float delaySendDamage = 0.5f;
        #endregion

        #region �p�H���
        private Transform traPlayer;
        private string namePlayer = "����k";
        private Animator ani;
        private NavMeshAgent nma;
        private string parameterIdleWalk = "�����}��";
        private string parameterAttack = "����Ĳ�o";
        [SerializeField]      // �ǦC����� : ��ܨp�H���
        private StateEmeny state;
        private bool isIdle;
        private bool isWalk;
        private bool isTrack;
        private bool isAttack;
        //�H���樫�y��
        private Vector3 v3RandomWalk { get => Random.insideUnitSphere * rangeTrack + transform.position; }
        //�̲׮y�� : �z�LAPI ���o���椺�i���쪺��m
        private Vector3 v3RandomWalkFinal;
        //���a�O�_�b�l�ܽd��
        private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 6).Length > 0; }
        #endregion

        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            #region �����B�l�ܡB�H���樫

            
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeAttack);

            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, rangeTrack);

            if (state == StateEmeny.Walk)
            {
                Gizmos.color = new Color(0, 0.2f, 1, 0.3f);
                Gizmos.DrawSphere(v3RandomWalkFinal, 0.3f);
            }
            #endregion
            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(0, 0.5f, 0.5f, 0.3f);
            //ø�s��ΡA�ݭn��ۨ������ɨϥ� matrix ���w���׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z
                , transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = speed;
            traPlayer = GameObject.Find(namePlayer).transform;

            nma.SetDestination(transform.position);                 //������ �@�}�l�N���Ұ� (���M�i�঳BUG)
        }
        private void Update()
        {
            StateManager();
        }
        #endregion

        #region ��k : �p�H
        /// <summary>
        /// ���A�޲z
        /// </summary>
        private void StateManager()
        {
            switch (state)
            {
                case StateEmeny.Idle:
                    Idle();
                    break;
                case StateEmeny.Walk:
                    Walk();
                    break;
                case StateEmeny.Track:
                    Track();
                    break;
                case StateEmeny.Attack:
                    Attack();
                    break;
                case StateEmeny.Hurt:
                    break;
                case StateEmeny.Dead:
                    break;
                default:
                    break;
            }
        }        
        /// <summary>
        /// ���� : �H����ƫ�i�樫�����A
        /// </summary>
        private void Idle()
        {
            if (playerInTrackRange) state = StateEmeny.Track;             //�p�G ���a�i�J �l�ܽd�� �N�������l�ܪ��A  
            //isIdle�i�J����
            if (isIdle) return;
            isIdle = true;
            ani.SetBool(parameterIdleWalk, false);
            StartCoroutine(IdleEffect());
        }
        /// <summary>
        /// ���ݮĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator IdleEffect()
        {
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);

            state = StateEmeny.Walk;    //�i�J�������A
            //isIdle�X�h����
            isIdle = false;
        }
        /// <summary>
        /// ���� : �H����ƫ�i�J���ݪ��A
        /// </summary>
        private void Walk()
        {
            if (playerInTrackRange) state = StateEmeny.Track;             //�p�G ���a�i�J �l�ܽd�� �N�������l�ܪ��A 

            nma.SetDestination(v3RandomWalkFinal);                         //�N�z���A�]�w�ت��a(�y��)                     
            ani.SetBool(parameterIdleWalk, nma.remainingDistance > 0.1);   //�����ʵe - ���ت��a�Z���j��0.1�ɨ���

            //isWalk�i�J����
            if (isWalk) return;
            isWalk = true;
            print("�H���y�� : " + v3RandomWalk);

            // �ϥ� NavMesh.SamplePosition �ӴM��i���쪺�y��
            NavMeshHit hit;                                                                 //��������I��
            NavMesh.SamplePosition(v3RandomWalk, out hit, rangeTrack, NavMesh.AllAreas);    //��������.���o�y��(�H���y�СB�I����T�B�b�|�B�ϰ�) - ���椺�i�樫���y��
            v3RandomWalkFinal = hit.position;                                               //�̲׮y�� = �I����T �� �y��
            
            StartCoroutine(WalkEffect());
        }
        /// <summary>
        /// �����ĪG
        /// </summary>
        /// <returns></returns>
        private IEnumerator WalkEffect()
        {
            float randomWalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomWalk);

            state = StateEmeny.Idle;    //�i�J���ݪ��A
            //isWalk�X�h����
            isWalk = false;
        }        
        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Track()
        {
            if (!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;

            nma.isStopped = false;                                  //������ �Ұ�
            nma.SetDestination(traPlayer.position);
            ani.SetBool(parameterIdleWalk, true);
            if (nma.remainingDistance <= rangeAttack) state = StateEmeny.Attack;          //�ζZ���P�_ �O�_�i�J�������A             
        }
        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            nma.isStopped = true;                                   //������ ����
            ani.SetBool(parameterIdleWalk, false);                  //�����
            nma.SetDestination(traPlayer.position);
            LookAtPlayer();
            if (nma.remainingDistance > rangeAttack) state = StateEmeny.Track;

            if (isAttack) return;                                   //�p�G ���b������ �N���X(�קK���Ƨ���)

            isAttack = true;                                        //���b������
            
            ani.SetTrigger(parameterAttack);

            StartCoroutine(DelaySendDamageToTarget());              //�Ұʩ���ǰe�ˮ`���ؼШ�{
        }
        /// <summary>
        /// ����ǰe�ˮ`���ؼ�
        /// </summary>
        /// <returns></returns>
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            //���z ��θI��(�����I�B�@�b�ؤo�B���סB�ϼh)
            Collider[] hits = Physics.OverlapBox(transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                v3AttackSize / 2, Quaternion.identity, 1 << 6);

            if (hits.Length > 0) hits[0].GetComponent<HurtSystem>().Hurt(attack);

            float waitToNextAttack = timeAttack - delaySendDamage;              
           yield return new WaitForSeconds(waitToNextAttack);

            isAttack = false;
        }
        #endregion        
        [Header("���۪��a�t��")]
        public float speedLookAt = 10;
        /// <summary>
        /// ���V���a
        /// </summary>
        private void LookAtPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
            ani.SetBool(parameterIdleWalk, transform.rotation != angle);
        }
    }
}