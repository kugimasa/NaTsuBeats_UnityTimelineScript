using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    public class ButterflyFlightClip : PlayableAsset
    {
        public ExposedReference<ButterflyMover> _butterflyMover;
        public bool _isHideAtEnd;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ButterflyMover resolvedButterflyMover = _butterflyMover.Resolve(graph.GetResolver());
            if (resolvedButterflyMover == null)
            {
                return default;
            }
            var behaviour = new BackgroundTimelineBehaviour();
            behaviour.SetButterfly(resolvedButterflyMover, _isHideAtEnd);
            var playable = ScriptPlayable<BackgroundTimelineBehaviour>.Create(graph, behaviour);
            return playable;
        }
    }
}