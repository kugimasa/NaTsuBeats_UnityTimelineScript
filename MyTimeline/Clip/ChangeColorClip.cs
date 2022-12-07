using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace MyTimeline
{
    public class ChangeColorClip : PlayableAsset
    {
        private enum Type
        {
            SKY,
            COLOR_ADDITIVE,
            SPRITE_ALPHA,
        }

        private enum ALPHA_EFFECT
        {
            SHOW,
            FADEOUT,
            FADEIN,
            HIDE
        }

        [SerializeField] private Type _type;
        [ShowIf("@this._type == Type.SKY"), SerializeField] ExposedReference<Camera> _sceneCamera;
        [ShowIf("@this._type == Type.COLOR_ADDITIVE"), SerializeField] ExposedReference<RawImage> _renderTexture;
        [ShowIf("@this._type != Type.SPRITE_ALPHA"), SerializeField] private Gradient _gradient;
        [ShowIf("@this._type == Type.SPRITE_ALPHA"), SerializeField] private ExposedReference<SpriteRenderer> _spriteRenderer;
        [ShowIf("@this._type == Type.SPRITE_ALPHA"), SerializeField] private ALPHA_EFFECT _alphaEffect;

        private Camera _resolvedCamera;
        private RawImage _resolvedRenderTexture;
        private SpriteRenderer _resolvedSpriteRenderer;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            _resolvedCamera = _sceneCamera.Resolve(graph.GetResolver());
            _resolvedRenderTexture = _renderTexture.Resolve(graph.GetResolver());
            _resolvedSpriteRenderer = _spriteRenderer.Resolve(graph.GetResolver());
            if (_resolvedCamera != null || _resolvedRenderTexture != null || _resolvedSpriteRenderer != null)
            {
                var playable = ScriptPlayable<BackgroundColorBehaviour>.Create(graph);
                var behaviour = playable.GetBehaviour();
                // 自身をBehaviourにセット
                behaviour.SetChangeColorClip(this);
                return playable;
            }
            return default;
        }
        
        public void UpdateColor(float t)
        {
            if (_resolvedCamera != null)
            {
                _resolvedCamera.backgroundColor = _gradient.Evaluate(t);
            }
            if (_resolvedRenderTexture != null)
            {
                _resolvedRenderTexture.color = _gradient.Evaluate(t);
            }
        }

        public void FadeSprite(float t)
        {
            if (_resolvedSpriteRenderer != null)
            {
                switch (_alphaEffect)
                {
                    case ALPHA_EFFECT.SHOW:
                        t = 1;
                        break;
                    case ALPHA_EFFECT.FADEIN:
                        break;
                    case ALPHA_EFFECT.FADEOUT:
                        t = 1 - t;
                        break;
                    case ALPHA_EFFECT.HIDE:
                        t = 0;
                        break;
                }
                var col = _resolvedSpriteRenderer.color;
                _resolvedSpriteRenderer.color = new Color(col.r, col.g, col.b, t);
            }
        }
    }
}