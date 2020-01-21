/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Linq;
using System.Text;
using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>" )]
    public static class DapperHelperQueries
    {
        public static string SelectSessionLastInsertedUserId()
        {
            return "SELECT currval( pg_get_serial_sequence('AppUser', 'id') )";
        }

        public static string SelectSessionLastInsertedShoutId()
        {
            return "SELECT currval( pg_get_serial_sequence('Shout', 'id') )";
        }

        /// <summary>
        ///
        /// This query must be made in the same connection of the inserted value.
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"> The column name must be lower case. </param>
        /// <returns></returns>
        public static string SelectSessionLastInserted(string tableName, string columnName)
        {
            return $"SELECT currval( pg_get_serial_sequence('{tableName}', '{columnName}') )";
        }

        /// <summary>
        /// 
        /// You have to pass the value you want to search for to Dapper
        ///   on the parameter object with the name "IPv4".
        ///     
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public static string CreateNewLog(string userId, short eventTypeId)
        {
            return $@"INSERT INTO AuditLog (UserId, IPv4, EventTypeId)
                      VALUES ( { userId }, @IPv4, { eventTypeId } )
                   ";
        }

        public static string GetShoutQueryWithoutWhere()
        {
            return $@"SELECT Shout.Id, Shout.UserId, AppUser.UserName, Shout.Content, Shout.CreateDate, Shout.LikesCount, Shout.DislikesCount
                      FROM Shout
                          INNER JOIN AppUser
                          ON Shout.UserId = AppUser.Id
                   ";
        }


        /// <summary>
        /// 
        /// Used inside the WHERE, and if any, the last AND of the condition.
        /// 
        /// </summary>
        /// <param name="tablename"> ATENTION: This is not parameterized. Do not get it from any client. </param>
        /// <param name="lastId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string PaginatedQueryDESC(string tableName, int lastId, short limit)
        {
            return $@" {tableName}.Id {( lastId > 0 ? "< " + lastId.ToString() :
                                                   $@" <= (
                                                               SELECT Id
                                                               FROM { tableName }
                                                               ORDER BY Id
                                                               LIMIT 1
                                                   )"
                                        )}
                       ORDER BY {tableName}.Id DESC
                       LIMIT {limit}
                   ";
        }

        /// <summary>
        /// <para>
        ///
        /// You have to pass the value you want to search for to Dapper
        ///   on the parameter object with the name "Value1".
        ///
        /// </para>
        /// <para>
        /// 
        /// The query returns 0 or 1.
        ///
        /// </para>
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string Exists(string tableName, string columnName)
        {
            return DapperHelperQueries.Exists( tableName, new string[] { columnName } );
        }

        /// <summary>
        /// <para>
        ///
        ///   You have to pass the values you want to search for to Dapper
        ///     on the parameter object ( E.g.: { Value1 = x, Value2 = y } ).
        ///
        /// </para>
        /// <para>
        ///
        ///   The query returns 0 or 1.
        ///
        /// </para>
        ///
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static string Exists(string tableName, string[] columnNames)
        {
            StringBuilder statement = new StringBuilder();

            for (int  i = 0; i < columnNames.Length; ++i)
            {
                statement.Append( columnNames[i] ).Append( " = @Value" ).Append( i + 1 );

                if (i != columnNames.Length - 1)
                {
                    statement.Append( " AND " );
                }
            }

            // COALESCE() is the PostgreSQL's similar function to ISNULL() in SQLServer.
            return $@"SELECT COALESCE(
                          (
                               SELECT 1
                               FROM {tableName}
                               WHERE { statement.ToString() }
                          ),
                          0
                      )";
        }
    }
}
