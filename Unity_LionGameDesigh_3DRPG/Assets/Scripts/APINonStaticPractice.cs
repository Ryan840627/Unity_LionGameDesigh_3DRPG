using UnityEngine;

public class APINonStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer SprRen;
    public Transform spr2;
    public Rigidbody2D rig;

    void Start()
    {
        
        print("��v���`�� : " + cam.depth);
        print("�Ϥ��C�� : " + SprRen.color);
        cam.backgroundColor = Random.ColorHSV();
        SprRen.flipY = true;
    }

    
    void Update()
    {
        spr2.Rotate(20,30,40);
        rig.AddForce(Vector2.up,ForceMode2D.Impulse);
    }
}
