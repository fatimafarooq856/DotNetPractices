For EFCore:

Remove-Migration name_of_bad_migration
Step 1: Add your new migration

add-migration my_new_migration
Step 2: Apply your migration to the database

update-database


add-migration test