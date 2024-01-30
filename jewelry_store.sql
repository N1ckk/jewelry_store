/*
 Navicat Premium Data Transfer

 Source Server         : Local instance MySQL80
 Source Server Type    : MySQL
 Source Server Version : 80031 (8.0.31)
 Source Host           : localhost:3306
 Source Schema         : jewelry_store

 Target Server Type    : MySQL
 Target Server Version : 80031 (8.0.31)
 File Encoding         : 65001

 Date: 30/01/2024 19:32:31
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category`  (
  `ca_id` int NOT NULL AUTO_INCREMENT,
  `ca_name` varchar(45) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`ca_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES (1, 'Каблучки');
INSERT INTO `category` VALUES (2, 'Сережки');
INSERT INTO `category` VALUES (3, 'Ланцюжки');
INSERT INTO `category` VALUES (4, 'Підвіски');
INSERT INTO `category` VALUES (5, 'Кулони');
INSERT INTO `category` VALUES (6, 'Браслети');
INSERT INTO `category` VALUES (7, 'Хрестики');

-- ----------------------------
-- Table structure for client
-- ----------------------------
DROP TABLE IF EXISTS `client`;
CREATE TABLE `client`  (
  `c_id` int NOT NULL AUTO_INCREMENT,
  `c_name` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `c_surname` varchar(55) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `c_f_name` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `c_adress` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  `c_phone_number` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`c_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of client
-- ----------------------------
INSERT INTO `client` VALUES (1, 'Ростислав', 'Вовк', 'Олексійович', 'вул. Ірпінська, 5', '+380677151312');
INSERT INTO `client` VALUES (2, 'Матвій', 'Сорокін', 'Валерійович', 'вул. Героїв Харкова, 100', '+380970500129');
INSERT INTO `client` VALUES (3, 'Микита', 'Бєляєв', 'Васильович', 'вул. Пальміро Тольятті, 15', '+380971462137');
INSERT INTO `client` VALUES (4, 'Олександр', 'Рубценко', 'Васильович', 'вул. Рузвельта, 2', '+380503332157');
INSERT INTO `client` VALUES (5, 'Дмитро', 'Ковальов', 'Остапович', 'вул. Сумська, 19', '+380971351777');
INSERT INTO `client` VALUES (6, 'Сергій', 'Макаренко', 'Микитович', 'вул. Рузвельта', '+380973457444');
INSERT INTO `client` VALUES (7, 'Данило', 'Макаренко', 'Микитович', 'м. Харків, 4 відділення НП', '+380973457443');
INSERT INTO `client` VALUES (8, 'Данило', 'Зінченко', 'Микитович', 'м. Харків, 4 відділення НП', '+380973457445');

-- ----------------------------
-- Table structure for orders
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders`  (
  `o_id` int NOT NULL AUTO_INCREMENT,
  `c_id` int NULL DEFAULT NULL,
  `or_date` date NULL DEFAULT NULL,
  `or_status` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`o_id`) USING BTREE,
  INDEX `c_id`(`c_id` ASC) USING BTREE,
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`c_id`) REFERENCES `client` (`c_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orders
-- ----------------------------
INSERT INTO `orders` VALUES (1, 3, '2023-10-06', 'Опалата карткою');
INSERT INTO `orders` VALUES (2, 1, '2023-11-04', 'Опалата карткою');
INSERT INTO `orders` VALUES (3, 4, '2023-09-01', 'Опалата карткою');
INSERT INTO `orders` VALUES (4, 1, '2023-12-07', 'Післяпалата');
INSERT INTO `orders` VALUES (5, 2, '2023-12-05', 'Оплата карткою');
INSERT INTO `orders` VALUES (6, 3, '2023-10-04', 'Післяпалата');
INSERT INTO `orders` VALUES (7, 2, '2023-12-04', 'Післяплата');
INSERT INTO `orders` VALUES (8, 6, '2023-12-24', 'Післяплата');
INSERT INTO `orders` VALUES (9, 8, '2024-01-08', 'Післяоплата');
INSERT INTO `orders` VALUES (10, 6, '2024-01-15', 'Післяоплата');
INSERT INTO `orders` VALUES (11, 8, '2024-01-15', 'Післяоплата');

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product`  (
  `p_id` int NOT NULL AUTO_INCREMENT,
  `ca_id` int NULL DEFAULT NULL,
  `p_name` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `p_material` varchar(70) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `p_sample` int NOT NULL,
  `p_sex` enum('чоловічий','жіночий','дитячий','унісекс') CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `p_path` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`p_id`) USING BTREE,
  INDEX `ca_id`(`ca_id` ASC) USING BTREE,
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`ca_id`) REFERENCES `category` (`ca_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES (1, 5, 'Кулон Ніжки для малюка з діамантом', 'Золото', 585, 'жіночий', '/Images/UZ69712_000125504.png');
INSERT INTO `product` VALUES (2, 1, 'Каблучка з срібла з емаллю', 'Срібло', 925, 'жіночий', '/Images/1.png');
INSERT INTO `product` VALUES (3, 1, 'Каблучка зі срібла', 'Срібло', 925, 'жіночий', '/Images/2.png');
INSERT INTO `product` VALUES (4, 6, 'Браслет з перлами', 'Срібло', 925, 'жіночий', '/Images/BR2756-B_12_708.png');
INSERT INTO `product` VALUES (5, 6, 'Браслет з фіанітом', 'Срібло', 925, 'жіночий', '/Images/УЗ70470_17_5_5_12_64.png');
INSERT INTO `product` VALUES (6, 6, 'Браслет з червоного золота', 'Золото', 585, 'чоловічий', '/Images/UZ10067919_000121557.png');
INSERT INTO `product` VALUES (7, 2, 'Сережки з червоного золота з фіанітом', 'Золото', 585, 'жіночий', '/Images/8167584925901d0c09a63d61f20b25fa.png');
INSERT INTO `product` VALUES (8, 4, 'Підвіска з комбінованого золота з смарагдом', 'Золото', 585, 'жіночий', '/Images/УЗ60925_14_1_8170.png');
INSERT INTO `product` VALUES (9, 3, 'Ланцюг з червоного золота', 'Золото', 585, 'жіночий', '/Images/66912-4.5-2_01.png');
INSERT INTO `product` VALUES (10, 3, 'Ланцюжок з червоного золота ', 'Золото', 585, 'жіночий', '/Images/UZ10062168_000142351.png');
INSERT INTO `product` VALUES (11, 7, 'Хрест з срібла', 'Срібло', 925, 'жіночий', '/Images/31429_12_1.png');
INSERT INTO `product` VALUES (12, 7, 'Хрест з срібла с1', 'Срібло', 925, 'чоловічий', '/Images/UZ32579_111111.png');
INSERT INTO `product` VALUES (13, 7, 'Хрестик зі срібла', 'Срібло', 925, 'чоловічий', '/Images/с31350-1.png');
INSERT INTO `product` VALUES (14, 2, 'Сережки зі срібла Капкейк з емаллю та фіанітами', 'Срібло', 925, 'дитячий', '/Images/UZ30617982_000155919.png');

-- ----------------------------
-- Table structure for product_details
-- ----------------------------
DROP TABLE IF EXISTS `product_details`;
CREATE TABLE `product_details`  (
  `pd_id` int NOT NULL AUTO_INCREMENT,
  `p_id` int NULL DEFAULT NULL,
  `pd_size` double NULL DEFAULT NULL,
  `pd_weight` double NOT NULL,
  `pd_quantity` int NULL DEFAULT NULL,
  `pd_price` int NULL DEFAULT NULL,
  PRIMARY KEY (`pd_id`) USING BTREE,
  INDEX `p_id`(`p_id` ASC) USING BTREE,
  CONSTRAINT `product_details_ibfk_1` FOREIGN KEY (`p_id`) REFERENCES `product` (`p_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product_details
-- ----------------------------
INSERT INTO `product_details` VALUES (1, 3, 16, 2.5, 51, 2400);
INSERT INTO `product_details` VALUES (2, 5, 20, 3.4, 18, 2000);
INSERT INTO `product_details` VALUES (3, 2, 16, 2.84, 45, 2000);
INSERT INTO `product_details` VALUES (4, 1, 0, 1.53, 18, 13000);
INSERT INTO `product_details` VALUES (5, 4, 18, 3.55, 21, 1900);
INSERT INTO `product_details` VALUES (6, 4, 18.5, 3.58, 20, 1950);
INSERT INTO `product_details` VALUES (7, 2, 16.5, 2.95, 17, 2050);
INSERT INTO `product_details` VALUES (8, 2, 18, 3.2, 9, 2150);
INSERT INTO `product_details` VALUES (9, 6, 18, 5.82, 14, 20000);
INSERT INTO `product_details` VALUES (10, 6, 19, 6.03, 14, 22000);
INSERT INTO `product_details` VALUES (11, 7, 0, 4.05, 20, 9500);
INSERT INTO `product_details` VALUES (12, 8, 0, 0.48, 20, 5000);
INSERT INTO `product_details` VALUES (13, 9, 40, 23.54, 19, 9999);
INSERT INTO `product_details` VALUES (14, 10, 50, 23.54, 19, 70000);
INSERT INTO `product_details` VALUES (15, 11, 0, 3.5, 25, 5500);
INSERT INTO `product_details` VALUES (16, 12, 0, 2.1, 25, 1200);
INSERT INTO `product_details` VALUES (17, 13, 0, 1.59, 20, 1000);
INSERT INTO `product_details` VALUES (18, 14, 0, 2, 35, 1500);

-- ----------------------------
-- Table structure for product_orders
-- ----------------------------
DROP TABLE IF EXISTS `product_orders`;
CREATE TABLE `product_orders`  (
  `po_id` int NOT NULL AUTO_INCREMENT,
  `pd_id` int NULL DEFAULT NULL,
  `o_id` int NOT NULL,
  `po_quantity` int NULL DEFAULT NULL,
  `po_summary` int NULL DEFAULT NULL,
  PRIMARY KEY (`po_id`) USING BTREE,
  INDEX `pd_id`(`pd_id` ASC) USING BTREE INVISIBLE,
  INDEX `product_orders_ibfk_2`(`o_id` ASC) USING BTREE,
  CONSTRAINT `f` FOREIGN KEY (`pd_id`) REFERENCES `product_details` (`pd_id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `product_orders_ibfk_2` FOREIGN KEY (`o_id`) REFERENCES `orders` (`o_id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product_orders
-- ----------------------------
INSERT INTO `product_orders` VALUES (1, 2, 1, 1, 2400);
INSERT INTO `product_orders` VALUES (2, 3, 7, 1, 2400);
INSERT INTO `product_orders` VALUES (3, 1, 4, 2, 2880);
INSERT INTO `product_orders` VALUES (4, 4, 2, 1, 15600);
INSERT INTO `product_orders` VALUES (5, 3, 3, 1, 2400);
INSERT INTO `product_orders` VALUES (6, 2, 5, 3, 7200);
INSERT INTO `product_orders` VALUES (7, 4, 6, 1, 15600);
INSERT INTO `product_orders` VALUES (8, 10, 8, 1, 22000);
INSERT INTO `product_orders` VALUES (9, 8, 9, 1, 2150);
INSERT INTO `product_orders` VALUES (10, 4, 10, 1, 13000);
INSERT INTO `product_orders` VALUES (11, 9, 11, 1, 20000);

-- ----------------------------
-- Table structure for workers
-- ----------------------------
DROP TABLE IF EXISTS `workers`;
CREATE TABLE `workers`  (
  `w_id` int NOT NULL AUTO_INCREMENT,
  `w_login` varchar(40) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  `w_pass` varchar(70) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`w_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb3 COLLATE = utf8mb3_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of workers
-- ----------------------------
INSERT INTO `workers` VALUES (1, 'admin', 'e2208f127758e710416cb215916721ac0264eeb4736e119f0d602ad5e7305545');

SET FOREIGN_KEY_CHECKS = 1;
