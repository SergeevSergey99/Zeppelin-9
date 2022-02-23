using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
	[CreateAssetMenu(menuName = "Dialogue/CharacterInfo")]
	
	
	public class CharacterInfo : ScriptableObject {
		public Color color;
		//public Sprite sprite;
		public GameObject portret;
		public List<Pose> poses;
		[System.Serializable] public class Pose {
			public string poseName;
			public Sprite sprite;
		}
	}
}