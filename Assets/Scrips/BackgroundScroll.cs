using UnityEngine;
using UnityEngine.AdaptivePerformance;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // MeshRenderer 컴포넌트를 가져옵니다.
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // 시간에 따라 변경될 X 오프셋 값을 계산합니다.
        float offsetX = Time.time * scrollSpeed;

        // mainTextureOffset을 통해 Surface Inputs의 Offset 값을 변경합니다.
        // URP를 사용하는 경우 "_BaseMap" 또는 일반적인 경우 "_MainTex" 프로퍼티로 접근할 수도 있습니다.

        meshRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}
