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

			GridManager tar = target as GridManager;
			bool startingCreate = tar.startingCreate;

			GUILayout.Space(20f);

			GUILayout.Label("그리드 컨트롤러");

			startingCreate = GUILayout.Toggle(startingCreate, "시작시 그리드 생성");
			tar.startingCreate = startingCreate;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("그리드 생성") && Application.isPlaying)
				tar.CreateGrid();

			if (GUILayout.Button("그리드 제거") && Application.isPlaying)
				tar.DeleteGrid();
			
			if (GUILayout.Button("그리드 확장") && Application.isPlaying)
				tar.ExtendGrid();

			GUILayout.EndHorizontal();
		}
	}
}
