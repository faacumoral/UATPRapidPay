
IF NOT EXISTS(SELECT 1 FROM Users WHERE Username = 'test') BEGIN
	INSERT INTO Users(Username, Password)
	VALUES ('test', 'tu8rcbs+rlgrmwx4a+ugxq==') --'1234' password, only testing purposes
END