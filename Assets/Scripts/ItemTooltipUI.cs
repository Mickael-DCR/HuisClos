using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ItemTooltipUI : MonoBehaviour
{
    public static ItemTooltipUI Instance;

    [SerializeField] private GameObject _tooltipPanel;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Item _itemPlaceholder;
    private Item _currentItemDisplayed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    public void ShowTooltip(Item item)
    {
        _iconImage.sprite = item.Icon;
        _descriptionText.text = item.Description;
        _currentItemDisplayed = item;
    }

    public void ShowBaseTooltip()
    {
        _iconImage.sprite = _itemPlaceholder.Icon;
        _descriptionText.text = _itemPlaceholder.Description;
    }

    public void HideTooltip(Item item) // Called when an item is removed from inventory but was displayed in tooltip
    {
        if (InventoryManager.Instance.FindItem(item) == _currentItemDisplayed)
        {
            ShowBaseTooltip();
        }
    }
}