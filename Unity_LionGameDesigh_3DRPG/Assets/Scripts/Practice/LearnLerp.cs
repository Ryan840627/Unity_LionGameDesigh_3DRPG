using UnityEngine;

/// <summary>
/// 學習lerp
/// </summary>
public class LearnLerp : MonoBehaviour
{
    public float a = 0, b = 100;
    public float c = 0, d = 200;
    public Color colora = Color.white, colorb = Color.red;
    public Vector3 vc1 = Vector3.zero, vc2 = Vector3.one * 100;
    private void Start()
    {
        print("a b 兩點 差值 0.5 : " + Mathf.Lerp(a, b, 0.5f));
        print("a b 兩點 差值 0.375 : " + Mathf.Lerp(a, b, 0.375f));
    }
    private void Update()
    {
        d = Mathf.Lerp(c, d, 0.75f);
        colorb = Color.Lerp(colora, colorb, 0.8f);
        vc2 = Vector3.Lerp(vc1, vc2, 0.8f);


    }

}
