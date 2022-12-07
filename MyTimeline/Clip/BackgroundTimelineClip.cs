using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    [Serializable]
    public class BackgroundTimelineClip : PlayableAsset
    {
        public ExposedReference<BackgroundMover> _backgroundMover;
        public bool _isStop;
        public bool _isReset;
        [HideIf("@this._isReset == true"), SerializeField]
        private float _duration;
    
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            BackgroundMover resolvedBackgroundMover = _backgroundMover.Resolve(graph.GetResolver());
            if (resolvedBackgroundMover == null)
            {
                return default;
            }
            var behaviour = new BackgroundTimelineBehaviour();
            behaviour.SetBackground(resolvedBackgroundMover, _isStop, _isReset, _duration);
            var playable = ScriptPlayable<BackgroundTimelineBehaviour>.Create(graph, behaviour);
            return playable;
        }
    }
}
