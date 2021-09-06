using UnityEngine;       // �ޥ�Unity API (�ܮw-��ƻP�\��)
using UnityEngine.Video; // �ޥ� �v�� API

// �׹��� ���O  ���O�W�� : �~�����O
// MonoBehaviour : Unity �����O�A�n���b����W�@�w�n�~��
// �~�ӫ�|�ɦ������O������
// �b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
// ���Φ���:���Field�B�ݩ�Property(�ܼ�)�B��kMethod�B�ƥ�Event
/// <summary>
/// �ĤT�H�ٱ��
/// ���ʡB���D
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region ���Field
    // �x�s�C���Y���A�Ҧp:���ʳt�סB���D���׵���...
    // �`�Υ|�j����:��� int�B�B�I�� float�B�r�� string�B���L�� bool
    // ���y�k:�׹��� ������� ���W�� (���w �w�]��) ����
    // �׹���: 
    // 1.���} public  - ���\�Ҧ����O�s�� - ��ܦb�ݩʭ��O�W - �ݭn�վ㪺��Ƴ]�w�����}
    // 2.�p�H private - �T��Ҧ����O�s�� - ���æb�ݩʭ��O�W - �w�]��
    // �� Unity �H�ݩʭ��O��Ƭ��D
    // �� ��_�{���w�]�ȽЫ� ... > Reset
    // ����ݩ� Attribute : ���U�����
    // ����ݩʻy�k : [�ݩʦW��(�ݩʭ�)]
    // Header ���D
    // Tooltip ����:�ƹ����d�b���W�٤W�|��ܼu�X����
    // Range �d��:�i�ϥΦb�ƭ�������ƤW�A�Ҧp : int,float
    [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(0, 500)]
    public float speed = 10.5f;
    [Header("���D����"), Range(0, 1000)]
    public int jumpheight = 100;
    [Header("�ˬd�a�����"), Tooltip("�T�{�H���O�_�b�a�O�W")]
    public bool OnTheGorund = false;
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

    private AudioSource aud;
    private Rigidbody rid;
    private Animator ani;

    #region Unity �������
    /**�m��Unity �������
    // �C�� Color
    public Color color;
    public Color white = Color.white;                          //�����C��
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f,0f,0.5f);             //�ۭq�C��RGB
    public Color color2 = new Color(0.5f, 0.5f, 0.5f,0.5f);    //�ۭq�C��RGBA

    //�y�� Vector 2 - 4
    public Vector2 v2;
    public Vector2 v2uRight = Vector2.right;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2One = Vector2.one;
    public Vector2 V2Costom = new Vector2(10.5f, 21.5f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector4 v4 = new Vector4(1,2,3,4);

    // ���� �C�|��� enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump= KeyCode.Space;

    // �C���������
    public AudioClip sound;    // ���� mp3 , ogg , wav
    public VideoClip video;    // �v�� mp4
    public Sprite sprite;      // �Ϥ� png , jpeg - ���䴩 gif
    public Texture texture2D;  // 2D �Ϥ� png , jpeg
    public Material material;  // ����y
    [Header("����")]
    // ���� Component : �ݩʭ��O�W�i�H�P�|��
    public Transform tra;
    public Animation anuOld;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    // ���L�C
    // 1. ��ĳ���n�ϥΦ��W��
    // 2. �ϥιL��API
    */
    #endregion

    #endregion

    #region �ݩ�Property
    /**�ݩʽm��
    // �ݩʤ��|��ܦb���O�W
    // �x�s��ơA�P���ۦP
    // �t���b�� : �i�H�]�w�s���v�� Get set
    // �ݩʻy�k : �׹��� ������� �ݩʦW�� { ��:�s: }
    public int ReadAndWrite { get; set; }
    // ��Ū�ݩ� : �u����o get
    public int read { get;}
    // ��Ū�ݩ� : �z�L get �]�w�w�]�ȡA����rreturn���Ǧ^��
    public int readValue 
    {
        get
        {
            return 100;
        }
    }
    // �߼g�ݩ� : �T��A�����n��get
    // public int write { set; }
    // value �����O���w����
    private int _hp;
    public int hp 
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        } 
    }
    

    public int MyProperty { get; set; }
    */

    public KeyCode keyJump { get; }

    #endregion

    #region �m�ߤ�kMethod
    /*
    // �w�q�P��@�������{�����϶��B�\��
    // ��k�y�k:�׹��� �Ǧ^������� ��k�W�� (�Ѽ�1, ...�Ѽ�N) {�{���϶�}
    // �`�ζǦ^����:�L�Ǧ^  void - ����k�S���Ǧ^���
    // �榡��: ctrl + K D
    // �ۭq��k:
    // �ۭq��k�ݭn�Q�I�s�~�|�����k�����{��
    // �W���C�⬰�H���� - �S���Q�I�s
    // �W���C�⬰�G���� - ���Q�I�s
    private void Test()
    {
        print("�ڬO�ۭq��k");
    }
    private int ReturnJump()
    {
        return 999;
    }

    // �Ѽƻy�k:������� �ѼƦW�� ���w �w�]��
    // ���w�]�Ȫ��Ѽƥi�H����J�޼ơA��񦡰Ѽ�
    // ����񦡰Ѽƥu���b()�k��
    private void Skill(int damage, string effect = "�ǹЯS��",string sound = "�I�I�I")
    {
        print("�Ѽƪ��� - �ˮ`��: " + damage);
        print("�Ѽƪ��� - �ޯ�S��:"+ effect);
        print("�Ѽƪ��� - �n���S��:" + sound);
    }

    // ��Ӳ�:���ϥΰѼ�
    // ���C���@�P�X�R��
    private void skill100()
    {
        print("�ˮ`��: " + 100);
        print("�ޯ�S��:");
    }
    private void skill150()
    {
        print("�ˮ`��: " + 150);
        print("�ޯ�S��:");
    }
    private void skill200()
    {
        print("�ˮ`��: " + 200);
        print("�ޯ�S��:");
    }

    // ���D���n���ܭ��n
    // BMI = �魫 / ���� * ����
    /// <summary>
    /// �p��BMI��k
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="height"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    private float BMI(float weight, float height, string name = "����")
    {
        print(name + " ��BMI");
        return weight / (height * height);
    }
    */
    #endregion

    // ���| Ctrl M O
    // �i�} Ctrl M L
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="moveSpeed">���ʳt��</param>
    private void Move(float moveSpeed) 
    { 

    }
    /// <summary>
    /// ���ʫ����J
    /// </summary>
    /// <returns>���ʫ����</returns>
    private float MoveKeyInput() 
    {
        return 0f;
    }
    /// <summary>
    /// �ˬd�a�O
    /// </summary>
    /// <returns>�O�_�I��a�O</returns>
    private bool CheckGround() 
    {
        return false;
    } 
    /// <summary>
    /// ���D
    /// </summary>
    private void Jump() 
    {

    }
    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void UpdateAnime()
    {

    }




    

    #region �ƥ�Event
    // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start ���� Console Main
    // �}�l�ƥ� : �C���}�l�ɰ���@��
    private void Start()
    {

        #region ��X��k
        // ��X��k
        //print("HELLOWORLD!");

        //Debug.Log("�@��T��");
        //Debug.LogWarning("ĵ�i�T��");
        //Debug.LogError("���~�T��");
        #endregion

        #region �ݩʽm��
        /*�ݩʽm��
        // ���P�ݩ� ���oGet �B�]�wSet
        print("�����  - ���ʳt�� : " + speed);
        print("�ݩʸ��  - Ū�g�ݩ� : " + ReadAndWrite);
        speed = 30.5f;
        ReadAndWrite = 60;
        print("�ק�᪺���");
        print("�����  - ���ʳt�� : " + speed);
        print("�ݩʸ��  - Ū�g�ݩ� : " + ReadAndWrite);
        // ��Ū�ݩ�
        // read = 7 ; // ��Ū�ݩʤ���]�wSet
        print("��Ū�ݩ� : " + read);
        print("��Ū�ݩ�  - ���w�]�� : " + readValue);

        // �ݩʦs���m��
        print("HP : " + _hp);
        hp = 100;
        print("HP : " + _hp);
        */
        #endregion

        #region ��k�m��
        /*
        // �I�s�ۭq��k�y�k: ��k�W��();
        Test();
        Test();
        // �I�s���Ǧ^�Ȫ���k
        // 1.�ϰ��ܼƫ��w�Ǧ^�� - �ϰ��ܼ�ĵ��b�����c{�j�A��}���s��
        int j = ReturnJump();
        print("���D��: " + j);
        // 2.�N�Ǧ^��k���Ȩϥ�
        print("���D�ȡA��Ȩϥ�: " + (ReturnJump() + 1));

        skill100();
        skill150();
        skill200();
        // �I�s���ѼƤ�k�ɡA������J�������޼�
        Skill(100);
        Skill(999,"�z���S��");
        // ���h�ӿ�񦡰ѼƮɥi�ϥΫ��W�Ѽƻy�k:  �ѼƦW��:��
        Skill(500, sound:"������") ;

        print(BMI(68, 1.723f, "��"));
        */
        #endregion


    }

    // ��s�ƥ� : �@������� 60 ���A60 FPS -Frame Per Second
    // �B�z����ʪ��B�ʡA���ʪ���A��ť���a��J����

    private void Update()
    {

    }
    #endregion
}
