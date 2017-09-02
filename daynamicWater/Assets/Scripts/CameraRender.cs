using UnityEngine;
using System.Collections;

public class CameraRender : MonoBehaviour {


	public Material ma;
	void Start () {

	}


	void Update () {

	}

	void OnRenderImage(RenderTexture src,RenderTexture dest){
		Graphics.Blit (src, dest, ma);
	}

	public void OnClickSliderChange(float value){
		ma.SetFloat ("_BurnSize",value*10);
	}
}
