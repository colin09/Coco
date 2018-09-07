-- db.sql

drop database if exists awesome;

create database awesome;

use awesome;

grant select, insert, update, delete on awesome.* to 'www-data'@'localhost' identified by 'www-data';

grant create, drop, select, insert, update, delete on awesome.* to 'sa'@'%' identified by 'password';


drop table if EXISTS users;
create table users (
    `Id` varchar(50) not null,
    `EMail` varchar(50) not null,
    `Password` varchar(50) not null,
    `Admin` bool not null,
    `Name` varchar(50) not null,
    `Logo` varchar(500) not null,
    `CreateDate` real not null,
    unique key `idx_email` (`EMail`),
    key `idx_created_at` (`CreateDate`),
    primary key (`Id`)
) engine=innodb default charset=utf8;

drop table if EXISTS blogs;
create table blogs (
    `Id` varchar(50) not null,
    `UserId` varchar(50) not null,
    `UserName` varchar(50) not null,
    `UserLogo` varchar(500) not null,
    `Name` varchar(50) not null,
    `Summary` varchar(200) not null,
    `Content` mediumtext not null,
    `CreateDate` real not null,
    key `idx_created_at` (`CreateDate`),
    primary key (`Id`)
) engine=innodb default charset=utf8;

drop table if EXISTS comments;
create table comments (
    `Id` varchar(50) not null,
    `BlogId` varchar(50) not null,
    `UserId` varchar(50) not null,
    `UserName` varchar(50) not null,
    `UserLogo` varchar(500) not null,
    `Content` mediumtext not null,
    `CreateDate` real not null,
    key `idx_created_at` (`CreateDate`),
    primary key (`Id`)
) engine=innodb default charset=utf8;