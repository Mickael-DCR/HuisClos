using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create New Item", fileName = "New Item") ]
public class Item : ScriptableObject
{
    public GameObject ItemPrefabInspect;
    public GameObject ItemPrefab3D;
    public GameObject ItemPrefabUI;
    public Sprite Icon;
    public string Description;
}
