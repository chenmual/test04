using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fraction
{
	/// <summary>
	/// 分子部分(作为整数存储)
	/// </summary>
	public int molecule;

	/// <summary>
	/// 分母部分(作为分子存储)
	/// </summary>
	public int denominator;

	public Fraction(int o) {
		this.molecule = o;
		this.denominator = 1;
	}

	public Fraction(float f) {
	}

	public Fraction add(Fraction fra) {
		// 两个分数相加
		return fra;
	}

	public Fraction add(float f) {
		// 分数+小数
		// 将小数化为分数
		Fraction fra = new Fraction(f);
		return fra;
	}

	public Fraction add(int b) {
		// 分子+整数
		// 将整数化为分数
		Fraction fra = new Fraction(b);
		return add(fra);
	}


	public void dec() {

	}

}
