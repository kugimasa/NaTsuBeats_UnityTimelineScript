using UnityEngine;

public class GroundColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _top;
    [SerializeField] private SpriteRenderer _middle;
    [SerializeField] private SpriteRenderer _bottom;
    [SerializeField] private Material _topMatBase;
    [SerializeField] private Material _middleMatBase;
    [SerializeField] private Material _bottomMatBase;
    [SerializeField] private float _gradWidth;
    [SerializeField] private float _posterization;
    private static readonly int ColorRight = Shader.PropertyToID("_ColorRight");
    private static readonly int ColorLeft = Shader.PropertyToID("_ColorLeft");
    private static readonly int T = Shader.PropertyToID("_T");

    private Material _topMat;
    private Material _middleMat;
    private Material _bottomMat;
    private static readonly int PosterizePower = Shader.PropertyToID("_PosterizePower");
    private static readonly int Width = Shader.PropertyToID("_Width");

    /// <summary>
    /// クリップの色情報をセット
    /// </summary>
    public void SetColorInfo(Gradient topC, Gradient middleC, Gradient bottomC)
    {
        if (!_topMat)
        {
            _topMat = new Material(_top.sharedMaterial);
            _top.material = _topMat;
        }
        if (!_middleMat)
        {
            _middleMat = new Material(_middle.sharedMaterial);
            _middle.material = _middleMat;
        }
        if (!_bottomMat)
        {
            _bottomMat = new Material(_bottom.sharedMaterial);
            _bottom.material = _bottomMat;
        }
        _topMat.SetFloat(PosterizePower, _posterization);
        _topMat.SetFloat(Width, _gradWidth);
        _topMat.SetColor(ColorRight, topC.Evaluate(0));
        _topMat.SetColor(ColorLeft, topC.Evaluate(1));
        
        _middleMat.SetFloat(PosterizePower, _posterization);
        _middleMat.SetFloat(Width, _gradWidth);
        _middleMat.SetColor(ColorRight, middleC.Evaluate(0));
        _middleMat.SetColor(ColorLeft, middleC.Evaluate(1));
        
        _bottomMat.SetFloat(PosterizePower, _posterization);
        _bottomMat.SetFloat(Width, _gradWidth);
        _bottomMat.SetColor(ColorRight, bottomC.Evaluate(0));
        _bottomMat.SetColor(ColorLeft, bottomC.Evaluate(1));
    }

    /// <summary>
    /// グラデーション位置の更新
    /// </summary>
    public void UpdateGroundColor(float t)
    {
        if (_topMat)
        {
            _topMat.SetFloat(T, t);
        }
        if (_bottomMat)
        {
            _middleMat.SetFloat(T, t);
        }
        if (_bottomMat)
        {
            _bottomMat.SetFloat(T, t);
        }
    }

    /// <summary>
    /// 生成したTimeline用のマテリアルを削除
    /// </summary>
    public void RemoveMaterial()
    {
        if (_topMat)
        {
            DestroyImmediate(_topMat);
            _top.material = _topMatBase;
        }
        if (_middleMat)
        {
            DestroyImmediate(_middleMat);
            _middle.material = _middleMatBase;
        }
        if (_bottomMat)
        {
            DestroyImmediate(_bottomMat);
            _bottom.material = _bottomMatBase;
        }
    }
}