using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create New Item", fileName = "New Item") ]
public class Items : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _itemPrefab;

    public virtual void PickUp()
    {
        
    }
}
