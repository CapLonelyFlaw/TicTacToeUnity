using System.Collections.Generic;
using UnityEngine;
using GameCore;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] private List<DifficultyType> _availableDifficulties;
    [SerializeField] private Text _difficulyTitle;
    [SerializeField] private DifficultyType _defaultDifficulty;

    public DifficultyType SelectedDifficulty { get { return _availableDifficulties[_index]; } }

    private int _index;

    private void Awake()
    {
        if (_availableDifficulties.Count > 0)
        {
            var index = _availableDifficulties.IndexOf(_defaultDifficulty);
            if (index >= 0)
            {
                _index = index;
                SetTitle();
            }
            else
            {
                Debug.LogWarning(_defaultDifficulty.ToString(" difficulty not found"));
            }
        }
        else
        {
            Debug.LogWarning("You must add some difficulties");
        }
    }

    public void OnNextButtonClick()
    {
        _index = _index + 1 == _availableDifficulties.Count ? 0 : _index + 1;
        SetTitle();
    }

    public void OnPrevButtonClick()
    {
        _index = _index - 1 < 0 ? _availableDifficulties.Count - 1 : _index - 1;
        SetTitle();
    }

    private void SetTitle()
    {
        _difficulyTitle.text = _availableDifficulties[_index].ToString().ToUpper();
    }
}
