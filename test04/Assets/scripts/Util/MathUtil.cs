using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtil {

	/// <summary>
	/// 最大公约数
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static int getMaxCommonDivisor(int a, int b) {
		int ret = 1;
		int len = a > b ? b : a;
		for (int i = 2; i <= len; i++) {
			if ((a % i == 0) && (b % i == 0)) {
				ret = i;
			}
		}
		return ret;
	}

	/// <summary>
	/// 最小公倍数
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static int getMinCommonMultiple(int a, int b) {
		int ret = a * b;
		int max = ret;
		int start = a < b ? b : a;
		for (int i = start; i < max; i += start) {
			if ((i % a == 0) && (i % b == 0)) {
				ret = i;
				break;
			}
		}
		return ret;
	}

	/// <summary>
	/// 将小数点右移 直到成为整数
	/// </summary>
	/// <param name="a"></param>
	/// <returns>返回最终的整数</returns>
	public static int floatPointMoveRight(float a) {
		float temp = a;
		int tempInt = Mathf.RoundToInt(temp);
		int max = 10;
		while (temp - (float)tempInt != 0f) {
			temp = temp * 10;
			tempInt = Mathf.RoundToInt(temp);
			max--;
			if (max < 1) {
				Debug.Log("floatPointMoveRight error: " + a.ToString());
			}
		}
		return tempInt;
	}
}
