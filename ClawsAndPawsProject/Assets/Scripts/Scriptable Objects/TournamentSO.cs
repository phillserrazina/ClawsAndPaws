using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Combat/Tournament", fileName="New Tournament List")]
public class TournamentSO : ScriptableObject
{
    public OpponentSO[] opponentOrder;
}
