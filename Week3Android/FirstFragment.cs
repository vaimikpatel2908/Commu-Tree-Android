using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Week3Android
{
    public class FirstFragment : Fragment
    {
        List<Person> usersList;
        Activity context;
        DBHelper dBHelper;
        Spinner fsSpinner;
        SearchView fsSearchView;
        string[] spinnerArray = { "None", "Name", "City", "Education", "Age" };
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

            fsSpinner = firstView.FindViewById<Spinner>(Resource.Id.fsSpinnerId);
            fsSearchView = firstView.FindViewById<SearchView>(Resource.Id.fsSearchViewId);
            myListView = firstView.FindViewById<ListView>(Resource.Id.fslistview);

            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            usersList = dBHelper.SelectAllUsers(preferences.GetInt("userId", 0));

            fsSpinner.Adapter = new ArrayAdapter(context, Android.Resource.Layout.SimpleListItem1, spinnerArray);

            var adapter = new MyCustomAdapter(context, usersList);
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
            bool LoadData = true;

            List<Person> filteredList = new List<Person>();

            var newValue = e.NewText.Trim();
            // "None","Name", "City", "Education", "Age" 
            if (selectedSpinnerText == "None" || newValue == "")
            {
                filteredList = usersList;
                //var filteredAdapter = new MyCustomAdapter(context, usersList);

                //myListView.SetAdapter(filteredAdapter);
            }
            else if (selectedSpinnerText == "Name" && newValue != "")
            {
                foreach (Person person in usersList)
                {
                    if (person.username.ToLower().Contains(newValue.ToLower()))
                    {
                        filteredList.Add(person);
                    }
                }
            }
            else if (selectedSpinnerText == "Email" && newValue != "")
            {
                foreach (Person person in usersList)
                {
                    if (person.email.ToLower().Contains(newValue.ToLower()))
                    {
                        filteredList.Add(person);
                    }
                }
            }
            else if (selectedSpinnerText == "City" && newValue != "")
            {
                foreach (Person person in usersList)
                {
                    if (person.city.ToLower().Contains(newValue.ToLower()))
                    {
                        filteredList.Add(person);
                    }
                }
            }
            else if (selectedSpinnerText == "Education" && newValue != "")
            {
                foreach (Person person in usersList)
                {
                    if (person.education.ToLower().Contains(newValue.ToLower()))
                    {
                        filteredList.Add(person);
                    }
                }
            }
            else if (selectedSpinnerText == "Age" && newValue != "")
            {
                string[] agerange = newValue.Split('-');


                if (agerange.Length > 1)
                {
                    foreach (Person person in usersList)
                    {
                        if (person.age >= Convert.ToInt32(agerange[0]) && person.age <= Convert.ToInt32(agerange[1]))
                        {
                            filteredList.Add(person);
                        }
                    }
                }
                else
                {
                    foreach (Person person in usersList)
                    {
                        if (person.age <= Convert.ToInt32(agerange[0]))
                        {
                            filteredList.Add(person);
                        }
                    }

                }
            }
            else
            {
                LoadData = false;
                Toast.MakeText(context, "No Search Result Found", ToastLength.Short);
            }
            if (LoadData)
            {
                var filteredAdapter = new MyCustomAdapter(context, filteredList);

                myListView.SetAdapter(filteredAdapter);
            }
        }
    }
}