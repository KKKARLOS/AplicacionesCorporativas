USE HCDIS
GO

Update dbo.EACAttribute Set Description = 'Calle'
 Where Description like 'Dirección 1' and Name = 'Address1' and EACElementID = 31

 Update dbo.EACAttribute Set Description = 'Dirección 1'
 Where Description like 'Dirección 1' and Name = 'Address2' and EACElementID = 31
 
 Update dbo.EACAttribute Set Description = 'Nivel de confidencialidad'
 Where Description like 'Nivel de confidencialidad del Paciente Idenficador';
 
 Update dbo.EACElement Set Description = 'Datos sensibles'
 Where Description like 'Datos sensibles de la persona';
 
 Update dbo.EACElement Set Description = 'Números de historia'
 Where Description like 'Números de historia por centro';
 
  Update dbo.EACElement Set Description = 'Paciente'
 Where Description like 'Clientes';
 
   Update dbo.EACElement Set Description = 'Dirección'
 Where Description like 'Direcciones';
 
    Update dbo.EACElement Set Description = 'Identificadores'
 Where Description like 'Identificadores asociados a la persona';
 
     Update dbo.EACElement Set Description = 'Teléfonos'
 Where Description like 'Telefonos asociados a la persona';
 
 GO