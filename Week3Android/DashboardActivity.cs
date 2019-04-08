using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.App.ActionBar;

namespace Week3Android
{
    [Activity(Label = "DashboardActivity")]
    public class DashboardActivity : Activity
    {
        Button updateBtn;
        EditText usernameTxt, emailTxt, ageTxt, passwordTxt;
        DBHelper dBHelper;
        //TextView userIdTxt;
        //TableLayout table;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Dashboard);
            dBHelper = new DBHelper(this);
            usernameTxt = FindViewById<EditText>(Resource.Id.usernameTxt);
            emailTxt = FindViewById<EditText>(Resource.Id.emailTxt);
            ageTxt = FindViewById<EditText>(Resource.Id.ageTxt);
            passwordTxt = FindViewById<EditText>(Resource.Id.passwordTxt);
            updateBtn = FindViewById<Button>(Resource.Id.UpdateBtn);

            //userIdTxt.Text = Convert.ToString(Intent.GetIntExtra("userId", 0));
            Person user = dBHelper.GetDataByUserId(Intent.GetIntExtra("userId",0));
            usernameTxt.Text = user.username;
            emailTxt.Text = user.email;
            ageTxt.Text = Convert.ToString(user.age);
            passwordTxt.Text = user.password;

            updateBtn.Click += delegate
             {

                 if (updateBtn.Text.Trim().ToLower() == "Edit".ToLower())
                 {
                     updateBtn.Text = "Update";
                     usernameTxt.Enabled = true;
                     ageTxt.Enabled = true;
                     passwordTxt.Enabled = true;
                 }else if(updateBtn.Text.Trim().ToLower() == "Update".ToLower())
                 {
                     if(usernameTxt.Text.Trim()=="")
                     {
                         dBHelper.ShowAlert("Data validation Error", "Enter Username");
                     }
                     else if (ageTxt.Text.Trim() == "")
                     {
                         dBHelper.ShowAlert("Data validation Error", "Enter age");
                     }
                     else if(passwordTxt.Text.Trim() == "")
                     {
                         dBHelper.ShowAlert("Data validation Error", "Enter password");
                     }
                     else
                     {
                         try
                         {
                             int age = Convert.ToInt32(ageTxt.Text);
                             updateBtn.Text = "Edit";
                             usernameTxt.Enabled = false;
                             ageTxt.Enabled = false;
                             passwordTxt.Enabled = false;
                            // dBHelper.updateUserDataByUserId(user.uId, usernameTxt.Text, age, passwordTxt.Text);
                         }
                         catch(FormatException fex)
                         {
                             Console.WriteLine(fex);
                             dBHelper.ShowAlert("Data Validation Error", "Enter age in number format");
                         }
                         catch(System.Exception ex)
                         {
                             Console.WriteLine(ex);
                             dBHelper.ShowAlert("Error", "Something Went Worng");
                         }
                     }
                 }
             };

        }
    }
}