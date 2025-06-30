using UnityEngine;

public class BuffPickup : MonoBehaviour
{
    public BuffBase buffData;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pito");
        BuffInventory inventory = other.GetComponent<BuffInventory>();
        if (inventory != null)
        {
            inventory.AddBuff(buffData);
            Destroy(gameObject); // Desaparece al recoger
        }
    }
}
