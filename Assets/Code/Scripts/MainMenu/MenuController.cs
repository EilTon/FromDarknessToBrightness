using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	#region Declarations public
	public List<Button> _buttons;
	public float _delay;
	public Color _chooseColor;
	public Color _normalColor;
	public Color _highligtedColor;
	#endregion

	#region Declarations private
	private float _vertical;
	private Button _currentButton;
	private int _cursor = 0;
	private float _timerDelay;
	#endregion

	#region Declarations Event Args

	#endregion

	#region Declarations Event Handler

	#endregion

	#region Declarations Event Call

	#endregion

	#region Functions Unity
	private void Awake()
	{
		#region Initialize

		#endregion
	}

	private void Start()
	{
		#region Initialize
		_currentButton = _buttons[0];
		_currentButton.GetComponent<Image>().color = _chooseColor;
		#endregion
	}

	private void Update()
	{
		#region Movement
		NavigateMenu();
		#endregion

		#region Actions
	
		#endregion

		#region Timer
		if (_timerDelay<_delay)
		{
			_timerDelay += Time.deltaTime;
		}
		
		#endregion
	}

	private void FixedUpdate()
	{
		#region Movement

		#endregion

		#region Actions

		#endregion

		#region Timer

		#endregion
	}
	#endregion

	#region Helper
	void NavigateMenu()
	{
		_vertical = Input.GetAxis("Vertical");
		if (_vertical > 0 && _timerDelay > _delay)
		{
			_cursor++;
			if (_cursor == _buttons.Count)
			{
				_cursor = 0;
			}
			_timerDelay = 0;

		}

		else if (_vertical < 0 && _timerDelay > _delay)
		{
			_cursor--;
			if (_cursor == -1)
			{
				_cursor = _buttons.Count -1;
			}
			_timerDelay = 0;
		}
		_currentButton.GetComponent<Image>().color = _normalColor;
		_currentButton = _buttons[_cursor];
		_currentButton.GetComponent<Image>().color = _chooseColor;

	}
	#endregion

	#region Coroutine

	#endregion
}
