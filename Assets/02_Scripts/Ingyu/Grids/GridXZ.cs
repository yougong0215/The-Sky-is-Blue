using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    public class GridXZ<T>
    {
        private int width;
        public int Width => width;
        private int height;
        public int Height => height;
        private float cellSize;
        private Vector3 originPosition;
        public Vector3 OriginPosition => originPosition;

        private T[,] gridArray;

        public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<T>, int, int, T> gridFunc)
	    {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new T[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
		    {
                for (int z = 0; z < gridArray.GetLength(1); z++)
			    {
                    gridArray[x, z] = gridFunc(this, x, z);
			    }
		    }
        }

		public Vector3 GetWorldPosition(int x, int z) //그리드 좌표를 기반으로 월드 좌표를 반환하는 함수
        {
            return new Vector3(x, 0, z) * cellSize + originPosition;
        }

        public void GetXZ(Vector3 worldPosition, out int x, out int z) //벡터 값을 기반으로 그리드 좌표를 반환하는 함수
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        }

        public T GetGridArrayObject(int x, int z) //그리드 오브젝트를 반환하는 함수
		{
			try
			{
                return gridArray[x, z];
			}
			catch (Exception e)
			{
                Debug.LogError(e.Message);
                return default(T);
			}
		}

        public void ExtendGridArray(int extendSize) //그리드 배열을 확장하는 함수
		{
            T[,] newGridArray = new T[gridArray.GetLength(0) + extendSize, gridArray.GetLength(1) + extendSize];
            int originWidth = gridArray.GetLength(0);
            int originHeight = gridArray.GetLength(1);

            for (int x = 0; x < originWidth; x++)
			{
                Array.Copy(gridArray, x * originHeight, newGridArray, x * newGridArray.GetLength(1), originHeight);
			}

            gridArray = (T[,])newGridArray.Clone();

            width += extendSize;
            height += extendSize;

            for (int i = 0; i < gridArray.GetLength(0); i++)
			{
                for (int j = 0; j < gridArray.GetLength(1); j++)
				{
                    Debug.Log(i.ToString() + ", " + j.ToString() + ": " + (gridArray[i, j] != null).ToString());
				}
			}
		}
    }
}
