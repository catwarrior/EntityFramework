// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Sqlite
{
    public class SqliteDatabaseFactory : ISqliteDatabaseFactory
    {
        private readonly DbContext _context;
        private readonly ISqliteDataStoreCreator _dataStoreCreator;
        private readonly ISqliteConnection _connection;
        private readonly IMigrator _migrator;
        private readonly ILoggerFactory _loggerFactory;

        public SqliteDatabaseFactory(
            [NotNull] DbContext context,
            [NotNull] ISqliteDataStoreCreator dataStoreCreator,
            [NotNull] ISqliteConnection connection,
            [NotNull] IMigrator migrator,
            [NotNull] ILoggerFactory loggerFactory)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(dataStoreCreator, nameof(dataStoreCreator));
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(migrator, nameof(migrator));
            Check.NotNull(loggerFactory, nameof(loggerFactory));

            _context = context;
            _dataStoreCreator = dataStoreCreator;
            _connection = connection;
            _migrator = migrator;
            _loggerFactory = loggerFactory;
        }

        public virtual Database CreateDatabase() =>
            new RelationalDatabase(_context, _dataStoreCreator, _connection, _migrator, _loggerFactory);
    }
}
