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

namespace Week3Android
{
    public class Favourites
    {
        public int favId;
        public int userId;
        public int favUserId;
        public int requestStatus;

        public Favourites()
        {
            this.favId = 0;
            this.userId = 0;
            this.favUserId = 0;
            this.requestStatus = 0;
        }

        public Favourites(int FavId,int UserId,int FavUserId,int RequestStatus)
        {
            this.favId = FavId;
            this.userId = UserId;
            this.favUserId = FavUserId;
            this.requestStatus = RequestStatus;
        }


    }
}