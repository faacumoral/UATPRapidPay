﻿CREATE TABLE [dbo].[Payments]
(
	[PaymentID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Amount] DECIMAL(10,2) NOT NULL,
	[CardID] INT NOT NULL,
	[Fee] DECIMAL(10,2) NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [FK_Payments_CardID] FOREIGN KEY ([CardID]) REFERENCES [Cards]([CardID])
)
