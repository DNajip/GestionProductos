use GestionProductosBD;
go

/* =====================================================
   procedimientos almacenados
   ===================================================== */


-- funcionalidad: registrar un nuevo usuario

create procedure crear_usuario
    @nombre varchar(50),
    @apellido varchar(50),
    @usuario varchar(30),
    @password varchar(100),
    @email varchar(50),
    @telefono varchar(10)
as
begin
    insert into usuarios (nombre, apellido, usuario, password, email, telefono)
    values (@nombre, @apellido, @usuario, @password, @email, @telefono);
end;
go

-- funcionalidad: validar usuario para inicio de sesión
create procedure login_usuario
    @usuario varchar(30),
    @password varchar(100)
as
begin
    select id, nombre, apellido
    from usuarios
    where usuario = @usuario
      and password = @password;
end;
go

-- funcionalidad: listar proveedores
create procedure listar_proveedores
as
begin
    select * from proveedores;
end;
go
-- funcionalidad: crear un nuevo proveedor
create procedure crear_proveedor
    @proveedor varchar(50)
as
begin
    insert into proveedores (proveedor)
    values (@proveedor);
end;
go
-- funcionalidad: crear un nuevo producto
create procedure crear_producto
    @producto varchar(50),
    @existencia int,
    @estado varchar(8),
    @id_proveedor int
as
begin
    insert into productos (producto, existencia, estado, id_proveedor)
    values (@producto, @existencia, @estado, @id_proveedor);
end;
go

-- funcionalidad: listar todos los productos
--create procedure listar_productos
--as
--begin
--    select * from productos;
--end;
--go
CREATE PROCEDURE listar_productos
AS
BEGIN
    SELECT
        p.id,
        p.producto,
        p.existencia,
        p.estado,
        pr.proveedor
    FROM productos p
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id;
END;
GO

-- funcionalidad: obtener un producto por su id
create procedure obtener_producto
    @id int
as
begin
    select *
    from productos
    where id = @id;
end;
go
-- funcionalidad: actualizar un producto existente
create procedure actualizar_producto
    @id int,
    @producto varchar(50),
    @existencia int,
    @estado varchar(8),
    @id_proveedor int
as
begin
    update productos
    set producto = @producto,
        existencia = @existencia,
        estado = @estado,
        id_proveedor = @id_proveedor
    where id = @id;
end;
go
-- funcionalidad: eliminar un producto
create procedure eliminar_producto
    @id int
as
begin
    delete from productos
    where id = @id;
end;
go
-- funcionalidad: crear una opción para un producto
create procedure crear_opcion
    @opcion varchar(30),
    @estado varchar(8),
    @id_producto int
as
begin
    insert into opciones (opcion, estado, id_producto)
    values (@opcion, @estado, @id_producto);
end;
go
-- funcionalidad: listar opciones de un producto
create procedure listar_opciones
    @id_producto int
as
begin
    select *
    from opciones
    where id_producto = @id_producto;
end;
go
-- funcionalidad: actualizar una opción
create procedure actualizar_opcion
    @id int,
    @opcion varchar(30),
    @estado varchar(8)
as
begin
    update opciones
    set opcion = @opcion,
        estado = @estado
    where id = @id;
end;
go
-- funcionalidad: eliminar una opción
create procedure eliminar_opcion
    @id int
as
begin
    delete from opciones
    where id = @id;
end;
go

--buscar producto con el filtro de busqueda
CREATE PROCEDURE buscar_productos
    @texto VARCHAR(50)
AS
BEGIN
    SELECT
        p.id,
        p.producto,
        p.existencia,
        p.estado,
        pr.proveedor
    FROM productos p
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id
    WHERE p.producto LIKE '%' + @texto + '%';
END;
GO

--Listar productos por estado
create procedure listar_productos_por_estado
 @estado varchar(8)
 as
 begin
    select
        p.id,
        p.producto,
        p.existencia,
        p.estado,
        pr.proveedor
    from productos p 
    inner join proveedores pr on p.id_proveedor = pr.id
    where p.estado = @estado;
    end;
    go