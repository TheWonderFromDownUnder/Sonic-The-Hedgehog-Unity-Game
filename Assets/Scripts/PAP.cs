using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Animation Parameters
public class PAP : MonoBehaviour
{
    public const string moveX = "moveX";
    public const string isMoving = "isMoving";
    public const string axisXInput = "Horizontal";
    public const string axisYInput = "Vertical";
    public const string forceX = "forceX";
    public const string impulseX = "impulseX";
    public const string impulseY = "impulseY";
    public const string velocityX = "velocityX";
    public const string velocityY = "velocityY";
    public const string isOnGround = "isOnGround";
    public const string landedOnGround = "landedOnGround";
    public const string JumpTriggerName = "jump";
    public const string CrouchTriggerName = "crouch";
    public const string LookupTriggerName = "lookup";
    public const string SubmitTriggerName = "submit";

    // Used as a trigger, turn off when consumed
    public const string stopVelocity = "stopVelocity";

    // Player Input Action Names
    public const string axisXinput = "Horizontal";
    public const string axisYinput = "Vertical";

    public const string jumpKeyName = "Jump";
    public const string crouchKeyName = "Crouch";
    public const string lookupKeyName = "Lookup";
    public const string submitKeyName = "Submit";

}
