using UnityEngine;
using System.Collections;

public class ViveWandController : MonoBehaviour {

	// Variables for various controller buttons/states
	private Valve.VR.EVRButtonId grip_id = Valve.VR.EVRButtonId.k_EButton_Grip;
	private bool grip_down = false;
	private bool grip_up = false;
	private bool grip_pressed = false;

	private Valve.VR.EVRButtonId trigger_id = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool trigger_down = false;
	private bool trigger_up = false;
	private bool trigger_pressed = false;

	private Valve.VR.EVRButtonId trackpad_id = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private bool trackpad_down = false;
	private bool trackpad_up = false;
	private bool trackpad_pressed = false;

	// variables for tracking the controller's position
	private Vector3 prev_local_pos;
	private Vector3 prev_pos;


	// Variables that link to which controller this script controls
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller;

	void Start () {
		// Initialize our variables based on SteamVR
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);

		updatePreviousPositions ();
	}

	// updates previous position variables with the current position
	private void updatePreviousPositions() {
		prev_local_pos = getLocalPosition ();
		prev_pos = getPosition ();
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

			trackpad_down = controller.GetPressDown (trackpad_id);
			trackpad_up = controller.GetPressUp (trackpad_id);
			trackpad_pressed = controller.GetPress (trackpad_id);

			updatePreviousPositions ();
		} else {
			print ("Controller not initialized");
		}
	}

	// return the controller's current local position
	public Vector3 getLocalPosition() {
		return trackedObj.transform.localPosition;
	}

	// return the controller's current global position
	public Vector3 getPosition() {
		return trackedObj.transform.position;
	}

	// return the controller's previous local position
	public Vector3 getPrevLocalPosition() {
		return prev_local_pos;
	}

	// return the controller's current global position
	public Vector3 getPrevPosition() {
		return prev_pos;
	}

	// return the trigger's current state
	public bool getTriggerPressed() {
		return trigger_pressed;
	}
	public bool getTriggerDown() {
		return trigger_down;
	}
	public bool getTriggerUp() {
		return trigger_up;
	}

	// return the grip's current state
	public bool getGripPressed() {
		return grip_pressed;
	}
	public bool getGripDown() {
		return grip_down;
	}
	public bool getGripUp() {
		return grip_up;
	}

	// return the trackpad's current state
	public bool getTrackpadPressed() {
		return trackpad_pressed;
	}
	public bool getTrackpadDown() {
		return trackpad_down;
	}
	public bool getTrackpadUp() {
		return trackpad_up;
	}
}
