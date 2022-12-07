using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace MyTimeline
{
    // TODO: シーン切り替え以外のシグナルとして流用
    [CustomStyle("MyTimelineAnnotation")]
    [Serializable, DisplayName("シーンセットシグナル")]
    public class SceneSetNotification : Marker, INotification, INotificationOptionProvider
    {
        [SerializeField] private bool _emitOnce;
        [SerializeField] private bool _emitInEditor;
        [SerializeField] private GameObject _setObject;
        
        // 通知を識別するために一意な値を指定する
        // ref: https://speakerdeck.com/lycoris102/timeline-signals-tutorial?slide=22
        public PropertyName id => new PropertyName("SceneSet");
        // Editor実行時にも確認を行いたい場合はINotificationOptionProviderを継承している必要がある
        public NotificationFlags flags => (_emitOnce ? NotificationFlags.TriggerOnce : default) |
                                          (_emitInEditor ? NotificationFlags.TriggerInEditMode : default);

        public GameObject SetObject => _setObject;
    }
}