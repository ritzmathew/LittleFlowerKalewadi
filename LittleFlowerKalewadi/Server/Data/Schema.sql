use school

DROP TABLE Parent

CREATE TABLE Parent (
    user_id int IDENTITY(1,1) PRIMARY KEY,
    email_address varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    source varchar(255) NOT NULL,
    first_name varchar(255),
    last_name varchar(255),
    date_of_birth DATETIME,
    notifications int,
    created_date DATETIME
);


INSERT INTO Parent (email_address,password,source,first_name,last_name,
date_of_birth,notifications,created_date)
VALUES
('test.user@gmail.com','Aa12345!','APPL','Julius','Caesar',NULL,1,NULL)