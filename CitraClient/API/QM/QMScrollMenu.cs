using System;
using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CitraClient.API.QM
{
	public class QMScrollMenu
	{
		private Action<QMScrollMenu> action;

		public QMSingleButton backButton;

		public QMNestedButton baseMenu;

		public int currentIndex;

		private int index;

		public QMSingleButton indexButton;

		public QMSingleButton nextButton;

		private int pos;

		private int posX = 2;

		private int posY;

		public List<ScrollObject> qmButtons = new List<ScrollObject>();

		public QMScrollMenu(QMNestedButton btnMenu, string btnText, int buttonX, int buttonY, Action<QMScrollMenu> menuOpenAction, string btnTooltip, string menuTitle)
		{
			baseMenu = btnMenu;
			SetAction(action);
			nextButton = new QMSingleButton(baseMenu, 4f, 1.125f, "Next", delegate
			{
				ShowMenu(currentIndex + 1);
			}, "Go to next page", null, halfBtn: true);
			indexButton = new QMSingleButton(baseMenu, 4f, 1.565f, "Page:\n" + (currentIndex + 1) + " of " + (index + 1), delegate
			{
			}, "", null, halfBtn: true);
			backButton = new QMSingleButton(baseMenu, 4f, 2.005f, "Back", delegate
			{
				ShowMenu(currentIndex - 1);
			}, "Go back", null, halfBtn: true);
			indexButton.GetGameObject().GetComponentInChildren<Button>().enabled = false;
			indexButton.GetGameObject().GetComponentInChildren<Image>().enabled = false;
		}

		public QMScrollMenu(QMNestedButton basemenu)
		{
			baseMenu = basemenu;
			nextButton = new QMSingleButton(baseMenu, 4f, 1.125f, "Next", delegate
			{
				ShowMenu(currentIndex + 1);
			}, "Go to next page", null, halfBtn: true);
			indexButton = new QMSingleButton(baseMenu, 4f, 1.565f, "Page:\n" + (currentIndex + 1) + " of " + (index + 1), delegate
			{
			}, "", null, halfBtn: true);
			backButton = new QMSingleButton(baseMenu, 4f, 2.005f, "Back", delegate
			{
				ShowMenu(currentIndex - 1);
			}, "Go back", null, halfBtn: true);
			indexButton.GetGameObject().GetComponentInChildren<Button>().enabled = false;
			indexButton.GetGameObject().GetComponentInChildren<Image>().enabled = false;
		}

		public void ShowMenu(int MenuIndex)
		{
			if (MenuIndex > index || MenuIndex < 0)
			{
				return;
			}
			foreach (ScrollObject qmButton in qmButtons)
			{
				if (qmButton.index == MenuIndex)
				{
					qmButton.buttonBase?.SetActive(state: true);
				}
				else
				{
					qmButton.buttonBase?.SetActive(state: false);
				}
			}
			currentIndex = MenuIndex;
			indexButton.SetButtonText("Page:\n" + (currentIndex + 1) + " of " + (index + 1));
		}

		public void SetAction(Action<QMScrollMenu> open)
		{
			action = open;
			baseMenu.GetMainButton().GetGameObject().GetComponent<Button>()
				.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>((Action)delegate
			{
				Clear();
				action(this);
				ShowMenu(0);
			}));
		}

		public void Clear()
		{
			foreach (ScrollObject qmButton in qmButtons)
			{
				UnityEngine.Object.Destroy(qmButton.buttonBase.GetGameObject());
			}
			qmButtons.Clear();
			posX = 0;
			posY = 0;
			pos = 0;
			index = 0;
			currentIndex = 0;
		}

		public void Add(QMButtonBase button)
		{
			if (posX < 4)
			{
				posX++;
			}
			if (posX > 3 && posY < 3)
			{
				posX = 1;
				posY++;
			}
			if (pos == 12)
			{
				posX = 1;
				posY = 0;
				pos = 0;
				index++;
			}
			pos++;
			button.SetLocation(posX, posY);
			button.SetActive(state: false);
			qmButtons.Add(new ScrollObject
			{
				buttonBase = button,
				index = index
			});
		}
	}
}
