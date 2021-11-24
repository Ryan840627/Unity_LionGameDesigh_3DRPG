using UnityEngine;
using UnityEngine.Events;

namespace Ryan 
{ 
    /// <summary>
    /// ���˨t��
    /// �B�z��q�B���˻P���`
    /// </summary>
    public class HurtSystem : MonoBehaviour
    {
        #region ���G���}
        [Header("��q"), Range(0, 5000)]
        public float hp = 100f;
        [Header("���˨ƥ�")]
        public UnityEvent onHurt;
        [Header("���`�ƥ�")]
        public UnityEvent onDead;
        [Header("�ʵe�Ѽ�:���˻P���`")]
        public string parameterHurt = "����Ĳ�o";
        public string parameterDead = "���`�}��";
        #endregion
        #region�@���G�p�H�P�O�@
        private Animator ani;

        //�p�H�����\�l���O�s��
        //���}���\�Ҧ����O�s��
        //protected �O�@ �ȭ��l���O�s��
        protected float hpMax;
        #endregion
        #region�@�ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            hpMax = hp;
        }
        private void Update()
        {
            
        }
        #endregion
        #region ��k : ���}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="damage">�����쪺�ˮ`</param>
        //�����Ʊ�Q�l���O�мg�����[�W virtual ����
        public virtual bool Hurt(float damage)
        {
            if (ani.GetBool(parameterDead)) return true;  //�p�G ���`�ѼƤw�g�Ŀ� �N���X
            hp -= damage;
            ani.SetTrigger(parameterHurt);
            onHurt.Invoke();
            if (hp <= 0)
            {
                Dead();
                return ani.GetBool(parameterDead);
            }
            else return false;
        }
        #endregion
        #region ��k : �p�H
        /// <summary>
        /// ���`
        /// </summary>
        private void Dead()
        {
            ani.SetBool(parameterDead, true);
            onDead.Invoke();
        }
        #endregion
    }
}