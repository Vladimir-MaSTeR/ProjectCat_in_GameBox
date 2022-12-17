using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickRotationCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	/// <summary>
	/// родитель камеры
	/// </summary>
	[SerializeField] private Transform _objCamera;
	/// <summary>
	/// задний фон
	/// </summary>
	[SerializeField] private RectTransform _fonObj;
	[Header("ѕоворот в право")]
	[SerializeField] private bool onRotationRight = true;

	 private bool _clickDown = false;

	// Start is called before the first frame update
	void Start()
	{

		//_fonObj.anchoredPosition = new Vector3(-283f, 3417f, 20000f);

	}

	

	public void OnPointerClick(PointerEventData eventData)
	{
	//	clickTurnR(onRotationRight);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_clickDown = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_clickDown = true;
		_rotationCamera(); 
		
	}


	private void _rotationCamera()
    {
		if (_clickDown == true)
		{
			clickTurnR(onRotationRight);
			Invoke("_rotationCamera", 0.05f);
		}
	}


	private void clickTurnR(bool _turn)
	{

		float yZoomDelta = _objCamera.localEulerAngles.y;
		if (_turn == true)
		{
			if (yZoomDelta > 360 - 12f || yZoomDelta < 20) //-15f+3f
			{

				yZoomDelta = yZoomDelta - .5f;
				checkFonPosit(.5f);

				// Debug.Log("R+ " + yZoomDelta);
			}
			else
			{
				_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, -12f, _objCamera.rotation.z);


			}
		}
		else // if (_turn == false)
		{
			{

				if (yZoomDelta < 18 || yZoomDelta > 360 - 15f) // 15f+3f
				{
					yZoomDelta = yZoomDelta + .5f;
					checkFonPosit(-.5f);

					// Debug.Log("L+ " + yZoomDelta);
				}
				else
				{

					_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, 18f, _objCamera.rotation.z);

				}

			}

		}
		_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, yZoomDelta, _objCamera.rotation.z);

	}


	private void checkFonPosit(float _deltaZoom)
	{
		float xTurnFon = _fonObj.localPosition.x + ((4500f / 15f) * _deltaZoom);

		var _fonlocalPosition = new Vector3(xTurnFon, _fonObj.localPosition.y, _fonObj.localPosition.z);
		// Debug.Log(_fonlocalPosition);

		_fonObj.localPosition = _fonlocalPosition;
	}

}
