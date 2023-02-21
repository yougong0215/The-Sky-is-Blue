using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
	/// <summary>
	/// ���ӳ����� ���Ǵ� ������ �׸��带 �����ϴ� Ŭ����
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

		[HideInInspector]
		public bool startingCreate = false;


		#region Unity Methods

		private void Start()
		{
			if (startingCreate == true)
			{
				CreateGrid();
			}
		}

		#endregion

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
		/// �׸��带 �����ϴ� �Լ�
		/// </summary>
		public void CreateGrid()
		{
			//�׸��尡 null�� �ƴϸ� ��ȯ
			if (grid != null)
			{
				Debug.Log("grid is not Null");
				return;
			}

			grid = new GridXZ<GridObject>(width, height, cellSize, gridOriginPosition, (GridXZ<GridObject> grid, int x, int z) => new GridObject(grid, x, z));
			CreateFloorTile(width, height, 1);
		}

		/// <summary>
		/// �׸��带 Ȯ���ϴ� �Լ�
		/// </summary>
		public void ExtendGrid()
		{
			//�׸��尡 null�̸� ��ȯ
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
		/// �׸��带 �����ϴ� �Լ�
		/// </summary>
		public void DeleteGrid()
		{
			//�׸��尡 null�̸� ��ȯ
			if (grid == null)
				return;

			grid = null;
			RemoveFloorTile();
		}
		#endregion

		#region FloorTile
		private void CreateFloorTile(int width, int height, int interval) //�ٴ� Ÿ���� �����ϴ� �Լ�
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

		private void RemoveFloorTile() //�ٴ� Ÿ���� �����ϴ� �Լ�
		{
			foreach (Transform trm in floorTiles)
			{
				Destroy(trm.gameObject);
			}
		}
		#endregion
	}
}