using UnityEngine;

/// <summary>
/// �{��API : �R�AStatic
/// </summary>
public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        #region �R�A�ݩ�
        //�P�D�R�A�t��
        //1.���ݭn���骫��
        //���o
        //�y�k
        //���O�W�١A�R�A�ݩ�
        float r = Random.value;
        print("���o�R�A�ݩ��H���ȡA�H���� : " + r);

        //�]�wSet
        //�y�k:
        //���O�W�١A�R�A�ݩ� ���w ��
        // �u�n�ݨ� Read Only �N����]�w
        Cursor.visible = true;
        // Random.value = 99.9f; �߿W�ݩʤ���]�w
        #endregion

        #region �R�A��k
        // �I�s�B�ѼơB�Ǧ^��
        // ñ�� : �ѼơB�Ǧ^��
        //�y�k:
        //���O�W�١A�R�A��k(�����޼�)
        float range =  Random.Range(10.5f, 20.9f);
        print("�H���d�� 10.5 ~ 20.9 : " + range);

        //API �����ܭ��n : �ϥξ�Ʈɤ��]�t�̤j��
        int rangeint = Random.Range(1, 3);
        print("�H���d�� 1 ~ 3 : " + rangeint);
        #endregion
    }

    private void Update()
    {
        #region �R�A�ݩ�
        // print("�g�L�h�[ :" + Time.timeSinceLevelLoad);
        #endregion

        #region �R�A��k
        float h = Input.GetAxis("Horizontal");
        print("������ : " + h);
        #endregion
    }
}
