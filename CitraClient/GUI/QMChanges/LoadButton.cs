using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CitraClient.GUI.QMChanges
{
	public class LoadButton
	{
		public static GameObject Buttonbase;

		public static Text Label;

		public static List<LoadButton> Allbutton = new List<LoadButton>();

		public static GameObject Button;

		public static string APINAME = "-CitraAPI";

		public LoadButton(Vector3 Position, Vector3 Scale, string ButtonText, string ObjName, Action action, Color LabelColor, Color ButtonColor, Sprite Buttonimage = null)
		{
			Buttonbase = UnityEngine.Object.Instantiate(LoadButtonExtensions.LoadButtonTemplate(), LoadButtonExtensions.LoadButtonTemplate().transform.parent);
			Buttonbase.name = ObjName + APINAME;
			Buttonbase.GetComponent<RectTransform>().localPosition = Position;
			Buttonbase.GetComponent<RectTransform>().localRotation = Quaternion.identity;
			Buttonbase.GetComponent<RectTransform>().localScale = Scale;
			Button = Buttonbase.transform.Find("GoButton").gameObject;
			Button.GetComponent<Image>().color = ButtonColor;
			Button.GetComponent<Image>().overrideSprite = Buttonimage;
			Label = Button.transform.Find("Text").gameObject.GetComponent<Text>();
			Label.text = ButtonText;
			Label.color = LabelColor;
			Buttonbase.transform.Find("Decoration_Left").gameObject.SetActive(value: false);
			Buttonbase.transform.Find("Loading Elements").gameObject.SetActive(value: false);
			Buttonbase.transform.Find("Decoration_Right").gameObject.SetActive(value: false);
			Buttonbase.transform.Find("Panel_Backdrop").gameObject.SetActive(value: false);
			Allbutton.Add(this);
			Button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
			if (action != null)
			{
				Button.GetComponent<Button>().onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(action));
			}
		}

		public GameObject GetButtonBase()
		{
			return Buttonbase;
		}

		public GameObject GetButtonObj()
		{
			return Button;
		}
	}
}
