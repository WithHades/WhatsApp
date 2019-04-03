/*
1.配置信息表
2.手机号码表
3.任务表
4.群发消息表
5.手机设备表
6.动态消息群发

*/
CREATE TABLE phonenums (
    [id ]            integer PRIMARY KEY autoincrement,                -- 设置主键
    [phonenum]       varchar(15) default '',
    [status]         int (4) default 1,
    [devid]          int(4) default -1    
);

CREATE TABLE config (
    [id ]            integer PRIMARY KEY autoincrement,                -- 设置主键
    [lang]       	int(4) default '1',		--语言
    [rownums]         int (4) default 6,	--每行个数
    [groupnums]          int(4) default 12    --每组数量
);

create table config(
	id int(11) not null AUTO_INCREMENT,
	lang int(4) not null default 1 comment '语言',
	rownums int(4) default 6 comment '每行个数',
	groupnums int(4) default 12 comment '每组数量',
	created int(11),
primary key(id)
)ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='数量配置表';

create table phonenum(
	id int(11) not null AUTO_INCREMENT,
	simulator_id int (11) not null default -1 comment '所属模拟器编号',
	phonenum varchar(15) default '',
	status int(4) default 1,
	created int(11),
primary key(id)
)ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COMMENT='手机号码表';