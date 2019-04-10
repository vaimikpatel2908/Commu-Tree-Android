using System;
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
        EditText usernameTxt, emailTxt, ageTxt,passwordTxt, dobTxt, addressTxt, cityTxt, mrgStatusTxt, educationTxt, professionTxt;
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
            educationTxt = FindViewById<EditText>(Resource.Id.educaionTxt);
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
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Error", "Try Again");
                    }
                }
            };
        }
    }
}