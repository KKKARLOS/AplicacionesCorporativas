USE HCDIS
GO

Update dbo.EACAttribute Set Description = 'Calle'
 Where Description like 'Direcci�n 1' and Name = 'Address1' and EACElementID = 31

 Update dbo.EACAttribute Set Description = 'Direcci�n 1'
 Where Description like 'Direcci�n 1' and Name = 'Address2' and EACElementID = 31
 
 Update dbo.EACAttribute Set Description = 'Nivel de confidencialidad'
 Where Description like 'Nivel de confidencialidad del Paciente Idenficador';
 
 Update dbo.EACElement Set Description = 'Datos sensibles'
 Where Description like 'Datos sensibles de la persona';
 
 Update dbo.EACElement Set Description = 'N�meros de historia'
 Where Description like 'N�meros de historia por centro';
 
  Update dbo.EACElement Set Description = 'Paciente'
 Where Description like 'Clientes';
 
   Update dbo.EACElement Set Description = 'Direcci�n'
 Where Description like 'Direcciones';
 
    Update dbo.EACElement Set Description = 'Identificadores'
 Where Description like 'Identificadores asociados a la persona';
 
     Update dbo.EACElement Set Description = 'Tel�fonos'
 Where Description like 'Telefonos asociados a la persona';
 
 GO