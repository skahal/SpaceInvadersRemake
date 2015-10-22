using UnityEngine;
using System.Collections;

public class AlienArm : MonoBehaviour {

	private Spine.Slot m_slot;

	public string SlotName;

	void Start() {
		m_slot = GetComponent<BoundingBoxFollower> ().skeletonRenderer.skeleton.FindSlot (SlotName);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.IsProjectile ()) {
			m_slot.Attachment = null;
		}
	}
}
