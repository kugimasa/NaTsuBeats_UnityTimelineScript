using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    public class BackgroundColorBehaviour : PlayableBehaviour
    {
        private GroundColorClip _groundColorClip;
        private ChangeColorClip _changeColorClip;

        public void SetGroundColorClip(GroundColorClip clip)
        {
            _groundColorClip = clip;
        }

        public void SetChangeColorClip(ChangeColorClip clip)
        {
            _changeColorClip = clip;
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            // 地面の色変換処理
            if (_groundColorClip != null)
            {
                _groundColorClip.SetColorInfo();
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var time = playable.GetTime();
            var duration = playable.GetDuration();
            var t = (float)(time / duration);
            // 地面の色変換処理
            if (_groundColorClip != null)
            {
                // グラデーション位置の更新
                _groundColorClip.UpdateGroundColor(t);
            }
            // 色調フィルター変換処理
            if (_changeColorClip != null)
            {
                // 色調を更新
                _changeColorClip.UpdateColor(t);
                // スプライトのフェード処理
                _changeColorClip.FadeSprite(t);
            }
            base.ProcessFrame(playable, info, playerData);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            // マテリアルの削除
            if (_groundColorClip != null)
            {
                // _groundColorClip.RemoveMaterial();
            }
        }
    }
}