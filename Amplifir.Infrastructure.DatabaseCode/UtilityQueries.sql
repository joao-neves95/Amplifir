/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

-- SHOW ALL TABLES.

--SELECT *
--FROM pg_catalog.pg_tables
--WHERE schemaname != 'pg_catalog' and schemaname != 'information_schema';

------------------------

-- DROP ALL TABLES (public schema)

-- (Create the script with sql)

--SELECT 'DROP TABLE IF EXISTS "' || tablename || '" CASCADE;' 
--FROM pg_tables
--WHERE schemaname = 'public';

-- (run result)

------------------------

-- SELECTS

--select * from appuser;
--select * from appuserprofile;
--select * from AuditLog;

-- End of SELECTS

-- SHOUT REACTIONS

--select * from shoutreaction where ShoutId = 1;

--delete from shoutreaction where id = 11;

--update shout
--set likesCount = , dislikesCount = 
--where id = 1

-- End of SHOUT REACTIONS

-- DELETE USER.

--delete from appuserprofile where userid = 14;
--delete from auditlog where userid = 14;
--delete from appuser where id = 14;

-- End of DELETE USER.
