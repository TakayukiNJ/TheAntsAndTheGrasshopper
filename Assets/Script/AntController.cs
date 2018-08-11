using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;

	public float gravity;
	public float speedZ;
	public float speedJump;
	
	// Use this for initialization
	void Start () {
		// 必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		// Inputを検知して前にすすめる
		if (Input.GetAxis("Vertical") > 0.0f) {
			moveDirection.z = Input.GetAxis("Vertical") * speedZ;
		} else {
			moveDirection.z = 0;
		}

		// 方向転換
		transform.Rotate(0, Input.GetAxis("Holizontal") * 3, 0);

		// ジャンプ
		if (Input.GetButton("Jump")) {
			moveDirection.y = speedJump;
			animator.SetTrigger("Jump");
		}

		// 重力分の力を毎フレーム追加
		moveDirection.y -= gravity * Time.deltaTime;

		// 移動実行
		Vector3.globalDirection = transform.transformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);

		// 移動後接地したらY方向の速度はリセットする
		if (controller.isGrounded) moveDirection.y = 0;

		// 速度が0以上なら走っているフラグをtrueにする
		animator.SetBool("run", moveDirection.z > 0.0f);
	}
}
