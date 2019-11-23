using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Allows for multiple platforms to be controlled by one minigame.

public class PlatformLink : MonoBehaviour {

    public PlatformMovement linkedPlatform1;
    public PlatformMovement linkedPlatform2;


	void Update ()
  {
        if (linkedPlatform1.GetActive()) linkedPlatform2.MakeActive(); // If platforms are linked: make them both active.
	}
}
