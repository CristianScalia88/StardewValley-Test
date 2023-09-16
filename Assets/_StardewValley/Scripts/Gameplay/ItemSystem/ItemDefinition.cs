using UnityEngine;

[CreateAssetMenu]
public class ItemDefinition : ScriptableObject
{
    public int id = -1;
    public string name;
    public Sprite sprite;
    public int price;
}
