﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RS2
{
	/// <summary>
	/// ChoiceWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class ChoiceWindow : Window
	{
		public uint ID { get; set; }
		public enum eType
		{
			eItem, eSkill, eArt,
		}
		public eType Type { get; set; }

		public ChoiceWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CreateItemList("");
			foreach (var item in ListBoxItem.Items)
			{
				NameValueInfo info = item as NameValueInfo;
				if (info.Value == ID)
				{
					ListBoxItem.SelectedItem = item;
					ListBoxItem.ScrollIntoView(item);
					break;
				}
			}
		}

		private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			CreateItemList(TextBoxFilter.Text);
		}

		private void ListBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ButtonDecision.IsEnabled = ListBoxItem.SelectedIndex >= 0;
		}

		private void ButtonDecision_Click(object sender, RoutedEventArgs e)
		{
			NameValueInfo info = ListBoxItem.SelectedItem as NameValueInfo;
			if (info == null) return;
			ID = info.Value;
			Close();
		}

		private void CreateItemList(String filter)
		{
			List<NameValueInfo> items = null;
			switch(Type)
			{
				case eType.eItem:
					items = Info.Instance().Items;
					break;

				case eType.eSkill:
					items = Info.Instance().Skills;
					break;

				case eType.eArt:
					items = Info.Instance().Arts;
					break;
			}

			ListBoxItem.Items.Clear();
			foreach (var item in items)
			{
				if (String.IsNullOrEmpty(filter) || item.Name.IndexOf(filter) >= 0)
				{
					ListBoxItem.Items.Add(item);
				}
			}
		}
	}
}
