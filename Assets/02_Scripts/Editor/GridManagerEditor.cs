using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ingyu
{
	[CustomEditor(typeof(GridManager))]
	public class GridManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("그리드 생성"))
			{
				(target as GridManager).CreateGrid();
			}

			if (GUILayout.Button("그리드 제거"))
			{
				(target as GridManager).DeleteGrid();
			}
			
			if (GUILayout.Button("그리드 확장"))
			{
				(target as GridManager).ExtendGrid();
			}
		}
	}
}
