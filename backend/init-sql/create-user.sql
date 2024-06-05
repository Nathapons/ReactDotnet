CREATE LOGIN mynewuser WITH PASSWORD = 'root'; CREATE USER mynewuser FOR LOGIN mynewuser; ALTER SERVER ROLE sysadmin ADD MEMBER mynewuser;
