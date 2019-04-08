using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using System;

namespace Week3Android
{
    [Activity(Label = "UserDetailsActivity")]
    public class UserDetailsActivity : Activity
    {
        Button favBtn;
        EditText usernameTxt, emailTxt, ageTxt, passwordTxt;
        DBHelper dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserDetails);
            dBHelper = new DBHelper(this);
            usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
            emailTxt = FindViewById<EditText>(Resource.Id.emailTxt);
            ageTxt = FindViewById<EditText>(Resource.Id.ageTxt);
            passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
            favBtn = FindViewById<Button>(Resource.Id.AddToFavorites);

            ISharedPreferences sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
            int userId = sharedPreferences.GetInt("userId", 0);
            //userIdTxt.Text = Convert.ToString(Intent.GetIntExtra("userId", 0));
            int favUserId = Intent.GetIntExtra("UserProfileId", 0);
            Person user = dBHelper.GetDataByUserId(favUserId);
            usernameTxt.Text = user.username;
            emailTxt.Text = user.email;
            ageTxt.Text = Convert.ToString(user.age);
            passwordTxt.Text = user.password;

            if (dBHelper.HasRequest(favUserId, userId))
            {
                favBtn.Text = "Accept Request";
            }
            else { 

            int status=dBHelper.checkRequestStatus(userId, user.uId);

            //pending Request
            if (status == 0)
            {
                favBtn.Text = "Send Request";
            }
            else if(status == 1)
            {
                favBtn.Text = "Cancel Request";
            }else if(status == 2)
            {
                favBtn.Text = "Unfriend";
            }
            }
            favBtn.Click += delegate
            {
                if (favBtn.Text.Trim().ToLower() == "Send Request".ToLower())
                {
                    try
                    {
                        dBHelper.InsertToFavourites(userId, user.uId,1);
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
                    try
                    {
                        dBHelper.AcceptRequest(favUserId, userId);
                        favBtn.Text = "Unfriend";

                        Toast.MakeText(this, user.username+" added as friend", ToastLength.Short).Show();
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
