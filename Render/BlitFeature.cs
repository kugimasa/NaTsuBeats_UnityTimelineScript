using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render
{
    public class BlitFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class Settings
        {
            public bool IsEnable = true;
            public Material material;
            public RenderPassEvent renderEvent = RenderPassEvent.AfterRenderingOpaques;
        }
    
        // 必ず "settings" という変数名をつけなくてはならない 
        [SerializeField] private Settings settings = new Settings();

        private BlitRenderPass _blitRenderPass;
    
        public override void Create()
        {
            _blitRenderPass = new BlitRenderPass(name, settings.material, settings.renderEvent);
        }

        // カメラに対して毎フレーム呼ばれる処理
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!settings.IsEnable) return;
            RenderTargetIdentifier cameraColorTarget = renderer.cameraColorTarget;
            // このタイミングでカメラのカラーターゲットをセット
            _blitRenderPass.SetUpColorTarget(cameraColorTarget);
        
            // パスをレンダラーに渡す
            renderer.EnqueuePass(_blitRenderPass);        
        }
    }
}