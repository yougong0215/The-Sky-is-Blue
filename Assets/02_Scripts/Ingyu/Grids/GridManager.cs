using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
	/// <summary>
	/// 게임내에서 사용되는 마을의 그리드를 제어하는 클래스
	/// </summary>
	public class GridManager : MonoBehaviour
	{
		private GridXZ<GridObject> grid;
		
		public Transform floorTile;

		public int width;
		public int height;
		public float cellSize;
		public Vector3 gridOriginPosition;

		private Transform[] floorTiles;

		#region Grid
		public class GridObject
		{
			private GridXZ<GridObject> grid;
			private int x;
			private int z;
			
			public GridObject(GridXZ<GridObject> grid, int x, int z)
			{
				this.grid = grid;
				this.x = x;
				this.z = z;
			}
		}

		/// <summary>
		/// 그리드를 생성하는 함수
		/// </summary>
		public void CreateGrid()
		{
			if (grid != null)
			{
				Debug.Log("grid is not Null");
				return;
			}

			grid = new GridXZ<GridObject>(width, height, cellSize, gridOriginPosition, (GridXZ<GridObject> grid, int x, int z) => new GridObject(grid, x, z));
			CreateFloorTile(width, height, 1);
		}

		/// <summary>
		/// 그리드를 확장하는 함수
		/// </summary>
		public void ExtendGrid()
		{
			if (grid == null)
			{
				Debug.Log("grid is Empty");
				return;
			}

			grid.ExtendGridArray(1);
			width = grid.Width;
			height = grid.Height;

			RemoveFloorTile();
			CreateFloorTile(width, height, 1);
		}

		/// <summary>
		/// 그리드를 삭제하는 함수
		/// </summary>
		public void DeleteGrid()
		{
			if (grid == null)
				return;

			grid = null;
			RemoveFloorTile();
		}
		#endregion

		#region FloorTile
		private void CreateFloorTile(int width, int height, int interval) //바닥 타일을 생성하는 함수
		{
			floorTiles = new Transform[(width / interval) * (height / interval)];

			int count = 0;
			for (int x = 0; x < (width / interval); x++)
			{
				for (int z = 0; z < (height / interval); z++)
				{
					floorTiles[count] = Instantiate(floorTile, grid.GetWorldPosition(x * interval, z * interval), Quaternion.identity);
					floorTiles[count].transform.localScale = Vector3.one * cellSize;
					count++;
				}
			}
		}

		private void RemoveFloorTile() //바닥 타일을 제거하는 함수
		{
			foreach (Transform trm in floorTiles)
			{
				Destroy(trm.gameObject);
			}
		}
		#endregion
	}
}
