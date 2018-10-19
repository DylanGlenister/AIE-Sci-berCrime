using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public bool m_bRoundOver;
    public int m_iRound;

    private void Awake()
    {
        m_iRound = 1;
    }
}
