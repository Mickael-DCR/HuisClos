using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create New Disk", fileName = "New Disk") ]
public class DiskItem : ScriptableObject
{
    public GameObject ItemPrefabInspect;
    public GameObject ItemPrefab3D;
    public GameObject ItemPrefabUI;
    public Sprite Icon;
    public Texture Texture;
}
