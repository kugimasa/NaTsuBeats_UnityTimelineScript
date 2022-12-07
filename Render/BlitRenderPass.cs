using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Render
{
    public class BlitRenderPass : ScriptableRenderPass
    {
        private readonly string _profilerTag;
        private RenderTargetIdentifier _cameraColorTarget;
        private readonly Material _material;
        private RenderTargetHandle _tempTexture;
        
        public BlitRenderPass(string profilerTag, Material material, RenderPassEvent renderPassEvent)
        {
            _profilerTag = profilerTag;
            _material = material;
            this.renderPassEvent = renderPassEvent;
        }

        // カメラのカラーターゲットをセット
        public void SetUpColorTarget(RenderTargetIdentifier cameraColorTarget)
        {
            _cameraColorTarget = cameraColorTarget;
        }

        // Executeの前に必ず呼ばれる処理
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(_tempTexture.id, cameraTextureDescriptor);
        }

        // カメラに対して毎フレーム呼ばれる処理
        // 実際のレンダリングがここで行われている訳ではない
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // コマンドバッファの取得
            CommandBuffer cmd = CommandBufferPool.Get(_profilerTag);
            cmd.Clear();

            // 実際にシェーダーに対してColorTargetを渡している
            cmd.Blit(_cameraColorTarget, _tempTexture.Identifier(), _material, 0);
            cmd.Blit(_tempTexture.Identifier(), _cameraColorTarget);
        
            // ScriptableRenderContextに処理させる
            context.ExecuteCommandBuffer(cmd);
        
            // コマンドバッファの解放
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        // Executeの後に呼ばれる
        // Configureで確保された情報を解放する
        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_tempTexture.id);
        }
    }
}