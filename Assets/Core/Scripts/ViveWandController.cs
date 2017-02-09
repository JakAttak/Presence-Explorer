using UnityEngine;
using System.Collections;

public class ViveWandController : MonoBehaviour {

	// Variables for various controller buttons/states
	private Valve.VR.EVRButtonId grip_id = Valve.VR.EVRButtonId.k_EButton_Grip;
	public bool grip_down = false;
	public bool grip_up = false;
	public bool grip_pressed = false;

	private Valve.VR.EVRButtonId trigger_id = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public bool trigger_down = false;
	public bool trigger_up = false;
	public bool trigger_pressed = false;

	// Variables that link to which controller this script controls
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	void Start () {
		// Initialize our variables based on SteamVR
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	void FixedUpdate () {
		if (controller != null) {
			// Update variables to current controller state
			grip_down = controller.GetPressDown (grip_id);
			grip_up = controller.GetPressUp (grip_id);
			grip_pressed = controller.GetPress (grip_id);

			trigger_down = controller.GetPressDown (trigger_id);
			trigger_up = controller.GetPressUp (trigger_id);
			trigger_pressed = controller.GetPress (trigger_id);
		} else {
			print ("Controller not initialized");
		}
	}
}
