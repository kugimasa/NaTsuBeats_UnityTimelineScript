using PathCreation;
using UnityEngine;

public class ButterflyMover : MonoBehaviour
{
    [SerializeField] private GameObject _butterfly;
    [SerializeField] private PathCreator _path;

    public void Flight(float t)
    {
        _butterfly.SetActive(true);
        _butterfly.transform.position = _path.path.GetPointAtTime(t, EndOfPathInstruction.Stop);
    }

    public void Hide()
    {
        _butterfly.SetActive(false);
    }
}