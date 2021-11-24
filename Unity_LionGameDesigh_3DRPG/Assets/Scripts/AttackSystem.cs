using System.Collections;
using UnityEngine;


namespace Ryan
{
    /// <summary>
    /// �����t��
    /// ���a���������ť
    /// �����ϰ�B�����O�P�y���ˮ`
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        #region ��� : ���}
        [Header("�����O"), Range(0, 500)]
        public float attack = 20;
        [Header("�����N�o�ɶ�"), Range(0, 5)]
        public float timeAttack = 1.3f;
        [Header("����ǰe�ˮ`�ɶ�"), Range(0, 3)]
        public float delaySendDamage = 0.2f;
        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one;
        [Header("�����ʵe�Ѽ�")]
        public string parameterAttack = "�����ϼhĲ�o";
        #endregion
        #region ���:�p�H
        private Animator ani;
        private bool isAttack;
        #endregion
        #region �ݩ�:�p�H
        private bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }
        #endregion
        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
        }
        private void Update()
        {
            Attack();
        }
        #endregion
        #region ø�s�ϧ�
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.5f, 0.2f, 0.3f);
            //ø�s��ΡA�ݭn��ۨ������ɨϥ� matrix ���w���׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z
                , transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
        }
        #endregion
               
        #region ��k:�p�H
        private void Attack()
        {
            if (keyAttack  && !isAttack)
            {
                isAttack = true;
                ani.SetTrigger(parameterAttack);
                StartCoroutine(DelayHit());
            }
        }

        private IEnumerator DelayHit()
        {
            yield return new WaitForSeconds(delaySendDamage);

            Collider[] hit = Physics.OverlapBox(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z
                , v3AttackSize / 2, Quaternion.identity, 1 << 7);

            if (hit.Length > 0) hit[0].GetComponent<HurtSystem>().Hurt(attack);

            float waitToNextAttack = timeAttack - delaySendDamage;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }
        #endregion
        #region
        #endregion
    }
}
