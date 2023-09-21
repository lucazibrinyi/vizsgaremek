-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 11, 2023 at 05:04 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `easyquote_web`
--
CREATE DATABASE IF NOT EXISTS `easyquote_web` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `easyquote_web`;

-- --------------------------------------------------------

--
-- Table structure for table `failed_jobs`
--

CREATE TABLE `failed_jobs` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `uuid` varchar(255) NOT NULL,
  `connection` text NOT NULL,
  `queue` text NOT NULL,
  `payload` longtext NOT NULL,
  `exception` longtext NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2014_10_12_000000_create_users_table', 1),
(2, '2014_10_12_100000_create_password_reset_tokens_table', 1),
(3, '2019_08_19_000000_create_failed_jobs_table', 1),
(4, '2019_12_14_000001_create_personal_access_tokens_table', 1),
(5, '2023_09_01_120000_create_products_table', 1),
(6, '2023_09_01_120001_create_orders_table', 1),
(7, '2023_09_01_120010_create_ordered_products_table', 1);

-- --------------------------------------------------------

--
-- Table structure for table `ordered_products`
--

CREATE TABLE `ordered_products` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `order_id` bigint(20) UNSIGNED DEFAULT NULL,
  `product_id` bigint(20) UNSIGNED DEFAULT NULL,
  `quantity` smallint(6) NOT NULL,
  `price` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `user_id` bigint(20) UNSIGNED DEFAULT NULL,
  `postal_code` varchar(6) NOT NULL,
  `city` varchar(100) NOT NULL,
  `address` varchar(100) NOT NULL,
  `phone_number` varchar(50) NOT NULL,
  `payment_deadline` date DEFAULT NULL,
  `accepted` tinyint(1) NOT NULL DEFAULT 0,
  `survey` tinyint(1) NOT NULL DEFAULT 0,
  `completed` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`id`, `user_id`, `postal_code`, `city`, `address`, `phone_number`, `payment_deadline`, `accepted`, `survey`, `completed`, `created_at`, `updated_at`) VALUES
(1, 6, '1000', 'Százholdas Pagony', 'Faház út', '06201234567', NULL, 0, 0, 0, '2023-01-04 23:00:00', '2023-01-04 23:00:00'),
(2, 7, '1500', 'Százholdas Pagony', 'Faodu utca', '06301234567', NULL, 0, 0, 0, '2023-01-05 23:00:00', '2023-01-05 23:00:00'),
(3, 8, '2000', 'Százholdas Pagony', 'Sátoralja köz', '06701234567', NULL, 0, 0, 0, '2023-01-06 23:00:00', '2023-01-06 23:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `password_reset_tokens`
--

CREATE TABLE `password_reset_tokens` (
  `email` varchar(255) NOT NULL,
  `token` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `personal_access_tokens`
--

CREATE TABLE `personal_access_tokens` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `tokenable_type` varchar(255) NOT NULL,
  `tokenable_id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) NOT NULL,
  `token` varchar(64) NOT NULL,
  `abilities` text DEFAULT NULL,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `expires_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(150) NOT NULL,
  `category` varchar(50) NOT NULL,
  `sub_category` varchar(50) NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT 1,
  `img_url` varchar(100) DEFAULT NULL,
  `description` text NOT NULL,
  `price` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`id`, `name`, `category`, `sub_category`, `active`, `img_url`, `description`, `price`, `created_at`, `updated_at`) VALUES
(1, 'Kezelő - TM50', 'riaszto', 'kezelo', 1, 'partm50w.jpg', 'Multifunkciós érintőképernyős kezelő 1 címezhető zónabemenettel, nagy méretű, ragyogó, színes kijelzővel EVO, SP és MG központokhoz.', 83983, NULL, NULL),
(2, 'Kezelő - TM70', 'riaszto', 'kezelo', 1, 'tm70w.jpg', 'Paradox TM70 multifunkciós érintőképernyős kezelőegység - fehér színben. 1 címezhető zónabemenettel, nagy méretű, ragyogó, színes kijelzővel EVO, SP és MG központokhoz.', 107206, NULL, NULL),
(3, 'Kezelő - K656 W', 'riaszto', 'kezelo', 1, 'k656w.jpg', 'Érintőgombos LCD kezelő LED háttérvilágítással, modern, kompakt kivitelben 1 címezhető zóna bemenettel EVO központokhoz.', 62212, NULL, NULL),
(4, 'Kezelő - K32LCD+', 'riaszto', 'kezelo', 1, 'k32lcd.jpg', 'Két partíciós 32-zónás LCD kezelő. Programozható címkékkel és 1 kezelő zóna bemenettel, StayD támogatással MG, SP központ sorozathoz.', 44432, NULL, NULL),
(5, 'Kezelő - K641+', 'riaszto', 'kezelo', 1, 'k641.jpg', 'Egy vagy több partícióhoz rendelhető (max 8) buszos LCD kezelő, programozható címkékkel és 1 címezhető zónabemenettel.', 56406, NULL, NULL),
(6, 'Kezelő - K32+', 'riaszto', 'kezelo', 1, 'k32.jpg', 'Két partíciós 32-zónás LED kezelő, 1 kezelő zóna bemenettel, StayD támogatással, MG,SP,E központ sorozathoz.', 34272, NULL, NULL),
(7, 'Kezelő - K10 V', 'riaszto', 'kezelo', 1, 'k10v.jpg', 'Két partíciós 10-zónás LED kezelő (álló), 1 kezelő zóna bemenettel, szabadalmaztatott világító gombos kijelzéssel (a világító gomb jelzi a nyitott zónát),StayD támogatással, MG, SP, E központ sorozathoz.', 22842, NULL, NULL),
(8, 'Kezelő - K10 H', 'riaszto', 'kezelo', 1, 'k10h.jpg', 'Két partíciós 10-zónás LED kezelő (álló), 1 kezelő zóna bemenettel, szabadalmaztatott világító gombos kijelzéssel (a világító gomb jelzi a nyitott zónát),StayD támogatással, MG, SP, E központ sorozathoz.', 22842, NULL, NULL),
(9, 'Központ - SP4000', 'riaszto', 'kozpont', 1, 'sp4000.jpg', '2 partíciós 4-32 zónás 32 felhasználós központ felügyelt sziréna és AUX kimenettel, telefonvonali kommunikátorral, GSM, GPRS, IP és hangmodul (VDMP3) kompatibilis, 1 integrált PGM kimenettel választható StayD üzemmóddal. Rádiós eszközökkel egyirányú a kommunkiáció, a központ csak fogadja a rádiós jeleket.', 30099, NULL, NULL),
(10, 'Központ - SP6000', 'riaszto', 'kozpont', 1, 'sp6000.jpg', 'Paradox Spectra SP6000+ központ - 8 alaplapi (16 ATZ-vel), max 32 zónás rádiós központ (2 partíció, 32 kód).', 48786, NULL, NULL),
(11, 'Központ - EVO192', 'riaszto', 'kozpont', 1, 'evo192.jpg', '8 partíciós, 8–192 zónás, 999 felhasználós központ beépített beléptetőrendszerrel (32 ajtó vezérlés), felügyelt sziréna és AUX kimenettel, telefonvonali kommunikátorral, GSM, GPRS, IP és hangmodul (VDMP3) kompatibilis, 5 integrált PGM kimenettel.', 53140, NULL, NULL),
(12, 'Központ - MG5050+', 'riaszto', 'kozpont', 1, 'mg5050.jpg', 'Paradox Magellan MG5050+ központ - 8 alaplapi (16 ATZ-vel), max 32 zónás rádiós központ (2 partíció, 32 kód).', 62575, NULL, NULL),
(13, 'Sziréna - VL-640L', 'riaszto', 'szirena', 1, 'szirvl-640.jpg', 'Kültéri akkumulátoros sziréna LED villogóval LED-es reklámcélú alkonyatkapcsolós háttérvilágítással. Cserélhető búra (vásárolható narancssárga és kék színben is).', 14984, NULL, NULL),
(14, 'Sziréna - SOLO', 'riaszto', 'szirena', 1, 'szirsolo.jpg', 'Kültéri akkumulátoros hang-fény jelző, pozitív-negatív indítójel.', 15213, NULL, NULL),
(15, 'Sziréna - PS-128-1', 'riaszto', 'szirena', 1, 'szirps128.jpg', 'Kültéri akkumulátoros hang-fény jelző, pozitív-negatív indítójel. Levehető előlapi burkolat. 115 dB', 23889, NULL, NULL),
(16, 'Mozgásérzékelő - Optex - FLX-S-ST', 'riaszto', 'mozgaserzekelo', 1, 'optex.png', 'Optex FLX-S-ST hagyományos beltéri PIR, forgatható lencsével. 12 m / 85 fok vagy 18 m / 5 fok (folyosó).', 7800, NULL, NULL),
(17, 'Mozgásérzékelő - DG75', 'riaszto', 'mozgaserzekelo', 1, 'dg75.jpg', 'Digitális, kétoptikás, csúcsminőségű, kisállattűrő mozgásérzékelő, magas téves riasztás elleni védelemmel (40 kg valódi kisállat-immunitással).', 14095, NULL, NULL),
(18, 'Mozgásérzékelő - PMD2P', 'riaszto', 'mozgaserzekelo', 1, 'pmd2.jpg', 'Rádiós digitális, egyoptikás mozgásérzékelő (18 kg kisállat immunitással) EN200-300 és EN501310 minősítés.', 22842, NULL, NULL),
(19, 'Mozgásérzékelő - 476+', 'riaszto', 'mozgaserzekelo', 1, 'pro.jpg', 'Analóg mozgásérzékelő relés kimenettel.', 6384, NULL, NULL),
(20, 'TELL GSM Compact GSM II', 'riaszto', 'riasztokiegeszito', 1, 'gsmtgsm-compact-gsm-ii.jpg', '2 bemenet / 2 relé kimenetes kontaktus vezérelt kommunikátor. GSM jelentés. Jelzés 4 felhasználónak és távfelügyeleti számra.', 52007, NULL, NULL),
(21, 'Akkumulátor - Seal Lead Acid 4,2 Ah', 'riaszto', 'riasztokiegeszito', 1, 'ak42power.jpg', 'S. L. A. szünetmentes riasztó akkumulátor, 12 V, 4,2 Ah, f1 4,8-as csúszósaru, 90 x 70 x 98 mm.', 5842, NULL, NULL),
(22, 'Akkumulátor - Seal Lead Acid 6,5 Ah', 'riaszto', 'riasztokiegeszito', 1, 'ak65power.jpg', 'S. L. A. szünetmentes riasztó akkumulátor, 12 V, 6,5 Ah, f1 4,8-as csúszósaru, 151 x 65 x 93 mm.', 7064, NULL, NULL),
(23, 'Transzformátor - 16V AC 30VA transzformátor', 'riaszto', 'riasztokiegeszito', 1, 'trafo30.jpg', 'Műgyantával kiöntött transzformátor 16,5 V AC 30 VA , 90 x 70 x 52 mm, biztosítékkal.', 4166, NULL, NULL),
(24, 'Transzformátor - 16V AC 45VA transzformátor', 'riaszto', 'riasztokiegeszito', 1, 'trafo45.jpg', 'Műgyantával kiöntött transzformátor 16,5 V AC 45 VA , 90 x 70 x 65 mm, biztosítékkal.', 4724, NULL, NULL),
(25, 'Vezeték', 'riaszto', 'riasztokiegeszito', 1, 'utp305belden.jpg', 'Bitner CAT5e beltéri, fali UTP kábel, 4 x 2 ér.', 230, NULL, NULL),
(28, 'Kamera - Techson TCA EB5 E102 IH50 VF 2 Mpx-es Analóg HD kamera', 'kamera', 'kamera', 1, 'E1-01.jpg', 'Techson Analóg HD kamera. 2 Mpx-es, kültéri, eyeball, 2,8 - 12 mm varifokális objektív, videomód átkapcsolás: Nyomógomb', 35905, NULL, NULL),
(30, 'Kamera - Techson TCA EB2 E105 IH50 Z4 5 Mpx-es Analóg HD kamera', 'kamera', 'kamera', 1, 'tcaeb2e105ih50z4.jpg', 'Techson Analóg HD kamera. 5 Mpx-es, kültéri, eyeball, 2,8 - 12 mm varifokális objektív, 4x motoros zoom, videomód átkapcsolás: Nyomógomb', 51326, NULL, NULL),
(31, 'Kamera - Hikvision DS-2CD2163G2-I(4mm) 6 Mpx-es IP kamera', 'kamera', 'kamera', 1, 'DS-2CD2163G2-I(4mm).PNG', 'Hikvision 6 megapixeles felbontású, kültéri, dome IP kamera\r\nA kamera fix objektívjének fókusztávolsága: 4 mm. Beállítási módja: fix fókusz, látószöge: 87°.\r\nA készülék IP67 védettségű és IK10 vandálbiztos kialakítású. A beépített infravörös megvilágítás típusa: EXIR LED. A kamera valós, 120 dB-es WDR-rel rendelkezik.\r\nAz elérhető intelligens, analitikai funkciók a következők: vonalátlépés, területfigyelés.\r\n\r\nA kamera tápellátása történhet PoE rendszerű NVR, switch vagy tápfeladó segítségével is. ', 109263, NULL, NULL),
(32, 'Kamera - Hikvision DS-2CD2T83G2-4I(6mm) 8 Mpx-es IP kamera ', 'kamera', 'kamera', 1, 'DS-2CD2T83G2-4I.PNG', 'Hikvision 8 megapixeles felbontású, kültéri, kompakt IP kamera\r\nA kamera fix objektívjének fókusztávolsága: 6 mm. Beállítási módja: fix fókusz, látószöge: 54°.\r\nA készülék IP67 védettségű. A kamera valós, 120 dB-es WDR-rel rendelkezik.\r\n\r\nA kamera tápellátása történhet PoE rendszerű NVR, switch vagy tápfeladó segítségével is.', 128803, NULL, NULL),
(33, 'Rögzítő - Techson TCR A11 S204-NS 4 csatornás Analóg HD rögzítő', 'kamera', 'rogzito', 1, 'tcra11s204ns.jpg', 'Techson SmartSpeed 4+2 csatornás analóg HD DVR, 2 Mpx, H.265, VCA, 1 HDD', 49149, NULL, NULL),
(34, 'Rögzítő - Techson TCR A31 S808-NS 8 csatornás Analóg HD rögzítő', 'kamera', 'rogzito', 1, 'tcra31s808ns.jpg', 'Techson SmartSpeed 8+8 csatornás analóg HD DVR, 8 Mpx, H.265, VCA, 1 HDD, 4K monitorkimenet', 118636, NULL, NULL),
(35, 'Rögzítő - Hikvision NK42W0H(D)/EU 4 csatornás IP rögzítő', 'kamera', 'rogzito', 1, 'nk44w0h-1t.PNG', 'Hikvision IP videorögzítő.\r\n4 IP csatornát tartalmaz. A csatlakoztatható legnagyobb felbontású kamerák a következők: IP: 4 Mpx.\r\nA rögzítő támogatja a H.264, H.265 tömörítést.\r\nAz elsődleges HDMI kimenet felbontása: Full HD (1920x1080).\r\nA beépíthető merevlemezek [3,5\" SATA HDD (max: 6 TB / HDD)] száma: 1.\r\nA rögzítő 1 x 10/100 Mb Ethernet csatlakozót és 802.11b/g/n Wi-Fi csatolót tartalmaz.\r\nAz IP-kamerákhoz elérhető, analitikai funkciók a következők: vonalátlépés, területfigyelés.', 250638, NULL, NULL),
(36, 'Rögzítő - Hikvision DS-7632NI-I2/16P 32 csatornás IP rögzítő', 'kamera', 'rogzito', 1, 'ds-7632ni-i216p.jpg', 'Hikvision Flow control IP videorögzítő.\r\n32 IP csatornát tartalmaz. A készülék hátlapján 16 PoE csatlakozó található, amely az arra alkalmas IP kamerákat táppal is ellátja. A csatlakoztatható legnagyobb felbontású kamerák a következők: IP: 6 Mpx.\r\nA rögzítő támogatja a H.264, H.265, MPEG-4 tömörítést. A kamerák felőli kommunikációs sebesség: 256 Mbit/s. Kommunikácós sebesség a rögzítőtől a kliensek felé: 256 Mbit/s.\r\nAz elsődleges HDMI kimenet felbontása: 4K (3840x2160).\r\nA beépíthető merevlemezek [3,5\" SATA HDD (max: 6 TB / HDD)] száma: 2.\r\nA készülék 4 riasztásbemenettel és 1 relékimenettel rendelkezik. A rögzítő 1 x 10/100/1000 Mb Ethernet csatlakozót tartalmaz.\r\nAz IP-kamerákhoz elérhető, analitikai funkciók a következők: vonalátlépés, területfigyelés, tárgyfigyelés, objektumazonosítás, objektumszámlálás, személyszámlálás, tűzriasztás, rendszámfelismerés, hőtérkép.', 572773, NULL, NULL),
(37, 'Háttértárak - Western Digital HDD 3,5\'\' WD SATA3 Purple 1TB WD10PURZ', 'riaszto', 'hattertarak', 1, 'winwd10purz.jpg', 'Western Digital HDD 3,5\'\' WD SATA3 Purple 1TB WD10PURZ, HDD, 5400 rpm, SATA III, 64 MB, Surveillance.', 26543, NULL, NULL),
(38, 'Háttértárak - Western Digital HDD 3,5\'\' WD SATA3 Purple 3TB WD30PURZ', 'kamera', 'hattertarak', 1, 'winwd30purz.jpg', 'Western Digital HDD 3,5\'\' WD SATA3 Purple 3TB WD30PURZ, HDD, 5400 rpm, SATA III, 64 MB, Surveillance.', 49022, NULL, NULL),
(39, 'Háttértárak - Western Digital WD42PURZ 4TB HDD 3,5\'\' Purple ', 'kamera', 'hattertarak', 1, 'wd42purz-0.jpg', 'Western Digital WD42PURZ 4TB HDD 3,5\'\' Purple, HDD, 5400 rpm, SATA III, 256 MB, Surveillance ', 51395, NULL, NULL),
(40, 'Háttértárak - Western Digital WD63PURZ 6TB HDD 3,5\'\' Purple ', 'kamera', 'hattertarak', 1, 'wd60purz.jpg', 'Western Digital WD63PURZ 6TB HDD 3,5\'\' Purple, HDD, 5400 rpm, SATA III, 256 MB, Surveillance ', 64611, NULL, NULL),
(41, 'Techson videobalun', 'kamera', 'kamerakiegeszitok', 1, 'SP111-HD.png', 'Techson Passzív rögzítő- vagy kameraoldali Lengő BNC-s videobalun. 1 csatornás HD videobalun, 1 BNC, kameraoldali 250m-ig.', 1250, NULL, NULL),
(42, 'Techson BKJ8E szerelődoboz kamerához', 'kamera', 'kamerakiegeszitok', 1, 'bkj8e.jpg', 'Szerelődoboz Techson Eyeball kamerákhoz.', 7346, NULL, NULL),
(43, 'Tápegység - SPS-CCTV-3A', 'kamera', 'kamerakiegeszitok', 1, 'tap3a-spscctv.jpg', 'Power Supply beépíthető tápegység, 12 V DC, 3 A, 36 W, 1 pár kimeneti sorkapocs.', 5931, NULL, NULL),
(44, 'Tápegység - SPS-CCTV-5A', 'kamera', 'kamerakiegeszitok', 1, 'tap5a-spscctv.jpg', 'Power Supply beépíthető tápegység, 12 V DC, 5 A, 60 W, 1 pár kimeneti sorkapocs.', 7146, NULL, NULL),
(45, 'Tápcsatlakozó - SP100M', 'kamera', 'kamerakiegeszitok', 1, 'sp100m.jpg', 'Tápcsatlakozó, dugó.', 325, NULL, NULL),
(46, 'Elosztó - HK4 4 fejes elosztó', 'kamera', 'kamerakiegeszitok', 1, 'trachk4.jpg', 'Tracon hordozható elosztósáv, 230 V AC, 16 A, 4 x SHUKO, 3 x 1,0 mm2, H05VV-F, 1,5 m, kapcsolóval, fehér.', 4415, NULL, NULL),
(47, 'SWITCH - TP-Link switch TL-SG105', 'kamera', 'kamerakiegeszitok', 1, 'szgswitch-tl-sg105.jpg', 'TP-Link switch TL-SG105, beltéri, asztali, nem menedzselhető, Gigabit LAN port 5.', 10185, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(150) NOT NULL,
  `email` varchar(150) NOT NULL,
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `password` varchar(255) NOT NULL,
  `employee` tinyint(1) NOT NULL DEFAULT 0,
  `remember_token` varchar(100) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `email`, `email_verified_at`, `password`, `employee`, `remember_token`, `created_at`, `updated_at`) VALUES
(1, 'boss', 'boss@easyquote.hu', NULL, 'df1aec32a2215ede9da14f328f370247', 1, NULL, '2022-12-31 23:00:00', '2022-12-31 23:00:00'),
(2, 'iroda1', 'iroda1@easyquote.hu', NULL, 'ad6f3a73ecfa6e953427b92c24e662ca', 1, NULL, '2022-12-31 23:00:00', '2022-12-31 23:00:00'),
(3, 'iroda2', 'iroda2@easyquote.hu', NULL, 'c802362c5e51d2538d93f7ca9dc1167d', 1, NULL, '2023-01-01 23:00:00', '2023-01-01 23:00:00'),
(4, 'iroda3', 'iroda3@easyquote.hu', NULL, '6765cf26e6fcc852a0717006d9266431', 1, NULL, '2023-01-02 23:00:00', '2023-01-02 23:00:00'),
(5, 'felmero', 'felmero@easyquote.hu', NULL, '$2y$10$VLwRmP.zPqAAfsW7S8ujOOaAIlRbaXNKUVVVe/rHkwaDH3HcakCy6', 1, NULL, '2023-01-03 23:00:00', '2023-01-03 23:00:00'),
(6, 'Micimackó', 'minimacko@thepooh.hu', NULL, '$2y$10$CVIFUTmOvMMTzZa3/C.v9.9qcunMIqahgfZre7XjTXqIENyrxLnhi', 0, NULL, '2023-01-04 23:00:00', '2023-01-04 23:00:00'),
(7, 'Malacka', 'malacka@theshy.hu', NULL, '$2y$10$o1WoYjLbVSndgrRCmjPD8OeyhLgD41JCXs3S85Bz/bzgDu05TaLIC', 0, NULL, '2023-01-05 23:00:00', '2023-01-05 23:00:00'),
(8, 'Füles', 'fules@thesad.hu', NULL, '$2y$10$79Ez8kY37LlM6niW2aEjKe9vh/LPRmKYemMTYGwY6BSkbtS2Lb5FK', 0, NULL, '2023-01-06 23:00:00', '2023-01-06 23:00:00');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`);

--
-- Indexes for table `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `ordered_products`
--
ALTER TABLE `ordered_products`
  ADD PRIMARY KEY (`id`),
  ADD KEY `ordered_products_order_id_foreign` (`order_id`),
  ADD KEY `ordered_products_product_id_foreign` (`product_id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`),
  ADD KEY `orders_user_id_foreign` (`user_id`);

--
-- Indexes for table `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  ADD PRIMARY KEY (`email`);

--
-- Indexes for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  ADD KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `users_email_unique` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `ordered_products`
--
ALTER TABLE `ordered_products`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `ordered_products`
--
ALTER TABLE `ordered_products`
  ADD CONSTRAINT `ordered_products_order_id_foreign` FOREIGN KEY (`order_id`) REFERENCES `orders` (`id`) ON DELETE SET NULL,
  ADD CONSTRAINT `ordered_products_product_id_foreign` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE SET NULL;

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
