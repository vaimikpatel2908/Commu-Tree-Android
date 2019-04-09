using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;
using Java.Lang;

namespace Week3Android
{
    [Activity(Label = "Commu-Tree")]
    public class MainActivity : Activity
    {
        Button registerBtn;
        EditText usernameTxt,emailTxt,ageTxt,passwordTxt;
        DBHelper dBHelper;
        TextView LoginLink;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
            emailTxt = FindViewById<EditText>(Resource.Id.emailTxt);
            ageTxt = FindViewById<EditText>(Resource.Id.ageTxt);
            passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
            registerBtn = FindViewById<Button>(Resource.Id.SignUpBtn);
            LoginLink= FindViewById<TextView>(Resource.Id.LoginLink);


            dBHelper = new DBHelper(this);
            LoginLink.Click += delegate
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            };

            registerBtn.Click += delegate
            {
                if(usernameTxt.Text == "")
                {
                    Toast.MakeText(this, "Enter Username", ToastLength.Short);
                    //dBHelper.ShowAlert("Error", "Enter Username");
                }
                else if (emailTxt.Text == "")
                {
                    Toast.MakeText(this, "Enter Email", ToastLength.Short);
                    //dBHelper.ShowAlert("Error", "Enter Email");
                }
                else if (ageTxt.Text == "")
                {
                    Toast.MakeText(this, "Enter Age", ToastLength.Short);
                    //dBHelper.ShowAlert("Error", "Enter Age");
                }
                else if (passwordTxt.Text == "")
                {
                    Toast.MakeText(this, "Enter Password", ToastLength.Short);
                    //dBHelper.ShowAlert("Error", "Enter Password");
                }
                else
                {

                    try
                    {
                        int ageValue = Convert.ToInt32(ageTxt.Text);

                        dBHelper.InsertQuery(usernameTxt.Text, emailTxt.Text, ageValue, passwordTxt.Text);
                        //SetContentView(Resource.Layout.Login);
                        Intent intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                    }
                    catch (FormatException fex)
                    {
                        Console.WriteLine(fex);
                        dBHelper.ShowAlert("Data Violation Error", "Enter age value in correct format");
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex);
                        dBHelper.ShowAlert("Try Again", "Something went wrong");
                    }
                }
            };
        }
    }
}