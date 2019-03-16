/*
 Navicat Premium Data Transfer

 Source Server         : MySql
 Source Server Type    : MySQL
 Source Server Version : 50724
 Source Host           : 127.0.0.1:3306
 Source Schema         : Ocelot

 Target Server Type    : MySQL
 Target Server Version : 50724
 File Encoding         : 65001

 Date: 13/02/2019 14:36:23
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for CcGlobalConfiguration
-- ----------------------------
DROP TABLE IF EXISTS `CcGlobalConfiguration`;
CREATE TABLE `CcGlobalConfiguration`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GatewayName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RequestIdKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `BaseUrl` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DownstreamSchem` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ServiceDiscoveryProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LoadBalancerOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `HttpHandlerOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `QosOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `IsDefault` int(255) NULL DEFAULT NULL,
  `InfoStatus` int(255) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for CcReroute
-- ----------------------------
DROP TABLE IF EXISTS `CcReroute`;
CREATE TABLE `CcReroute`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` int(11) NULL DEFAULT NULL,
  `UpstreamPathTemplate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `UpstreamHttpMethod` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `UpstreamHost` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DownstreamScheme` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DownstreamPathTemplate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DownstreamHostAndPorts` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `AuthenticationOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `RequestIdKey` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `CacheOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ServiceName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `LoadBalancerOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `QosOptions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `DelegatingHandlers` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Priority` int(11) NULL DEFAULT NULL,
  `InfoStatus` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for CcRerouteConfig
-- ----------------------------
DROP TABLE IF EXISTS `CcRerouteConfig`;
CREATE TABLE `CcRerouteConfig`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RouteId` int(11) NULL DEFAULT NULL,
  `RerouteId` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for CcRerouteGroup
-- ----------------------------
DROP TABLE IF EXISTS `CcRerouteGroup`;
CREATE TABLE `CcRerouteGroup`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Detail` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ParentId` int(11) NULL DEFAULT NULL,
  `InfoStatus` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;














#插入全局测试信息
insert into CcGlobalConfiguration(GatewayName,RequestIdKey,IsDefault,InfoStatus)
values('测试网关','test_gateway',1,1);

#插入路由分类测试信息
insert into CcRerouteGroup(Name,InfoStatus) values('测试分类',1);

#插入路由测试信息 
insert into CcReroute values(1,1,'/ctr/values','[ "GET" ]','','http','/api/Values','[{"Host": "localhost","Port": 9000 }]','','','','','','','',0,1);

#插入网关关联表
insert into CcRerouteConfig values(1,1,1);


select * from CcGlobalConfiguration;
select * from CcRerouteConfig;
select * from CcReroute;
select * from CcRerouteGroup;