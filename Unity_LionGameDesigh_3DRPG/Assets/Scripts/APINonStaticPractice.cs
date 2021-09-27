using UnityEngine;

public class APINonStaticPractice : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer SprRen;
    public Transform spr2;
    public Rigidbody2D rig;

    void Start()
    {
        
        print("攝影機深度 : " + cam.depth);
        print("圖片顏色 : " + SprRen.color);
        cam.backgroundColor = Random.ColorHSV();
        SprRen.flipY = true;
    }

    
    void Update()
    {
        spr2.Rotate(20,30,40);
        rig.AddForce(Vector2.up,ForceMode2D.Impulse);
    }
}
