using System.Collections;
using System.Collections.Generic;
using Argyle.UnclesToolkit.Base;
using UnityEngine;

public class Wall : ArgyleComponent
{
	//mostly just an identifier right now
	
	private BoxCollider2D col;
	public BoxCollider2D Col
	{
		get
		{
			if (col == null)
				col = GetComponent<BoxCollider2D>();

			return col;
		}
	}

}
