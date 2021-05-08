using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VResearch.Interactions
{
    public class DoorSystem : MonoBehaviour
    {
		private DoorOpen[] childDoors;

        private void Awake()
        {
			childDoors = GetComponentsInChildren<DoorOpen>();
        }

        private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				foreach (DoorOpen door in childDoors)
					door.OpenDoor();
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
				foreach (DoorOpen door in childDoors)
					door.CloseDoor();
		}
	}
}
