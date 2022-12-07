using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    public class BackgroundSceneClip : PlayableAsset
    {
        [SerializeField] private Sprite _sceneSprite;

        public Sprite SceneSprite => _sceneSprite;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BackgroundSceneBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.Clip = this;
            return playable;
        }
    }
}