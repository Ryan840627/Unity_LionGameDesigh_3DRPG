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
    [Header("���ʳt��"),Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"),Range(1,500)]
    public float speed;

    #region Unity �������
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

    #endregion

    #endregion

    #region �ݩ�Property

    #endregion

    #region ��kMethod

    #endregion

    #region �ƥ�Event

    #endregion
}
