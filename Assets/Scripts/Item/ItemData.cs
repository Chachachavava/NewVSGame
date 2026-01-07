using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField]
    public string itemName{ get; private set; }
    [field: SerializeField]
    public Sprite icon{ get; private set; }
    [field: SerializeField]
    public ItemTier tiar{ get; private set; }
    [field: SerializeField]
    public ItemEffectType itemEffectType { get; private set; }
    [field: SerializeField]
    public float baseValue { get; private set; }
    [field: SerializeField]
    public float stackValue { get; private set; }
    public void SetData(string n, ItemTier t, ItemEffectType type, float b, float s)
    {
        itemName = n;
        tiar = t; 
        itemEffectType = type; 
        baseValue = b; 
        stackValue = s;
    }
}
