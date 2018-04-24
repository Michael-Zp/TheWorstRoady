using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void Start()
    {
        EventSystem.Instance.GuitarOfPlayerDestroyedEvent += GetComponent<PlayerInventory>().DestroyActiveItem;
        EventSystem.Instance.StunPlayerEvent += GetComponent<PlayerMovement>().StunAndKnockBack;
        EventSystem.Instance.PunchGuitarOutOfHandsEvent += GetComponent<PlayerInventory>().PunchActiveItemOutOfHand;
    }
}
