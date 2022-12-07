using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace MyTimeline
{
    [Serializable]
    // ClipをTrackに追加できるようにするために必要
    [TrackClipType(typeof(BackgroundTimelineClip), false)]
    [TrackClipType(typeof(ButterflyFlightClip), false)]
    // マーカーをトラック上にセットするために必要
    [TrackBindingType(typeof(GameObject))]
    public class BackgroundTimelineTrack : PlayableTrack
    {
    }
}