using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public Transform prefabItemWorld { get; private set; }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.prefabItemWold, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item item;

    public void SetItem(Item item)
    {
        this.item = item;

        string path = "items/" + this.item.itemType;
        GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        instance.transform.SetParent(transform);
        instance.transform.position = transform.position;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
