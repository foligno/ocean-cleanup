// Check required for build process.
#if UNITY_EDITOR
using OceanCleanup.Shared.Components;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;

namespace OceanCleanup.Shared.Editor
{
	[CustomEditor(typeof(Spawner))]
	public class SpawnerEditor : UnityEditor.Editor
	{
		private readonly BoxBoundsHandle _boxBoundsHandle = new();
		private readonly Bounds _defaultBounds = new(Vector3.zero, Vector3.one);

		private bool EditingRegion => EditMode.editMode == EditMode.SceneViewEditMode.Collider && EditMode.IsOwner(this);

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			Spawner currentSpawner = target as Spawner;
		
			// Invalid object, escape
			if (!currentSpawner) return;
		
			// No need to show bounds settings unless random location is enabled 
			if (currentSpawner.RandomLocation)
			{
				EditMode.DoEditModeInspectorModeButton(EditMode.SceneViewEditMode.Collider,
					"Edit Spawn Region",
					EditorGUIUtility.IconContent("EditCollider"),
					GetBoundsOfTarget, 
					this);
				
				EditorGUI.BeginChangeCheck();
				currentSpawner.SpawnRegion = EditorGUILayout.BoundsField("Spawn Region:",
					currentSpawner.SpawnRegion, 
					null);
		
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(currentSpawner, "Change extents of SpawnRegion");						

					EditorUtility.SetDirty(currentSpawner);
				}
			}
		}

		private void OnSceneGUI()
		{
			Spawner currentSpawner = target as Spawner;
		
			// Invalid object, escape
			if (!currentSpawner) return;
		
			// No need to show bounds unless object is selected and random location is enabled 
			if (!Selection.Contains(currentSpawner.gameObject)) return;
			if (!currentSpawner.RandomLocation) return;
		
			Handles.color = Color.yellow;

			// Hide handles if not editing
			if (EditingRegion)
			{
				CopyRegionPropertiesToHandle(currentSpawner);
			
				// Set handle matrix to local instead of world
				// Offset it by spawnregion center point
				Handles.matrix = currentSpawner.gameObject.transform.localToWorldMatrix;

				EditorGUI.BeginChangeCheck();
				_boxBoundsHandle.DrawHandle();
		
				if (EditorGUI.EndChangeCheck())
				{
					Undo.RecordObject(currentSpawner, "Change extents of SpawnRegion");						
					CopyHandlePropertiesToRegion(currentSpawner);
					EditorUtility.SetDirty(currentSpawner);
				}
			}
			else
			{
				Handles.DrawWireCube(currentSpawner.gameObject.transform.position + currentSpawner.SpawnRegion.center, currentSpawner.SpawnRegion.size);
			}
		}

		private Bounds GetBoundsOfTarget()
		{
			Spawner currentSpawner = target as Spawner;
			
			return currentSpawner?.SpawnRegion ?? new Bounds(_defaultBounds.center, _defaultBounds.size);
		}
	
		private void CopyRegionPropertiesToHandle(Spawner currentSpawner)
		{
			_boxBoundsHandle.center = currentSpawner.SpawnRegion.center;
			_boxBoundsHandle.size = currentSpawner.SpawnRegion.extents * 2;
		}

		private void CopyHandlePropertiesToRegion(Spawner currentSpawner)
		{
			currentSpawner.SpawnRegion.center = _boxBoundsHandle.center;
			currentSpawner.SpawnRegion.extents = _boxBoundsHandle.size / 2;
		}
	}
}

#endif