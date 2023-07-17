IF '$(SeedTestData)' = 1
BEGIN
    PRINT 'Inserting seed data (only for testing)'
    :r .\Seed\Users.sql
END;

