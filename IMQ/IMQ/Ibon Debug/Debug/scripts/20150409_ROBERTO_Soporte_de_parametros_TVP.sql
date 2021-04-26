CREATE TYPE TVPInteger AS TABLE 
(
	ID INT
    PRIMARY KEY (ID)
)
GO

CREATE TYPE TVPString AS TABLE 
(
	Code NVARCHAR(450) -- El tama�o m�ximo para �ndices es de 900. Al ser NVARCHAR se queda en 450
    PRIMARY KEY (Code)
)
GO
