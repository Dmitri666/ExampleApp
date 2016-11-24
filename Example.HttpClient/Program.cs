// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using Example.Data.Contract.Model;
using Example.HttpClient.Model;
using Qdata.Json.Contract;
using QData.LinqConverter;

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The _access token.
        /// </summary>
        private static string _accessToken;

        #endregion

        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                JoinQueryTest();
                //WhereQueryTest();
                //StaticQueryTest();
                //StaticQueryTest1();
             
            }
            Console.ReadLine();
        }

        private static void JoinQueryTest()
        {
            Console.WriteLine("JoinQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerContactDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Where( x => x.FirstName.Contains("a") && x.Firma.Contains("x"))
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);


            var contactDtos = client.GetCustomerContact<CustomerContactDto>(new QDescriptor() { Root = root });
            if (contactDtos == null)
            {
                return;
            }
            foreach (var contact in contactDtos)
            {
                Console.WriteLine("Firma={0} FirstName={1}", contact.Firma, contact.FirstName);


            }
        }

        private static void JoinQueryTest1()
        {
            Console.WriteLine("JoinQueryTest");
            var client = new WebApiClient();
            var list = new List<ContactDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Where(
                        x =>
                        x.Id > id.Value && x.EdvNr > 0 || x.Customer.EdvNr > 0)
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);


            var contactDtos = client.GetContacts<ContactDto>(new QDescriptor() { Root = root });
            if (contactDtos == null)
            {
                return;
            }
            foreach (var contact in contactDtos)
            {
                Console.WriteLine("id={0} FirstName={1}", contact.Id, contact.FirstName);


            }
        }

        private static void WhereQueryTest()
        {
            Console.WriteLine("WhereQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Where(
                        x =>
                        x.Id > id.Value && x.Firma11.Contains(desc.Value) || x.Firma21.Contains("h") )
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);


            var customers = client.GetCustomers<CustomerDto>(new QDescriptor() { Root = root });
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma1={1}", customer.Id, customer.Firma11);


            }
        }

        private static void StaticQueryTest()
        {
            Console.WriteLine("StaticQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Where(
                        x =>
                        x.Id > id.Value && x.Firma11.Contains(desc.Value)
                        && x.Contacts.Any(y => y.Id > id.Value && y.FirstName.Contains(desc.Value))
                        || x.Firma21.Contains("h") ).Select(x => new Projection() { Id = x.Id, Firma1 = x.Firma11 })
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);

            
            var customers = client.GetCustomers<Projection>(new QDescriptor() { Root = root } );
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma1={1}", customer.Id, customer.Firma1);
                

            }
        }

        private static void StaticQueryTest1()
        {
            Console.WriteLine("StaticQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Select(x => new { Id1 = x.Id, Firma4 = x.Firma11 }).Where(
                        x =>
                        x.Id1 > id.Value && x.Firma4.Contains(desc.Value))
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);


            var customers = client.GetCustomers<Projection1>(new QDescriptor() { Root = root });
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma4={1}", customer.Id, customer.Firma4);


            }
        }

        #endregion
    }
}