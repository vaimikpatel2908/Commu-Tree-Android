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
    [Activity(Label = "Commu-Tree", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText emailTxt,passwordTxt;
        Button loginBtn;
        DBHelper dBHelper;
        TextView createAcclink;

        public override void OnBackPressed()
        {
            // This prevents a user from being able to hit the back button and leave the login page.
            return;

            //base.OnBackPressed();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            emailTxt = FindViewById<EditText>(Resource.Id.emailTxt);
            passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
            loginBtn = FindViewById<Button>(Resource.Id.LoginBtn);
            dBHelper = new DBHelper(this);
            createAcclink = FindViewById<TextView>(Resource.Id.createAccLink);

            createAcclink.Click += delegate {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            loginBtn.Click += delegate
            {
                if(emailTxt.Text == "")
                {
                    dBHelper.ShowAlert("Verification Error", "Enter Email Id");
                }else if(passwordTxt.Text=="")
                {
                    dBHelper.ShowAlert("Verification Error", "Enter Password");
                }
                else
                {
                    int userId= dBHelper.VerifyUserByCredentials(emailTxt.Text.Trim(), passwordTxt.Text.Trim());

                    if (userId != 0)
                    {
                        ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                        ISharedPreferencesEditor editor = prefs.Edit();
                        editor.PutInt("userId", userId);
                        editor.Commit();    // applies changes synchronously on older APIs
                        editor.Apply();

                        Intent userlistScn = new Intent(this, typeof(UserListActivity));
                        //userlistScn.PutExtra("userId", userId);
                        StartActivity(userlistScn);
                    }
                    else
                    {
                        dBHelper.ShowAlert("Error", "Username or password is wrong");
                    }
                }
            };
        }
    }
}