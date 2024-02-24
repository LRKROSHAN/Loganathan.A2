using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {    

        if (other.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            GameManager.instance.CollectCoin(); // Calls the CollectCoin method in the GameManager
            gameObject.SetActive(false); // Deactivates the coin. Alternatively, you can use Destroy(gameObject); if you don't plan to reuse the coin.
        }
    }
}
