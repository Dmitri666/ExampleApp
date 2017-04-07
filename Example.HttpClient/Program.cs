// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Example.HttpClient.Model;
using Qdata.Json.Contract;
using QData.LinqConverter;

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Example.Data.Contract.CrmModel;

    using QData.Client;
    using QData.Common;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The _access token.
        /// </summary>
        private static Uri contactAccsessPoint = new Uri("http://pc-dle-2.covis.lan/Example.WebApi/api/crm/contact");
        private static Uri customerAccsessPoint = new Uri("http://pc-dle-2.covis.lan/Example.WebApi/api/crm/customer");
        

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
            for (int i = 0; i < 10; i++)
            {
                WhereQueryTest1();
                //JoinQueryTest1();
                //WhereQueryTest1();
                //WhereQueryTest();
                //StaticQueryTest();
                //AnonymeSelectorQueryTest();
             
            }
            Console.ReadLine();
        }

        private static void WhereQueryTest3()
        {
           
        }

        private static void WhereQueryTest1()
        {
            Console.WriteLine("WhereQueryTest");
            var client = new QDataClient<CustomerDto>();
            var set = new QSet<CustomerDto>();
            var query = set.Where(x => x.ContactsCount > 2 && x.Contacts.Any(c => c.FirstName.Contains("a")) );
            
            var customers = client.Get(customerAccsessPoint,set.ConvertToQDescriptor(query));
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma1={1}", customer.Id, customer.Firma11);


            }
        }

        private static void WhereQueryTest2()
        {
            Console.WriteLine("WhereQueryTest2");
            var client = new QDataClient<ContactDto>();
            var set = new QSet<ContactDto>();
            var query = set.Where(x => x.Customer.Firma11.Contains("a") && x.Id > 0);
            
            var contacts = client.Get(contactAccsessPoint, set.ConvertToQDescriptor(query));
            if (contacts == null)
            {
                return;
            }
            foreach (var contact in contacts)
            {
                Console.WriteLine("id={0} firma1={1}", contact.Id, contact.FirstName);


            }
        }
        private static void JoinQueryTest1()
        {
            Console.WriteLine("JoinQueryTest");
            var client = new QDataClient<ContactDto>();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };

            var set = new QSet<ContactDto>();

            var query =
                set
                    .Where(
                        x =>
                        x.Id > id.Value ).Select(x => new { FirstName = x.FirstName , CustomerName = x.Customer.Firma11 } );


            var contacts = query.ToList();
            client.Get(contactAccsessPoint, set.ConvertToQDescriptor(query),query.ElementType,contacts);
            
            foreach (var contact in contacts)
            {
                Console.WriteLine("id={0} FirstName={1}", contact.FirstName, contact.CustomerName);


            }
        }

        private static void WhereQueryTest()
        {
            Console.WriteLine("WhereQueryTest");
            var client = new QDataClient<CustomerDto>();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var set = new QSet<CustomerDto>();

            var query =
                set.Where(
                    x => x.Id > id.Value && x.Firma11.Contains(desc.Value) || x.Firma21.Contains("h"));
                    
            var customers = client.Get(customerAccsessPoint,set.ConvertToQDescriptor(query));
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
            var client = new QDataClient<CustomerDto>();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var set = new QSet<CustomerDto>();
            var query =
                    set.Where(
                        x =>
                        x.Id > id.Value && x.Firma11.Contains(desc.Value)
                        && x.Contacts.Any(y => y.Id > id.Value && y.FirstName.Contains(desc.Value))
                        || x.Firma21.Contains("h")).Select(x => new Projection() { Id = x.Id, Firma1 = x.Firma11 });

            var customers = client.Get<Projection>(customerAccsessPoint, set.ConvertToQDescriptor(query));
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma1={1}", customer.Id, customer.Firma1);


            }
        }

        private static void AnonymeSelectorQueryTest()
        {
            Console.WriteLine("AnonymeSelectorQueryTest");
            var client = new QDataClient<CustomerDto>();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var set = new QSet<CustomerDto>();
            var query =
                set.Where(x => x.Id > 0 || x.ContactsCount > 50)
                    .Select(x => new { Id1 = x.Id, Firma4 = x.Firma11 }).Where(
                        x =>
                        x.Id1 > id.Value && x.Firma4.Contains(desc.Value));
            

            var customers = query.ToList();
            client.Get(customerAccsessPoint, set.ConvertToQDescriptor(query), query.ElementType, customers);
            
            if (customers == null)
            {
                return;
            }
            
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma4={1}", customer.Id1, customer.Firma4);


            }
        }

        #endregion
    }
}