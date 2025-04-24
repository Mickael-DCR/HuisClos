using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create New Item", fileName = "New Item") ]
public class Item : ScriptableObject
{
    public GameObject ItemPrefab;
    public string ItemName;
    public Sprite Icon;

    public virtual void Use(GameObject target)
    {
        
    }
    
    
}
