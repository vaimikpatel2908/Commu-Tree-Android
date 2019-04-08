using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Week3Android
{
    public class SecondFragment : Fragment
    {
        List<Person> usersList;
        Activity context;
        DBHelper dBHelper;
        ListView myListView;

        public SecondFragment(Activity maincontext)
        {
            context = maincontext;
            dBHelper = new DBHelper(context);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View firstView = inflater.Inflate(Resource.Layout.SecondFragment, container, false);

            myListView = firstView.FindViewById<ListView>(Resource.Id.sslistview);

            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            usersList = dBHelper.GetUserFavoritesList(preferences.GetInt("userId", 0));

            var adapter = new MyCustomAdapter(context, usersList);

            myListView.SetAdapter(adapter);

            myListView.ItemClick += userListviewClickEvent;

            return firstView;
           // return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void userListviewClickEvent(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent UserDetailsIntent = new Intent(context, typeof(UserDetailsActivity));
            UserDetailsIntent.PutExtra("UserProfileId", usersList[e.Position].uId);
            StartActivity(UserDetailsIntent);
        }
    }
}