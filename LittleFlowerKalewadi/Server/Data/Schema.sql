DROP TABLE Users

CREATE TABLE Users (
    user_id int PRIMARY KEY,
    email_address varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    source varchar(255) NOT NULL,
    first_name varchar(255),
    last_name varchar(255),
    date_of_birth DATETIME,
    notifications int,
    created_date DATETIME
);


INSERT INTO Users (user_id,email_address,password,source,first_name,last_name,
date_of_birth,notifications,created_date)
VALUES
(1,'test.user@gmail.com','Aa12345!','APPL','Julius','Caesar',NULL,1,NULL)

select * from Users