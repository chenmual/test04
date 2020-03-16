using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICalc : MonoBehaviour {

	public const string spt = "一";

	public int state = 0;
	public int count = 0;
	public int right = 0;

	public Text txtOpt1;
	public Text txtOpt2;
	public Text txtOpt;
	public Text txtResult;

	public Text txtIsRight;
	public Text txtCounter;

	public InputField iptResultF;
	public InputField iptResultA;
	public InputField iptResultB;

	public int questionType = 0;//0分数加减法

	private int numeratorRet;
	private int denominatorRet;

	public Button btnNext;
	public Button btnCheck;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(int nextState) {
		
	}

	public void calcNextQuestion() {
		switch (questionType) {
			case 0:
			default:
				int denominator1 = Random.Range(2, 24);
				int numerator1 = Random.Range(1, denominator1 - 1);

				int next = 24;
				if (denominator1 > 10) {
					if (Random.Range(1, 4) != 1) {//25%的概率刷10以内的分数
						next = 10;
					}
				}

				int denominator2 = Random.Range(2, next);
				int numerator2 = Random.Range(1, denominator2 - 1);

				denominatorRet = MathUtil.getMaxCommonDivisor(denominator1, numerator1);
				if (denominatorRet > 1) {
					denominator1 = denominator1 / denominatorRet;
					numerator1 = numerator1 / denominatorRet;
				}
				denominatorRet = MathUtil.getMaxCommonDivisor(denominator2, numerator2);
				if (denominatorRet > 1) {
					denominator2 = denominator2 / denominatorRet;
					numerator2 = numerator2 / denominatorRet;
				}

				denominatorRet = MathUtil.getMinCommonMultiple(denominator2, denominator1);
				int fac1 = numerator1 * (denominatorRet / denominator1);//第一个数的分子
				int fac2 = numerator2 * (denominatorRet / denominator2);//第二个数的分子

				txtOpt.text = "+";
				numeratorRet = fac1 + fac2;
				if (fac1 > fac2) {
					if (Random.Range(1, 3) != 1) {//算减法
						txtOpt.text = "-";
						numeratorRet = fac1 - fac2;
					}
				}

				int fac = MathUtil.getMaxCommonDivisor(denominatorRet, numeratorRet);
				if (fac > 1) {
					denominatorRet = denominatorRet / fac;
					numeratorRet = numeratorRet / fac;
					Debug.Log("fac=" + fac);
				}
				Debug.Log(numerator1 + "/" + denominator1 + " opt" + txtOpt.text  + " " + numerator2 + "/" + denominator2 + "=" + numeratorRet + "/" + denominatorRet);

				txtOpt1.text = numerator1 + "\n" + spt + "\n" + denominator1;
				txtOpt2.text = numerator2 + "\n" + spt + "\n" + denominator2;
				txtResult.text = spt;

				iptResultA.gameObject.SetActive(true);
				iptResultA.transform.Translate(new Vector3(-160f, 0, 0));
				iptResultB.gameObject.SetActive(true);
				iptResultB.transform.Translate(new Vector3(-160f, 0, 0));
				break;
		}
	}

	public bool checkCorrect() {
		if (iptResultA.text == numeratorRet.ToString() && iptResultB.text == denominatorRet.ToString()) {
			return true;
		}
		return false;
	}

	public void onCheckBtn() {
		count++;
		txtIsRight.gameObject.SetActive(true);
		if (checkCorrect()) {
			txtIsRight.text = "正确";
			txtIsRight.color = Color.green;
			right++;
		} else {
			txtIsRight.text = "错误";
			txtIsRight.color = Color.red;
		}
		txtResult.text = numeratorRet + "\n" + spt + "\n" + denominatorRet;
		refreshCounter();
		btnNext.gameObject.SetActive(true);
		btnCheck.gameObject.SetActive(false);
		iptResultA.transform.Translate(new Vector3(160f, 0, 0));
		//iptResultA.gameObject.SetActive(false);
		iptResultB.transform.Translate(new Vector3(160f, 0, 0));
		//iptResultB.gameObject.SetActive(false);
	}

	public void refreshCounter() {
		txtCounter.text = "答对数量/答题总数\n" + right + "/" + count;
	}

	public void onNextBtn() {
		txtIsRight.gameObject.SetActive(false);
		txtResult.text = spt;
		calcNextQuestion();
		btnNext.GetComponentInChildren<Text>().text = "下一题";
		btnNext.gameObject.SetActive(false);
		btnCheck.gameObject.SetActive(true);
		iptResultA.text = "";
		iptResultB.text = "";
	}
}
