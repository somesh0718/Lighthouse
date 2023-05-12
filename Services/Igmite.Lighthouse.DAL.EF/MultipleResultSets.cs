////using System.Data.Entity.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data.Common;

//namespace Igmite.Lighthouse.DAL.EF
//{
//    public static class MultipleResultSets
//    {
//        public static MultipleResultSetWrapper MultipleResults(this IgmiteDbContext db, string storedProcedure)
//        {
//            return new MultipleResultSetWrapper(db, storedProcedure);
//        }

//        public class MultipleResultSetWrapper
//        {
//            private readonly IgmiteDbContext _db;
//            private readonly string _storedProcedure;
//            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;

//            public MultipleResultSetWrapper(IgmiteDbContext db, string storedProcedure)
//            {
//                _db = db;
//                _storedProcedure = storedProcedure;
//                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
//            }

//            public MultipleResultSetWrapper With<TResult>()
//            {
//                _resultSets.Add((adapter, reader) => adapter
//                    .ObjectContext
//                    .Translate<TResult>(reader)
//                    .ToList());

//                return this;
//            }

//            public List<IEnumerable> Execute()
//            {
//                var results = new List<IEnumerable>();

//                using (var connection = _db.Database.GetDbConnection())
//                {
//                    connection.Open();
//                    var command = connection.CreateCommand();
//                    command.CommandText = "EXEC " + _storedProcedure;

//                    using (var reader = command.ExecuteReader())
//                    {
//                        var adapter = ((IObjectContextAdapter)_db);
//                        foreach (var resultSet in _resultSets)
//                        {
//                            results.Add(resultSet(adapter, reader));
//                            reader.NextResult();
//                        }
//                    }

//                    return results;
//                }
//            }
//        }
//    }
//}