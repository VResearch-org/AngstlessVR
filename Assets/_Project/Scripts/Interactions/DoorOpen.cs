using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
	public enum Axis_t
	{
		XAxis,
		YAxis,
		ZAxis
	};
	[Tooltip("The axis in which to animate the door")]
	public Axis_t axisOfRotation = Axis_t.XAxis;
	[Tooltip("The opening distance")]
	[SerializeField] float distance = 1.5f;
	private static float speed = 1.0f;
	private Vector3 targetPosition;

    private void Start()
    {
        switch (axisOfRotation)
        {
			case Axis_t.XAxis:
				targetPosition = new Vector3(transform.localPosition.x + distance, transform.localPosition.y, transform.localPosition.z);
				break;
			case Axis_t.YAxis:
				targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + distance, transform.localPosition.z);
				break;
			case Axis_t.ZAxis:
				targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + distance);
				break;
		}
    }

    private void OnTriggerEnter(Collider other)
    {
		StopAllCoroutines();
		if (other.CompareTag("Player"))
			StartCoroutine(nameof(MoveDoor), true);
    }

    private void OnTriggerExit(Collider other)
    {
		StopAllCoroutines();
		if (other.CompareTag("Player"))
			StartCoroutine(nameof(MoveDoor), false);
	}

	private IEnumerator MoveDoor(bool shouldOpen)
    {
        if (shouldOpen)
        {
			while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
			{
				float step = speed * Time.deltaTime;
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, step);
				yield return null;
			}
			transform.localPosition = targetPosition;
		}

        else
        {
			while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.01f)
			{
				float step = speed * Time.deltaTime;
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, step);
				yield return null;
			}
			transform.localPosition = Vector3.zero;
		}
			
	}
}
