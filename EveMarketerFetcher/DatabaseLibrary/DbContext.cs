using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;


namespace DatabaseLibrary {
    class DbContext {

        internal string m_dbPath;
        internal bool m_dbPooling;
        internal string m_key;
        internal bool m_dbParseViaFramework;
        internal int m_connectionTimeout;

        public DbContext(string dbPath, bool pooling = true, string key = "", bool parseViaFramework = true, int connectionTimeout = 5) {
            m_dbPath = dbPath;
            m_dbPooling = pooling;
            m_key = key;
            m_dbParseViaFramework = parseViaFramework;
            m_connectionTimeout = connectionTimeout;
        }

        internal SQLiteConnection GetConnection(int connectionTimeout = 0) {
            return new SQLiteConnection(GetConnectionString(connectionTimeout), m_dbParseViaFramework);
        }

        internal string GetConnectionString(int connectionTimeout) {

            if(connectionTimeout == 0) {
                connectionTimeout = m_connectionTimeout;
            }

            if (string.IsNullOrEmpty(m_key))
                return string.Format("Data Source='{0}'; Pooling={1}; Connection Timeout={2};", m_dbPath, m_dbPooling.ToString(), connectionTimeout.ToString());
            else
                return string.Format("Data Source='{0}'; Pooling={1}; Encryption=SQLCipher; Password={2}; Connection Timeout={3};", m_dbPath, m_dbPooling.ToString(), m_key.ToString(), connectionTimeout.ToString());
        }
    }
}
