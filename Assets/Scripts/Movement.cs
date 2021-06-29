using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Movement : MonoBehaviour
{
    public float MaxSpeed = 2.0f;
    public SteamVR_Action_Vector2 _Movement = null;

    public float additionalHeight;
    public CharacterController character;

    public Vector3 capsuleCenter;
    void Start(){
        character = GetComponent<CharacterController>();
    }

    void Update(){

       capsuleFollowHead();
       Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(_Movement.axis.x, 0, _Movement.axis.y));
       character.Move(MaxSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
       //transform.position += MaxSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
    }

    public void capsuleFollowHead(){
        character.height = Player.instance.eyeHeight + additionalHeight;
        capsuleCenter = transform.InverseTransformPoint(Player.instance.hmdTransform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }
}
