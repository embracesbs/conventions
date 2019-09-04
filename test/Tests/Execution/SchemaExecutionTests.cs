using GraphQL;
using GraphQL.Conventions.Relay;
using GraphQL.Conventions.Tests;
using GraphQL.Conventions.Tests.Templates.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Execution
{
    public class SchemaExecutionTests
    {
        [Test]
        public void Can_Have_Decimals_In_Schema()
        {
            var schema = SchemaBuilderHelpers.Schema<SchemaTypeWithDecimal>();
            schema.ShouldHaveQueries(1);
            schema.ShouldHaveMutations(0);
            schema.Query.ShouldHaveFieldWithName("test");
            var result = schema.Execute((e) => e.Query = "query { test }");
            ResultHelpers.AssertNoErrorsInResult(result);
        }

        class SchemaTypeWithDecimal
        {
            public QueryTypeWithDecimal Query { get; }
        }

        class QueryTypeWithDecimal
        {
            public decimal Test => 10;
        }

        //public class BugReproSchema
        //{
        //    public BugReproQuery Query { get; }
        //}

        //public class BugReproQuery
        //{
        //    public BugReproQuery() { }

        //    public Stuff<Broken> A() => new Stuff<Broken> { Value = new Broken() }; // Changes the name of the method to 'Broken' and it will work...

        //    public Stuff<Connection<Holder>> Holders() =>
        //        new Stuff<Connection<Holder>>
        //        {
        //            Value = new Connection<Holder>
        //            {
        //                Edges = new List<Edge<Holder>>
        //                    {
        //                    new Edge<Holder>
        //                    {
        //                        Cursor = Cursor.New<Holder>(0),
        //                        Node = new Holder
        //                        {
        //                        }
        //                    }
        //                    },
        //                PageInfo = new PageInfo
        //                {
        //                    EndCursor = Cursor.New<Holder>(0),
        //                    HasNextPage = false,
        //                    HasPreviousPage = false,
        //                    StartCursor = Cursor.New<Holder>(0)
        //                },
        //                TotalCount = 1
        //            }
        //        };
        //}

        //public class Holder
        //{
        //    public IEnumerable<ICommonInterface> InterfaceConnection() => new[] { new Broken() };
        //}

        //public interface ICommonInterface
        //{
        //    int Test { get; }
        //}

        //public class Broken : ICommonInterface
        //{
        //    public int Test => 1;
        //}

        //public class Stuff<T>
        //{
        //    public T Value { get; set; }
        //}
    }
}
