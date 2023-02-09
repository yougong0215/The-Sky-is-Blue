using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
	public class MousePosition3D : MonoBehaviour
	{
		public static MousePosition3D Instance;

		[SerializeField] private Camera cam;
		[SerializeField] private LayerMask layerMask;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}

		public Vector3 GetMousePosition()
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
			{
				return raycastHit.point;
			}
			else
			{
				return Vector3.zero;
			}
		}
	}
}
