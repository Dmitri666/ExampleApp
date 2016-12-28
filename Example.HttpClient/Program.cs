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
            for (int i = 0; i < 1; i++)
            {
                JoinQueryTest1();
                WhereQueryTest2();
                //WhereQueryTest();
                //StaticQueryTest();
                //AnonymeSelectorQueryTest();
             
            }
            Console.ReadLine();
        }


        private static void WhereQueryTest1()
        {
            Console.WriteLine("WhereQueryTest");
            var client = new QDataClient();
            var set = new QSet<CustomerDto>();
            var descroptor = set.Where(x => x.Contacts.Count(c => c.Id > 1) > 2 && x.Firma11.Contains("t")).OrderBy(x => x.Firma11).Take(1).Skip(2).Select( x=> new CustomerDto() { Id = x.Id }).ToQDescriptor();
            //var descroptor = set.Where(x => x.Id > 0).OrderBy(x => x.Firma11).Take(3).Skip(1).Select(x => new CustomerDto() { Id = x.Id }).ToQDescriptor();

            
            var customers = client.Get<CustomerDto>(customerAccsessPoint,descroptor);
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
            var client = new QDataClient();
            var set = new QSet<ContactDto>();
            var descroptor = set.Where(x => x.Customer.Firma11.Contains("a") && x.Id > 0).ToQDescriptor();
            
            var contacts = client.Get<ContactDto>(contactAccsessPoint, descroptor);
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
            var client = new QDataClient();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            
            var query =
                new QSet<ContactDto>()
                    .Where(
                        x =>
                        x.Id > id.Value ).Select(x => new { FirstName = x.FirstName , CustomerName = x.Customer.Firma11 } );


            var contacts = query.ToList();
            client.Get(contactAccsessPoint, query.ToQDescriptor(),query.ElementType,contacts);
            
            foreach (var contact in contacts)
            {
                Console.WriteLine("id={0} FirstName={1}", contact.FirstName, contact.CustomerName);


            }
        }

        private static void WhereQueryTest()
        {
            Console.WriteLine("WhereQueryTest");
            var client = new QDataClient();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var descriptor =
                new QSet<CustomerDto>()
                    .Where(
                        x =>
                        x.Id > id.Value && x.Firma11.Contains(desc.Value) || x.Firma21.Contains("h") )
                    .ToQDescriptor();
            
            var customers = client.Get<CustomerDto>(customerAccsessPoint,descriptor);
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
            var client = new QDataClient();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var descriptor =
                new QSet<CustomerDto>()
                    .Where(
                        x =>
                        x.Id > id.Value && x.Firma11.Contains(desc.Value)
                        && x.Contacts.Any(y => y.Id > id.Value && y.FirstName.Contains(desc.Value))
                        || x.Firma21.Contains("h") ).Select(x => new Projection() { Id = x.Id, Firma1 = x.Firma11 })
                    .ToQDescriptor();
            
            var customers = client.Get<Projection>(customerAccsessPoint, descriptor);
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
            var client = new QDataClient();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                new QSet<CustomerDto>()
                    .Select(x => new { Id1 = x.Id, Firma4 = x.Firma11 }).Where(
                        x =>
                        x.Id1 > id.Value && x.Firma4.Contains(desc.Value));
            

            var customers = query.ToList();
            client.Get(customerAccsessPoint, query.ToQDescriptor(), query.ElementType, customers);
            
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