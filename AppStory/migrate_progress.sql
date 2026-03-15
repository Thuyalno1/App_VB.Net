-- ============================================
-- MIGRATE: Status (String) → Progress (Integer %)
-- ============================================

-- 1. Thêm cột Progress
ALTER TABLE Tasks ADD Progress INT DEFAULT 0;

-- 2. Migrate dữ liệu từ Status sang Progress
UPDATE Tasks SET Progress = 0   WHERE Status = N'Chờ xử lý';
UPDATE Tasks SET Progress = 50  WHERE Status = N'Đang thực hiện';
UPDATE Tasks SET Progress = 90  WHERE Status = N'Chờ duyệt';
UPDATE Tasks SET Progress = 100 WHERE Status = N'Đã hoàn thành';

-- 3. Xóa cột Status (chạy sau khi kiểm tra dữ liệu đã migrate đúng)
-- ALTER TABLE Tasks DROP COLUMN Status;
