using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ingyu
{
    /// <summary>
    /// ī�޶� �̵� �� �� ����� ����ϴ� �Լ�
    /// </summary>
    public class CameraController : MonoBehaviour
    {
		[Header("Reference")]
		[SerializeField] private Transform cameraTransform;
		private Camera mainCam;

		[Header("Movement")]
		[SerializeField] private bool moveLock = false;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float movementTime;
		[Tooltip("Viewport ��ǥ��")]
		[SerializeField] private float moveValue = 0.4f;
		private float screenWidth;
		private float screenHeight;

		[Header("Zoom")]
		[Tooltip("ī�޶��� ������ ���� �ڵ����� ����")]
		[SerializeField] private Vector3 zoomAmount;
		[SerializeField] private int currentZoomValue;
		[SerializeField] private int minZoomValue;
		[SerializeField] private int maxZoomValue;

		[Header("Target Values")]
        public Vector3 targetPosition;
		public Vector3 targetZoomPosition;

		private void Start()
		{
			mainCam = Camera.main;
			screenWidth = Screen.width;
			screenHeight = Screen.height;

			//ī�޶��� ��ǥ ��ġ & �� ���� ���� ī�޶��� ��ġ & �� ������ �ʱ�ȭ
			targetPosition = transform.position;
			targetZoomPosition = cameraTransform.localPosition;

			//ī�޶� ������ ���߾� �� �������� ��� �� �ʱ�ȭ
			zoomAmount = new Vector3(0, -Mathf.Tan(cameraTransform.localEulerAngles.x * Mathf.Deg2Rad), 1);

			//zoomValue�� �Ҵ�Ǿ� ���� �ÿ��� zoomValue�� ���� �°� ī�޶� ��ġ�� ����
			targetZoomPosition += zoomAmount * currentZoomValue;
			cameraTransform.localPosition = targetZoomPosition;
		}

		private void Update()
		{
			//��ǥ�� ����
			ZoomInOut();
			Movement();

			//��ǥ������ �̵�
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementTime);
			cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetZoomPosition, Time.deltaTime * movementTime);
		}
		
		//���� & �ܾƿ�
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

		//�����¿� �밢�� �̵�
		private void Movement()
		{
			//ī�޶� ���� ���
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

			//�밢�� �̵���
			float diagonal = 0.3f;

			if (mousePos.y >= moveValue || (Mathf.Abs(mousePos.x) > moveValue && mousePos.y >= diagonal))
			{
				targetPosition += (transform.forward * movementAmount);
			}
			if (mousePos.y <= -moveValue || (Mathf.Abs(mousePos.x) > moveValue && mousePos.y <= -diagonal))
			{
				targetPosition += (transform.forward * -movementAmount);
			}
			if (mousePos.x >= moveValue || (Mathf.Abs(mousePos.y) > moveValue && mousePos.x >= diagonal))
			{
				targetPosition += (transform.right * movementAmount);
			}
			if (mousePos.x <= -moveValue || (Mathf.Abs(mousePos.y) > moveValue && mousePos.x <= -diagonal))
			{
				targetPosition += (transform.right * -movementAmount);
			}
		}
	}
}
