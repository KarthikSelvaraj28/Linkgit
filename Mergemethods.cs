using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Linqjointables.MODEL;
using System.Net.Sockets;
using System.Net.Http.Headers;

namespace Linqjointables.DAL
{
    public class Mergemethods
    {
        private string connectionString;

        public Mergemethods()
        {

            var config = new ConfigurationBuilder()


                .AddJsonFile("appsettings.json")


                .Build();


            connectionString = config.GetConnectionString("DefaultConnection");


        }


        private List<Stumodel> GetStudents()


        {
            List<Stumodel> students = new List<Stumodel>();


            using (SqlConnection conn = new SqlConnection(connectionString))


            {
                conn.Open();


                string query = "SELECT StudentID, FirstName, LastName, Age, Email, Phone, Address FROM stuinformation";




                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();




                while (reader.Read())




                {
                    students.Add(new Stumodel
                    {
                        StudentID = reader.GetInt32(0),


                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),

                        Age = reader.GetInt32(3),

                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),


                        Address = reader.GetString(6)
                    });
                }
            }
            return students;
        }


        private List<Recmodel> GetRecords()
        {
            List<Recmodel> records = new List<Recmodel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT RecordID, StudentID, Tamil, English, Maths, Science, Social FROM Record";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    records.Add(new Recmodel
                    {
                        RecordID = reader.GetInt32(0),

                        StudentID = reader.GetInt32(1),

                        Tamil = reader.GetInt32(2),

                        English = reader.GetInt32(3),

                        Maths = reader.GetInt32(4),

                        Science = reader.GetInt32(5),

                        Social = reader.GetInt32(6)
                    });
                }
            }
            return records;
        }


        public void MergeAndDisplayRecords()
        {



           

            
            string FirstName =null ;
            string LastName=null ;
            string Address =null ;
            string Email = null;
            string phone=null ;

            int? age = null;
            int? studentId =null;
            int? tamil =null ;
            int? english=null  ;
            int? maths = null ;
            int? science =null ;
            int? social =null ;



            Console.WriteLine("Enter Column Name to filter: ");
            string ColumnName = Console.ReadLine();


            Console.WriteLine("Enter the values: ");
            string Values = Console.ReadLine();



            if(ColumnName.ToLower() == "firstname")
            {
                FirstName = Values;
            }
            else if (ColumnName.ToLower() == "lastname")
            {
                LastName = Values;
            }
            else if(ColumnName.ToLower()== "address")
            {
                Address = Values;
            }
            else if(ColumnName.ToLower()== "email")
            
            {
                Email = Values;
            }
            else if(ColumnName.ToLower()== "phone")
            {
                phone = Values;
            }
            else if(ColumnName.ToLower()=="age")
            {
                age = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);

            }

            else if (ColumnName == "studentid")
            {
                studentId = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }

            else if (ColumnName == "tamil")
            {
                tamil = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }
            else if (ColumnName == "english")
            {
                english = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }
            else if (ColumnName == "maths")
            {
                maths = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }
            else if (ColumnName == "science")
            {
                science = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }
            else if (ColumnName == "social")
            {
                social = string.IsNullOrEmpty(Values) ? (int?)null : int.Parse(Values);
            }
            else
            {
                Console.WriteLine("Invalid column name.");
            }





            //  Console.Write("Enter a keyword to search in the Address field: ");
            //string keyword = Console.ReadLine();


            //Console.Write("Enter an age to search for : ");
            //string ageInput = Console.ReadLine();

            //int? age = string.IsNullOrEmpty(ageInput) ? (int?)null : int.Parse(ageInput);

            // String keyword = Console.ReadLine();
            List<Stumodel> students = GetStudents();
            List<Recmodel> records = GetRecords();

           
            List<StudRecModel> studRecModels = students
                .Join(records,
                      stu => stu.StudentID,
                      rec => rec.StudentID,
                      (stu, rec) => new StudRecModel { StudentID = stu.StudentID, FirstName = stu.FirstName, LastName = stu.LastName, Age = stu.Age, Email = stu.Email, Phone = stu.Phone, Address = stu.Address, Tamil = rec.Tamil, English = rec.English, Maths = rec.Maths, Science = rec.Science, Social = rec.Social }
                      )
               .Where(data =>


    (string.IsNullOrEmpty(FirstName) || data.FirstName.ToLower().Contains(FirstName.ToLower())) && 
    (string.IsNullOrEmpty(LastName) || data.LastName.ToLower().Contains(LastName.ToLower())) &&  
    (string.IsNullOrEmpty(Address) || data.Address.ToLower().Contains(Address.ToLower())) && 
    (string.IsNullOrEmpty(Email) || data.Email.ToLower().Contains(Email.ToLower())) && 
    (string.IsNullOrEmpty(phone) || data.Phone.ToLower().Contains(phone.ToLower())) &&


        (!age.HasValue || data.Age == age.Value) &&

    (!studentId.HasValue || data.StudentID == studentId.Value) &&  
    (!tamil.HasValue || data.Tamil == tamil.Value) && 
    (!english.HasValue || data.English == english.Value) &&  
    (!maths.HasValue || data.Maths == maths.Value) &&  
    (!science.HasValue || data.Science == science.Value) &&  
    (!social.HasValue || data.Social == social.Value)  
)
.ToList();




            // .Where(data =>
            // (string.IsNullOrEmpty(keyword) || data.Address.ToLower().Contains(keyword.ToLower())) &&
            //(!age.HasValue || data.Age == age.Value) &&
            // (string.IsNullOrEmpty(firstNameInput) || data.FirstName.ToLower().Contains(firstNameInput.ToLower())) &&
            //(string.IsNullOrEmpty(lastNameInput) || data.LastName.ToLower().Contains(lastNameInput.ToLower())) &&
            //(string.IsNullOrEmpty(emailInput) || data.Email.ToLower().Contains(emailInput.ToLower())) &&
            //(string.IsNullOrEmpty(phoneInput) || data.Phone.ToLower().Contains(phoneInput.ToLower())) &&


            //  int? phoneInput = string.IsNullOrEmpty(phoneInput) ? (int?)null : int.Parse(phoneInput)


            //(string.IsNullOrEmpty(studentIdInput) || data.StudentID.ToString().Contains(studentIdInput)) &&
            //(string.IsNullOrEmpty(tamilInput) || data.Tamil.ToString().Contains(tamilInput)) &&
            //(string.IsNullOrEmpty(englishInput) || data.English.ToString().Contains(englishInput)) &&
            //(string.IsNullOrEmpty(mathsInput) || data.Maths.ToString().Contains(mathsInput)) &&
            //(string.IsNullOrEmpty(scienceInput) || data.Science.ToString().Contains(scienceInput)) &&
            //(string.IsNullOrEmpty(socialInput) || data.Social.ToString().Contains(socialInput))
            // )
            //.ToList();




















            if (studRecModels.Any())
            {
                Console.WriteLine("\nMerged Student & Marks Data:\n");


                foreach (var data in studRecModels)
                {
                   
                    
                    Console.WriteLine($"ID: {data.StudentID}, Name: {data.FirstName} {data.LastName}, Age: {data.Age}");
                   
                    Console.WriteLine($"Email: {data.Email}, Phone: {data.Phone}, Address: {data.Address}");
                    
                    Console.WriteLine($"Marks: Tamil={data.Tamil}, English={data.English}, Maths={data.Maths}, Science={data.Science}, Social={data.Social}");
                    
                    Console.WriteLine("------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No records found matching your criteria.");
            }
        }
    }
}