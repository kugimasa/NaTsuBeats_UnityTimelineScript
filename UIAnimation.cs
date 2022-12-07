using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _titleUI;
    [SerializeField] private GameObject _NaTsuUI;
    [SerializeField] private GameObject _kugiUI;

    public void TitleUIMoveUp()
    {
        //_titleUI.transform;
    }
    
    public void ToggleNaTsuUI()
    {
        _NaTsuUI.SetActive(!_NaTsuUI.activeInHierarchy);
    }

    public void ToggleKugiUI()
    {
        _kugiUI.SetActive(!_kugiUI.activeInHierarchy);
    }
}