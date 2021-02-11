DROP TABLE [dbo].[User]

CREATE TABLE [dbo].[User] (
    user_id int PRIMARY KEY,
    email_address varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    role_id smallint NOT NULL,
    first_name varchar(255),
    last_name varchar(255),
    date_of_birth DATETIME,
    created_date DATETIME
);


INSERT INTO Users (user_id,email_address,password,source,first_name,last_name,
date_of_birth,notifications,created_date)
VALUES
(1,'test.user@gmail.com','Aa12345!','APPL','Julius','Caesar',NULL,1,NULL)

select * from Users

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [smallint] IDENTITY(1,1) NOT NULL,
	[role_desc] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([role_id], [role_desc]) VALUES (1, N'Adminstrator')
GO
INSERT [dbo].[Role] ([role_id], [role_desc]) VALUES (2, N'Teacher')
GO
INSERT [dbo].[Role] ([role_id], [role_desc]) VALUES (3, N'Parent')
GO
INSERT [dbo].[Role] ([role_id], [role_desc]) VALUES (4, N'Child')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF