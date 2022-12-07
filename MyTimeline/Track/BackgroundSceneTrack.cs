using UnityEngine;
using UnityEngine.Timeline;

namespace MyTimeline
{
    [TrackClipType(typeof(BackgroundSceneClip))]
    [TrackBindingType(typeof(SpriteRenderer))]
    public class BackgroundSceneTrack : TrackAsset
    {
        
    }
}