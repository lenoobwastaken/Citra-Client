using System;
using System.Linq;
using System.Reflection;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine.UI;

namespace CitraClient.API.QM
{
	public static class Popups
	{
		public delegate void ShowAlertDelegate(VRCUiPopupManager popupManager, string title, string body, float timeout);

		public delegate void ShowStandardPopupV21Fn(VRCUiPopupManager popupManager, string title, string body, string buttonText, Il2CppSystem.Action onClick, Il2CppSystem.Action<VRCUiPopup> additionalSetup = null);

		public delegate void ShowStandardPopupV2Fn(VRCUiPopupManager popupManager, string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction, Il2CppSystem.Action<VRCUiPopup> additionalSetup = null);

		private delegate void ShowInputPopupWithCancelDelegate(VRCUiPopupManager popupManager, string title, string preFilledText, InputField.InputType inputType, bool useNumericKeypad, string submitButtonText, Il2CppSystem.Action<string, List<KeyCode>, Text> submitButtonAction, Il2CppSystem.Action cancelButtonAction, string placeholderText = "Enter text....", bool hidePopupOnSubmit = true, Il2CppSystem.Action<VRCUiPopup> additionalSetup = null, bool param_11 = false, int param_12 = 0);

		private static ShowStandardPopupV21Fn _showStandardPopupV21Fn;

		private static ShowAlertDelegate _showAlertDelegate;

		private static ShowInputPopupWithCancelDelegate _showInputPopupWithCancelDelegate;

		private static VRCUiManager _uiManagerInstance;

		private static ShowAlertDelegate ShowAlertFn
		{
			get
			{
				if (_showAlertDelegate != null)
				{
					return _showAlertDelegate;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods().Single(delegate(MethodInfo m)
				{
					if (m.ReturnType != typeof(void))
					{
						return false;
					}
					return m.GetParameters().Length == 3 && XrefScanner.XrefScan(m).Any((XrefInstance x) => x.Type == XrefType.Global && x.ReadAsObject()?.ToString() == "UserInterface/MenuContent/Popups/AlertPopup");
				});
				_showAlertDelegate = (ShowAlertDelegate)System.Delegate.CreateDelegate(typeof(ShowAlertDelegate), method);
				return _showAlertDelegate;
			}
		}

		private static ShowStandardPopupV21Fn ShowUiStandardPopupV21
		{
			get
			{
				if (_showStandardPopupV21Fn != null)
				{
					return _showStandardPopupV21Fn;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault((MethodInfo it) => it.GetParameters().Length == 5 && !it.Name.Contains("PDM") && XrefScanner.XrefScan(it).Any((XrefInstance jt) => jt.Type == XrefType.Global && jt.ReadAsObject()?.ToString() == "UserInterface/MenuContent/Popups/StandardPopupV2"));
				_showStandardPopupV21Fn = (ShowStandardPopupV21Fn)System.Delegate.CreateDelegate(typeof(ShowStandardPopupV21Fn), method);
				return _showStandardPopupV21Fn;
			}
		}

		private static ShowInputPopupWithCancelDelegate ShowInputPopupWithCancelFn
		{
			get
			{
				if (_showInputPopupWithCancelDelegate != null)
				{
					return _showInputPopupWithCancelDelegate;
				}
				MethodInfo method = typeof(VRCUiPopupManager).GetMethods().Single((MethodInfo m) => m.Name.StartsWith("Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_Int32_") && !m.Name.Contains("PDM") && XrefScanner.XrefScan(m).Any((XrefInstance x) => x.Type == XrefType.Global && x.ReadAsObject()?.ToString() == "UserInterface/MenuContent/Popups/InputKeypadPopup"));
				_showInputPopupWithCancelDelegate = (ShowInputPopupWithCancelDelegate)System.Delegate.CreateDelegate(typeof(ShowInputPopupWithCancelDelegate), method);
				return _showInputPopupWithCancelDelegate;
			}
		}

		public static VRCUiManager Instance
		{
			get
			{
				if (_uiManagerInstance == null)
				{
					_uiManagerInstance = (VRCUiManager)typeof(VRCUiManager).GetMethods().First((MethodInfo x) => x.ReturnType == typeof(VRCUiManager)).Invoke(null, new object[0]);
				}
				return _uiManagerInstance;
			}
		}

		public static void HideCurrentPopup()
		{
			Instance.HideScreen("POPUP");
		}

		public static void ShowAlert(this VRCUiPopupManager popupManager, string title, string body, float timeout = 0f)
		{
			ShowAlertFn(popupManager, title, body, timeout);
		}

		public static void ShowInputPopupWithCancel(this VRCUiPopupManager popupManager, string title, string preFilledText, InputField.InputType inputType, bool useNumericKeypad, string submitButtonText, System.Action<string, List<KeyCode>, Text> submitButtonAction, System.Action cancelButtonAction, string placeholderText = "Enter text....", bool hidePopupOnSubmit = true, System.Action<VRCUiPopup> additionalSetup = null)
		{
			ShowInputPopupWithCancelFn(popupManager, title, preFilledText, inputType, useNumericKeypad, submitButtonText, submitButtonAction, cancelButtonAction, placeholderText, hidePopupOnSubmit, additionalSetup);
		}

		public static void ShowStandardPopupV2(this VRCUiPopupManager popupManager, string title, string body, string leftButtonText, System.Action leftButtonAction, string rightButtonText, System.Action rightButtonAction, System.Action<VRCUiPopup> additonalSetup)
		{
			popupManager.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_0(title, body, leftButtonText, leftButtonAction, rightButtonText, rightButtonAction, additonalSetup);
		}

		public static void ShowStandardPopupV2(this VRCUiPopupManager popupManager, string title, string body, string buttonText, System.Action onClick, System.Action<VRCUiPopup> onCreated = null)
		{
			ShowUiStandardPopupV21(popupManager, title, body, buttonText, onClick, onCreated);
		}
	}
}
