using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ryan
{
    /// <summary>
    /// �]�t���������˨t��
    /// �i�H�B�z�����s
    /// </summary>
    public class HurtSystemWithUI : HurtSystem
    {
        [Header("�n��s�����")]
        public Image imgHp;

        /// <summary>
        /// ����ĪG�M�Ϊ��������q
        /// </summary>
        private float hpEffectOriginal;

        //�мg�����O���� override
        public override void Hurt(float damage)
        {
            hpEffectOriginal = hp;

            // base �Ӧ����������O�� �����O�������e
            base.Hurt(damage);

            StartCoroutine(HpBarEffect());
        }

        /// <summary>
        /// ����ĪG
        /// </summary>
        private IEnumerator HpBarEffect()
        {
            while (hpEffectOriginal !=hp)                           //�� ����e��q �������q
            {
                hpEffectOriginal--;                                 //����
                imgHp.fillAmount = hpEffectOriginal / hpMax;        //��s���
                yield return new WaitForSeconds(0.01f);             //����
            }
        }
    }
}

