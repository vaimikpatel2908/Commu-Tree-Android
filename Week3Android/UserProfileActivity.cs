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
    [Activity(Label = "UserProfileActivity")]
    public class UserProfileActivity : Activity
    {
        Button favBtn;
        ImageView userImageView;
        EditText usernameTxt, emailTxt, ageTxt, passwordTxt, dobTxt, addressTxt, cityTxt, mrgStatusTxt, educationTxt, professionTxt;
        DBHelper dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserProfile);
            // Create your application here

            dBHelper = new DBHelper(this);
            userImageView = FindViewById<ImageView>(Resource.Id.userImageView);
            usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
            emailTxt = FindViewById<EditText>(Resource.Id.emailTxt);
            passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
            ageTxt = FindViewById<EditText>(Resource.Id.ageTxt);
            dobTxt = FindViewById<EditText>(Resource.Id.dobTxt);
            addressTxt = FindViewById<EditText>(Resource.Id.addressTxt);
            cityTxt = FindViewById<EditText>(Resource.Id.cityTxt);
            mrgStatusTxt = FindViewById<EditText>(Resource.Id.mrgStatusTxt);
            educationTxt = FindViewById<EditText>(Resource.Id.educationTxt);
            professionTxt = FindViewById<EditText>(Resource.Id.professionTxt);
            favBtn = FindViewById<Button>(Resource.Id.AddToFavorites);

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            int userId = sharedPreferences.GetInt("userId", 0);

            Person user = dBHelper.GetDataByUserId(userId);

            // assigning Data to textviews
            usernameTxt.Text = user.username;
            emailTxt.Text = user.email;
            ageTxt.Text = Convert.ToString(user.age);
            dobTxt.Text = user.dob;
            passwordTxt.Text = user.password;
            addressTxt.Text = user.address;
            cityTxt.Text = user.city;
            mrgStatusTxt.Text = user.maritalStatus;
            educationTxt.Text = user.education;
            professionTxt.Text = user.profession;
            userImageView.SetImageResource(user.userImage);

            favBtn.Click += delegate
            {
                if (favBtn.Text.Trim().ToLower() == "Edit".ToLower())
                {
                    //send request code
                    try
                    {
                        usernameTxt.Enabled = true;
                        //emailTxt.Enabled = true;
                        ageTxt.Enabled = true;
                        passwordTxt.Enabled = true;
                        dobTxt.Enabled = true;
                        addressTxt.Enabled = true;
                        cityTxt.Enabled = true;
                        mrgStatusTxt.Enabled = true;
                        educationTxt.Enabled = true;
                        professionTxt.Enabled = true;
                        favBtn.Text = "Update";
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
                else if (favBtn.Text.Trim().ToLower() == "Update".ToLower())
                {
                    //cancel request
                    try
                    {
                        dBHelper.updateUserDataByUserId(userId, usernameTxt.Text, Convert.ToInt32(ageTxt.Text), passwordTxt.Text, dobTxt.Text, addressTxt.Text, cityTxt.Text, mrgStatusTxt.Text, professionTxt.Text, educationTxt.Text);

                        usernameTxt.Enabled = false;
                        //emailTxt.Enabled = false;
                        ageTxt.Enabled = false;
                        dobTxt.Enabled = false;
                        passwordTxt.Enabled = false;
                        addressTxt.Enabled = false;
                        cityTxt.Enabled = false;
                        mrgStatusTxt.Enabled = false;
                        educationTxt.Enabled = false;
                        professionTxt.Enabled = false;
                        favBtn.Text = "Edit";
                        Intent intent = new Intent(this, typeof(UserProfileActivity));
                        StartActivity(intent);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
            };
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
                            dBHelper.InsertQuery(person.username, person.email, person.age, person.password);
                        }
                        Toast.MakeText(this, "Data Seeded to application", ToastLength.Long);

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
    }
}