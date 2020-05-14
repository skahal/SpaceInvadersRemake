using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestScript))]
public class TestEditor : Editor
{
	string benchmarkResult;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var test = target as TestScript;
		test.OnGUI();
	}
}
