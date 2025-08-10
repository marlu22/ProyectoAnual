-- Stored Procedure to get password history for a user
CREATE PROCEDURE sp_get_historial_contrasenas_by_usuario_id
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id, id_usuario, fecha_cambio, contrasena_script
    FROM historial_contrasena
    WHERE id_usuario = @id_usuario;
END
GO

-- Stored Procedure to set the 2FA code and expiry for a user
CREATE PROCEDURE sp_set_2fa_code
    @username NVARCHAR(255),
    @code NVARCHAR(10),
    @expiry DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE usuarios
    SET Codigo2FA = @code, Codigo2FAExpiracion = @expiry
    WHERE usuario = @username;
END
GO
