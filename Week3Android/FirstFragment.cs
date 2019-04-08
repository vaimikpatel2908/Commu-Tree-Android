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
    public class FirstFragment : Fragment
    {
        List<Person> usersList;
        Activity context;
        DBHelper dBHelper;
        Spinner fsSpinner;
        SearchView fsSearchView;
        string[] spinnerArray = { "None","Name", "City", "Education", "Age" };
        ListView myListView;
        string selectedSpinnerText;

        public FirstFragment(Activity maincontext)
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
            View firstView = inflater.Inflate(Resource.Layout.FirstFragment, container, false);
            
            fsSpinner= firstView.FindViewById<Spinner>(Resource.Id.fsSpinnerId);
            fsSearchView = firstView.FindViewById<SearchView>(Resource.Id.fsSearchViewId);
            myListView = firstView.FindViewById<ListView>(Resource.Id.fslistview);

            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            usersList = dBHelper.SelectAllUsers(preferences.GetInt("userId",0));

            fsSpinner.Adapter= new ArrayAdapter(context, Android.Resource.Layout.SimpleListItem1, spinnerArray);

            var adapter = new MyCustomAdapter(context,usersList);
            myListView.SetAdapter(adapter);

            fsSpinner.ItemSelected += FsSpinner_ItemSelected;

            myListView.ItemClick += userListviewClickEvent;

            fsSearchView.QueryTextChange += SearchViewChangeEvent;

            return firstView;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void FsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedSpinnerText = spinnerArray[e.Position];
            //dBHelper.ShowAlert("spinnertext", selectedSpinnerText);
        }

        private void userListviewClickEvent(object sender, AdapterView.ItemClickEventArgs e)
        {
            //dBHelper.ShowAlert("User Data", usersList[e.Position].username.ToString());
            Intent UserDetailsIntent = new Intent(context, typeof(UserDetailsActivity));
            UserDetailsIntent.PutExtra("UserProfileId", usersList[e.Position].uId);
            StartActivity(UserDetailsIntent);
        }


        private void SearchViewChangeEvent(object sender, SearchView.QueryTextChangeEventArgs e)
        {

            List<Person> filteredList;

            var newValue = e.NewText;

            if (selectedSpinnerText != "None" || newValue == "")
            {
                var filteredAdapter = new MyCustomAdapter(context, usersList);

                myListView.SetAdapter(filteredAdapter);
            }
            else
            {
                Toast.MakeText(context, "Searching", ToastLength.Short);
            }

        }
    }
}