using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material mat;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // Time.time을 사용해 시간에 따라 계속 증가하는 오프셋(Offset) 값을 만듭니다.
        float offset = scrollSpeed * Time.time;

        // URP(Universal Render Pipeline) Surface Inputs의 Base Map 오프셋을 변경합니다.
        mat.SetTextureOffset("_BaseMap", new Vector2(offset, 0));

        // 만약 Standard 셰이더 등 _MainTex를 사용하는 재질이라면 위 줄을 주석 처리하고 아래 줄을 사용하세요:
        // mat.mainTextureOffset = new Vector2(offset, 0);
    }
}
