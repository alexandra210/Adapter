using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterProject
{
    
    class Program
    {
        Dictionary<string, string> Countries = new Dictionary<string, string>(3);

        public Program()
        {
            Countries.Add("RU", "Russia");
            Countries.Add("UA", "Ukraine");
            Countries.Add("CA", "Canada");
        }

        public Program(string code, string country)
        {
            Countries.Add(code, country);
        }

        static void Main(string[] args)
        {

            newCustomer customer = new newCustomer();
            newContact contact = new newContact();
            newIncomeData data = new newIncomeData();
            IncomeDataAdapter adapter = new IncomeDataAdapter(data);

            Console.WriteLine("Contact methods:");
            Console.WriteLine(contact.getName());
            Console.WriteLine(contact.getPhoneNumber());


            Console.WriteLine("\nCustomer methods:");
            Console.WriteLine(customer.getCompanyName());
            Console.WriteLine(customer.getCountryName());

            

            Console.WriteLine("\nAdapter:");
            Console.WriteLine(adapter.getCompanyName());
            Console.WriteLine(adapter.getCountryName());
            Console.WriteLine(adapter.getName());
            Console.WriteLine(adapter.getPhoneNumber());

            Console.ReadLine();
        }

        //Класс адаптер
        public class IncomeDataAdapter : Contact, Customer
        {
            IncomeData Data;

            public IncomeDataAdapter(IncomeData Data)
            {
                this.Data = Data;
            }

            public string getCompanyName()
            {
                return Data.getCompany();
            }

            public string getCountryName()
            {
                Program Country = new Program();
                return Country.Countries[Data.getCountryCode()];
            }

            public string getName()
            {
                return Data.getContactLastName() + " " + Data.getContactFirstName();
            }

            public string getPhoneNumber()
            {
                string number = Data.getPhoneNumber().ToString();
                for (int i = 0; i < 10 - number.Length; i++)
                {
                    number = "0" + number;
                }
                string phoneNumber = "+" + Data.getCountryPhoneCode() + "(" + number.Substring(0, 3) + ")" + number.Substring(3);
                return phoneNumber;
            }

        }

        public interface IncomeData
        {

            string getCountryCode(); //For example: UA

            string getCompany(); //For example: JavaRush Ltd.

            string getContactFirstName(); //For example: Ivan

            string getContactLastName(); //For example: Ivanov

            int getCountryPhoneCode(); //For example: 38

            int getPhoneNumber(); //For example: 501234567

        }

        public class newIncomeData : IncomeData
        {
            Program Country;

            public string getCountryCode()
            {
                Country = new Program();
                Random rnd = new Random();
                string randomCountryCode = Country.Countries.ElementAt(rnd.Next(0, Country.Countries.Count)).Key;
                return randomCountryCode;
            }

            public string getCompany()
            {
                return "Bla Bla Car";
            }

            public string getContactFirstName()
            {
                return "Василий";
            }

            public string getContactLastName()
            {
                return "Иванов";
            }

            public int getCountryPhoneCode()
            {
                return 62;
            }

            public int getPhoneNumber()
            {
                return 304273284;
            }
        }


        public interface Customer
        {

            string getCompanyName(); //For example: JavaRush Ltd.

            string getCountryName(); //For example: Ukraine

        }

        public class newCustomer : Customer
        {
            Program Country;

            public string getCompanyName()
            {
                return "Bla Bla Car";
            }

            public string getCountryName()
            {
                Country = new Program();
                Random rnd = new Random();
                string randomCountry = Country.Countries.ElementAt(rnd.Next(0, Country.Countries.Count)).Value;
                return randomCountry;
            }
        }

        public interface Contact
        {

            string getName(); //For example: Ivanov, Ivan

            string getPhoneNumber(); //For example: +38(050)123-45-67

        }

        public class newContact : Contact
        {
            public string getName()
            {
                return "Иванов Василий";
            }

            public string getPhoneNumber()
            {
                return "+62(030)4273284";
            }
        }
    }
}
