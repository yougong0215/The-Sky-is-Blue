using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    public class GridBuildingSystem : MonoBehaviour
    {
        private GridManager gridManager;

		[Header("Setting")]
		[SerializeField]
		private Transform currentBuild;

		private void Awake()
		{
			gridManager = GetComponent<GridManager>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				gridManager.Grid.GetXZ(MousePosition3D.Instance.GetMousePosition(), out int x, out int z);

				if (gridManager.Grid.GetGridArrayObject(x, z).CanBuild())
				{
					Transform tr = Instantiate(currentBuild, new Vector3(x, gridManager.Grid.OriginPosition.y, z), Quaternion.identity);
					gridManager.Grid.GetGridArrayObject(x, z).SetTransform(tr);
				}

				print($"{x}, {z}");
			}
		}
	}
}
