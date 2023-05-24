CREATE TABLE  File(
	 Id   nvarchar(50) NOT NULL PRIMARY KEY,
	 Name   nvarchar(200) NOT NULL,
                 Extension   nvarchar(50) NOT NULL, 
                 Md5 varchar(50) NOT NULL,
	 Size  decimal(18,2)  NOT NULL ,	
	 [Path]   nvarchar(50) NOT NULL
	 Remark   nvarchar(200) NULL,
	 Version   timestamp  NOT NULL,
	 UpdateTime   datetime  NOT NULL,
	 AddTime   datetime  NOT NULL,
	 AddUserId   nvarchar(50) NOT NULL,
	 UpdateUserId   nvarchar(50) NOT NULL,
	 AddUserRealName   nvarchar(50) NOT NULL,
	 UpdateUserRealName   nvarchar(50) NOT NULL,
	 )