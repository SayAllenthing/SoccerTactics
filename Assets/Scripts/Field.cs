using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour 
{
	int width;
	int length;

	public void Init(int _width, int _length)
	{
		width = _width;
		length = _length;
	}
}
