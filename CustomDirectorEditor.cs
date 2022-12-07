using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomDirector))]
public class CustomDirectorEditor : Editor
{
    private float _sliderVal = 0.0f;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CustomDirector director = (CustomDirector)target;
        
        if (GUILayout.Button("Reset"))
        {
            director.ResetBackground();
        }

        _sliderVal = GUILayout.HorizontalSlider(_sliderVal, 0.0f, 1.0f);
        director.Fade(_sliderVal);
        GUILayout.Space(20);
    }
}