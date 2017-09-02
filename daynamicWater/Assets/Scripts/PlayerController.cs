using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public FlipPage fp;
	public Slider slider;
	public Transform BillBoard;
	public Text InfoTxt;
	void Start () {
		InfoTxt.text = "通过 WSDA或者方向键可以控制相机移动";
	}
	

	void Update () {

		if (Input.GetKey (KeyCode.UpArrow)||Input.GetKey(KeyCode.W)) {
			goFront (1);
		}
		if (Input.GetKey (KeyCode.DownArrow)||Input.GetKey(KeyCode.S)) {
			goFront (-1);
		}
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) {

			turnRight (1);
		}

		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) {
			turnRight (-1);
		}
	
	}


	private void goFront(int k){
		transform.Translate (transform.forward*k,Space.World);
	}

	private void turnRight(int k){
		transform.Rotate (transform.up * k,Space.World);
	}




	public void OnClickLookWater(){

		InfoTxt.text = "动态水";
		transform.position = new Vector3 (305,13,277);
		transform.rotation = Quaternion.Euler (0,173,0);

	}

	public void OnClickFlipPage(){
		InfoTxt.text = "再次点击可以查看翻页效果";
		transform.position = new Vector3 (370,13,242);
		transform.rotation = Quaternion.Euler (0,277,0);
		fp.FlipPage2 ();
	}


	public void OnClickCurve(){
		InfoTxt.text = "基于UGUI的3D曲线效果";
		transform.position = new Vector3 (306,13,275);
		transform.rotation = Quaternion.Euler (0,100,0);

	}

	public void OnClickBillBoard(){

		//transform.position = new Vector3 (306,13,275);
		//transform.rotation = Quaternion.Euler (0,100,0);
		InfoTxt.text = "摄像机在移动，但是广告牌始终朝向视角方向";
		StartCoroutine(roundBillBoard());
	}

	public IEnumerator roundBillBoard(){
		for (int i = 0; i < 360; i++) {
		 transform.LookAt (BillBoard,Vector3.up);
			transform.RotateAround (BillBoard.transform.position, Vector3.up, 1);
			yield return   new WaitForSeconds (0.04f);
		}
	}


	public void OnClickGaussBlur(){
		
		slider.gameObject.SetActive (!slider.gameObject.activeInHierarchy);
		if (slider.gameObject.activeInHierarchy) {
			InfoTxt.text = "拖动滑条可以改变高斯模糊的程度";
		}
	}



}
