﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Week3Android
{
    [Activity(Label = "Commu-Tree")]
    public class UserListActivity : Activity
    {
        DBHelper dBHelper;
        Fragment[] _fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Step # 1:
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            int userid = prefs.GetInt("userId", 0);

            SetContentView(Resource.Layout.UsersList);
            // Create your application here

            dBHelper = new DBHelper(this);

            if (userid == 0)            {
                Intent loginIntent = new Intent(this, typeof(LoginActivity));
                StartActivity(loginIntent);
            }

            //Step # 2:
            ////Add 2 pages : 2 separate Screen (Fragment classes)
            _fragments = new Fragment[]
             {
                new FirstFragment(this),
                new SecondFragment(this),
                new ThirdFragment(this)
             };

            //// Get our button from the layout resource,
            AddTabToActionBar("People", Resource.Drawable.icon8); //First Tab
            AddTabToActionBar("Friends", Resource.Drawable.icon8); //Second Tab
            AddTabToActionBar("Requests", Resource.Drawable.icon8); //Second Tab

            // Get our button from the layout resource,
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        // add your code   
                        Intent intent = new Intent(this, typeof(UserListActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuItem2:
                    {
                        //Seed Data Menu
                        // add your code  

                        List<Person> listOfUsers = new Person().seedData();

                        foreach (Person person in listOfUsers)
                        {
                            dBHelper.SeedData(person.username, person.email, person.age, person.password,person.dob,person.address,person.city,person.maritalStatus,person.education,person.profession);
                        }
                        Toast.MakeText(this, "Data Seeded to application",ToastLength.Long);

                        Intent intent = new Intent(this, typeof(UserListActivity));
                        StartActivity(intent);

                        return true;
                    }
                case Resource.Id.menuItem3:
                    {
                        //User Profile Menu
                        // add your code
                        Intent intent = new Intent(this, typeof(UserProfileActivity));
                        StartActivity(intent);
                        return true;
                    }
                case Resource.Id.menuItem4:
                    {
                        //Clean Database Menu
                        // add your code
                        dBHelper.cleanData();
                        Intent RegisterIntent = new Intent(this, typeof(MainActivity));
                        StartActivity(RegisterIntent);
                        return true;
                    }
                case Resource.Id.menuItem5:
                    {
                        //Logout Menu
                        // add your code
                        Intent loginIntent = new Intent(this, typeof(LoginActivity));
                        StartActivity(loginIntent);
                        return true;
                    }
                case Resource.Id.menuItem6:
                    {
                        //Home Menu
                        // add your code
                        Intent intent = new Intent(this, typeof(UserListActivity));
                        StartActivity(intent);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        void AddTabToActionBar(string tabTitle, int ImageId)
        {
            ActionBar.Tab tab = ActionBar.NewTab().SetText(tabTitle);

            //tab.SetIcon(ImageId);
            tab.SetText(tabTitle);

            tab.TabSelected += TabOnTabSelected;

            ActionBar.AddTab(tab);
        }



        void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;

            //Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
            Fragment frag = _fragments[tab.Position];

            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }

    }
}