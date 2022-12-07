using UnityEngine;
using UnityEngine.Playables;

namespace MyTimeline
{
    public class BackgroundSceneBehaviour : PlayableBehaviour
    {
        public BackgroundSceneClip Clip { get; set; }
        
        // TODO: OnPlayableCreateの時点でセットすれば良い...??
        public override void OnPlayableCreate(Playable playable)
        {
            // TODO: ここでセットするには、playerDataを取得する必要がある
            // ScriptPlayableOutput??
            base.OnPlayableCreate(playable);
        }

        // FIXME: 毎フレームセットする必要はない
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var spriteRender = playerData as SpriteRenderer;
            if (!spriteRender) return;
            // 背景をセット
            spriteRender.sprite = Clip.SceneSprite;
        }
    }
}