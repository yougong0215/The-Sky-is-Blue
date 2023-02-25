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

			GUILayout.Label("�׸��� ��Ʈ�ѷ�");

			startingCreate = GUILayout.Toggle(startingCreate, "���۽� �׸��� ����");
			tar.startingCreate = startingCreate;

			GUILayout.BeginHorizontal();

			if (GUILayout.Button("�׸��� ����") && Application.isPlaying)
				tar.CreateGrid();

			if (GUILayout.Button("�׸��� ����") && Application.isPlaying)
				tar.DeleteGrid();
			
			if (GUILayout.Button("�׸��� Ȯ��") && Application.isPlaying)
				tar.ExtendGrid();

			GUILayout.EndHorizontal();
		}
	}
}
