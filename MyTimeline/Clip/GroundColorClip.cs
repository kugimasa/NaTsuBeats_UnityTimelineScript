using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    public class GroundColorClip : PlayableAsset
    {
        [SerializeField] private ExposedReference<GroundColor> _ground;
        [SerializeField, LabelText("地面・上")] private Gradient _topColor;
        [SerializeField, LabelText("地面・中")] private Gradient _middleColor;
        [SerializeField, LabelText("地面・下")] private Gradient _bottomColor;

        private GroundColor _resolvedGround;

        public void SetColorInfo()
        {
            if (_resolvedGround == null)
            {
                Debug.LogWarning("GroundColor not resolved from graph.");
                return;
            }
            _resolvedGround.SetColorInfo(_topColor, _middleColor, _bottomColor);
        }

        public void UpdateGroundColor(float t)
        {
            if (_resolvedGround == null)
            {
                Debug.LogWarning("GroundColor not resolved from graph.");
                return;
            }
            _resolvedGround.UpdateGroundColor(t);
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            _resolvedGround = _ground.Resolve(graph.GetResolver());
            if (_resolvedGround == null)
            {
                return default;
            }
            var playable = ScriptPlayable<BackgroundColorBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 自身をBehaviourにセット
            behaviour.SetGroundColorClip(this);
            return playable;
        }
    }
}