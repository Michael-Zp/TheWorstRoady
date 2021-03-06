﻿using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void Start()
    {
        EventSystem.Instance.GuitarOfPlayerDestroyedEvent += GetComponent<PlayerInventory>().DestroyActiveItem;
        EventSystem.Instance.StunPlayerEvent += GetComponent<PlayerMovement>().StunAndKnockBack;
        EventSystem.Instance.PunchGuitarOutOfHandsEvent += GetComponent<PlayerInventory>().PunchActiveItemOutOfHand;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GuitarOfPlayerDestroyedEvent -= GetComponent<PlayerInventory>().DestroyActiveItem;
        EventSystem.Instance.StunPlayerEvent -= GetComponent<PlayerMovement>().StunAndKnockBack;
        EventSystem.Instance.PunchGuitarOutOfHandsEvent -= GetComponent<PlayerInventory>().PunchActiveItemOutOfHand;
    }

    public bool GetActiveItem(out ItemType type)
    {
        if(GetComponent<PlayerInventory>().GetTypeOfActiveItem(out type))
        {
            return true;
        }
        return false;
    }
}
