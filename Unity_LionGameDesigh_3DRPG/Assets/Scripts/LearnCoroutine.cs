using UnityEngine;
using System.Collections; //�ޥ� �t�� ���X �R�W�Ŷ� ��P�{�� API

namespace Ryan.Practice
{
    /// <summary>
    /// �{�Ѩ�P�{�� Courtine
    /// </summary>
    public class LearnCoroutine : MonoBehaviour
    {
        // �w�q��P�{�Ǥ�k
        // IEnumerator ����P�{�ǶǦ^�ȡA�i�Ǧ^�ɶ�
        // yield ���B
        // new WaitForSeconds(�B�I��) -���ݮɶ�
        private IEnumerator TestCourtine()
        {
            print("��P�{�Ƕ}�l����");
            yield return new WaitForSeconds(2);
            print("��P�{�ǵ��ݨ�����榹��");
        }
        public Transform sphere;

        private IEnumerator SphereScale()
        {
            for (int i = 0; i < 10; i++)
            {
                sphere.localScale += Vector3.one;
                yield return new WaitForSeconds(1);
            }
        }

        private void Start()
        {
            // �Ұʨ�P�{��
            StartCoroutine(TestCourtine());
            StartCoroutine(SphereScale());
        }
    }
}


