     	 --公司信息	 CREATE TABLE  CompanyInfo(	 Id   nvarchar(50) NOT NULL PRIMARY KEY,	 Name nvarchar(50) NOT NULL,	 FullName nvarchar(200) NOT NULL,	 TelPhone   nvarchar(50) NULL,	 PositionName   nvarchar(50) NULL,	 Province   nvarchar(50) NULL,	 City   nvarchar(50) NULL,	 Area   nvarchar(50) NULL,	 Address   nvarchar(200) NULL,	 DepositBank nvarchar(200) NULL,     BankAccount nvarchar(50) NULL,	 Status   smallint  NOT NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --供应商	 CREATE TABLE  SupplierInfo(	 Id   nvarchar(50) NOT NULL PRIMARY KEY,	 	 CompanyInfoId nvarchar(50) NOT NULL,		 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --服务商	 CREATE TABLE  FacilitatorInfo(	 Id   nvarchar(50) NOT NULL PRIMARY KEY,	 	 CompanyInfoId nvarchar(50) NOT NULL,		 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --供应商联系人信息	 CREATE TABLE  SupplierContact(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	 	 SupplierInfoId nvarchar(50) NOT NULL,	     UserInfoId nvarchar(50) NOT NULL,		 Isprincipal bit NOT NULL DEFAULT(0),	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 	 --服务商联系人信息	 CREATE TABLE  FacilitatorContact(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	 	 FacilitatorId nvarchar(50) NOT NULL,	     UserInfoId nvarchar(50) NOT NULL,		 Isprincipal bit NOT NULL DEFAULT(0),--是否负责人	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 	 	 --供应商的服务商信息	 CREATE TABLE  SupplierFacilitators(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	 	 SupplierInfoId nvarchar(50) NOT NULL,	     FacilitatorId nvarchar(50) NOT NULL,		 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	  --商品信息（提供供应商录入商品的模板）	 CREATE TABLE  Commodity(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	        Name nvarchar(50) NOT NULL,	 Price decimal(18, 2) NOT NULL,	 Unit nvarchar(50) NOT NULL,	 Cover nvarchar(200) NULL,	 Description varchar(2000) NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 		 	 --供应商对应服务商商品信息	 CREATE TABLE  SupplierCommodity(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	 	 SupplierInfoId nvarchar(50) NOT NULL,	     FacilitatorId nvarchar(50) NOT NULL,	 Name nvarchar(50) NOT NULL,	 Price decimal(18, 2) NOT NULL,	 Unit nvarchar(50) NOT NULL,	 Cover nvarchar(200) NULL,	 Description varchar(2000) NULL,     CalculateType nvarchar(50) NOT NULL,--计算类型 Full：按满数(送货)量计算  NET：按净数量计算   Empty:按空数量计算	 IsOweGood bit NOT NULL DEFAULT(1),--是否计算欠货,例如按重量计算的情况就不算欠货。0：不计算欠货 1：计算欠货 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --供应商对应服务商商品价格历史	 CREATE TABLE  SupplierCommodityHistory(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	  	 SupplierCommodityId nvarchar(50) NOT NULL,	 NewPrice decimal(18, 2) NOT NULL,     OldPrice decimal(18, 2) NOT NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --送货登记的模板	 CREATE TABLE  DeliverCommodityTemplate(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	  	 SupplierInfoId nvarchar(50) NOT NULL,     Name varchar(50) NOT NULL,	 Sort int NOT NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --送货登记货物模板详情	 CREATE TABLE  DeliverCommodity(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,     DeliverCommodityTemplateId	varchar(50) NOT NULL,	 SupplierCommodityId nvarchar(50) NOT NULL, 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 	 	  --送货登记服务商的模板	 CREATE TABLE  FacilitatorDeliverCommodityTemplate(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	  	 SupplierInfoId nvarchar(50) NOT NULL,	 FacilitatorId nvarchar(50) NOT NULL,     Name varchar(50) NOT NULL,	 IsDefault bit NOT NULL DEFAULT(0),	 Sort int NOT NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --送货登记服务商货物模板详情	 CREATE TABLE  FacilitatorDeliverCommodity(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,     FacilitatorDeliverCommodityTemplateId	varchar(50) NOT NULL,	 SupplierCommodityId nvarchar(50) NOT NULL, 	 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 	 --送货信息	 CREATE TABLE  DeliverInfo(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,     SupplierInfoId nvarchar(50) NOT NULL,	     FacilitatorId nvarchar(50) NOT NULL,      TotalAmount decimal(18, 2) NOT NULL,	 DeliveryContactId nvarchar(50) NOT NULL,	 ConsigneeContactId nvarchar(50)  NULL, 	 IsConfirmDeliver bit NOT NULL DEFAULT(0),--是否确认收货	 IsPayment bit NOT NULL DEFAULT(0),--是否付款	 Status   smallint  NOT NULL,--状态 0：草稿  1：已提交  2：收货已确认	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --送货详情	 CREATE TABLE  DeliverDetailInfo(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,     DeliverInfoId nvarchar(50) NOT NULL,	     SupplierCommodityId nvarchar(50) NOT NULL,      Price decimal(18, 2) NOT NULL,	 FullNumber  decimal(18, 2) NOT NULL,	 EmptyNumer decimal(18, 2)  NOT NULL,	 NetNumber decimal(18, 2)  NOT NULL,--净数量 	 Amount decimal(18, 2)  NOT NULL,	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	 --欠货详情	 CREATE TABLE  OweGoodsInfo(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,     SupplierInfoId nvarchar(50) NOT NULL,	 FacilitatorId nvarchar(50) NOT NULL,	 SupplierCommodityId nvarchar(50) NOT NULL, 	 OweNumber decimal(18, 2)  NOT NULL,--欠货物数量 	 Remark   nvarchar(200) NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)	 	  --送货特殊详情	 CREATE TABLE  SpecialDeliverDetailInfo(	 Id  nvarchar(50) NOT NULL PRIMARY KEY,	     DeliverInfoId nvarchar(50) NULL,	     SupplierCommodityId nvarchar(50) NOT NULL,      Price decimal(18, 2)  NULL,	 FullNumber  decimal(18, 2)  NULL,	 EmptyNumer decimal(18, 2)   NULL,	 NetNumber decimal(18, 2)   NULL,--净数量 	 Amount decimal(18, 2)  NOT NULL,	 Remark   nvarchar(200) NOT NULL,	 Version   timestamp  NOT NULL,	 UpdateTime   datetime  NOT NULL,	 AddTime   datetime  NOT NULL,	 AddUserId   nvarchar(50) NOT NULL,	 UpdateUserId   nvarchar(50) NOT NULL,	 AddUserRealName   nvarchar(50) NOT NULL,	 UpdateUserRealName   nvarchar(50) NOT NULL)