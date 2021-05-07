using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QRCode.UI
{
    public static class UI
    {
        //---<UI VIEW>-------------------------------------------------------------------------------------------------<
        private static Dictionary<string, UIView> Views = new Dictionary<string, UIView>();
        
        public static void RegisterView(UIView view)
        {
            if(Views.ContainsKey(view.viewName) || string.IsNullOrEmpty(view.viewName))
                return;
            
            Views.Add(view.viewName, view);
        }

        public static UIView GetView(string name) => Views[name];

        public static void ShowView(string name) => GetView(name).Show();
        public static void HideView(string name) => GetView(name).Hide();

        public static void ShowViewAndHideOthers(string name)
        {
            foreach (var view in Views.Values)
            {
                if (name == view.viewName)
                    view.Show();
                else
                {
                    view.Hide();
                }
            }
        }
    }
}
