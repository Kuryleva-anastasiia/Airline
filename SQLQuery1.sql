update Пользователи
set image = (SELECT MyImage.* from Openrowset(Bulk 'C:\College\ТРПО\avatar.png', Single_Blob) MyImage) where id = 12

