using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
	public enum Direction
	{
		Down,
		Up,
		Right,
		Left
	}

    public class GridBuildingSystem : MonoBehaviour
    {
        private GridManager gridManager;

		private Direction buildDir = Direction.Down;

		[Header("Setting")]
		[SerializeField]
		private BuildingSO currentBuild;

		private void Awake()
		{
			gridManager = GetComponent<GridManager>();
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (!gridManager.Grid.GetXZ(MousePosition3D.Instance.GetMousePosition(), out int x, out int z))
					return;

				List<Vector2Int> positionList = currentBuild.GetGridPositionList(new Vector2Int(x, z), Direction.Down);

				bool canBuild = true;
				foreach (Vector2Int vec in positionList)
				{
					if (!gridManager.Grid.GetGridArrayObject(vec.x, vec.y).CanBuild())
					{
						canBuild = false;
						break;
					}
				}

				if (canBuild == true)
				{
					Transform tr = Instantiate(currentBuild.prefab, new Vector3(x, gridManager.Grid.OriginPosition.y, z), Quaternion.identity);
					foreach (Vector2Int vec in positionList)
					{
						gridManager.Grid.GetGridArrayObject(vec.x, vec.y).SetTransform(tr);
					}
				}
				else
				{
					Debug.Log("Can't Build here");
				}

				print($"{x}, {z}");
			}
		}
	}
}
