using UnityEngine;

/// <summary>
/// �{��API : �D�R�A Non Static
/// </summary>
public class APINonStatic : MonoBehaviour
{
    public Transform tral; //�׹��� �n�s���D�R�A���O ���W��
    public Camera cam;
    public Light lig;

    private void Start()
    {
        #region �D�R�A�ݩ�
        //�P�R�A�t��
        // 1. �ݭn���骫��
        // 2. ���o���骫�� - �w�q���ñN�n�s��������s�J���
        // 3. �C������(���h���O�U���O)�B����(�ݩʭ��O�U)�����s�b������
        //���o Get
        //�y�k : ���W�١A�D�R�A���O
        print("��v���y�� : " + tral.position);
        print("��v���`�� : " + cam.depth);

        //�]�w
        //�y�k : ���W�١A�D�R�A�ݩ� ���w ��
        tral.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region �D�R�A��k
        //�I�s
        //�y�k : 
        //���W�١A�D�R�A��k�W��(�����޼�);
        lig.Reset();
        #endregion
    }
}
