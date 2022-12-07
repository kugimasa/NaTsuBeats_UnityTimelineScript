using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace MyTimeline
{
    public class BackgroundTimelineBehaviour : PlayableBehaviour
    {
        private BackgroundMover _backgroundMover;
        private bool _isStop = false;
        private bool _isReset = false;
        private float _duration;
        private ButterflyMover _butterflyMover;
        private bool _isHideAtEnd = false;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SetBackground(BackgroundMover background, bool isStop, bool isReset, float duration)
        {
            _backgroundMover = background;
            _isStop = isStop;
            _isReset = isReset;
            _duration = duration;
        }

        public void SetButterfly(ButterflyMover butterfly, bool isHideAtEnd)
        {
            _butterflyMover = butterfly;
            _isHideAtEnd = isHideAtEnd;
        }

        /// <summary>
        /// Clip再生時(先頭から出なくても良い)
        /// </summary>
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (_backgroundMover != null)
            {
                _backgroundMover.SetDuration(_duration);
            }
        }

        /// <summary>
        /// Timeline上でトラックの再生処理
        /// </summary>
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var time = playable.GetTime();
            var duration = playable.GetDuration();
            var t = (float)(time / duration);
            // 背景移動処理
            if (_backgroundMover != null)
            {
                // リセット処理
                if (_isReset)
                {
                    _backgroundMover.Reset();
                    return;
                }
                // 再生
                _backgroundMover.ScrollOnFixedFrameRate(info.deltaTime, (float)time, _isStop);
            }
            if (_butterflyMover != null)
            {
                _butterflyMover.Flight(t);
            }
        }

        /// <summary>
        /// Clip終了時
        /// </summary>
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (_butterflyMover != null)
            {
                if (_isHideAtEnd)
                {
                    _butterflyMover.Hide();
                }
            }
        }
    }
}