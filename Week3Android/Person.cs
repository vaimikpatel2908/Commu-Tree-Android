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
    public class Person
    {
        public int uId;
        public int userImage;
        public string username;
        public int age;
        public string email;
        public string password;
        public string dob;
        public string address;
        public string city;
        public string maritalStatus;
        public string education;
        public string profession;

        public List<Person> usersList = new List<Person>();

        public Person()
        {
            this.uId = 0;
            this.userImage = 0;
            this.username = "";
            this.age = 0;
            this.email = "";
            this.password = "";
            this.dob = "";
            this.address = "";
            this.city = "";
            this.maritalStatus = "";
            this.education = "";
            this.profession = "";
        }

        public Person(int uId, string username, int image)
        {
            this.uId = uId;
            this.username = username;
            this.userImage = image;
        }

        public Person(int uId,int image,string username,int age,string email,string password,string dob,string address,string city,string maritalstatus,string education,string profession)
        {
            this.uId = uId;
            this.userImage = image;
            this.username = username;
            this.age = age;
            this.email = email;
            this.password = password;
            this.dob = dob;
            this.address = address;
            this.city = city;
            this.maritalStatus = maritalstatus;
            this.education = education;
            this.profession = profession;
        }

        public void addToList(Person person)
        {
            usersList.Add(person);
        }


    }
}