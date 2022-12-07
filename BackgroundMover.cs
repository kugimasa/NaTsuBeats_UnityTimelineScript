using System;
using MyTimeline;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

public class BackgroundMover : MonoBehaviour
{
    [Serializable]
    struct BackgroundImage
    {
        public GameObject Front;
        public GameObject Back;

        /// <summary>
        /// 背景の非表示
        /// </summary>
        /// <param name="hidePos">非表示位置</param>
        public void Hide(float hidePos)
        {
            HideFront(hidePos);
            HideBack(hidePos);
        }

        public void HideFront(float hidePos)
        {
            if (Front != null)
            {
                Front.transform.position = new Vector3(hidePos, 0f);
            }
        }

        public void HideBack(float hidePos)
        {
            if (Back != null)
            {
                Back.transform.position = new Vector3(hidePos, 0f);
            }
        }
    }
    [Title("背景")] [SerializeField] private BackgroundImage _background;
    [Title("パラメータ一覧")]
    [SerializeField] private float _transferDist ;
    [SerializeField] private float _loopTime;
    private BackgroundImage _image;
    private float _time;
    private float _duration;

    /// <summary>
    /// Stop設定の時はClipから直でdurationをセットする
    /// </summary>
    /// <param name="duration"></param>
    public void SetDuration(float duration)
    {
        _duration = duration;
    }
    
    public void ScrollOnFixedFrameRate(float deltaTime, float clipTime, bool isStop)
    {
        if (_image.Front == null || _image.Back == null)
        {
            return;
        }
        // 停止中クリップの処理
        if (isStop)
        {
            return;
        }
        // 切り替え時間判定
        if (_time >= _loopTime)
        {
            SwapFrontBack();
        }
        // Timeline停止中の処理
        if (deltaTime == 0.0f)
        {
            var totalClipTime = clipTime + _duration;
            // 入れ替えが必要かの判定
            if (CheckNeedSwap(totalClipTime))
            {
                SwapFrontBack();
            }
            // 時間をセット
            _time = totalClipTime % _loopTime;
        }
        else
        {
            // 時間の更新
            _time += deltaTime;
        }
        // 移動
        // FrontとBackを進める
        var currentPos = Vector3.right * (_transferDist * _time / _loopTime);
        _image.Front.transform.position = currentPos;
        _image.Back.transform.position = currentPos + new Vector3(- _transferDist, 0);
    }

    /// <summary>
    /// Frontにあった画像をBackの後ろに移動し、参照を入れ替える
    /// </summary>
    private void SwapFrontBack()
    {
        // 時間のリセット
        _time = 0.0f;
        // FrontをBackの後ろに移動
        _image.Front.transform.position = _image.Back.transform.position - _transferDist * Vector3.right;
        // 参照の入れ替え
        (_image.Back, _image.Front) = (_image.Front, _image.Back);
    }

    /// <summary>
    /// 入れ替えが必要かの判定
    /// </summary>
    private bool CheckNeedSwap(float clipTime)
    {
        var frontImagePos = _background.Front.transform.position.x;
        var backImagePos = _background.Back.transform.position.x;
        var swapNum = (int)(clipTime / _loopTime);
        // 偶数回入れ替えた後
        if (swapNum % 2 == 0)
        {
            return  backImagePos > frontImagePos;
        }
        // 奇数回入れ替えた後
        else
        {
            return backImagePos < frontImagePos;
        }
    }

    /// <summary>
    /// ベタ打ち初期化
    /// </summary>
    public void Reset()
    {
        // 時間をリセット
        _time = 0.0f;
        _duration = 0.0f;
        _background.Front.transform.position = Vector3.zero;
        _background.Back.transform.position = new Vector3(-_transferDist, 0f);
        _image.Front = _background.Front;
        _image.Back = _background.Back;
    }
}
