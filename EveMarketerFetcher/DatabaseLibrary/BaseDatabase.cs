using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading;

namespace DatabaseLibrary {
    public class BaseDatabase {

        protected readonly Mutex m_dbLock = new Mutex(false, "evepricedbmutex");
        internal DbContext m_db;

        internal bool UseMutexWaits = true;

        public BaseDatabase(string dbPath) {
            m_db = new DbContext(dbPath);
        }

        public TResult ExecuteQuerySingleReturn<TResult>(SQLiteCommand command) {

            TResult result = default(TResult);

            try {
                result = ExecuteQuery(command, new Func<SQLiteDataReader, TResult>(reader => {
                    while (reader.Read()) {
                        if (reader[0] == System.DBNull.Value)
                            result = default(TResult);
                        else
                            result = (TResult)reader[0];
                    }
                    return result;
                }));
            }
            catch(Exception e) {

            }

            return result;
        }

        public TResult ExecuteQuery<TResult>(SQLiteCommand command, Func<SQLiteDataReader, TResult> readerAction) {
            TResult result = default(TResult);

            try {
                using(var conn = m_db.GetConnection()) {
                    try {
                        conn.Open();
                        command.Connection = conn;
                        using(SQLiteDataReader reader = command.ExecuteReader()) {
                            result = readerAction(reader);
                        }
                    }
                    catch (Exception e) {

                    }
                    finally {
                        conn.Close();
                    }
                }
            }
            catch(Exception e) {

            }
            return result;
        }

        public long ExecuteNonQuery(SQLiteCommand command, bool returnRowId = false) {
            return ExecuteNonQuery(new List<SQLiteCommand> { command });
        }

        public long ExecuteNonQuery(List<SQLiteCommand> commands, bool returnRowId = false) {
            long result = 0;

            if((UseMutexWaits && m_dbLock.WaitOne(TimeSpan.FromSeconds(5), true)) || !UseMutexWaits) {
                try {

                    using(var conn = m_db.GetConnection()) {
                        try {
                            conn.Open();

                            using (var transaction = conn.BeginTransaction(true)) {
                                try {
                                    foreach (var command in commands) {
                                        command.Connection = conn;

                                        result = command.ExecuteNonQuery();
                                    }

                                    transaction.Commit();

                                    if (returnRowId)
                                        result = conn.LastInsertRowId;


                                }
                                catch(Exception e) {
                                    transaction.Rollback();

                                    Console.WriteLine("[ExecuteNonQuery][exc]");
                                    foreach(var command in commands) {
                                        Console.WriteLine(command.CommandText);
                                    }
                                    Console.WriteLine(e.Message);
                                    throw e;
                                }
                            }
                        }
                        catch(Exception e) {
                            Console.WriteLine("[ExecuteNonQuery][exc]");
                            Console.WriteLine(conn.ConnectionString);
                            Console.WriteLine(e.Message);
                            throw e;
                        }
                        finally {
                            conn.Close();
                        }
                    }


                }
                catch (Exception e){
                    Console.WriteLine("[ExecuteNonQuery][e]");
                    Console.WriteLine(e.Message);
                    return -1;
                }
                finally {
                    try {
                        if (UseMutexWaits) {
                            m_dbLock.ReleaseMutex();
                        }

                    }
                    catch {
                        Console.WriteLine("MUTEX FAILED RELEASE!");
                    }
                }

            }
            return result;
        }
    }
}
