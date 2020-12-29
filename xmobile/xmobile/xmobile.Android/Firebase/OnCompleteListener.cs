using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xmobile.Droid.Firebase
{
    public class OnCompleteListener : Java.Lang.Object, IOnCompleteListener
    {

        public Action<Task> OnCompleteAction;

        public OnCompleteListener(Action<Task> onCompleteAction)
        {
            OnCompleteAction = onCompleteAction;
        }

        public void OnComplete(Task task)
        {
            OnCompleteAction(task);
        }

    }
}