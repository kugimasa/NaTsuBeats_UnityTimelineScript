using UnityEngine.Timeline;

namespace MyTimeline
{
    [TrackClipType(typeof(GroundColorClip), false)]
    [TrackClipType(typeof(ChangeColorClip), false)]
    public class BackgroundColorTrack : TrackAsset
    {
        // TODO: Mix処理
        // public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        // {
        //     return ScriptPlayable<BackgroundColorMixer>.Create(graph, inputCount);
        // }
    }
}