-- Migration script: Add IsApproved column to Tasks table
ALTER TABLE Tasks ADD COLUMN IsApproved TINYINT(1) DEFAULT 0;
