using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    /// <summary>
    /// 카메라 이동 및 줌 기능을 담당하는 함수
    /// </summary>
    public class CameraController : MonoBehaviour
    {
		[Header("Reference")]
		[SerializeField] private Transform cameraTransform;
		private Camera mainCam;

		[Header("Movement")]
		[SerializeField] private bool moveLock;
        [SerializeField] private float movementSpeed;
		[Range(0.01f, 1f)]
        [SerializeField] private float movementTime;
		[Range(0, 0.5f)]
		[SerializeField] private float camMoveAreaValue;

		[Header("Zoom")]
		[Tooltip("카메라의 각도에 따라 자동으로 지정")]
		[SerializeField] private Vector3 zoomAmount;
		[Range(0.01f, 1f)]
		[SerializeField] private float zoomTime;
		[SerializeField] private int currentZoomValue;
		[SerializeField] private int minZoomValue;
		[SerializeField] private int maxZoomValue;

		[Header("Target Values")]
        public Vector3 targetPosition;
		public Vector3 targetZoomPosition;

		private void Start()
		{
			mainCam = Camera.main;
			moveLock = true;

			//카메라의 목표 위치 & 줌 값을 현재 카메라의 위치 & 줌 값으로 초기화
			targetPosition = transform.position;
			targetZoomPosition = cameraTransform.localPosition;

			//카메라 각도에 맞추어 줌 증가값을 계산 후 초기화
			zoomAmount = new Vector3(0, -Mathf.Tan(cameraTransform.localEulerAngles.x * Mathf.Deg2Rad), 1);

			//zoomValue가 할당되어 있을 시에는 zoomValue에 값에 맞게 카메라 위치를 지정
			targetZoomPosition += zoomAmount * currentZoomValue;
			cameraTransform.localPosition = targetZoomPosition;
		}

		private void Update()
		{
			//목표값 설정
			ZoomInOut();
			Movement();

			//목표값으로 이동
			transform.position = Vector3.Lerp(transform.position, targetPosition, movementTime);
			cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetZoomPosition, zoomTime);
		}
		
		//줌인 & 줌아웃
		private void ZoomInOut()
		{
			float scrollY = Input.mouseScrollDelta.y;

			if (scrollY != 0)
			{
				currentZoomValue += (int)scrollY;
				currentZoomValue = Mathf.Clamp(currentZoomValue, minZoomValue, maxZoomValue);
				targetZoomPosition = zoomAmount * currentZoomValue;
			}
		}

		//상하좌우 대각선 이동
		private void Movement()
		{
			//카메라 고정 토글
			if (Input.GetKeyDown(KeyCode.Y))
			{
				moveLock = !moveLock;
			}
			if (moveLock == true)
			{
				targetPosition = transform.position;
				return;
			}

			float movementAmount = movementSpeed * Time.deltaTime;

			Vector3 mousePos = Input.mousePosition;
			mousePos = mainCam.ScreenToViewportPoint(mousePos);
			mousePos = mousePos - Vector3.one * 0.5f;
			mousePos.z = 0;

			if (mousePos.y >= camMoveAreaValue)
			{
				targetPosition += (transform.forward * movementAmount);
			}
			if (mousePos.y <= -camMoveAreaValue)
			{
				targetPosition += (transform.forward * -movementAmount);
			}
			if (mousePos.x >= camMoveAreaValue)
			{
				targetPosition += (transform.right * movementAmount);
			}
			if (mousePos.x <= -camMoveAreaValue)
			{
				targetPosition += (transform.right * -movementAmount);
			}
		}
	}
}
