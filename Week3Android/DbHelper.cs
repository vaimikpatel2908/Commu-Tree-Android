using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Week3Android
{
    public class DBHelper : SQLiteOpenHelper
    {
        public Context context;
        private static string _DatabaseName = "CommutreeDb.db";
        private const string TableName = "registrationtable";
        private const string ColumnID = "id";
        private const string ColumnImage = "image";
        private const string ColumnName = "name";
        private const string ColumnEmail = "email";
        private const string ColumnAge = "age";
        private const string ColumnPassword = "password";
        private const string ColumnDob = "dob";
        private const string ColumnAddress = "address";
        private const string ColumnCity = "city";
        private const string ColumnMaritalStatus = "mrg_status";
        private const string ColumnProfession = "profession";
        private const string ColumnEducation = "education";

        public const string CreateRegistrationTableQuery = "Create Table " + TableName + " ( " + ColumnID + " Integer primary key autoincrement, " + ColumnImage + " int," + ColumnName + " Text," + ColumnEmail + " Text," + ColumnAge + " Integer," + ColumnPassword + " Text ,"+ ColumnDob +" Text , "
            + ColumnAddress+" Text , "+ ColumnCity+" Text , "+ColumnMaritalStatus +" Text , "+ColumnProfession+
            " Text , "+ColumnEducation+" Text)";

        public const string DeleteQuery = "Drop Table If Exists " + TableName;

        private const string FavTableName = "Favourites";
        private const string FavColumnID = "favId";
        private const string FavColumnUserId = "userId";
        private const string FavColumnFavUserID = "favUserId";
        private const string FavColumnStatus = "Status";

        public const string CreateFavouritesTableQuery = "Create Table " + FavTableName + " ( " + FavColumnID + " Integer primary key autoincrement, " + FavColumnUserId + " int," + FavColumnFavUserID + " int, "+FavColumnStatus+" bool)";

        public const string FavoritesDeleteQuery = "Drop Table If Exists " + FavTableName;

        SQLiteDatabase DbObj;

        public DBHelper(Context context) : base(context, name: _DatabaseName, factory: null, version: 1)
        {
            this.context = context;
            DbObj = WritableDatabase; // step 5
        }

        //step 6
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(CreateRegistrationTableQuery);
            db.ExecSQL(CreateFavouritesTableQuery);
        }

        //step 7
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(DeleteQuery);
            db.ExecSQL(FavoritesDeleteQuery);
        }

        public void InsertQuery(string username, string emailID, int age, string password)
        {
            Random rnd = new Random();
            var id = rnd.Next(1, 1000);

            var insertValue = "Insert into " + TableName + "("+ColumnID+","+ColumnImage+","+ColumnName+","+ColumnEmail+","+ColumnAge+","+ColumnPassword+") values('" + id + "',"+getImage()+",'" + username + "','" + emailID + "','" + age + "','" + password + "')";
            //ShowAlert("Data Inserted", "Id :" + id + "\nName :" + username + "\nEmail :" + emailID + "\nAge :" + age + "\nPassword :" + password);

            //System.Console.WriteLine("MY SQL Insert " + insertValue);
            DbObj.ExecSQL(insertValue);
            Toast.MakeText(context, "Registered Successfully", ToastLength.Short).Show();
        }

        public List<Person> SelectAllUsers(int userId)
        {
            //first way to fetch records from table 
            ICursor cursor = DbObj.RawQuery("Select * from " + TableName +" where "+ ColumnID+"!="+userId, null);

            //second way to fetch records from the table
            //string[] columns = new string[] { ColumnID, ColumnName, ColumnEmail, ColumnAge, ColumnPassword };
            //ICursor cursor = DbObj.Query(TableName, columns, null, null, null, null, null);
            List<Person> personList = new List<Person>();
            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    System.Console.WriteLine("Profession : ---"+cursor.GetString(10));
                    System.Console.WriteLine("Education : ---" + cursor.GetString(11));

                    System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(4) + "---" + cursor.GetString(5) + "---" + cursor.GetString(6) + "---" + cursor.GetString(7) + "---" + cursor.GetString(8) + "---" + cursor.GetString(9) + "---" + cursor.GetString(11) + "---" + cursor.GetString(10) );

                    personList.Add(new Person(cursor.GetInt(0), cursor.GetInt(1), cursor.GetString(2), cursor.GetInt(4), cursor.GetString(3), cursor.GetString(5), cursor.GetString(6),cursor.GetString(7), cursor.GetString(8), cursor.GetString(9), cursor.GetString(11), cursor.GetString(10)));

                }
                return personList;
            }
            else
            {
                return null;
            }
        }

        public int VerifyUserByCredentials(string email, string password)
        {
            //DbObj.ExecSQL(DeleteQuery);
            //DbObj.ExecSQL(FavoritesDeleteQuery);
            //DbObj.ExecSQL(CreateRegistrationTableQuery);
            //DbObj.ExecSQL(CreateFavouritesTableQuery);

            //first way to fetch records from table 
            string verifyCredentitalsQuery = "Select * from " + TableName + " where " + ColumnEmail + "='" + email + "' and " + ColumnPassword + "='" + password + "'";
            ICursor cursor = DbObj.RawQuery(verifyCredentitalsQuery, null);

            //second way to fetch reocrds from the table
            //string[] columns = new string[] { ColumnID, ColumnName, ColumnEmail, ColumnAge, ColumnPassword };
            //ICursor cursor = DbObj.Query(TableName, columns, null, null, null, null, null);

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    int id = cursor.GetInt(cursor.GetColumnIndexOrThrow(ColumnID));

                    string Email = cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnEmail));

                    string pwd = cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnPassword));

                    if (Email.Equals(email) && pwd.Equals(password))
                    {
                        System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(3) + "---" + cursor.GetString(4));
                        //ShowAlert("Data Verified", "Login Successfull");
                        Toast.MakeText(context, "Login Successfull", ToastLength.Short).Show();
                        return id;
                    }
                }
            }
            else
            {
                ShowAlert("Error 404", "Data Not Found");
                System.Console.WriteLine("MY Select Query : " + verifyCredentitalsQuery);
            }

            return 0;
        }

        public Person GetDataByUserId(int uId)
        {
            //first way to fetch records from table 
            ICursor cursor = DbObj.RawQuery("Select * from " + TableName + " where " + ColumnID + "='" + uId + "'", null);

            //seond way to fetch reocrds from the table
            //string[] columns = new string[] { ColumnID, ColumnName, ColumnEmail, ColumnAge, ColumnPassword };
            //ICursor cursor = DbObj.Query(TableName, columns, null, null, null, null, null);

            if (cursor != null)
            {
                bool IsExists = false;

                if (cursor.MoveToFirst())
                {
                    IsExists = true;

                    //need to pass data here
                    Person person = new Person(cursor.GetInt(0), cursor.GetInt(1), cursor.GetString(2), cursor.GetInt(4), cursor.GetString(3), cursor.GetString(5), cursor.GetString(6), cursor.GetString(7), cursor.GetString(8), cursor.GetString(9), cursor.GetString(11), cursor.GetString(10));
                    Console.WriteLine();
                    //ShowAlert("Data Verified", "Login Successfull");
                    return person;

                }

            }
            else
            {
                Toast.MakeText(context, "Resource Not Found", ToastLength.Short).Show();
                //ShowAlert("Error 404", "Resource Not Found");
                //System.Console.WriteLine("MY Select Query : " + );
            }
            return new Person();
        }

        /// <summary>
        /// It takes user id, username,age,password as input and updates user data on the basis of user id.
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="username"></param>
        /// <param name="age"></param>
        /// <param name="password"></param>
        public void updateUserDataByUserId(int uID, string username, int age,string password, string dob, string address, string city, string maritalStatus, string profession, string education)
        {
            string updateQuery = "Update " + TableName + " set " + ColumnName + "='" + username + "',"+ColumnPassword+"='"+password +"',"
                + ColumnAge + "=" + age + "," + ColumnDob + "='" + dob + "'," + ColumnAddress + "='" + address + "',"
                + ColumnCity + "='" + city + "'," + ColumnMaritalStatus + "='" + maritalStatus + "',"
                + ColumnProfession + "='" + profession + "'," + ColumnEducation + "='" + education + "'" +
                " where " + ColumnID + "=" + uID;
            //Console.WriteLine(updateQuery);
            DbObj.ExecSQL(updateQuery);
            Toast.MakeText(context, "Data updated Successfully", ToastLength.Short).Show();
            //ShowAlert("Success", "Data updated successfully");
        }

        public void InsertToFavourites(int userId, int favUserId,int status)
        {
            Random rnd = new Random();
            var id = rnd.Next(1, 1000);

            var insertFavQuery = "Insert into " + FavTableName + "(" + FavColumnID + "," + FavColumnUserId + "," + FavColumnFavUserID + ","+FavColumnStatus+") values(" + id + "," + userId + "," + favUserId + ","+status+")";
            //ShowAlert("Data Inserted", "Id :" + id + "\nName :" + username + "\nEmail :" + emailID + "\nAge :" + age + "\nPassword :" + password);

            //System.Console.WriteLine("MY SQL Insert " + insertValue);
            DbObj.ExecSQL(insertFavQuery);
        }

        public void DeleteFromFavourites(int userId, int favUserId)
        {
            var deleteFavQuery = "Delete from " + FavTableName + " where ("+ FavColumnUserId+"="+userId + " and "+FavColumnFavUserID+"="+favUserId+ ")";
            //ShowAlert("Data Inserted", "Id :" + id + "\nName :" + username + "\nEmail :" + emailID + "\nAge :" + age + "\nPassword :" + password);

            //System.Console.WriteLine("MY SQL Insert " + insertValue);
            DbObj.ExecSQL(deleteFavQuery);
        }

        //getting all favourite users by userID and requeststatus is true
        public List<Person> GetUserFavoritesList(int userId)
        {
            //first way to fetch records from table 
            ICursor cursor = DbObj.RawQuery("Select "+FavColumnID+","+FavColumnUserId+","+FavColumnFavUserID +","+ColumnName +","+ ColumnImage +" from " + FavTableName +" join "+ TableName + " on "+ FavColumnFavUserID+"="+ColumnID +" where " + FavColumnUserId +"="+userId +" and "+FavColumnStatus+"=2", null);

            //second way to fetch records from the table
            //string[] columns = new string[] { ColumnID, ColumnName, ColumnEmail, ColumnAge, ColumnPassword };
            //ICursor cursor = DbObj.Query(TableName, columns, null, null, null, null, null);
            List<Person> favList = new List<Person>();
            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    //System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(3) + "---" + cursor.GetString(4));
                    System.Console.WriteLine(cursor.GetInt(2) + "------------" + cursor.GetString(3) + "------------" + cursor.GetInt(4));
                    favList.Add(new Person(cursor.GetInt(2), cursor.GetString(3), cursor.GetInt(4)));
                }
                return favList;
            }
            else
            {
                return null;
            }
        }


        public List<Person> GetRequests(int userId)
        {
            //first way to fetch records from table 
            ICursor cursor = DbObj.RawQuery("Select " + FavColumnID + "," + FavColumnUserId + "," + FavColumnFavUserID + "," + ColumnName + "," + ColumnImage + " from " + FavTableName + " join " + TableName + " on " + FavColumnUserId + "=" + ColumnID + " where " + FavColumnFavUserID + "=" + userId + " and " + FavColumnStatus + "=1", null);

            //second way to fetch records from the table
            //string[] columns = new string[] { ColumnID, ColumnName, ColumnEmail, ColumnAge, ColumnPassword };
            //ICursor cursor = DbObj.Query(TableName, columns, null, null, null, null, null);
            List<Person> favList = new List<Person>();
            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    //System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(3) + "---" + cursor.GetString(4));

                    favList.Add(new Person(cursor.GetInt(1), cursor.GetString(3), cursor.GetInt(4)));
                }
                return favList;
            }
            else
            {
                return null;
            }
        }


        //checking RequestStatus
        //status 0 -> no request sent
        //status 1 -> Pending(Request sent)
        //status 2 -> Accepted
        public int checkRequestStatus(int UserId, int favUserId)
        {
            //first way to fetch records from table 
            ICursor cursor = DbObj.RawQuery("Select * from " + FavTableName + " where " + FavColumnUserId + "=" + UserId +" and "+ FavColumnFavUserID +"="+ favUserId, null);

            int favouritesStatus=0;
            if (cursor != null)
            {
                if(cursor.MoveToFirst())
                {
                    //System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(3) + "---" + cursor.GetString(4));
                    favouritesStatus=cursor.GetInt(3);
                }
            }
            return favouritesStatus;
        }

        public bool HasRequest(int favUserId,int userId)
        {
            bool hasRequest = false;
            ICursor cursor = DbObj.RawQuery("Select * from " + FavTableName + " where " + FavColumnUserId + "=" + favUserId + " and " + FavColumnFavUserID + "=" + userId +" and "+FavColumnStatus+"=1", null);

            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    //System.Console.WriteLine(cursor.GetInt(0) + "---" + cursor.GetString(1) + "---" + cursor.GetString(2) + "---" + cursor.GetInt(3) + "---" + cursor.GetString(4));
                    hasRequest = true;
                }
            }
            return hasRequest;
        }

        public void AcceptRequest(int favUserId, int userId)
        {
            string acceptRequestQuery="Update " + FavTableName + " set "+FavColumnStatus+"=2 where " + FavColumnUserId + "=" + favUserId + " and " + FavColumnFavUserID + "=" + userId;
            DbObj.ExecSQL(acceptRequestQuery);
            InsertToFavourites(userId, favUserId, 2);
        }

        /// <summary>
        /// Input the title and message for alert
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void ShowAlert(String title,string message)
        {
            Android.App.AlertDialog.Builder alertDiaglog = new Android.App.AlertDialog.Builder(context);
            AlertDialog alert = alertDiaglog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                    // Ok button click task  
                    alert.Hide();
            });
            alert.SetButton2("Cancel", (c, ev) =>
            {
                    // Ok button click task  
                    alert.Cancel();
            });
            alert.Show();
        }

        public Double getImage()
        {
            Random random = new Random();
            int id=random.Next(1, 10);

            Double imageId = 0;

            switch(id){
                case 1:
                    imageId=Resource.Drawable.user1;
                    return imageId;
                case 2:
                    imageId = Resource.Drawable.user2;
                    return imageId;
                case 3:
                    imageId = Resource.Drawable.user3;
                    return imageId;
                case 4:
                    imageId = Resource.Drawable.user4;
                    return imageId;
                case 5:
                    imageId = Resource.Drawable.user5;
                    return imageId;
                case 6:
                    imageId = Resource.Drawable.user6;
                    return imageId;
                case 7:
                    imageId = Resource.Drawable.user7;
                    return imageId;
                case 8:
                    imageId = Resource.Drawable.user8;
                    return imageId;
                case 9:
                    imageId = Resource.Drawable.user9;
                    return imageId;
                case 10:
                    imageId = Resource.Drawable.user10;
                    return imageId;
            }
            return imageId;
        }

        public void SeedData(string username, string emailID, int age, string password,string dob,string address,string city,string maritalStatus,string education,string profession)
        {
            Random rnd = new Random();
            var id = rnd.Next(1, 1000);

            var insertValue = "Insert into " + TableName + "(" + ColumnID + "," + ColumnImage + "," + ColumnName + "," + ColumnEmail + "," + ColumnAge + "," + ColumnPassword + "," + ColumnDob + "," + ColumnAddress + "," + ColumnCity + "," + ColumnMaritalStatus + "," + ColumnEducation + "," + ColumnProfession + ") values('" + id + "'," + getImage() + ",'" + username + "','" + emailID + "','" + age + "','" + password + "','"+dob+"','"+address+ "','" + city + "','" + maritalStatus + "','" + education + "','" + profession + "')";
            //ShowAlert("Data Inserted", "Id :" + id + "\nName :" + username + "\nEmail :" + emailID + "\nAge :" + age + "\nPassword :" + password);

            System.Console.WriteLine("MY SQL Insert " + insertValue);
            DbObj.ExecSQL(insertValue);
            //Toast.MakeText(context, "Data Added Successfully", ToastLength.Short).Show();
        }

        public void cleanData()
        {
            DbObj.ExecSQL(DeleteQuery);
            DbObj.ExecSQL(FavoritesDeleteQuery);
            DbObj.ExecSQL(CreateRegistrationTableQuery);
            DbObj.ExecSQL(CreateFavouritesTableQuery);

            Toast.MakeText(context, "Data Cleared", ToastLength.Long);
        }
    }
}