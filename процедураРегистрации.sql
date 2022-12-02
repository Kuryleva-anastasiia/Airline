CREATE PROCEDURE [dbo].[Insert_User]
        @login NVARCHAR(50) ,
        @password NVARCHAR(50),
        @name NVARCHAR(50),
        @surname NVARCHAR(50),
        @thirdname NVARCHAR(50)
AS
BEGIN
       SET NOCOUNT ON;
       IF EXISTS(SELECT login FROM Вход WHERE login = @login)
       BEGIN
              SELECT -1 AS Id -- Username exists.
       END
      
       ELSE
       BEGIN
              INSERT INTO [Пользователи]
                        ([Фамилия]
                        ,[Имя]
                       ,[Отчество])
              VALUES
                        (@surname
                        ,@name
                        ,@thirdname)

            declare @id int;
            --select @id = Вход.id FROM Вход WHERE login = @login;
            SELECT @id = SCOPE_IDENTITY();

              INSERT INTO [Вход]
                        ([Id]
                        ,[login]
                       ,[password])
              VALUES
                        (@id
                        ,@login
                        ,@password)
             
             SELECT SCOPE_IDENTITY() AS Id -- UserId                     
     END
END
