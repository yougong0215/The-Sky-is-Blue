using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    [CreateAssetMenu(menuName = "SO/Building")]
    public class BuildingSO : ScriptableObject
    {
        public string nameString;
        public Transform prefab;
        public Transform visual;
        public int width;
        public int height;

        /// <summary>
        /// 건물의 범위를 Vector2Int로 반환하는 함수
        /// </summary>
        /// <param name="offset">범위 + offset</param>
        /// <param name="direction">방향 계산에 사용</param>
        /// <returns></returns>
        public List<Vector2Int> GetGridPositionList(Vector2Int offset, Direction direction)
		{
            List<Vector2Int> positionList = new List<Vector2Int>();

            switch (direction)
			{
                case Direction.Down:
                case Direction.Up:
                    for (int x = 0; x < width; x++)
					{
                        for (int y = 0; y < height; y++)
						{
                            positionList.Add(offset + new Vector2Int(x, y));
						}
					}
                    break;
                case Direction.Right:
                case Direction.Left:
                    for (int x = 0; x < height; x++)
                    {
                        for (int y = 0; y < width; y++)
                        {
                            positionList.Add(offset + new Vector2Int(x, y));
                        }
                    }
                    break;
            }

            return positionList;
		}
    }
}
