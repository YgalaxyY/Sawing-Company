-- SQL скрипт для создания таблицы пользователей и добавления учетных записей
-- Для швейной фабрики

-- Создание таблицы пользователей (если её нет)
CREATE TABLE IF NOT EXISTS `users` (
    `user_id` INT AUTO_INCREMENT PRIMARY KEY,
    `username` VARCHAR(50) NOT NULL UNIQUE,
    `password_hash` VARCHAR(255) NOT NULL,
    `role` ENUM('Admin', 'Staff') NOT NULL DEFAULT 'Staff',
    `full_name` VARCHAR(100),
    `is_active` BOOLEAN DEFAULT TRUE,
    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    `last_login` TIMESTAMP NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Очистка старых данных (опционально)
-- DELETE FROM users WHERE username IN ('admin', 'operator.ivanova', 'master.petrov');

-- Добавление администратора
-- Логин: admin
-- Пароль: FabricaAdmin2024!
INSERT INTO `users` (`username`, `password_hash`, `role`, `full_name`, `is_active`) VALUES 
('admin', SHA2('FabricaAdmin2024!', 256), 'Admin', 'Администратор системы', TRUE)
ON DUPLICATE KEY UPDATE 
    password_hash = SHA2('FabricaAdmin2024!', 256),
    role = 'Admin',
    full_name = 'Администратор системы',
    is_active = TRUE;

-- Добавление оператора (сотрудника)
-- Логин: operator.ivanova
-- Пароль: IvanoVa2024!
INSERT INTO `users` (`username`, `password_hash`, `role`, `full_name`, `is_active`) VALUES 
('operator.ivanova', SHA2('IvanoVa2024!', 256), 'Staff', 'Иванова Мария Петровна - Оператор', TRUE)
ON DUPLICATE KEY UPDATE 
    password_hash = SHA2('IvanoVa2024!', 256),
    role = 'Staff',
    full_name = 'Иванова Мария Петровна - Оператор',
    is_active = TRUE;

-- Добавление мастера (сотрудника)
-- Логин: master.petrov
-- Пароль: PetrovMaster2024!
INSERT INTO `users` (`username`, `password_hash`, `role`, `full_name`, `is_active`) VALUES 
('master.petrov', SHA2('PetrovMaster2024!', 256), 'Staff', 'Петров Иван Сергеевич - Мастер', TRUE)
ON DUPLICATE KEY UPDATE 
    password_hash = SHA2('PetrovMaster2024!', 256),
    role = 'Staff',
    full_name = 'Петров Иван Сергеевич - Мастер',
    is_active = TRUE;

-- Проверка добавленных пользователей
SELECT username, role, full_name, is_active, created_at FROM users;
