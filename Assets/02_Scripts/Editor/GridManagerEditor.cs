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

			if (GUILayout.Button("�׸��� ����"))
			{
				(target as GridManager).CreateGrid();
			}

			if (GUILayout.Button("�׸��� ����"))
			{
				(target as GridManager).DeleteGrid();
			}
			
			if (GUILayout.Button("�׸��� Ȯ��"))
			{
				(target as GridManager).ExtendGrid();
			}
		}
	}
}
