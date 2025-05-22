using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltipUI : MonoBehaviour
{
    public static ItemTooltipUI Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text descriptionText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    public void ShowTooltip(Item item)
    {
        iconImage.sprite = item.Icon;
        descriptionText.text = item.Description;
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}