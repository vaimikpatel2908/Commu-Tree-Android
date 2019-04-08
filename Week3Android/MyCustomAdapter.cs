using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Week3Android
{
    class MyCustomAdapter : BaseAdapter<Person>
    {
        public Activity context;
        public List<Person> userList;
        public DBHelper dBHelper;
        public TextView userFavIdTxt; 

        public MyCustomAdapter(Activity cont, List<Person> userlist)
        {
            this.context = cont;
            this.userList = userlist;
            this.dBHelper = new DBHelper(context);
        }

        public override Person this[int position]
        {
            get
            {
                return userList[position];
            }
        }

        public override int Count
        {
            get
            {
                return userList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Person user = userList[position];

            View myView = convertView;
            if (myView == null)
            {
                myView = context.LayoutInflater.Inflate(Resource.Layout.MyUserListView, null);
            }
            if (user.userImage == 0)
            {
                myView.FindViewById<ImageView>(Resource.Id.userImg).SetImageResource(Resource.Drawable.user);
            }
            else
            {
                myView.FindViewById<ImageView>(Resource.Id.userImg).SetImageResource(user.userImage);
            }

            myView.FindViewById<TextView>(Resource.Id.usernameTxt).Text = user.username;


            return myView;
        }

    }
}