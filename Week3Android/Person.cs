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

        public Person(int uId,int image,string username,int age,string email,string password,string dob,string address,string city,string maritalstatus, string education, string profession)
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

        public List<Person> seedData()
        {
            usersList.Add(new Person() { username = "Vaimik Patel",   email="vaimik@gmail.com",     age = 25, password = "vaimik" , education = "Graduate" ,address = "1 Lee Center Drive"  , city = "Toronto" , dob = "08/29/1994", maritalStatus = "Single" , profession = "N/A" });
            usersList.Add(new Person() { username = "Chirag Vira",    email = "chirag@gmail.com",   age = 20, password = "chirag", education = "Post Graduate Cerificate", address = "25 Bay Mills Boulevard", city = "Toronto", dob = "05/04/1993", maritalStatus = "Married", profession = "N/A" });
            usersList.Add(new Person() { username = "Milen Louis",    email = "milen@gmail.com",    age = 27, password = "milen", education = "Masters", address = "1399 Victoria Park Avenue", city = "Brampton", dob = "07/05/1996", maritalStatus = "Single", profession = "N/A" });
            usersList.Add(new Person() { username = "Meenakshi Sethi",email = "meenakshi@gmail.com",age = 18, password = "meenakshi", education = "Grade 12", address = "265 Danforth Avenue", city = "Toronto", dob = "08/16/1995", maritalStatus = "Married", profession = "N/A" });
            usersList.Add(new Person() { username = "Kushal Sarawagi",email = "kushal@gmail.com",   age = 22, password = "kushal", education = "Graduate", address = "24 Parkway Forest Drive", city = "Missisuaga", dob = "04/03/1994", maritalStatus = "Single", profession = "N/A" });
            usersList.Add(new Person() { username = "Ami Jani",       email = "ami@gmail.com",      age = 12, password = "ami", education = "Grade 12", address = "1399 Victoria Park Avenue", city = "Toronto", dob = "07/25/1994", maritalStatus = "Single", profession = "N/A" });
            usersList.Add(new Person() { username = "Shreya Patel",   email = "shreya@gmail.com",   age = 23, password = "shreya", education = "Doctorate", address = "265 Danforth Avenue", city = "North York", dob = "05/30/1996", maritalStatus = "Marreied", profession = "N/A" });
            usersList.Add(new Person() { username = "Pooja Patel",    email = "pooja@gmail.com",    age = 24, password = "pooja", education = "Masters", address = "25 Bay Mills Boulevard", city = "Toronto", dob = "10/10/1996", maritalStatus = "Single", profession = "N/A" });
            usersList.Add(new Person() { username = "Akif Shaikh",    email = "akif@gmail.com",     age = 28, password = "akif", education = "Post Graduate", address = "1 Lee Center Drive", city = "Toronto", dob = "12/12/1994", maritalStatus = "Single", profession = "N/A" });

            return usersList;
        }

        //Not using Now
        public void addToList(Person person)
        {
            usersList.Add(person);
        }
    }
}