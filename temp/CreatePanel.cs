using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePanel : MonoBehaviour {

	public int Xcount = 10;
	public int Ycount = 100;
	public float Width=10;
	public float Height=10;


	public MeshRenderer msr;
	public MeshFilter msf;
	public Mesh mesh;

	public Gradient Col;



	#region mono
	void Start () {
		createPanel ();

	}

	void OnGUI(){
		if (GUILayout.Button ("dfdfdf")) {
			FillMesh ();
		}
	}
	

	public
	void Update () {
		FillMesh ();
	}

	#endregion


	#region internal function

	private void createPanel(){
		this.gameObject.name = "myPanel";
		MeshRenderer _r = this.gameObject.GetComponent<MeshRenderer> ();
		if (_r == null)
			msr = this.gameObject.AddComponent<MeshRenderer> ();
		else
			msr = _r;

		MeshFilter _f = this.gameObject.GetComponent<MeshFilter> ();
		if (_f == null) {
			msf = this.gameObject.AddComponent<MeshFilter> ();
		} else {
			msf = _f;
		}
		mesh = new Mesh ();
		FillMesh ();
	}


	public Vector3[] triDots;
	public List<int> listTriIndex=new List<int>();
	float xoffset=0.0f;
	float yoffset=0.0f;

	public Texture2D tex;
	public Vector2[] uvs;


	public Vector3 oriDot;
	public float maxL;



	private void FillMesh()
	{
		triDots=new Vector3[Xcount*Ycount];
		uvs=new Vector2[Xcount*Ycount];

		listTriIndex.Clear ();
		if (tex != null) {
			DestroyImmediate (tex);
		}
		tex = new Texture2D (Xcount,Ycount,TextureFormat.RGB24,false);
		tex.filterMode = FilterMode.Bilinear;
		xoffset = Width / Xcount;
		yoffset = Height / Ycount;
		int _c= 0;

		Vector3 v = transform.position;
		for (int i = 0; i < Ycount; i++)
		{
			for (int j = 0; j < Xcount; j++) 
			{
				_c = i * Xcount + j;
				float d = Vector2.Distance (new Vector2(i,j),new Vector2(oriDot.x,oriDot.y));
				Color _dotColor = Col.Evaluate (0.0f);
				if (d > maxL) {
					triDots [_c] =  new Vector3 (j*xoffset,0,i*yoffset);
				} 
				else 
				{
					float d1 = (1-d / maxL) * oriDot.z;
					_dotColor = Col.Evaluate (1-d / maxL);
					triDots [_c] =  new Vector3 (j*xoffset,d1,i*yoffset);
				}
				tex.SetPixel (j,i,_dotColor);
				uvs [_c] = new Vector2 ((float)j/Xcount,(float)i/Ycount);
				if (i < Ycount - 1 && j < Xcount - 1) {


					listTriIndex.Add (_c);
					listTriIndex.Add (_c+Xcount);
					listTriIndex.Add (_c+Xcount+1);


					listTriIndex.Add (_c+Xcount+1);
					listTriIndex.Add (_c+1);
					listTriIndex.Add (_c);
				}
			}
		}
		tex.Apply ();
		mesh.vertices = triDots;
		mesh.triangles = listTriIndex.ToArray ();
		mesh.RecalculateNormals ();
		msf.mesh = mesh;
		mesh.uv = uvs;
		msr.material.mainTexture = tex;


	}

	#endregion
}
