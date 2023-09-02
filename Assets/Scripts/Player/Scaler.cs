using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
	[SerializeField] float defaultHeight = 0.75f;
	[SerializeField] Camera cam;

	private void Resize()
	{
		float headHeight = cam.transform.position.y;
		float scale = defaultHeight / headHeight;
		transform.localScale = Vector3.one * scale;
	}

	void OnEnable()
	{
		Resize();
	}
}
