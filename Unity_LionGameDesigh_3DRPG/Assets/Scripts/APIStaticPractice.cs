using UnityEngine;

/// <summary>
/// �R�A�ݩʻP��k API �Ұ�m��
/// </summary>
public class APIStaticPractice : MonoBehaviour
{
    
    void Start()
    {
        int cameraCount  = Camera.allCamerasCount;
        print("�Ҧ���v���ƶq : " + cameraCount);

        Vector2 gravity = Physics2D.gravity;
        print("2D �����O�j�p : " + gravity);
        
        Physics2D.gravity = new Vector2(0, -20);
        Vector2 gravity2D = Physics2D.gravity;
        print("2D �����O�]�w��Y = -20 : " + gravity2D);

        Time.timeScale = 0.5f;

        float pi= Mathf.PI;

        print("��P�v : " + pi);

        int number = Mathf.FloorToInt(9.999f);
        print("�� 9.999 �h�p���I : " + number);

        Vector3 a = new Vector3(1, 1, 1);
        Vector3 b = new Vector3(22, 22, 22);
        float distace = Vector3.Distance(a,  b);
        print("���o���I���Z�� : " + distace);

        Application.OpenURL("https://unity.com/");
    }

    
    void Update()
    {
        bool anykey =  Input.anyKey;
        print("�O�_��J���N�� : " + anykey);

        float timePass = Time.timeSinceLevelLoad;
        print("�C���g�L�ɶ� : " + timePass);

        if (Input.GetKeyDown("space"))
        {
            print("�O�_���U����(���w���ť���)");
        }

    }
}
