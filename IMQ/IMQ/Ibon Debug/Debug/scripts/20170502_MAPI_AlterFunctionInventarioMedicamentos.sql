/****** Object:  UserDefinedFunction [dbo].[InventarioDeMedicamentos]    Script Date: 05/08/2017 11:03:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ALTER FUNCTION [dbo].[InventarioDeMedicamentos](@CUALES NVARCHAR(10))
ALTER FUNCTION [dbo].[InventarioDeMedicamentos]()
RETURNS @InvMedicamentos TABLE 
(
     Codigo_Nacional			nvarchar(15)  NOT NULL
	,Nombre_medicamento			nvarchar(200) NULL
	,Descripcion				nvarchar(200) NULL
	,CodigoGrupoTerapeutico	    nvarchar(10) NULL
	,Descripcion_GrupoTerapéutico 	nvarchar(200) NULL
	,Codigo_Familia				nvarchar(10) NULL
	,Descripcion_Familia		nvarchar(200) NULL
	,Codigo_SubFamilia	        nvarchar(10) NULL
	,Descripcion_SubFamilia	    nvarchar(200) NULL
	,Forma_Farmaceutica	        nvarchar(50) NULL
	,Via_administracion1		nvarchar(50) NULL
	,Via_administracion2		nvarchar(50) NULL
	,Via_administracion3		nvarchar(50) NULL
	,FrecuenciaDefecto	        nvarchar(50) NULL
	,Dosis						float  NULL
	,unidades_dosis				nvarchar(10) NULL
	,Volumen					float  NULL
	,unidades_volumen			nvarchar(50) NULL
	,unidades_caja				int	 NULL
	,Estupefaciente				nvarchar(2) NULL
	,Suero						nvarchar(2) NULL
	,IndicadorGuia				int NULL
	,DescripcionGuia			nvarchar(12) NULL
	,Cod_Unidodis				nvarchar(15) NULL
	,Nombre_UNIDOSIS			nvarchar(200) NULL
	,Multidosis_fraccionable	nvarchar(2) NULL
	,Contenido_Total 			float NULL
	,Unid_Dispensación			nvarchar(50) NULL
	,CuentaContableCVB			nvarchar(10) NULL
	,PrecioVenta				float  NULL
	,IVA						nvarchar(20) NULL
	,Observaciones				nvarchar(250) NULL
	,Estado						nvarchar(50) NULL
	,TipoComponente1			nvarchar(50) NULL
	,CodComponente1				nvarchar(10) NULL
	,NombreComponente1			nvarchar(200) NULL
	,CodATCComponente1			nvarchar(10) NULL
	,NombreATCComponente1		nvarchar(200) NULL
	,CantComponente1			float NULL
	,UndComponente1				nvarchar(50) NULL
	,IndIngredientePrincipal1	nvarchar(2) NULL
	,TipoComponente2			nvarchar(50) NULL
	,CodComponente2				nvarchar(10) NULL
	,NombreComponente2			nvarchar(200) NULL
	,CodATCComponente2			nvarchar(10) NULL
	,NombreATCComponente2		nvarchar(200) NULL
	,CantComponente2			float NULL
	,UndComponente2				nvarchar(50) NULL
	,IndIngredientePrincipal2	nvarchar(2) NULL
	,TipoComponente3			nvarchar(50) NULL
	,CodComponente3				nvarchar(10) NULL
	,NombreComponente3			nvarchar(200) NULL
	,CodATCComponente3			nvarchar(10) NULL
	,NombreATCComponente3		nvarchar(200) NULL
	,CantComponente3			float NULL
	,UndComponente3				nvarchar(50) NULL
	,IndIngredientePrincipal3	nvarchar(2) NULL

)
AS 
	BEGIN

		
		DECLARE  @ITEMID INT		   			   
				,@Codigo_Nacional			nvarchar(15)
				,@Nombre_medicamento			nvarchar(200) 
				,@Descripcion				nvarchar(200) 
				,@CodigoGrupoTerapeutico	    nvarchar(10) 
				,@Descripcion_GrupoTerapéutico 	nvarchar(200) 
				,@Codigo_Familia				nvarchar(10) 
				,@Descripcion_Familia		nvarchar(200) 
				,@Codigo_SubFamilia	        nvarchar(10) 
				,@Descripcion_SubFamilia	    nvarchar(200) 
				,@Forma_Farmaceutica	        nvarchar(50) 
				,@Via_administracion1		nvarchar(50) 
				,@Via_administracion2		nvarchar(50) 
				,@Via_administracion3		nvarchar(50) 
				,@FrecuenciaDefecto	        nvarchar(50) 
				,@Dosis						float  
				,@unidades_dosis				nvarchar(10) 
				,@Volumen					float  
				,@unidades_volumen			nvarchar(50) 
				,@unidades_caja				int	 
				,@Estupefaciente				nvarchar(2) 
				,@Suero						nvarchar(2) 
				,@IndicadorGuia				int 
				,@DescripcionGuia			nvarchar(12) 
				,@Cod_Unidodis				nvarchar(15) 
				,@Nombre_UNIDOSIS			nvarchar(200) 
				,@Multidosis_fraccionable	nvarchar(2) 
				,@Contenido_Total 			float 
				,@Unid_Dispensación			nvarchar(50) 
				,@CuentaContableCVB			nvarchar(10) 
				,@PrecioVenta				float  
				,@IVA						nvarchar(20) 
				,@Observaciones				nvarchar(250) 
				,@Estado						nvarchar(50) 
				,@TipoComponente1			nvarchar(50) 
				,@CodComponente1				nvarchar(10) 
				,@NombreComponente1			nvarchar(200) 
				,@CodATCComponente1			nvarchar(10) 
				,@NombreATCComponente1		nvarchar(200) 
				,@CantComponente1			float 
				,@UndComponente1				nvarchar(50) 
				,@IndIngredientePrincipal1	nvarchar(2) 
				,@TipoComponente2			nvarchar(50) 
				,@CodComponente2				nvarchar(10) 
				,@NombreComponente2			nvarchar(200) 
				,@CodATCComponente2			nvarchar(10) 
				,@NombreATCComponente2		nvarchar(200) 
				,@CantComponente2			float 
				,@UndComponente2				nvarchar(50) 
				,@IndIngredientePrincipal2	nvarchar(2) 
				,@TipoComponente3			nvarchar(50) 
				,@CodComponente3				nvarchar(10) 
				,@NombreComponente3			nvarchar(200) 
				,@CodATCComponente3			nvarchar(10) 
				,@NombreATCComponente3		nvarchar(200) 
				,@CantComponente3			float 
				,@UndComponente3				nvarchar(50) 
				,@IndIngredientePrincipal3	nvarchar(2) 			   
			     
		DECLARE @ALMACENCVB int;
		DECLARE @IDUNIDOSIS int;        
		DECLARE @CONTADOR INT;   
		DECLARE @VIA nvarchar(50); 
		DECLARE @VIAP int; 

		declare @TipoComponente			 nvarchar(50) 
		declare @CodComponente			 nvarchar(10) 
		declare @NombreComponente		 nvarchar(200) 
		declare @CodATCComponente		 nvarchar(10) 
		declare @NombreATCComponente	 nvarchar(200) 
		declare @CantComponente			 float 
		declare @UndComponente			 nvarchar(50) 
		declare @IndIngredientePrincipal nvarchar(2) 

		declare @Codigo_Nacional_anterior			nvarchar(15)
		set @Codigo_Nacional_anterior = '0'

		DECLARE MEDICAMENTOS cursor FAST_FORWARD for		  
		  SELECT 
			   IT.ID
			  ,it.code
			  ,it.genericname
			  ,it.Description
			  ,ig.Code
			  ,it.TherapeuticGroup
			  ,it.FamilyID
			  ,f.Description
			  ,it.SubFamilyID	
			  ,sf.Description
			  ,ff.Name
			  ,fd.Name 
			  ,d.UnitaryDose AS Dosis
			  ,ud.Name as unidades_dosis
			  ,d.VolumeContent as Volumen
			  ,uv.Name as unidades_volumen
			  ,d.UnitsPerPack as unidades_caja
			  ,case when D.Psychoactive =1 then 'SI' else 'NO' end as Estupefaciente
              ,case when D.Suero =1 then 'SI' else 'NO' end as Suero
			  ,it.Price	
			  ,it.TaxID
			  ,it.Status
			  ,it.Observations		
			  ,d.HospitalFeature
			  ,DU.Code AS CodNacUnidodis
			  ,DU.GenericName as Nombre_UNIDOSIS
			  ,case when U.AllowMultiDose =1 then 'Sí' else 'NO' end as Multidosis_fraccionable
			  ,CC.AccountNumber AS CuentaContableCVB		  
			 from item it				
			 left join druginfo d on d.ItemID = it.ID
 			 /*left join (select top 1 code, description from ItemGroup) ig on d.TherapeuticGroup = ig.Description*/
			 left join ItemGroup ig on it.TherapeuticGroup = ig.Code
			 left join Family f on it.FamilyID = f.ID
			 left join SubFamily sf on sf.id = it.SubFamilyID
			 left join PharmaceuticalForm ff ON it.PharmaceuticalFormID = ff.ID
			 left join IB_IMQ_FrecuenciasEfarmaco fd on fd.ID = d.FrequencyDefID
			 left join PhysicalUnit ud on ud.ID= d.GiveUnitsID
			 left join PhysicalUnit uv on uv.ID= d.VolumeContentUnitID
			 LEFT JOIN DrugUnidosisRelationship DRU ON DRU.ParentItemID = IT.ID
			 LEFT JOIN UnidosisInfo U ON U.ItemID= DRU.ChildItemID
			 LEFT JOIN Item DU ON DU.ID = U.ItemID
			 LEFT JOIN ItemAccountRel CC ON CC.ItemID=IT.ID AND CC.CareCenterID = 9
			where it.ItemType = 25	

				SET @ITEMID = ''
				SET @Codigo_Nacional = ''			
				SET @Nombre_medicamento	 = ''	
				SET @Descripcion = ''				
				SET @CodigoGrupoTerapeutico	 = ''
				SET @Descripcion_GrupoTerapéutico = ''
				SET @Codigo_Familia	 = ''		
				SET @Descripcion_Familia = ''		
				SET @Codigo_SubFamilia = ''	        
				SET @Descripcion_SubFamilia = ''	
				SET @Forma_Farmaceutica	 = ''    
				SET @Via_administracion1 = ''		
				SET @Via_administracion2 = ''		
				SET @Via_administracion3 = ''		
				SET @FrecuenciaDefecto = ''	        
				SET @Dosis = ''						
				SET @unidades_dosis = ''
				SET @Volumen = ''					
				SET @unidades_volumen = ''			
				SET @unidades_caja = ''				
				SET @Estupefaciente	 = ''		
				SET @Suero	 = ''					
				SET @IndicadorGuia = ''				
				SET @DescripcionGuia = ''			
				SET @Cod_Unidodis = ''				
				SET @Nombre_UNIDOSIS = ''			
				SET @Multidosis_fraccionable = ''
				SET @Contenido_Total  = ''			
				SET @Unid_Dispensación = ''		
				SET @CuentaContableCVB = ''			
				SET @PrecioVenta = ''				
				SET @IVA = ''						
				SET @Observaciones = ''				
				SET @Estado	 = ''				
				SET @TipoComponente1 = ''			
				SET @CodComponente1	 = ''		
				SET @NombreComponente1 = ''			
				SET @CodATCComponente1	 = ''		
				SET @NombreATCComponente1 = ''		
				SET @CantComponente1 = ''			
				SET @UndComponente1	 = ''		
				SET @IndIngredientePrincipal1 = ''
				SET @TipoComponente2 = ''			
				SET @CodComponente2	 = ''		
				SET @NombreComponente2 = ''			
				SET @CodATCComponente2 = ''			
				SET @NombreATCComponente2 = ''		
				SET @CantComponente2 = ''			
				SET @UndComponente2	 = ''		
				SET @IndIngredientePrincipal2 = ''
				SET @TipoComponente3 = ''			
				SET @CodComponente3 = ''			
				SET @NombreComponente3 = ''			
				SET @CodATCComponente3 = ''			
				SET @NombreATCComponente3 = ''		
				SET @CantComponente3 = ''			
				SET @UndComponente3	 = ''		
				SET @IndIngredientePrincipal3 = ''	

				
		OPEN MEDICAMENTOS
		FETCH NEXT FROM MEDICAMENTOS INTO 
				 @ITEMID
				,@Codigo_Nacional			
				,@Nombre_medicamento		
				,@Descripcion				
				,@CodigoGrupoTerapeutico	
				,@Descripcion_GrupoTerapéutico
				,@Codigo_Familia			
				,@Descripcion_Familia		
				,@Codigo_SubFamilia	        
				,@Descripcion_SubFamilia	
				,@Forma_Farmaceutica	    
				,@FrecuenciaDefecto	        
				,@Dosis						
				,@unidades_dosis			
				,@Volumen					
				,@unidades_volumen			
				,@unidades_caja				
				,@Estupefaciente			
				,@Suero						
				,@PrecioVenta				
				,@IVA						
				,@Estado					
				,@Observaciones				
				,@IndicadorGuia				
				,@Cod_Unidodis				
				,@Nombre_UNIDOSIS			
				,@Multidosis_fraccionable	
				,@CuentaContableCVB			
								            
		WHILE @@FETCH_STATUS = 0
		BEGIN
 				 -- CALCULO DEL TIPO DE GUIA
				 IF (@IndicadorGuia = 1 OR @IndicadorGuia = 3) -- CVB O AMBAS
				 BEGIN
					IF (@IndicadorGuia = 1)
					   Set @DescripcionGuia = 'CVB'
					IF (@IndicadorGuia = 3)
					   Set @DescripcionGuia = 'AMBAS'
				 END
				 IF (@IndicadorGuia = 0 OR @IndicadorGuia = 2) -- CZ o ninguna
				 BEGIN
					 set @IDUNIDOSIS = (select top 1 childItemId 
										  from drugunidosisrelationship
										 where ParentItemID = @ITEMID)
					 set @ALMACENCVB = 0
					 set @ALMACENCVB = (select COUNT(S.id)
			                  			  FROM STOCKRULE S
											  ,ITEM I
										WHERE S.LocationID = 586
										and I.id = @IDUNIDOSIS
										AND I.ItemType = 17
										AND S.ITEMID = I.ID)
					 if @ALMACENCVB <> 0 
					    Set @DescripcionGuia = 'ALMACEN'
					 else
					    BEGIN
							if @IndicadorGuia = 2
							   Set @DescripcionGuia = 'CZ'
						END					 
				 END

				 --VIAS DE ADMINISTRACION
				set @Via_administracion1 = ''
				set @Via_administracion2 = ''
				set @Via_administracion3 = ''
				
				DECLARE vias_administracion cursor FAST_FORWARD for		  
				   select TOP 3 
				          ar.Name
				         ,vias.Viaprincipal
				     from IB_IMQ_ViasAdministracion Vias
			    left join AdministrationRoute ar on ar.id = vias.AdministrationRouteId
				    where vias.ItemId = @ITEMID

				OPEN vias_administracion
		        FETCH NEXT FROM vias_administracion INTO 
				         @VIA
						,@VIAP				
				SET @CONTADOR = 0
				WHILE @@FETCH_STATUS = 0
				BEGIN
				    SET @CONTADOR = @CONTADOR + 1
					IF @CONTADOR = 1 
				       set @Via_administracion1 = @VIA
				    IF @CONTADOR = 2 
				       set @Via_administracion2 = @VIA
				    IF @CONTADOR = 3 
				       set @Via_administracion3 = @VIA

					FETCH NEXT FROM vias_administracion INTO 
							 @VIA
							,@VIAP
				END
				CLOSE vias_administracion
				DEALLOCATE vias_administracion
				
				--COMPOSICION (SOLO LOS TRES PRIMEROS)
				DECLARE COMPONENTES cursor FAST_FORWARD for		  
				      SELECT TOP 3 
					         case when com.CompositionType=1 then 'Principio Activo' 
							      when com.CompositionType=2 then 'Excipiente' 
							 end as tipo
							 ,com.code
							 ,com.name
							 ,com.codeATC
							 ,com.NameATC
							 ,com.IngredientQty
							 ,uni.Name
							 ,case when com.Mainingredient=1 then 'SI' else 'NO' end as InGPrin						         
						FROM druginfo di
							,drugcompositioninfo com
					        ,PhysicalUnit uni
						where di.itemid = @ITEMID
						  and com.druginfoid = di.id
						  and uni.id = com.IngredientUnitId
						order by com.CompositionType, com.Mainingredient desc
								
				OPEN COMPONENTES
		        FETCH NEXT FROM COMPONENTES INTO 
					 @TipoComponente			
					,@CodComponente			
					,@NombreComponente		
					,@CodATCComponente		
					,@NombreATCComponente	
					,@CantComponente			
					,@UndComponente			
					,@IndIngredientePrincipal
				
				SET @CONTADOR = 0
				WHILE @@FETCH_STATUS = 0
				BEGIN
				    SET @CONTADOR = @CONTADOR + 1
					IF @CONTADOR = 1 
					begin
					   set @TipoComponente1	= @TipoComponente		
					   set @CodComponente1 = @CodComponente		
					   set @NombreComponente1 = @NombreComponente
					   set @CodATCComponente1 = @CodATCComponente	
					   set @NombreATCComponente1 = @NombreATCComponente
					   set @CantComponente1 = @CantComponente
					   set @UndComponente1 = @UndComponente		
					   set @IndIngredientePrincipal1 = @IndIngredientePrincipal
					end
					IF @CONTADOR = 2 
					begin
					   set @TipoComponente2	= @TipoComponente		
					   set @CodComponente2 = @CodComponente		
					   set @NombreComponente2 = @NombreComponente
					   set @CodATCComponente2 = @CodATCComponente	
					   set @NombreATCComponente2 = @NombreATCComponente
					   set @CantComponente2 = @CantComponente
					   set @UndComponente2 = @UndComponente		
					   set @IndIngredientePrincipal2 = @IndIngredientePrincipal
					end
					IF @CONTADOR = 3 
					begin
					   set @TipoComponente3	= @TipoComponente		
					   set @CodComponente3 = @CodComponente		
					   set @NombreComponente3 = @NombreComponente
					   set @CodATCComponente3 = @CodATCComponente	
					   set @NombreATCComponente3 = @NombreATCComponente
					   set @CantComponente3 = @CantComponente
					   set @UndComponente3 = @UndComponente		
					   set @IndIngredientePrincipal3 = @IndIngredientePrincipal
					end

					FETCH NEXT FROM COMPONENTES INTO 
					 @TipoComponente			
					,@CodComponente			
					,@NombreComponente		
					,@CodATCComponente		
					,@NombreATCComponente	
					,@CantComponente			
					,@UndComponente			
					,@IndIngredientePrincipal

				END
				CLOSE COMPONENTES
				DEALLOCATE COMPONENTES

				--IF @CUALES  = 'TODOS'
           IF @Codigo_Nacional_anterior <> @Codigo_Nacional
		   BEGIN
		 	    SET @Codigo_Nacional_anterior = @Codigo_Nacional

				INSERT INTO @InvMedicamentos VALUES 
				(
				 @Codigo_Nacional			
				,@Nombre_medicamento		
				,@Descripcion				
				,@CodigoGrupoTerapeutico	
				,@Descripcion_GrupoTerapéutico
				,@Codigo_Familia			
				,@Descripcion_Familia		
				,@Codigo_SubFamilia	        
				,@Descripcion_SubFamilia	
				,@Forma_Farmaceutica	    
				,@Via_administracion1		
				,@Via_administracion2		
				,@Via_administracion3		
				,@FrecuenciaDefecto	        
				,@Dosis						
				,@unidades_dosis			
				,@Volumen					
				,@unidades_volumen			
				,@unidades_caja				
				,@Estupefaciente			
				,@Suero						
				,@IndicadorGuia				
				,@DescripcionGuia			
				,@Cod_Unidodis				
				,@Nombre_UNIDOSIS			
				,@Multidosis_fraccionable	
				,@Contenido_Total 			
				,@Unid_Dispensación			
				,@CuentaContableCVB			
				,@PrecioVenta				
				,@IVA						
				,@Observaciones				
				,@Estado					
				,@TipoComponente1			
				,@CodComponente1			
				,@NombreComponente1			
				,@CodATCComponente1			
				,@NombreATCComponente1		
				,@CantComponente1			
				,@UndComponente1			
				,@IndIngredientePrincipal1	
				,@TipoComponente2			
				,@CodComponente2			
				,@NombreComponente2			
				,@CodATCComponente2			
				,@NombreATCComponente2		
				,@CantComponente2			
				,@UndComponente2			
				,@IndIngredientePrincipal2	
				,@TipoComponente3			
				,@CodComponente3			
				,@NombreComponente3			
				,@CodATCComponente3			
				,@NombreATCComponente3		
				,@CantComponente3			
				,@UndComponente3			
				,@IndIngredientePrincipal3	
				)
			END;
				
				-- Antes de ller un nuevo medicamento inicializao todas las variables
				SET @ITEMID = ''
				SET @Codigo_Nacional = ''			
				SET @Nombre_medicamento	 = ''	
				SET @Descripcion = ''				
				SET @CodigoGrupoTerapeutico	 = ''
				SET @Descripcion_GrupoTerapéutico = ''
				SET @Codigo_Familia	 = ''		
				SET @Descripcion_Familia = ''		
				SET @Codigo_SubFamilia = ''	        
				SET @Descripcion_SubFamilia = ''	
				SET @Forma_Farmaceutica	 = ''    
				SET @Via_administracion1 = ''		
				SET @Via_administracion2 = ''		
				SET @Via_administracion3 = ''		
				SET @FrecuenciaDefecto = ''	        
				SET @Dosis = ''						
				SET @unidades_dosis = ''
				SET @Volumen = ''					
				SET @unidades_volumen = ''			
				SET @unidades_caja = ''				
				SET @Estupefaciente	 = ''		
				SET @Suero	 = ''					
				SET @IndicadorGuia = ''				
				SET @DescripcionGuia = ''			
				SET @Cod_Unidodis = ''				
				SET @Nombre_UNIDOSIS = ''			
				SET @Multidosis_fraccionable = ''
				SET @Contenido_Total  = ''			
				SET @Unid_Dispensación = ''		
				SET @CuentaContableCVB = ''			
				SET @PrecioVenta = ''				
				SET @IVA = ''						
				SET @Observaciones = ''				
				SET @Estado	 = ''				
				SET @TipoComponente1 = ''			
				SET @CodComponente1	 = ''		
				SET @NombreComponente1 = ''			
				SET @CodATCComponente1	 = ''		
				SET @NombreATCComponente1 = ''		
				SET @CantComponente1 = ''			
				SET @UndComponente1	 = ''		
				SET @IndIngredientePrincipal1 = ''
				SET @TipoComponente2 = ''			
				SET @CodComponente2	 = ''		
				SET @NombreComponente2 = ''			
				SET @CodATCComponente2 = ''			
				SET @NombreATCComponente2 = ''		
				SET @CantComponente2 = ''			
				SET @UndComponente2	 = ''		
				SET @IndIngredientePrincipal2 = ''
				SET @TipoComponente3 = ''			
				SET @CodComponente3 = ''			
				SET @NombreComponente3 = ''			
				SET @CodATCComponente3 = ''			
				SET @NombreATCComponente3 = ''		
				SET @CantComponente3 = ''			
				SET @UndComponente3	 = ''		
				SET @IndIngredientePrincipal3 = ''	

				FETCH NEXT FROM MEDICAMENTOS INTO 
						 @ITEMID
						,@Codigo_Nacional			
						,@Nombre_medicamento		
						,@Descripcion				
						,@CodigoGrupoTerapeutico	
						,@Descripcion_GrupoTerapéutico
						,@Codigo_Familia			
						,@Descripcion_Familia		
						,@Codigo_SubFamilia	        
						,@Descripcion_SubFamilia	
						,@Forma_Farmaceutica	    
						,@FrecuenciaDefecto	        
						,@Dosis						
						,@unidades_dosis			
						,@Volumen					
						,@unidades_volumen			
						,@unidades_caja				
						,@Estupefaciente			
						,@Suero						
						,@PrecioVenta				
						,@IVA						
						,@Estado					
						,@Observaciones				
						,@IndicadorGuia				
						,@Cod_Unidodis				
						,@Nombre_UNIDOSIS			
						,@Multidosis_fraccionable	
						,@CuentaContableCVB			

		END
		CLOSE MEDICAMENTOS
		DEALLOCATE MEDICAMENTOS
    
	RETURN;

	END;
