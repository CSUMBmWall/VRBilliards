using UnityEngine;
using System.Collections;

public class ObjectHighlight : MonoBehaviour {	
	
	private Renderer rend;
	private GameObject go;
	
	private Color startColor;
	private bool _displayObjectName = false;

	void OnGUI() {
		DisplayName();
	}

	void Start() {
		rend = GetComponent<Renderer>();
		go = GetComponent<GameObject>();
	}

	void OnMouseEnter() {
		startColor = rend.material.color;
		rend.material.color = Color.blue;
		_displayObjectName = true;
	}

	void OnMouseExit() {
		rend.material.color = startColor;
		_displayObjectName = false;
	}

	public void DisplayName() {
		if (_displayObjectName) {
			GUI.Box(new Rect(Event.current.mousePosition.x - 155, Event.current.mousePosition.y, 150,25), transform.name);
		}
	}
}

