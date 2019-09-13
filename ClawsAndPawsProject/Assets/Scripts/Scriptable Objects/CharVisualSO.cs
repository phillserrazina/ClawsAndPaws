using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Actors/Visual", fileName="New Visual")]
public class CharVisualSO : ScriptableObject
{
    public Sprite head;
    public Sprite nose;
    public Sprite mouth;
    public Sprite torso;
    public Sprite tail;
    public Sprite eye;
    public Sprite whiskers;
    [Space(10)]
    public Sprite rightEar;
    public Sprite leftEar;
    [Space(10)]
    public Sprite frontLeftPaw;
    public Sprite frontRightPaw;
    public Sprite backLeftPaw;
    public Sprite backRightPaw;
}
