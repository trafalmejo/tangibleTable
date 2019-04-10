using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
	{
		public Transform target;
		public float smoothTime = 0.3F;
		private Vector3 velocity = Vector3.zero;
		public float distance = 5.0F;
		private float yVelocity = 0.0F;

		void Update()
		{
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref yVelocity, smoothTime);
		Vector3 position = target.position;
		position += Quaternion.Euler(0, yAngle, 0) * new Vector3(0, 0, -distance);
		transform.position = position;
		transform.LookAt(target);
	}
	}
