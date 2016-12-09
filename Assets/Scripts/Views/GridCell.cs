using Helpers;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public int CellIndex { get { return _cellIndex; } }

    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _cross;
    [SerializeField] private Sprite _toe;
    [SerializeField] private int _cellIndex;

    private bool _isEmpty = true;

    public ActionWrapper<GridCell> OnCellClick = new ActionWrapper<GridCell>();

    void Awake()
    {
        if (_isEmpty)
            _image.sprite = null;
    }

    public void SetCross()
    {
        _image.color = Color.white;
        _image.sprite = _cross;
        _isEmpty = false;
        _button.interactable = false;
    }

    public void SetToe()
    {
        _image.color = Color.white;
        _image.sprite = _toe;
        _isEmpty = false;
        _button.interactable = false;
    }

    public void SetHalfAlpha()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.23f);
    }

    public void OnPointerClick()
    {
        if (_isEmpty)
        {
            OnCellClick.Dispatch(this);
        }
    }
}
