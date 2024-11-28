using UnityEngine;

public class TextureSwitcher : MonoBehaviour
{
    public Texture[] Textures; // 存放纹理数组
    public Renderer TargetRendererMesh; // 目标的Renderer

    public Color[] TexturesColors; // 存放颜色数组

    public float MaxTime = 10f; // 最大时间
    public float MinTime = 5f; // 最小时间

    private float FinTime; // 下次切换时间
    private Color FinColor; // 最终颜色

    // 初始化
    void Start()
    {
        TextureSwitch(); // 在开始时切换纹理
    }

    // 每帧更新
    void Update()
    {
        FinTime -= Time.deltaTime; // 减少剩余时间
        if (FinTime <= 0.0f)
        {
            TextureSwitch(); // 时间到了，切换纹理
        }
    }

    // 切换纹理和颜色
    void TextureSwitch()
    {
        // 随机选择颜色和纹理
        FinColor = TexturesColors[Random.Range(0, TexturesColors.Length)];
        TargetRendererMesh.material.mainTexture = Textures[Random.Range(0, Textures.Length)];

        // 确保目标材质的 Shader 支持 "_TintColor" 或 "_Color"
        if (TargetRendererMesh.material.HasProperty("_TintColor"))
        {
            TargetRendererMesh.material.SetColor("_TintColor", FinColor);
        }
        else if (TargetRendererMesh.material.HasProperty("_Color"))
        {
            TargetRendererMesh.material.SetColor("_Color", FinColor);
        }

        // 设置下次切换的时间
        FinTime = Random.Range(MinTime, MaxTime);
    }
}

