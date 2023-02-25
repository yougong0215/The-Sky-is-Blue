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
		[SerializeField] private bool moveLock;
        [SerializeField] private float movementSpeed;
		[Range(0.01f, 1f)]
        [SerializeField] private float movementTime;
		[Range(0, 0.5f)]
		[SerializeField] private float camMoveAreaValue;

		[Header("Zoom")]
		[Tooltip("ī�޶��� ������ ���� �ڵ����� ����")]
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
			transform.position = Vector3.Lerp(transform.position, targetPosition, movementTime);
			cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetZoomPosition, zoomTime);
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
