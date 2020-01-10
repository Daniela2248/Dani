using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SOAP.Models;
using System.Xml.Linq;


namespace API_SOAP
{
    public class SampleService : UsersService
    {
        public string Test(string s)
        {
            Console.WriteLine("Test Method Executed!");
            return s;
        }
        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }
        public Users TestCustomModel(Users customModel)
        {
            return customModel;

        }

        public Users TestUser(Users inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
