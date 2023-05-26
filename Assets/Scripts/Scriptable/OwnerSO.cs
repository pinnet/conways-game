using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "player.asset", menuName = "Player")]
public class OwnerSO : ScriptableObject
{
    [SerializeField] Object payer;
}
