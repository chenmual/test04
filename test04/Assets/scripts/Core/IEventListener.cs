using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
	void onEvent(int eventCode, params object[] objs);
}