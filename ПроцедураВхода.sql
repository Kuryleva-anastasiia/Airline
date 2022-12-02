CREATE PROCEDURE [dbo].[LoginByPassword]   
@login varchar(50),   
@pass varchar(50)   
AS   
BEGIN   
SELECT login, password  
FROM  Вход  
WHERE login = @login   
AND  password = @pass  
END
