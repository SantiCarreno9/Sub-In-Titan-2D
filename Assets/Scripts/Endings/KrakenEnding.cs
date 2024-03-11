using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenEnding : MonoBehaviour
{
    [SerializeField] private Transform _submersible;
    [SerializeField] private Transform _krakenTail;

    public void AttachSubmersibleToKrakenTail()
    {
        _submersible.SetParent(_krakenTail);
    }    
}
