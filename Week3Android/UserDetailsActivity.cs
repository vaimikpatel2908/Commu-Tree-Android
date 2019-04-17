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
    [Activity(Label = "Commu-Tree")]
    public class UserDetailsActivity : Activity
    {
        Button favBtn;
        ImageView userImageView, mailImg, locationImg;
        TextView usernameTxt, emailTxt, ageTxt, dobTxt, addressTxt, cityTxt, mrgStatusTxt, educationTxt, professionTxt;
        DBHelper dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserDetails);
            dBHelper = new DBHelper(this);
            userImageView = FindViewById<ImageView>(Resource.Id.userImageView);
            usernameTxt = FindViewById<TextView>(Resource.Id.usernameTxt);
            emailTxt = FindViewById<TextView>(Resource.Id.emailTxt);
            ageTxt = FindViewById<TextView>(Resource.Id.ageTxt);
            dobTxt = FindViewById<TextView>(Resource.Id.dobTxt);
            addressTxt = FindViewById<TextView>(Resource.Id.addressTxt);
            cityTxt = FindViewById<TextView>(Resource.Id.cityTxt);
            mrgStatusTxt = FindViewById<TextView>(Resource.Id.mrgStatusTxt);
            educationTxt = FindViewById<TextView>(Resource.Id.educaionTxt);
            professionTxt = FindViewById<TextView>(Resource.Id.professionTxt);
            favBtn = FindViewById<Button>(Resource.Id.AddToFavorites);

            mailImg = FindViewById<ImageView>(Resource.Id.mailImg);
            locationImg = FindViewById<ImageView>(Resource.Id.locationImg);

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            int userId = sharedPreferences.GetInt("userId", 0);

            //getting userid of user which is clicked on list view
            int favUserId = Intent.GetIntExtra("UserProfileId", 0);

            //getting userdetails 
            Person user = dBHelper.GetDataByUserId(favUserId);

            // assigning Data to textviews
            usernameTxt.Text = user.username;
            emailTxt.Text = user.email;
            ageTxt.Text = Convert.ToString(user.age);
            dobTxt.Text = user.dob;
            addressTxt.Text = user.address;
            cityTxt.Text = user.city;
            mrgStatusTxt.Text = user.maritalStatus;
            educationTxt.Text = user.education;
            professionTxt.Text = user.profession;
            userImageView.SetImageResource(user.userImage);


            if (dBHelper.HasRequest(favUserId, userId))
            {
                //If Request Exists for same user

                favBtn.Text = "Accept Request";
            }
            else
            {
                int status = dBHelper.checkRequestStatus(userId, user.uId);

                //pending Request
                if (status == 0)
                {
                    favBtn.Text = "Send Request";
                }
                else if (status == 1)
                {
                    favBtn.Text = "Cancel Request";
                }
                else if (status == 2)
                {
                    favBtn.Text = "Unfriend";
                }
            }
            favBtn.Click += delegate
            {
                if (favBtn.Text.Trim().ToLower() == "Send Request".ToLower())
                {
                    //send request code
                    try
                    {
                        dBHelper.InsertToFavourites(userId, user.uId, 1);
                        favBtn.Text = "Cancel Request";
                        Toast.MakeText(this, "Request Sent", ToastLength.Short).Show();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
                else if (favBtn.Text.Trim().ToLower() == "Cancel Request".ToLower())
                {
                    //cancel request
                    try
                    {
                        dBHelper.DeleteFromFavourites(userId, user.uId);
                        favBtn.Text = "Send Request";
                        Toast.MakeText(this, "Request Cancelled", ToastLength.Short).Show();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
                else if (favBtn.Text.Trim().ToLower() == "Unfriend".ToLower())
                {
                    //unfriend 
                    try
                    {
                        dBHelper.DeleteFromFavourites(userId, user.uId);
                        dBHelper.DeleteFromFavourites(user.uId, userId);
                        favBtn.Text = "Send Request";

                        Toast.MakeText(this, "Removed From Friend", ToastLength.Short).Show();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
                else if (favBtn.Text.Trim().ToLower() == "Accept Request".ToLower())
                {
                    //accept request
                    try
                    {
                        dBHelper.AcceptRequest(favUserId, userId);
                        favBtn.Text = "Unfriend";

                        Toast.MakeText(this, user.username + " added as friend", ToastLength.Short).Show();
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
            };
            mailImg.Click += delegate
            {
                var email = new Intent(Android.Content.Intent.ActionSend);
                email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] {
            user.email
        });
                email.PutExtra(Android.Content.Intent.ExtraCc, new string[] {
            "vaimikpatel2908@gmail.com"
        });
                email.PutExtra(Android.Content.Intent.ExtraSubject, "Hello "+user.username);
                email.PutExtra(Android.Content.Intent.ExtraText, "Hello "+ user.username+" This is a mail from commu-tree application!");
                email.SetType("message/rfc822");
                Intent chooser = Intent.CreateChooser(email, "Send Mail");
                StartActivity(chooser);
            };

            locationImg.Click += delegate
            {
                redirectToMap(user.address);
            };

        }

        public void redirectToMap(string address)
        {
            Coordinates coordinates = new Coordinates();

            bool isFound = false;
            foreach (var item in coordinates.coordinates)
            {
                if (item.address.Trim().ToLower().Equals(address.ToLower()))
                {
                    isFound = true;
                    //string coordString="geo:"+item.latitude+","+item.longitude+"q=vaimik";
                    string coordString = "geo:" + item.latitude + "," + item.longitude + "?q="+address;
                    var geoUri = Android.Net.Uri.Parse(coordString);
                    var mapIntent = new Intent(Intent.ActionView, geoUri);
                    Intent mapchooser = Intent.CreateChooser(mapIntent, "Open Map");
                    StartActivity(mapchooser);
                }
            }
            if (!isFound)
            {
                Toast.MakeText(this, "Address Not found", ToastLength.Long);
            }
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
